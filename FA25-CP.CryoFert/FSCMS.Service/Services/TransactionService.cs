using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.Payments;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.ReponseModel.FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FSCMS.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TransactionService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get transaction by Id
        /// </summary>
        public async Task<BaseResponse<TransactionResponseModel>> GetTransactionByIdAsync(Guid transactionId)
        {
            const string methodName = nameof(GetTransactionByIdAsync);
            _logger.LogInformation("{MethodName} called with transactionId: {TransactionId}", methodName, transactionId);

            try
            {
                if (transactionId == Guid.Empty)
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "TransactionId is required",
                        Data = null
                    };

                var transaction = await _unitOfWork.Repository<Transaction>()
                    .AsQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == transactionId);

                if (transaction == null)
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Transaction not found",
                        Data = null
                    };

                return new BaseResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Transaction retrieved successfully",
                    Data = _mapper.Map<TransactionResponseModel>(transaction)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving transaction {TransactionId}", methodName, transactionId);
                return new BaseResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "An internal error occurred",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Create transaction
        /// </summary>
        public async Task<BaseResponse<TransactionResponseModel>> CreateTransactionAsync(CreateTransactionRequest request, HttpContext httpContext)
        {
            const string methodName = nameof(CreateTransactionAsync);
            _logger.LogInformation("{MethodName} called", methodName);

            try
            {
                if (request.Amount <= 0)
                {
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Amount must be greater than 0",
                        Data = null
                    };
                }

                var patient = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Include(x => x.Account)
                    .Where(x => x.Id == request.PatientId && !x.IsDeleted)
                    .FirstOrDefaultAsync();

                if (patient == null)
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Patient not found"
                    };

                // Check RelatedEntityType and RelatedEntityId
                if (string.IsNullOrWhiteSpace(request.RelatedEntityType))
                {
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_ENTITY",
                        Message = "Related entity type and ID must be provided",
                        Data = null
                    };
                }

                // Optional: Check entity exists in DB
                bool entityExists = request.RelatedEntityType switch
                {
                    "Patient" => await _unitOfWork.Repository<Patient>()
                                    .AsQueryable()
                                    .AnyAsync(p => p.Id == request.RelatedEntityId && !p.IsDeleted),
                    "ServiceRequest" => await _unitOfWork.Repository<ServiceRequest>()
                                    .AsQueryable()
                                    .AnyAsync(p => p.Id == request.RelatedEntityId && !p.IsDeleted),
                    "CryoStorageContract" => await _unitOfWork.Repository<CryoStorageContract>()
                                    .AsQueryable()
                                    .AnyAsync(m => m.Id == request.RelatedEntityId && !m.IsDeleted),
                    "Appointment" => await _unitOfWork.Repository<Appointment>()
                                    .AsQueryable()
                                    .AnyAsync(t => t.Id == request.RelatedEntityId && !t.IsDeleted),
                    _ => false
                };

                if (!entityExists)
                {
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "ENTITY_NOT_FOUND",
                        Message = $"Related entity {request.RelatedEntityType} with ID {request.RelatedEntityId} not found",
                        Data = null
                    };
                }

                var transaction = new Transaction
                {
                    TransactionCode = $"TX-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 6)}",
                    TransactionType = TransactionType.Payment,
                    Amount = request.Amount,
                    Currency = request.Currency,
                    PatientId = request.PatientId,
                    PatientName = patient.Account.FirstName+" "+patient.Account.LastName,
                    Description = request.Description,
                    RelatedEntityType = request.RelatedEntityType,
                    RelatedEntityId = request.RelatedEntityId,
                    Status = TransactionStatus.Pending,
                    TransactionDate = DateTime.UtcNow
                };

                // tích hợp VNPay
                if (transaction.Currency == "VND")
                {
                    var vnpay = new VnPay();
                    string ipAddress = HashAndGetIP.GetIpAddress(httpContext);

                    vnpay.AddRequestData("vnp_Version", VnPay.VERSION);
                    vnpay.AddRequestData("vnp_Command", "pay");
                    vnpay.AddRequestData("vnp_TxnRef", transaction.TransactionCode);
                    vnpay.AddRequestData("vnp_Amount", (transaction.Amount * 100).ToString("F0")); // VND * 100
                    vnpay.AddRequestData("vnp_CurrCode", transaction.Currency);
                    vnpay.AddRequestData("vnp_TmnCode", "YOUR_TMN_CODE");
                    vnpay.AddRequestData("vnp_OrderInfo", transaction.Description ?? "Payment");
                    vnpay.AddRequestData("vnp_Locale", "vn");
                    vnpay.AddRequestData("vnp_ReturnUrl", "YOUR_RETURN_URL");
                    vnpay.AddRequestData("vnp_IpAddr", ipAddress);

                    string paymentUrl = vnpay.CreateRequestUrl("https://sandbox.vnpayment.vn/paymentv2/vpcpay.html", "YOUR_HASH_SECRET");
                    transaction.PaymentGateway = "VNPay";
                    transaction.PaymentMethod = "VNPay";
                    transaction.ReferenceNumber = transaction.TransactionCode;
                    transaction.ProcessedDate = null;

                    await _unitOfWork.Repository<Transaction>().InsertAsync(transaction);
                    await _unitOfWork.CommitAsync();

                    transaction.Status = TransactionStatus.Completed;
                    var response = _mapper.Map<TransactionResponseModel>(transaction);
                    response.PaymentUrl = paymentUrl;

                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status201Created,
                        Message = "Transaction created successfully",
                        Data = response
                    };
                }

                // Trường hợp khác (Cash, Transfer, Insurance)
                await _unitOfWork.Repository<Transaction>().InsertAsync(transaction);
                await _unitOfWork.CommitAsync();

                return new BaseResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status201Created,
                    Message = "Transaction created successfully",
                    Data = _mapper.Map<TransactionResponseModel>(transaction)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating transaction", methodName);
                return new BaseResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "An internal error occurred while creating transaction",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Get transactions with paging/filter
        /// </summary>
        public async Task<DynamicResponse<TransactionResponseModel>> GetTransactionsAsync(GetTransactionsRequest request)
        {
            try
            {
                var query = _unitOfWork.Repository<Transaction>().AsQueryable();

                query = query.Where(t => t.PatientId == request.PatientId);

                if (!string.IsNullOrEmpty(request.RelatedEntityType))
                    query = query.Where(t => t.RelatedEntityType == request.RelatedEntityType);

                query = query.Where(t => t.RelatedEntityId == request.RelatedEntityId);

                if (request.Status.HasValue)
                    query = query.Where(t => t.Status == request.Status.Value);

                if (request.FromDate.HasValue)
                    query = query.Where(t => t.TransactionDate >= request.FromDate.Value);

                if (request.ToDate.HasValue)
                    query = query.Where(t => t.TransactionDate <= request.ToDate.Value);

                int total = await query.CountAsync();

                var data = await query
                    .OrderByDescending(t => t.TransactionDate)
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
                        Total = total
                    },
                    Data = _mapper.Map<List<TransactionResponseModel>>(data)
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    MetaData = new PagingMetaData(),
                    Data = new List<TransactionResponseModel>()
                };
            }
        }

        /// <summary>
        /// Update transaction
        /// </summary>
        public async Task<BaseResponse<TransactionResponseModel>> UpdateTransactionAsync(Guid transactionId, UpdateTransactionRequest request)
        {
            try
            {
                var transaction = await _unitOfWork.Repository<Transaction>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(t => t.Id == transactionId);

                if (transaction == null)
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Transaction not found",
                        Data = null
                    };

                _mapper.Map(request, transaction);

                await _unitOfWork.Repository<Transaction>().UpdateGuid(transaction, transaction.Id);
                await _unitOfWork.CommitAsync();

                return new BaseResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Transaction updated successfully",
                    Data = _mapper.Map<TransactionResponseModel>(transaction)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Delete transaction (soft delete)
        /// </summary>
        public async Task<BaseResponse> DeleteTransactionAsync(Guid transactionId)
        {
            try
            {
                var transaction = await _unitOfWork.Repository<Transaction>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(t => t.Id == transactionId);

                if (transaction == null)
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Transaction not found"
                    };

                transaction.IsDeleted = true;
                transaction.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Transaction>().UpdateGuid(transaction, transaction.Id);
                await _unitOfWork.CommitAsync();

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Transaction deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}
