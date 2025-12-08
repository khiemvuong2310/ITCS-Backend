using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
using FSCMS.Core.Enums;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using static FSCMS.Service.ReponseModel.CryoStorageContractResponse;

namespace FSCMS.Service.Services
{
    public class CryoStorageContractService : ICryoStorageContractService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CryoStorageContractService> _logger;
        private readonly ITransactionService _transactionService;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IMemoryCache _cache;
        private readonly IMediaService _mediaService;

        public CryoStorageContractService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CryoStorageContractService> logger, ITransactionService transactionService, IEmailTemplateService emailTemplateService,
            IMemoryCache cache,
            IEmailService emailService, IMediaService mediaService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            _emailTemplateService = emailTemplateService;
            _cache = cache;
            _emailService = emailService;
            _mediaService = mediaService;
        }

        #region Get All
        public async Task<DynamicResponse<CryoStorageContractResponse>> GetAllAsync(GetCryoStorageContractsRequest request)
        {
            try
            {
                var query = _unitOfWork.Repository<CryoStorageContract>()
                    .AsQueryable()
                    .Include(x => x.Patient)
                    .Include(x => x.CryoPackage)
                    .Where(x => !x.IsDeleted);

                // Filtering
                if (request.CryoPackageId.HasValue)
                    query = query.Where(x => x.CryoPackageId== request.CryoPackageId);

                if (request.PatientId.HasValue)
                    query = query.Where(x => x.PatientId == request.PatientId);

                if (request.Status.HasValue)
                    query = query.Where(x => x.Status == request.Status);

                // Count total
                var total = await query.CountAsync();

                // Sorting
                query = request.Sort?.ToLower() switch
                {
                    "startdate" => (request.Order?.ToLower() == "desc")
                        ? query.OrderByDescending(x => x.StartDate)
                        : query.OrderBy(x => x.StartDate),
                    _ => query.OrderByDescending(x => x.CreatedAt)
                };

                // Pagination
                var items = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                var data = _mapper.Map<List<CryoStorageContractResponse>>(items);

                return new DynamicResponse<CryoStorageContractResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Contracts retrieved successfully",
                    Data = data,
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = total
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving contracts");
                return new DynamicResponse<CryoStorageContractResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    MetaData = new PagingMetaData(),
                    Data = new List<CryoStorageContractResponse>()
                };
            }
        }
        #endregion

        #region Get By Id
        public async Task<BaseResponse<CryoStorageContractDetailResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Repository<CryoStorageContract>()
                    .AsQueryable()
                    .Include(x => x.Patient)
                        .ThenInclude(d => d.Account)
                    .Include(x => x.CryoPackage)
                    .Include(x => x.CPSDetails)
                        .ThenInclude(d => d.LabSample)
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (entity == null)
                {
                    return new BaseResponse<CryoStorageContractDetailResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Contract not found"
                    };
                }

                var data = _mapper.Map<CryoStorageContractDetailResponse>(entity);

                data.PatientName = entity.Patient?.Account.FirstName + " " +entity.Patient?.Account.LastName;

                return new BaseResponse<CryoStorageContractDetailResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Contract retrieved successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting contract by ID: {Id}", id);
                return new BaseResponse<CryoStorageContractDetailResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
        #endregion

        #region Create
        public async Task<BaseResponse> SendOtpEmailAsync(SentOtpEmailRequest request, Guid patientId)
        {
            try
            {
                var contract = await _unitOfWork.Repository<CryoStorageContract>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(x => x.Id == request.ContractId && !x.IsDeleted);
                if (contract == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Contract not found"
                    };
                }
                var account = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(x => x.Id == patientId && !x.IsDeleted);
                if (account == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        Message = "UnAuthenticate"
                    };
                }
                if (patientId != contract.PatientId)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Contract and patient not valid"
                    };
                }
                // Tạo OTP 6 chữ số
                var otp = new Random().Next(100000, 999999).ToString();

                // Hash OTP để bảo mật
                var otpHash = BCrypt.Net.BCrypt.HashPassword(otp);

                // Store in cache with expiration
                var cacheKey = $"verification_otp_{request.ContractId}_{account.Email}";
                _cache.Set(cacheKey, otpHash, TimeSpan.FromMinutes(15));

                await _emailService.SendEmailAsync(
                    account.Email,
                    $"OTP Cryo Storage Contract {contract.ContractNumber}",
                    await _emailTemplateService.GetCryoStorageContractOtpTemplateAsync(otp)
                );

                return new BaseResponse
                {
                    Code = 200,
                    Message = "OTP has been sent to your email"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Failed to send OTP: " + ex.Message
                };
            }
        }

        public async Task<BaseResponse<CryoStorageContractResponse>> VerifyOtpAsync(VerifyOtpRequest request, Guid userId)
        {
            try
            {
                // Get verification code from cache
                var account = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(x => x.Id == userId && !x.IsDeleted);
                if (account == null)
                {
                    return new BaseResponse<CryoStorageContractResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "User not found"
                    };
                }
                var cacheKey = $"verification_otp_{request.ContractId}_{account.Email}";
                if (!_cache.TryGetValue(cacheKey, out string storedCode))
                {
                    return new BaseResponse<CryoStorageContractResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "No verification opt found for this email or opt has expired"
                    };
                }

                if (string.IsNullOrWhiteSpace(storedCode) || string.IsNullOrWhiteSpace(request.Otp))
                {
                    return new BaseResponse<CryoStorageContractResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Verification otp is missing or invalid"
                    };
                }

                // Check verification code (case insensitive)
                if (!BCrypt.Net.BCrypt.Verify(request.Otp, storedCode))
                {
                    return new BaseResponse<CryoStorageContractResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid verification code"
                    };
                }

                // Remove from cache after successful verification
                _cache.Remove(cacheKey);
                var contract = await _unitOfWork.Repository<CryoStorageContract>()
                        .AsQueryable()
                        .Include(x => x.CryoPackage)
                        .FirstOrDefaultAsync(x => x.Id == request.ContractId && !x.IsDeleted);
                var patient = await _unitOfWork.Repository<Account>().GetByIdGuid(userId);
                contract.SignedBy = $"{patient.FirstName} {patient.LastName}";
                contract.SignedDate = DateTime.UtcNow;
                contract.Status = ContractStatus.Pending;
                await _unitOfWork.CommitAsync();
                var data = _mapper.Map<CryoStorageContractResponse>(contract);
                return new BaseResponse<CryoStorageContractResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = $"Contract {contract.ContractNumber} signed successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CryoStorageContractResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Verification failed: " + ex.Message
                };
            }

        }

        public async Task<BaseResponse<CryoStorageContractResponse>> CreateAsync(CreateCryoStorageContractRequest request)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Validate Patient & Package
                var patient = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Include(x => x.Account)
                    .FirstOrDefaultAsync(x => x.Id == request.PatientId && !x.IsDeleted);
                if (patient == null)
                {
                    return new BaseResponse<CryoStorageContractResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Patient Not Found",
                        Data = null
                    };
                }
                var package = await _unitOfWork.Repository<CryoPackage>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(x => x.Id == request.CryoPackageId && !x.IsDeleted);

                if (package == null)
                {
                    return new BaseResponse<CryoStorageContractResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "CryoPackage Not Found",
                        Data = null
                    };
                }

                if (request.Samples == null)
                {
                    return new BaseResponse<CryoStorageContractResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Lab Sample is required",
                        Data = null
                    };
                }

                // Map entity
                var entity = _mapper.Map<CryoStorageContract>(request);

                // Map CPSDetails if provided
                if (request.Samples != null && request.Samples.Any())
                {
                    foreach (var c in request.Samples)
                    {
                        var sample = await _unitOfWork.Repository<LabSample>()
                            .AsQueryable()
                            .FirstOrDefaultAsync(x => x.Id == c.LabSampleId && !x.IsDeleted && x.SampleType == package.SampleType && x.PatientId == patient.Id && x.Status == SpecimenStatus.Collected);
                        if (sample == null)
                        {
                            return new BaseResponse<CryoStorageContractResponse>
                            {
                                Code = StatusCodes.Status400BadRequest,
                                Message = $"Invalid LabSampleId: {c.LabSampleId}",
                                Data = null
                            };
                        }
                        var cPSDetails = new CPSDetail
                        {
                            LabSampleId = c.LabSampleId,
                            StorageStartDate = null,
                            StorageEndDate = null,
                            Status = "Pending",
                            Notes = c.Notes,
                            MonthlyFee = package.Price / package.DurationMonths,
                            CryoStorageContractId = entity.Id,
                            CryoStorageContract = entity,
                            LabSample = sample
                        };
                        await _unitOfWork.Repository<CPSDetail>().InsertAsync(cPSDetails);
                    }
                }
                var patientCode = patient?.PatientCode ?? "UNK";
                var random = new Random();
                entity.ContractNumber = $"CT-{patientCode}-{DateTime.UtcNow:yyMMdd}-{random.Next(10, 99)}";
                entity.Status = ContractStatus.Draft;
                entity.TotalAmount = package.Price * request.Samples.Count;
                entity.StartDate = null;
                entity.EndDate = null;
                entity.PaidAmount = 0;
                entity.SignedDate = null;
                entity.SignedBy = null;
                entity.IsAutoRenew = false;
                await _unitOfWork.Repository<CryoStorageContract>().InsertAsync(entity);
                CreateTransactionRequest createTransactionRequest = new CreateTransactionRequest
                {
                    Amount = entity.TotalAmount,
                    RelatedEntityType = EntityTypeTransaction.CryoStorageContract,
                    RelatedEntityId = entity.Id
                };
                await _transactionService.CreateTransactionAsync(createTransactionRequest, patient.Id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                var data = _mapper.Map<CryoStorageContractResponse>(entity);
                data.PatientName = $"{patient.Account.FirstName} {patient.Account.LastName}";
                data.CryoPackageName = package.PackageName;

                return new BaseResponse<CryoStorageContractResponse>
                {
                    Code = StatusCodes.Status201Created,
                    Message = "Contract created successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error creating contract");
                return new BaseResponse<CryoStorageContractResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
        #endregion

        #region Update
        public async Task<BaseResponse<CryoStorageContractResponse>> UpdateAsync(Guid id, UpdateCryoStorageContractRequest request)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var entity = await _unitOfWork.Repository<CryoStorageContract>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (entity == null)
                {
                    return new BaseResponse<CryoStorageContractResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Contract not found"
                    };
                }

                _mapper.Map(request, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<CryoStorageContract>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                var data = _mapper.Map<CryoStorageContractResponse>(entity);

                return new BaseResponse<CryoStorageContractResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Contract updated successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error updating contract {Id}", id);
                return new BaseResponse<CryoStorageContractResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
        #endregion

        #region Delete (Soft Delete)
        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var entity = await _unitOfWork.Repository<CryoStorageContract>()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted && x.Status != ContractStatus.Active && x.Status != ContractStatus.Expired);
                if (entity == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Contract not found"
                    };
                }

                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<CryoStorageContract>().UpdateGuid(entity, id);
                CancelltransactionRequest cancellTransactionRequest = new CancelltransactionRequest
                {
                    RelatedEntityType = EntityTypeTransaction.CryoStorageContract,
                    RelatedEntityId = entity.Id
                };
                await _transactionService.CancellTransactionAsync(cancellTransactionRequest);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Contract deleted successfully"
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error deleting contract {Id}", id);
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
        #endregion

        public async Task<BaseResponse<RenderCryoContractResponse>> RenderCryoContractAsync(Guid id)
        {
            try
            {
                // Lấy hợp đồng từ DB
                var contract = await _unitOfWork.Repository<CryoStorageContract>()
                    .AsQueryable()
                    .Include(x => x.Patient)
                        .ThenInclude(d => d.Account)
                    .Include(x => x.CryoPackage)
                    .Include(x => x.CPSDetails)
                        .ThenInclude(d => d.LabSample)
                            .ThenInclude(c => c.CryoLocation)
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (contract == null)
                {
                    return new BaseResponse<RenderCryoContractResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Contract not found"
                    };
                }

                if(contract.Status == ContractStatus.Active || contract.Status == ContractStatus.Expired)
                {
                    return new BaseResponse<RenderCryoContractResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Contract already active or expired"
                    };
                }

                // Lấy template HTML
                var templateText = await _mediaService.GetEtaTemplateFromCloudAsync(EntityTypeMedia.CryoStorageContract);

                // Replace các trường tĩnh
                templateText = templateText.Replace("<%= it.contract.contractNumber %>", contract.ContractNumber)
                                           .Replace("<%= it.contract.startDate %>", contract.StartDate?.ToString("yyyy-MM-dd") ?? "N/A")
                                           .Replace("<%= it.contract.endDate %>", contract.EndDate?.ToString("yyyy-MM-dd") ?? "N/A")
                                           .Replace("<%= it.contract.status %>", contract.Status.ToString())
                                           .Replace("<%= it.contract.patientName %>", $"{contract.Patient.Account.FirstName} {contract.Patient.Account.LastName}")
                                           .Replace("<%= it.contract.patientAddress %>", $"{contract.Patient.Account.Address}")
                                           .Replace("<%= it.contract.patientDob %>", $"{contract.Patient.Account.BirthDate}")
                                           .Replace("<%= it.contract.patientPhone %>", $"{contract.Patient.Account.Phone}")
                                           .Replace("<%= it.contract.signedBy %>", contract.SignedBy ?? "N/A")
                                           .Replace("<%= it.contract.signedDate %>", contract.SignedDate?.ToString("yyyy-MM-dd") ?? "N/A")
                                           .Replace("<%= it.contract.cryoPackageName %>", contract.CryoPackage.PackageName)
                                           .Replace("<%= it.contract.totalAmount %>", contract.TotalAmount.ToString("N0"))
                                           .Replace("<%= it.contract.paidAmount %>", (contract.PaidAmount?.ToString("N0") ?? "0"))
                                           .Replace("<%= it.contract.notes %>", string.IsNullOrEmpty(contract.Notes) ? "N/A" : contract.Notes)
                                           .Replace("<%= it.generatedAt %>", contract.CreatedAt.ToString("yyyy-MM-dd"))
                                           .Replace("<%= it.updatedAt %>", DateTime.UtcNow.ToString("yyyy-MM-dd"));

                // Build table samples
                string sampleRows = "";
                int index = 1;
                foreach (var s in contract.CPSDetails)
                {
                    var locationName = s.LabSample.CryoLocation?.Name ?? "N/A";
                    bool isActive = s.Status == "Storage";
                    sampleRows += $"<tr>" +
                                  $"<td>{index}</td>" +
                                  $"<td>{s.LabSample.SampleCode}</td>" +
                                  $"<td>{s.LabSample.SampleType}</td>" +
                                  $"<td>{locationName}</td>" +
                                  $"<td>{(isActive ? "Yes" : "No")}</td>" +
                                  $"</tr>";
                    index++;
                }

                // Replace toàn bộ vòng lặp sample table
                templateText = System.Text.RegularExpressions.Regex.Replace(templateText,
                    "<% it.contract.samples.forEach\\(\\(s, index\\) => \\{ %>.*?<% }\\) %>",
                    sampleRows,
                    System.Text.RegularExpressions.RegexOptions.Singleline);

                // Trả về kết quả
                RenderCryoContractResponse data = new RenderCryoContractResponse
                {
                    Contract = templateText
                };

                return new BaseResponse<RenderCryoContractResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Contract render successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error render contract ID: {Id}", id);
                return new BaseResponse<RenderCryoContractResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}
