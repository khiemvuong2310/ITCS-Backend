using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
using FSCMS.Core.Enums;
using FSCMS.Core.Models.Options;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.Payments;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.ReponseModel.FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using FSCMS.Service.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PayOS;
using PayOS.Models.Webhooks;
using ServiceStack;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FSCMS.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TransactionService> _logger;
        private readonly PaymentGatewayService _paymentGateway;
        private readonly IHubContext<TransactionHub> _hubContext;
        private readonly IMediaService _mediaService;
        private readonly VnPayOptions _options;

        public TransactionService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<TransactionService> logger,
            PaymentGatewayService paymentGateway,
            IHubContext<TransactionHub> hubContext,
            IMediaService mediaService,
            IOptions<VnPayOptions> options)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _paymentGateway = paymentGateway;
            _hubContext = hubContext;
            _mediaService = mediaService;
            _options = options.Value;
        }

        public async Task<BaseResponse<TransactionResponseModel>> CreateUrlPaymentAsync(CreateUrlPaymentRequest request)
        {
            try
            {
                object? entityExists = request.RelatedEntityType switch
                {
                    EntityTypeTransaction.ServiceRequest => await _unitOfWork.Repository<ServiceRequest>()
                                    .AsQueryable()
                                    .Include(x => x.Appointment)
                                    .Where(p => p.Id == request.RelatedEntityId && !p.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeTransaction.Appointment => await _unitOfWork.Repository<Appointment>()
                                    .AsQueryable()
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeTransaction.CryoStorageContract => await _unitOfWork.Repository<CryoStorageContract>()
                                    .AsQueryable()
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    _ => null
                };

                if (entityExists == null)
                {
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "ENTITY_NOT_FOUND",
                        Message = $"Related entity {request.RelatedEntityType} with ID {request.RelatedEntityId} not found",
                        Data = null
                    };
                }

                Guid? patientId = entityExists switch
                {
                    ServiceRequest mr => mr.Appointment?.PatientId,
                    Appointment tc => tc.PatientId,
                    Account acc => acc.Id,
                    CryoStorageContract csc => csc.PatientId,
                    _ => null
                };

                if (patientId == null)
                {
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "PATIENT_NOT_FOUND",
                        Message = "Cannot determine PatientId from related entity"
                    };
                }

                var transaction = await _unitOfWork.Repository<Transaction>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(p => p.RelatedEntityType == request.RelatedEntityType.ToString() && !p.IsDeleted && p.RelatedEntityId == request.RelatedEntityId && patientId == p.PatientId && p.Status == TransactionStatus.Pending);

                if (transaction == null)
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Transaction not found"
                    };

                if (transaction.PaymentGateway != null)
                {
                    transaction.Status = TransactionStatus.Cancelled;
                    await _unitOfWork.Repository<Transaction>().UpdateGuid(transaction, transaction.Id);
                    var newTransaction = new Transaction
                    {
                        TransactionCode = GenerateTransactionCode(),
                        TransactionType = transaction.TransactionType,
                        Amount = transaction.Amount,
                        Currency = transaction.Currency,
                        PatientId = transaction.PatientId,
                        PatientName = transaction.PatientName,
                        Description = transaction.Description,
                        RelatedEntityType = transaction.RelatedEntityType,
                        RelatedEntityId = transaction.RelatedEntityId,
                        Status = TransactionStatus.Pending,
                        TransactionDate = DateTime.UtcNow,
                        PaymentGateway = transaction.PaymentGateway,
                        PaymentMethod = transaction.PaymentMethod,
                        ReferenceNumber = transaction.ReferenceNumber
                    };
                    newTransaction.ReferenceNumber = newTransaction.TransactionCode;

                    await _unitOfWork.Repository<Transaction>().InsertAsync(newTransaction);
                    await _unitOfWork.CommitAsync();
                    transaction = newTransaction;
                }

                string paymentUrl = null;

                switch (request.PaymentGateway)
                {
                    case PaymentGateway.VnPay:
                        transaction.PaymentGateway = "VNPay";
                        transaction.PaymentMethod = "Online";
                        await _unitOfWork.Repository<Transaction>().UpdateGuid(transaction, transaction.Id);
                        paymentUrl = _paymentGateway.CreateVnPayUrl(transaction);
                        break;
                    case PaymentGateway.PayOS:
                        transaction.PaymentGateway = "PayOS";
                        transaction.PaymentMethod = "Online";
                        await _unitOfWork.Repository<Transaction>().UpdateGuid(transaction, transaction.Id);
                        paymentUrl = await _paymentGateway.CreatePayOSUrlAsync(transaction);
                        break;
                    default:
                        return new BaseResponse<TransactionResponseModel>
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Message = "Unsupported payment gateway"
                        };
                }

                var response = _mapper.Map<TransactionResponseModel>(transaction);
                response.PaymentUrl = paymentUrl;

                //var vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                //var vnTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vnTimeZone);

                //response.Exp = vnTime.AddMinutes(15).ToString("yyyyMMddHHmmss");
                //response.Local = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
                //response.Now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                await _unitOfWork.CommitAsync();
                return new BaseResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status201Created,
                    Message = "Url payment created successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Url Payment");
                return new BaseResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "An internal error occurred while creating Url Payment"
                };
            }
        }
        public async Task<BaseResponse> CancellTransactionAsync(CancelltransactionRequest request)
        {
            try
            {
                var transaction = await _unitOfWork.Repository<Transaction>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(p => p.RelatedEntityType == request.RelatedEntityType.ToString() && !p.IsDeleted && p.RelatedEntityId == request.RelatedEntityId);

                if (transaction == null)
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Transaction not found"
                    };

                transaction.Status = TransactionStatus.Cancelled;
                await _unitOfWork.Repository<Transaction>().UpdateGuid(transaction, transaction.Id);
                await _unitOfWork.CommitAsync();

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Transaction cancell successfully",
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating transaction");
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "An internal error occurred while cancell transaction"
                };
            }
        }
        #region Create Transaction & Redirect VNPay
        public async Task<BaseResponse<TransactionResponseModel>> CreateTransactionAsync(CreateTransactionRequest request, Guid patientId)
        {
            try
            {
                if (request.Amount <= 0)
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Amount must be greater than 0"
                    };

                var patient = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Include(p => p.Account)
                    .FirstOrDefaultAsync(p => p.Id == patientId && !p.IsDeleted);

                if (patient == null)
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Patient not found"
                    };

                var transaction = new Transaction
                {
                    TransactionCode = GenerateTransactionCode(),
                    TransactionType = TransactionType.Payment,
                    Amount = request.Amount,
                    Currency = "VND",
                    PatientId = patient.Id,
                    PatientName = $"{patient.Account.FirstName} {patient.Account.LastName}",
                    Description = $"{patient.Account.FirstName} {patient.Account.LastName} payment for {request.RelatedEntityType} {request.RelatedEntityId}",
                    RelatedEntityType = request.RelatedEntityType.ToString(),
                    RelatedEntityId = request.RelatedEntityId,
                    Status = TransactionStatus.Pending,
                    TransactionDate = DateTime.UtcNow,
                };

                transaction.ReferenceNumber = transaction.TransactionCode;

                await _unitOfWork.Repository<Transaction>().InsertAsync(transaction);

                var noti = new Notification(Guid.NewGuid(), "New Transaction", $"New Transaction {transaction.TransactionCode}", NotificationType.Payment);
                noti.Status = NotificationStatus.Sent;
                noti.PatientId = patientId;
                noti.SentTime = DateTime.UtcNow;
                noti.Channel = "Transaction";
                noti.RelatedEntityType = "Transaction";
                noti.RelatedEntityId = transaction.Id;
                await _unitOfWork.Repository<Notification>().InsertAsync(noti);

                await _unitOfWork.CommitAsync();

                var response = _mapper.Map<TransactionResponseModel>(transaction);
                response.PaymentUrl = null;

                return new BaseResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status201Created,
                    Message = "Transaction created successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating transaction");
                return new BaseResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "An internal error occurred while creating transaction"
                };
            }
        }
        #endregion

        #region VNPay Callback + Push SignalR
        public async Task<BaseResponse<TransactionResponseModel>> HandleVnPayCallbackAsync(IQueryCollection query)
        {
            using var transactionU = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var vnpay = new VnPay();

                foreach (var key in query.Keys)
                    vnpay.AddResponseData(key, query[key]!);

                string secureHash = query["vnp_SecureHash"];
                bool isValid = vnpay.ValidateSignature(secureHash, _paymentGateway.HashSecret);

                if (!isValid)
                {
                    await transactionU.RollbackAsync();
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid signature"
                    };
                }


                string txnRef = vnpay.GetResponseData("vnp_TxnRef");
                string vnp_TmnCode = vnpay.GetResponseData("vnp_TmnCode");
                string vnp_AmountRaw = vnpay.GetResponseData("vnp_Amount");
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                string vnp_TransactionNo = vnpay.GetResponseData("vnp_TransactionNo");
                string vnp_BankCode = vnpay.GetResponseData("vnp_BankCode");

                // 3. Validate merchant TmnCode
                if (!string.Equals(vnp_TmnCode, _options.vnp_TmnCode, StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogWarning("VNPay callback merchant code mismatch. Expected {Expected}, Got {Got}", _options.vnp_TmnCode, vnp_TmnCode);
                    await transactionU.RollbackAsync();
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid merchant"
                    };
                }

                // 4. Find transaction
                var transaction = await _unitOfWork.Repository<Transaction>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(t => t.TransactionCode == txnRef);

                if (transaction == null)
                {
                    _logger.LogWarning("VNPay callback: transaction not found. TxnRef={TxnRef}", txnRef);
                    await transactionU.RollbackAsync();
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Transaction not found"
                    };
                }

                // 5. Idempotency: if already completed, return success (avoid double processing)
                if (transaction.Status == TransactionStatus.Completed)
                {
                    _logger.LogInformation("VNPay callback received for already completed transaction {Txn}", txnRef);
                    await transactionU.CommitAsync(); // nothing changed, but commit tx
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status200OK,
                        Message = "Transaction already processed",
                        Data = _mapper.Map<TransactionResponseModel>(transaction)
                    };
                }

                // 6. Validate amount: VNPay sends amount in "cents" (amount * 100)
                if (!long.TryParse(vnp_AmountRaw, out var vnpAmount))
                {
                    _logger.LogWarning("VNPay callback: amount parse failed. AmountRaw={AmountRaw}", vnp_AmountRaw);
                    await transactionU.RollbackAsync();
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid amount"
                    };
                }

                long expectedAmount = (long)(transaction.Amount * 100M); // assuming transaction.Amount is decimal in VND
                if (vnpAmount != expectedAmount)
                {
                    _logger.LogWarning("VNPay callback amount mismatch. Txn={Txn}, Expected={Expected}, Got={Got}",
                        txnRef, expectedAmount, vnpAmount);
                    // Decide: mark as Failed and flag for reconciliation, or reject
                    transaction.Status = TransactionStatus.Failed;
                    transaction.ProcessedDate = DateTime.UtcNow;
                    transaction.BankTranNo = vnp_TransactionNo;
                    transaction.BankName = vnp_BankCode;
                    await _unitOfWork.Repository<Transaction>().UpdateGuid(transaction, transaction.Id);
                    await _unitOfWork.CommitAsync();
                    await transactionU.CommitAsync();

                    // Notify user/admin for manual reconciliation
                    await _hubContext.Clients.User(transaction.PatientId.ToString())
                        .SendAsync("TransactionUpdated", transaction.TransactionCode, transaction.Status.ToString());

                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Amount mismatch",
                        Data = _mapper.Map<TransactionResponseModel>(transaction)
                    };
                }

                // 7. Process based on response code & status
                transaction.ProcessedDate = DateTime.UtcNow;
                transaction.ProcessedBy = "VNPay";
                transaction.BankTranNo = vnp_TransactionNo;
                transaction.BankName = vnp_BankCode;
                await _unitOfWork.Repository<Transaction>().UpdateGuid(transaction, transaction.Id);
                await _unitOfWork.CommitAsync();
                if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                {
                    EntityTypeMedia? typeMedia = null;
                    switch (transaction.RelatedEntityType)
                    {
                        case "ServiceRequest":
                            var serviceRequest = await _unitOfWork.Repository<ServiceRequest>()
                                .AsQueryable()
                                .FirstOrDefaultAsync(p => p.Id == transaction.RelatedEntityId && !p.IsDeleted);
                            serviceRequest.Status = ServiceRequestStatus.InProcess;
                            await _unitOfWork.Repository<ServiceRequest>().UpdateGuid(serviceRequest, serviceRequest.Id);
                            await _unitOfWork.CommitAsync();
                            break;
                        case "Appointment":
                            var appointment = await _unitOfWork.Repository<Appointment>()
                                .AsQueryable()
                                .FirstOrDefaultAsync(p => p.Id == transaction.RelatedEntityId && !p.IsDeleted);
                            appointment.Status = AppointmentStatus.Confirmed;
                            await _unitOfWork.Repository<Appointment>().UpdateGuid(appointment, appointment.Id);
                            await _unitOfWork.CommitAsync();
                            break;
                        case "CryoStorageContract":
                            typeMedia = EntityTypeMedia.CryoStorageContract;
                            var cryoStorageContract = await _unitOfWork.Repository<CryoStorageContract>()
                                .AsQueryable()
                                .Include(p => p.CPSDetails)
                                .Include(p => p.CryoPackage)
                                .FirstOrDefaultAsync(p => p.Id == transaction.RelatedEntityId && !p.IsDeleted);
                            cryoStorageContract.Status = ContractStatus.Active;
                            if(cryoStorageContract.RenewFromContractId != null)
                            {
                                var mainContract = await _unitOfWork.Repository<CryoStorageContract>()
                                .AsQueryable()
                                .FirstOrDefaultAsync(p => p.Id == cryoStorageContract.RenewFromContractId && !p.IsDeleted);
                                mainContract.Status = ContractStatus.Renewed;
                                mainContract.EndDate = cryoStorageContract.EndDate;
                                await _unitOfWork.Repository<CryoStorageContract>().UpdateGuid(mainContract, mainContract.Id);
                                await _unitOfWork.CommitAsync();
                                foreach (var detail in cryoStorageContract.CPSDetails)
                                {
                                    detail.Status = "Storage";
                                    await _unitOfWork.Repository<CPSDetail>().UpdateGuid(detail, detail.Id);
                                }
                            }
                            else
                            {
                                cryoStorageContract.StartDate = DateTime.UtcNow;
                                cryoStorageContract.EndDate = DateTime.UtcNow.AddMonths(cryoStorageContract.CryoPackage.DurationMonths);
                                foreach (var detail in cryoStorageContract.CPSDetails)
                                {
                                    detail.StorageStartDate = DateTime.UtcNow;
                                    detail.StorageEndDate = DateTime.UtcNow.AddMonths(cryoStorageContract.CryoPackage.DurationMonths);
                                    detail.Status = "Storage";
                                    await _unitOfWork.Repository<CPSDetail>().UpdateGuid(detail, detail.Id);
                                    var sample = await _unitOfWork.Repository<LabSample>()
                                    .AsQueryable()
                                    .FirstOrDefaultAsync(p => p.Id == detail.LabSampleId && !p.IsDeleted);
                                    if (sample != null)
                                    {
                                        sample.Status = SpecimenStatus.Frozen;
                                        await _unitOfWork.Repository<LabSample>().UpdateGuid(sample, sample.Id);
                                    }
                                }
                            }
                            cryoStorageContract.PaidAmount = vnpAmount / 100;
                            await _unitOfWork.Repository<CryoStorageContract>().UpdateGuid(cryoStorageContract, cryoStorageContract.Id);
                            await _unitOfWork.CommitAsync();
                            break;
                        default:
                            break;
                    }
                    transaction.Status = TransactionStatus.Completed;
                    await _unitOfWork.Repository<Transaction>().UpdateGuid(transaction, transaction.Id);
                    await _unitOfWork.CommitAsync();
                    await transactionU.CommitAsync();
                    if (typeMedia != null)
                    {
                        await _mediaService.UploadPdfFromHtmlAsync(transaction.RelatedEntityId, (EntityTypeMedia)typeMedia);
                    }
                    // Push SignalR AFTER commit
                    await _hubContext.Clients.User(transaction.PatientId.ToString())
                        .SendAsync("TransactionUpdated", transaction.TransactionCode, transaction.Status.ToString());

                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status200OK,
                        Message = "Transaction processed",
                        Data = _mapper.Map<TransactionResponseModel>(transaction)
                    };
                }
                else
                {
                    // non-success codes -> Failed/Cancelled/Timeout
                    transaction.Status = TransactionStatus.Failed;
                    await _unitOfWork.Repository<Transaction>().UpdateGuid(transaction, transaction.Id);
                    var newTransaction = new Transaction
                    {
                        TransactionCode = $"TX-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 6)}",
                        TransactionType = transaction.TransactionType,
                        Amount = transaction.Amount,
                        Currency = transaction.Currency,
                        PatientId = transaction.PatientId,
                        PatientName = transaction.PatientName,
                        Description = transaction.Description,
                        RelatedEntityType = transaction.RelatedEntityType,
                        RelatedEntityId = transaction.RelatedEntityId,
                        Status = TransactionStatus.Pending,
                        TransactionDate = DateTime.UtcNow,
                        PaymentGateway = transaction.PaymentGateway,
                        PaymentMethod = transaction.PaymentMethod,
                        ReferenceNumber = transaction.ReferenceNumber
                    };
                    newTransaction.ReferenceNumber = newTransaction.TransactionCode;

                    await _unitOfWork.Repository<Transaction>().InsertAsync(newTransaction);
                    await _unitOfWork.CommitAsync();
                    await transactionU.CommitAsync();
                    // Notify AFTER commit
                    await _hubContext.Clients.User(transaction.PatientId.ToString())
                        .SendAsync("TransactionUpdated", transaction.TransactionCode, transaction.Status.ToString());

                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status200OK,
                        Message = "Transaction processed (failed)",
                        Data = _mapper.Map<TransactionResponseModel>(transaction)
                    };
                }
            }
            catch (Exception ex)
            {
                await transactionU.RollbackAsync();
                _logger.LogError(ex, "Error processing VNPay callback");
                return new BaseResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while processing payment"
                };
            }
        }

        public async Task<BaseResponse<TransactionResponseModel>> HandlePayOSWebhookAsync(string rawBody)
        {
            using var transactionU = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var payload = JsonSerializer.Deserialize<PayOSWebhookData>(
                rawBody,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (payload == null)
                {
                    await transactionU.RollbackAsync();
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid PayOS webhook payload",
                        Data = null
                    };
                }
                // 1️⃣ Verify signature
                if (!_paymentGateway.VerifySignature(rawBody, payload.Signature))
                {
                    await transactionU.RollbackAsync();
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid PayOS signature",
                        Data = null
                    };
                }
                // 2️⃣ Check success
                if (!payload.Success)
                {
                    await transactionU.RollbackAsync();
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "PayOS Payment failed",
                        Data = null
                    };
                }

                // 5️⃣ Find transaction
                var transaction = await _unitOfWork.Repository<Transaction>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(t =>
                        t.ReferenceNumber == payload.Data.OrderCode.ToString());

                if (transaction == null)
                {
                    await transactionU.RollbackAsync();
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Transaction not found",
                        Data = null
                    };
                }
                // 6️⃣ Idempotency
                if (transaction.Status == TransactionStatus.Completed)
                {
                    await transactionU.CommitAsync();
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status200OK,
                        Message = "Transaction Already processed",
                        Data = _mapper.Map<TransactionResponseModel>(transaction)
                    };
                }
                // 7️⃣ Validate amount
                if (transaction.Amount != payload.Data.Amount)
                {
                    transaction.Status = TransactionStatus.Failed;
                    transaction.ProcessedDate = DateTime.UtcNow;
                    transaction.BankTranNo = payload.Data.Reference;
                    await _unitOfWork.Repository<Transaction>().UpdateGuid(transaction, transaction.Id);
                    await _unitOfWork.CommitAsync();
                    await transactionU.CommitAsync();

                    // Notify user/admin for manual reconciliation
                    await _hubContext.Clients.User(transaction.PatientId.ToString())
                        .SendAsync("TransactionUpdated", transaction.TransactionCode, transaction.Status.ToString());

                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Amount mismatch",
                        Data = _mapper.Map<TransactionResponseModel>(transaction)
                    };
                }

                // 8️⃣ Update status
                transaction.ProcessedDate = DateTime.TryParseExact(payload.Data.TransactionDateTime, "yyyy-MM-dd HH:mm:ss",
                  System.Globalization.CultureInfo.InvariantCulture,
                  System.Globalization.DateTimeStyles.None,
                  out DateTime dt) ? dt : (DateTime?)null;

                transaction.ProcessedBy = "PayOS";
                transaction.BankTranNo = payload.Data.CounterAccountBankId.IsNullOrEmpty() ? null : payload.Data.CounterAccountBankId;
                transaction.BankName = payload.Data.CounterAccountBankName.IsNullOrEmpty() ? null : payload.Data.CounterAccountBankName;
                transaction.Currency = payload.Data.Currency;
                await _unitOfWork.Repository<Transaction>().UpdateGuid(transaction, transaction.Id);
                await _unitOfWork.CommitAsync();

                if (payload.Success)
                {
                    EntityTypeMedia? typeMedia = null;
                    switch (transaction.RelatedEntityType)
                    {
                        case "ServiceRequest":
                            var serviceRequest = await _unitOfWork.Repository<ServiceRequest>()
                                .AsQueryable()
                                .FirstOrDefaultAsync(p => p.Id == transaction.RelatedEntityId && !p.IsDeleted);
                            serviceRequest.Status = ServiceRequestStatus.InProcess;
                            await _unitOfWork.Repository<ServiceRequest>().UpdateGuid(serviceRequest, serviceRequest.Id);
                            await _unitOfWork.CommitAsync();
                            break;
                        case "Appointment":
                            var appointment = await _unitOfWork.Repository<Appointment>()
                                .AsQueryable()
                                .FirstOrDefaultAsync(p => p.Id == transaction.RelatedEntityId && !p.IsDeleted);
                            appointment.Status = AppointmentStatus.Confirmed;
                            await _unitOfWork.Repository<Appointment>().UpdateGuid(appointment, appointment.Id);
                            await _unitOfWork.CommitAsync();
                            break;
                        case "CryoStorageContract":
                            typeMedia = EntityTypeMedia.CryoStorageContract;
                            var cryoStorageContract = await _unitOfWork.Repository<CryoStorageContract>()
                                .AsQueryable()
                                .Include(p => p.CPSDetails)
                                .Include(p => p.CryoPackage)
                                .FirstOrDefaultAsync(p => p.Id == transaction.RelatedEntityId && !p.IsDeleted);
                            cryoStorageContract.Status = ContractStatus.Active;
                            if (cryoStorageContract.RenewFromContractId != null)
                            {
                                var mainContract = await _unitOfWork.Repository<CryoStorageContract>()
                                .AsQueryable()
                                .FirstOrDefaultAsync(p => p.Id == cryoStorageContract.RenewFromContractId && !p.IsDeleted);
                                mainContract.Status = ContractStatus.Renewed;
                                mainContract.EndDate = cryoStorageContract.EndDate;
                                mainContract.UpdatedAt = DateTime.UtcNow;
                                await _unitOfWork.Repository<CryoStorageContract>().UpdateGuid(mainContract, mainContract.Id);
                                await _unitOfWork.CommitAsync();
                                foreach (var detail in cryoStorageContract.CPSDetails)
                                {
                                    detail.Status = "Storage";
                                    await _unitOfWork.Repository<CPSDetail>().UpdateGuid(detail, detail.Id);
                                }
                            }
                            else
                            {
                                cryoStorageContract.StartDate = DateTime.UtcNow;
                                cryoStorageContract.EndDate = DateTime.UtcNow.AddMonths(cryoStorageContract.CryoPackage.DurationMonths);
                                foreach (var detail in cryoStorageContract.CPSDetails)
                                {
                                    detail.StorageStartDate = DateTime.UtcNow;
                                    detail.StorageEndDate = DateTime.UtcNow.AddMonths(cryoStorageContract.CryoPackage.DurationMonths);
                                    detail.Status = "Storage";
                                    await _unitOfWork.Repository<CPSDetail>().UpdateGuid(detail, detail.Id);
                                    var sample = await _unitOfWork.Repository<LabSample>()
                                    .AsQueryable()
                                    .FirstOrDefaultAsync(p => p.Id == detail.LabSampleId && !p.IsDeleted);
                                    if(sample != null)
                                    {
                                        sample.Status = SpecimenStatus.Frozen;
                                        await _unitOfWork.Repository<LabSample>().UpdateGuid(sample, sample.Id);
                                    }
                                }
                            }
                            cryoStorageContract.PaidAmount = payload.Data.Amount;
                            await _unitOfWork.Repository<CryoStorageContract>().UpdateGuid(cryoStorageContract, cryoStorageContract.Id);
                            await _unitOfWork.CommitAsync();
                            break;

                        default:
                            break;
                    }
                    transaction.Status = TransactionStatus.Completed;
                    await _unitOfWork.Repository<Transaction>().UpdateGuid(transaction, transaction.Id);
                    await _unitOfWork.CommitAsync();
                    await transactionU.CommitAsync();
                    if (typeMedia != null)
                    {
                        await _mediaService.UploadPdfFromHtmlAsync(transaction.RelatedEntityId, (EntityTypeMedia)typeMedia);
                    }
                    // Push SignalR AFTER commit
                    await _hubContext.Clients.User(transaction.PatientId.ToString())
                        .SendAsync("TransactionUpdated", transaction.TransactionCode, transaction.Status.ToString());

                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status200OK,
                        Message = "Transaction processed",
                        Data = _mapper.Map<TransactionResponseModel>(transaction)
                    };
                }
                else
                {
                    // non-success codes -> Failed/Cancelled/Timeout
                    transaction.Status = TransactionStatus.Failed;
                    await _unitOfWork.Repository<Transaction>().UpdateGuid(transaction, transaction.Id);
                    var newTransaction = new Transaction
                    {
                        TransactionCode = $"TX-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 6)}",
                        TransactionType = transaction.TransactionType,
                        Amount = transaction.Amount,
                        Currency = transaction.Currency,
                        PatientId = transaction.PatientId,
                        PatientName = transaction.PatientName,
                        Description = transaction.Description,
                        RelatedEntityType = transaction.RelatedEntityType,
                        RelatedEntityId = transaction.RelatedEntityId,
                        Status = TransactionStatus.Pending,
                        TransactionDate = DateTime.UtcNow,
                        PaymentGateway = transaction.PaymentGateway,
                        PaymentMethod = transaction.PaymentMethod,
                        ReferenceNumber = transaction.ReferenceNumber
                    };
                    newTransaction.ReferenceNumber = newTransaction.TransactionCode;

                    await _unitOfWork.Repository<Transaction>().InsertAsync(newTransaction);
                    await _unitOfWork.CommitAsync();
                    await transactionU.CommitAsync();
                    // Notify AFTER commit
                    await _hubContext.Clients.User(transaction.PatientId.ToString())
                        .SendAsync("TransactionUpdated", transaction.TransactionCode, transaction.Status.ToString());

                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status200OK,
                        Message = "Transaction processed (failed)",
                        Data = _mapper.Map<TransactionResponseModel>(transaction)
                    };
                }
            }
            catch (Exception ex)
            {
                await transactionU.RollbackAsync();
                _logger.LogError(ex, "PayOS webhook error");
                return new BaseResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while processing payment"
                };
            }
        }

        public async Task<BaseResponse<TransactionResponseModel>> CashPaymentAsync(CashPaymentRequest request)
        {
            using var transactionU = await _unitOfWork.BeginTransactionAsync();
            try
            {
                object? entityExists = request.RelatedEntityType switch
                {
                    EntityTypeTransaction.ServiceRequest => await _unitOfWork.Repository<ServiceRequest>()
                                    .AsQueryable()
                                    .Include(x => x.Appointment)
                                    .Where(p => p.Id == request.RelatedEntityId && !p.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeTransaction.Appointment => await _unitOfWork.Repository<Appointment>()
                                    .AsQueryable()
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeTransaction.CryoStorageContract => await _unitOfWork.Repository<CryoStorageContract>()
                                    .AsQueryable()
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    _ => null
                };

                if (entityExists == null)
                {
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "ENTITY_NOT_FOUND",
                        Message = $"Related entity {request.RelatedEntityType} with ID {request.RelatedEntityId} not found",
                        Data = null
                    };
                }

                Guid? patientId = entityExists switch
                {
                    ServiceRequest mr => mr.Appointment?.PatientId,
                    Appointment tc => tc.PatientId,
                    Account acc => acc.Id,
                    CryoStorageContract csc => csc.PatientId,
                    _ => null
                };

                if (patientId == null)
                {
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "PATIENT_NOT_FOUND",
                        Message = "Cannot determine PatientId from related entity"
                    };
                }

                var transaction = await _unitOfWork.Repository<Transaction>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(p => p.RelatedEntityType == request.RelatedEntityType.ToString() && !p.IsDeleted && p.RelatedEntityId == request.RelatedEntityId && patientId == p.PatientId && p.Status == TransactionStatus.Pending);

                if (transaction == null)
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Transaction not found"
                    };

                transaction.PaymentGateway = "Cash Payment";
                transaction.PaymentMethod = "Offline";
                transaction.ProcessedDate = DateTime.UtcNow;
                transaction.ProcessedBy = "Cash Payment";

                EntityTypeMedia? typeMedia = null;
                switch (transaction.RelatedEntityType)
                {
                    case "ServiceRequest":
                        var serviceRequest = await _unitOfWork.Repository<ServiceRequest>()
                            .AsQueryable()
                            .FirstOrDefaultAsync(p => p.Id == transaction.RelatedEntityId && !p.IsDeleted);
                        serviceRequest.Status = ServiceRequestStatus.InProcess;
                        await _unitOfWork.Repository<ServiceRequest>().UpdateGuid(serviceRequest, serviceRequest.Id);
                        await _unitOfWork.CommitAsync();
                        break;
                    case "Appointment":
                        var appointment = await _unitOfWork.Repository<Appointment>()
                            .AsQueryable()
                            .FirstOrDefaultAsync(p => p.Id == transaction.RelatedEntityId && !p.IsDeleted);
                        appointment.Status = AppointmentStatus.Confirmed;
                        await _unitOfWork.Repository<Appointment>().UpdateGuid(appointment, appointment.Id);
                        await _unitOfWork.CommitAsync();
                        break;
                    case "CryoStorageContract":
                        typeMedia = EntityTypeMedia.CryoStorageContract;
                        var cryoStorageContract = await _unitOfWork.Repository<CryoStorageContract>()
                            .AsQueryable()
                            .Include(p => p.CPSDetails)
                            .Include(p => p.CryoPackage)
                            .FirstOrDefaultAsync(p => p.Id == transaction.RelatedEntityId && !p.IsDeleted);
                        cryoStorageContract.Status = ContractStatus.Active;
                        cryoStorageContract.StartDate = DateTime.UtcNow;
                        cryoStorageContract.EndDate = DateTime.UtcNow.AddMonths(cryoStorageContract.CryoPackage.DurationMonths);
                        cryoStorageContract.PaidAmount = transaction.Amount;
                        foreach (var detail in cryoStorageContract.CPSDetails)
                        {
                            detail.StorageStartDate = DateTime.UtcNow;
                            detail.StorageEndDate = DateTime.UtcNow.AddMonths(cryoStorageContract.CryoPackage.DurationMonths);
                            detail.Status = "Storage";
                            await _unitOfWork.Repository<CPSDetail>().UpdateGuid(detail, detail.Id);
                        }
                        await _unitOfWork.Repository<CryoStorageContract>().UpdateGuid(cryoStorageContract, cryoStorageContract.Id);
                        await _unitOfWork.CommitAsync();
                        break;
                    default:
                        break;
                }
                transaction.Status = TransactionStatus.Completed;
                await _unitOfWork.Repository<Transaction>().UpdateGuid(transaction, transaction.Id);
                await _unitOfWork.CommitAsync();
                await transactionU.CommitAsync();
                if (typeMedia != null)
                {
                    await _mediaService.UploadPdfFromHtmlAsync(transaction.RelatedEntityId, (EntityTypeMedia)typeMedia);
                }
                // Push SignalR AFTER commit
                await _hubContext.Clients.User(transaction.PatientId.ToString())
                    .SendAsync("TransactionUpdated", transaction.TransactionCode, transaction.Status.ToString());

                return new BaseResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Transaction processed",
                    Data = _mapper.Map<TransactionResponseModel>(transaction)
                };
            }
            catch (Exception ex)
            {
                await transactionU.RollbackAsync();
                _logger.LogError(ex, "Server error");
                return new BaseResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while processing payment"
                };
            }
        }

        #endregion

        #region Helper
        private async Task<bool> CheckRelatedEntityExistsAsync(EntityTypeTransaction? entityType, Guid? entityId)
        {
            if (entityType == null || entityId == null) return false;

            return entityType switch
            {
                // Appointment considered valid for transactions when it's not deleted and not cancelled
                EntityTypeTransaction.Appointment => await _unitOfWork.Repository<Appointment>()
                    .AsQueryable()
                    .AnyAsync(e => e.Id == entityId && !e.IsDeleted && e.Status != AppointmentStatus.Cancelled),
                EntityTypeTransaction.CryoStorageContract => await _unitOfWork.Repository<CryoStorageContract>().AsQueryable().AnyAsync(e => e.Id == entityId && !e.IsDeleted && e.Status == ContractStatus.Pending),
                EntityTypeTransaction.ServiceRequest => await _unitOfWork.Repository<ServiceRequest>().AsQueryable().AnyAsync(e => e.Id == entityId && !e.IsDeleted && e.Status == ServiceRequestStatus.Pending),
                //EntityTypeTransaction.Patient => await _unitOfWork.Repository<Patient>().AsQueryable().AnyAsync(e => e.Id == entityId && !e.IsDeleted),
                _ => false
            };
        }
        #endregion

        public async Task<DynamicResponse<TransactionResponseModel>> GetAllTransactionsAsync(GetTransactionsRequest request)
        {
            try
            {
                if (request.RelatedEntityId != null && request.RelatedEntityType != null)
                {
                    bool entityExists = await CheckRelatedEntityExistsAsync(request.RelatedEntityType, request.RelatedEntityId);
                    if (!entityExists)
                        return new DynamicResponse<TransactionResponseModel>
                        {
                            Code = StatusCodes.Status404NotFound,
                            Message = $"Related entity {request.RelatedEntityType.ToString()} not found"
                        };
                }

                var query = _unitOfWork.Repository<Transaction>()
                    .AsQueryable()
                    .Where(t => !t.IsDeleted);

                // Filtering
                if (request.PatientId != Guid.Empty)
                {
                    var patient = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(p => p.Id == request.PatientId && !p.IsDeleted);

                    if (patient == null)
                        return new DynamicResponse<TransactionResponseModel>
                        {
                            Code = StatusCodes.Status404NotFound,
                            Message = "Patient not found"
                        };
                    query = query.Where(t => t.PatientId == request.PatientId);
                }

                if (request.TransactionId != Guid.Empty)
                {
                    var transaction = await _unitOfWork.Repository<Transaction>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(p => p.Id == request.TransactionId && !p.IsDeleted);

                    if (transaction == null)
                        return new DynamicResponse<TransactionResponseModel>
                        {
                            Code = StatusCodes.Status404NotFound,
                            Message = "Transaction not found"
                        };
                    query = query.Where(t => t.Id == request.TransactionId);
                }


                if (request.RelatedEntityType.HasValue)
                    query = query.Where(t => t.RelatedEntityType == request.RelatedEntityType.Value.ToString());

                if (request.RelatedEntityId != null)
                    query = query.Where(t => t.RelatedEntityId == request.RelatedEntityId);

                if (request.Status.HasValue)
                    query = query.Where(t => t.Status == request.Status.Value);

                if (request.FromDate.HasValue)
                    query = query.Where(t => t.TransactionDate >= request.FromDate.Value);

                if (request.ToDate.HasValue)
                    query = query.Where(t => t.TransactionDate <= request.ToDate.Value);

                // Total count
                var totalCount = await query.CountAsync();

                // Sorting
                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    var isDescending = request.Order?.ToLower() == "desc";

                    query = request.Sort.ToLower() switch
                    {
                        "amount" => isDescending ? query.OrderByDescending(t => t.Amount) : query.OrderBy(t => t.Amount),
                        "transactiondate" => isDescending ? query.OrderByDescending(t => t.TransactionDate) : query.OrderBy(t => t.TransactionDate),
                        "status" => isDescending ? query.OrderByDescending(t => t.Status) : query.OrderBy(t => t.Status),
                        _ => isDescending ? query.OrderByDescending(t => t.CreatedAt) : query.OrderBy(t => t.CreatedAt)
                    };
                }
                else
                {
                    query = query.OrderByDescending(t => t.CreatedAt);
                }

                // Pagination
                var transactions = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                return new DynamicResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Transactions retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = totalCount
                    },
                    Data = _mapper.Map<List<TransactionResponseModel>>(transactions)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving transactions");
                return new DynamicResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while retrieving transactions",
                    MetaData = new PagingMetaData(),
                    Data = new List<TransactionResponseModel>()
                };
            }
        }

        public string GenerateTransactionCode()
        {
            long seconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds(); // ví dụ 1700000000
            int rand = new Random().Next(0, 100); // 2 chữ số ngẫu nhiên
            long code = (seconds % 100000000) * 100 + rand; // kết hợp → 10 chữ số
            return code.ToString();
        }

    }
}
