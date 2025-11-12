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
using FSCMS.Service.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FSCMS.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TransactionService> _logger;
        private readonly PaymentGatewayService _paymentGateway;
        private readonly IHubContext<TransactionHub> _hubContext;

        public TransactionService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<TransactionService> logger,
            PaymentGatewayService paymentGateway,
            IHubContext<TransactionHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _paymentGateway = paymentGateway;
            _hubContext = hubContext;
        }

        #region Create Transaction & Redirect VNPay
        public async Task<BaseResponse<TransactionResponseModel>> CreateTransactionAsync(CreateTransactionRequest request, HttpContext httpContext)
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
                    .FirstOrDefaultAsync(p => p.Id == request.PatientId && !p.IsDeleted);

                if (patient == null)
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Patient not found"
                    };

                bool entityExists = await CheckRelatedEntityExistsAsync(request.RelatedEntityType, request.RelatedEntityId);
                if (!entityExists)
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = $"Related entity {request.RelatedEntityType.ToString()} not found"
                    };

                var transaction = new Transaction
                {
                    TransactionCode = $"TX-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 6)}",
                    TransactionType = TransactionType.Payment,
                    Amount = request.Amount,
                    Currency = "VND",
                    PatientId = patient.Id,
                    PatientName = $"{patient.Account.FirstName} {patient.Account.LastName}",
                    Description = request.Description ?? $"{patient.Account.FirstName} {patient.Account.LastName} payment for {request.RelatedEntityType}",
                    RelatedEntityType = request.RelatedEntityType.ToString(),
                    RelatedEntityId = request.RelatedEntityId,
                    Status = TransactionStatus.Pending,
                    TransactionDate = DateTime.UtcNow,
                };

                string paymentUrl = null;
                if (transaction.Currency == "VND")
                {
                    paymentUrl = _paymentGateway.CreateVnPayUrl(transaction, httpContext);
                    transaction.PaymentGateway = "VNPay";
                    transaction.PaymentMethod = "Online";
                    transaction.ReferenceNumber = transaction.TransactionCode;
                }

                await _unitOfWork.Repository<Transaction>().InsertAsync(transaction);
                await _unitOfWork.CommitAsync();

                var response = _mapper.Map<TransactionResponseModel>(transaction);
                response.PaymentUrl = paymentUrl;

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
            try
            {
                var vnpay = new VnPay();
                foreach (var key in query.Keys)
                    vnpay.AddResponseData(key, query[key]!);

                string secureHash = query["vnp_SecureHash"];
                bool isValid = vnpay.ValidateSignature(secureHash, _paymentGateway.HashSecret);

                if (!isValid)
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid signature"
                    };

                string txnRef = vnpay.GetResponseData("vnp_TxnRef");
                var transaction = await _unitOfWork.Repository<Transaction>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(t => t.TransactionCode == txnRef);

                if (transaction == null)
                    return new BaseResponse<TransactionResponseModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Transaction not found"
                    };

                string responseCode = vnpay.GetResponseData("vnp_ResponseCode");
                transaction.Status = responseCode == "00" ? TransactionStatus.Completed : TransactionStatus.Failed;
                transaction.ProcessedDate = DateTime.UtcNow;

                await _unitOfWork.Repository<Transaction>().UpdateGuid(transaction, transaction.Id);
                await _unitOfWork.CommitAsync();

                // Push SignalR
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
                _logger.LogError(ex, "Error processing VNPay callback");
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
                EntityTypeTransaction.Appointment => await _unitOfWork.Repository<Appointment>().AsQueryable().AnyAsync(e => e.Id == entityId && !e.IsDeleted),
                EntityTypeTransaction.CryoStorageContract => await _unitOfWork.Repository<CryoStorageContract>().AsQueryable().AnyAsync(e => e.Id == entityId && !e.IsDeleted),
                EntityTypeTransaction.ServiceRequest => await _unitOfWork.Repository<ServiceRequest>().AsQueryable().AnyAsync(e => e.Id == entityId && !e.IsDeleted),
                _ => false
            };
        }
        #endregion

        public async Task<DynamicResponse<TransactionResponseModel>> GetAllTransactionsAsync(GetTransactionsRequest request)
        {
            try
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

                if(request.RelatedEntityId != null && request.RelatedEntityType != null)
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
                    query = query.Where(t => t.PatientId == request.PatientId);

                if (request.RelatedEntityType.HasValue)
                    query = query.Where(t => t.RelatedEntityType == request.RelatedEntityType.Value.ToString());

                if (request.RelatedEntityId != Guid.Empty)
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

    }
}
