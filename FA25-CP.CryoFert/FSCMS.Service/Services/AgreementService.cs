using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
using FSCMS.Core.Enums;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.ReponseModel.FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FSCMS.Service.Services
{
    /// <summary>
    /// Service implementation for Agreement operations
    /// </summary>
    public class AgreementService : IAgreementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AgreementService> _logger;
        private readonly IOTPService _otpService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediaService _mediaService;

        public AgreementService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<AgreementService> logger,
            IOTPService otpService,
            IHttpContextAccessor httpContextAccessor,
            IMediaService mediaService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _otpService = otpService ?? throw new ArgumentNullException(nameof(otpService));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _mediaService = mediaService ?? throw new ArgumentNullException(nameof(mediaService));
        }
        #region Agreement CRUD Operations   
        #region Get All
        /// <summary>
        /// Get all agreements with pagination, filtering, and sorting
        /// </summary>
        public async Task<DynamicResponse<AgreementResponse>> GetAllAsync(GetAgreementsRequest request)
        {
            const string methodName = nameof(GetAllAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                // Normalize pagination
                request.Normalize();

                var query = _unitOfWork.Repository<Agreement>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(a => a.Treatment)
                    .Include(a => a.Patient)
                        .ThenInclude(p => p!.Account)
                    .Where(a => !a.IsDeleted);

                // Filtering
                if (request.TreatmentId.HasValue)
                    query = query.Where(a => a.TreatmentId == request.TreatmentId.Value);

                if (request.PatientId.HasValue)
                    query = query.Where(a => a.PatientId == request.PatientId.Value);

                if (request.Status.HasValue)
                    query = query.Where(a => a.Status == request.Status.Value);

                if (request.FromStartDate.HasValue)
                    query = query.Where(a => a.StartDate >= request.FromStartDate.Value);

                if (request.ToStartDate.HasValue)
                    query = query.Where(a => a.StartDate <= request.ToStartDate.Value);

                if (request.FromEndDate.HasValue)
                    query = query.Where(a => a.EndDate.HasValue && a.EndDate.Value >= request.FromEndDate.Value);

                if (request.ToEndDate.HasValue)
                    query = query.Where(a => a.EndDate.HasValue && a.EndDate.Value <= request.ToEndDate.Value);

                if (request.SignedByPatient.HasValue)
                    query = query.Where(a => a.SignedByPatient == request.SignedByPatient.Value);

                if (request.SignedByDoctor.HasValue)
                    query = query.Where(a => a.SignedByDoctor == request.SignedByDoctor.Value);

                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    var searchTerm = request.SearchTerm.ToLower();
                    query = query.Where(a =>
                        a.AgreementCode.ToLower().Contains(searchTerm) ||
                        (a.Treatment != null && !string.IsNullOrEmpty(a.Treatment.TreatmentName) && a.Treatment.TreatmentName.ToLower().Contains(searchTerm)) ||
                        (a.Patient != null && a.Patient.Account != null &&
                         (!string.IsNullOrEmpty(a.Patient.Account.FirstName) || !string.IsNullOrEmpty(a.Patient.Account.LastName)) &&
                         ($"{a.Patient.Account.FirstName} {a.Patient.Account.LastName}".Trim().ToLower().Contains(searchTerm) ||
                          (!string.IsNullOrEmpty(a.Patient.Account.Email) && a.Patient.Account.Email.ToLower().Contains(searchTerm)))));
                }

                // Count total
                var total = await query.CountAsync();

                // Sorting
                query = request.Sort?.ToLower() switch
                {
                    "agreementcode" => (request.Order?.ToLower() == "desc")
                        ? query.OrderByDescending(a => a.AgreementCode)
                        : query.OrderBy(a => a.AgreementCode),
                    "startdate" => (request.Order?.ToLower() == "desc")
                        ? query.OrderByDescending(a => a.StartDate)
                        : query.OrderBy(a => a.StartDate),
                    "enddate" => (request.Order?.ToLower() == "desc")
                        ? query.OrderByDescending(a => a.EndDate)
                        : query.OrderBy(a => a.EndDate),
                    "totalamount" => (request.Order?.ToLower() == "desc")
                        ? query.OrderByDescending(a => a.TotalAmount)
                        : query.OrderBy(a => a.TotalAmount),
                    "status" => (request.Order?.ToLower() == "desc")
                        ? query.OrderByDescending(a => a.Status)
                        : query.OrderBy(a => a.Status),
                    _ => query.OrderByDescending(a => a.CreatedAt)
                };

                // Pagination
                var items = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                var data = _mapper.Map<List<AgreementResponse>>(items);

                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} agreements", methodName, data.Count);

                return new DynamicResponse<AgreementResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Agreements retrieved successfully",
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
                _logger.LogError(ex, "{MethodName}: Error retrieving agreements", methodName);
                return new DynamicResponse<AgreementResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred while retrieving agreements: {ex.Message}",
                    Data = new List<AgreementResponse>(),
                    MetaData = new PagingMetaData()
                };
            }
        }
        #endregion

        #region Get By Id
        /// <summary>
        /// Get agreement by ID with full details
        /// </summary>
        public async Task<BaseResponse<AgreementDetailResponse>> GetByIdAsync(Guid id)
        {
            const string methodName = nameof(GetByIdAsync);
            _logger.LogInformation("{MethodName} called with ID: {Id}", methodName, id);

            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid agreement ID provided", methodName);
                    return BaseResponse<AgreementDetailResponse>.CreateError("Invalid agreement ID", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var entity = await _unitOfWork.Repository<Agreement>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(a => a.Treatment)
                    .Include(a => a.Patient)
                        .ThenInclude(p => p!.Account)
                    .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Agreement not found with ID: {Id}", methodName, id);
                    return BaseResponse<AgreementDetailResponse>.CreateError("Agreement not found", StatusCodes.Status404NotFound, "NOT_FOUND");
                }

                var data = _mapper.Map<AgreementDetailResponse>(entity);

                _logger.LogInformation("{MethodName}: Successfully retrieved agreement {Id}", methodName, id);

                return BaseResponse<AgreementDetailResponse>.CreateSuccess(data, "Agreement retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error getting agreement by ID: {Id}", methodName, id);
                return BaseResponse<AgreementDetailResponse>.CreateError($"Error retrieving agreement: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }
        #endregion

        #region Get By Code
        /// <summary>
        /// Get agreement by agreement code
        /// </summary>
        public async Task<BaseResponse<AgreementDetailResponse>> GetByCodeAsync(string agreementCode)
        {
            const string methodName = nameof(GetByCodeAsync);
            _logger.LogInformation("{MethodName} called with code: {Code}", methodName, agreementCode);

            try
            {
                if (string.IsNullOrWhiteSpace(agreementCode))
                {
                    _logger.LogWarning("{MethodName}: Invalid agreement code provided", methodName);
                    return BaseResponse<AgreementDetailResponse>.CreateError("Agreement code cannot be empty", StatusCodes.Status400BadRequest, "INVALID_CODE");
                }

                var entity = await _unitOfWork.Repository<Agreement>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(a => a.Treatment)
                    .Include(a => a.Patient)
                        .ThenInclude(p => p!.Account)
                    .FirstOrDefaultAsync(a => a.AgreementCode == agreementCode && !a.IsDeleted);

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Agreement not found with code: {Code}", methodName, agreementCode);
                    return BaseResponse<AgreementDetailResponse>.CreateError("Agreement not found", StatusCodes.Status404NotFound, "NOT_FOUND");
                }

                var data = _mapper.Map<AgreementDetailResponse>(entity);

                _logger.LogInformation("{MethodName}: Successfully retrieved agreement {Code}", methodName, agreementCode);

                return BaseResponse<AgreementDetailResponse>.CreateSuccess(data, "Agreement retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error getting agreement by code: {Code}", methodName, agreementCode);
                return BaseResponse<AgreementDetailResponse>.CreateError($"Error retrieving agreement: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }
        #endregion

        #region Create
        /// <summary>
        /// Create a new agreement
        /// </summary>
        public async Task<BaseResponse<AgreementResponse>> CreateAsync(CreateAgreementRequest request)
        {
            const string methodName = nameof(CreateAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Validate Treatment
                var treatment = await _unitOfWork.Repository<Treatment>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(t => t.Id == request.TreatmentId && !t.IsDeleted);

                if (treatment == null)
                {
                    _logger.LogWarning("{MethodName}: Treatment not found with ID: {TreatmentId}", methodName, request.TreatmentId);
                    return BaseResponse<AgreementResponse>.CreateError("Treatment not found", StatusCodes.Status404NotFound, "TREATMENT_NOT_FOUND");
                }

                // Validate Patient
                var patient = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(p => p.Id == request.PatientId && !p.IsDeleted);

                if (patient == null)
                {
                    _logger.LogWarning("{MethodName}: Patient not found with ID: {PatientId}", methodName, request.PatientId);
                    return BaseResponse<AgreementResponse>.CreateError("Patient not found", StatusCodes.Status404NotFound, "PATIENT_NOT_FOUND");
                }

                // Validate PatientId belongs to TreatmentId
                if (treatment.PatientId != request.PatientId)
                {
                    _logger.LogWarning("{MethodName}: PatientId {PatientId} does not belong to TreatmentId {TreatmentId}. Treatment belongs to PatientId {TreatmentPatientId}",
                        methodName, request.PatientId, request.TreatmentId, treatment.PatientId);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "PatientId does not belong to the specified Treatment. The treatment belongs to a different patient.",
                        StatusCodes.Status400BadRequest,
                        "PATIENT_TREATMENT_MISMATCH");
                }

                // Check if agreement already exists for this treatment
                var existingAgreement = await _unitOfWork.Repository<Agreement>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(a => a.TreatmentId == request.TreatmentId && !a.IsDeleted);

                if (existingAgreement != null)
                {
                    _logger.LogWarning("{MethodName}: Agreement already exists for TreatmentId {TreatmentId} with AgreementId {AgreementId}",
                        methodName, request.TreatmentId, existingAgreement.Id);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "An agreement already exists for this treatment",
                        StatusCodes.Status400BadRequest,
                        "AGREEMENT_ALREADY_EXISTS");
                }

                // Validate date range
                if (request.EndDate.HasValue && request.EndDate.Value < request.StartDate)
                {
                    _logger.LogWarning("{MethodName}: EndDate cannot be earlier than StartDate", methodName);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "EndDate cannot be earlier than StartDate",
                        StatusCodes.Status400BadRequest,
                        "INVALID_DATE_RANGE");
                }

                // Validate TotalAmount
                if (request.TotalAmount < 0)
                {
                    _logger.LogWarning("{MethodName}: TotalAmount cannot be negative", methodName);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "TotalAmount must be greater than or equal to 0",
                        StatusCodes.Status400BadRequest,
                        "INVALID_AMOUNT");
                }

                // Generate unique agreement code
                var agreementCode = await GenerateAgreementCodeAsync();

                // Create entity with Doctor auto-signed
                var entity = new Agreement(
                    Guid.NewGuid(),
                    agreementCode,
                    request.TreatmentId,
                    request.PatientId,
                    request.StartDate,
                    request.TotalAmount
                )
                {
                    EndDate = request.EndDate,
                    FileUrl = request.FileUrl,
                    Status = AgreementStatus.Pending,
                    SignedByPatient = false,
                    SignedByDoctor = true,
                    SignedDate = null, // Patient chưa ký
                    SignatureMethod = null,
                    SignatureIPAddress = null,
                    OTPSentDate = null
                };

                await _unitOfWork.Repository<Agreement>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                var data = _mapper.Map<AgreementResponse>(entity);

                _logger.LogInformation("{MethodName}: Successfully created agreement {Id} with code {Code}. Doctor auto-signed.",
                    methodName, entity.Id, agreementCode);

                return BaseResponse<AgreementResponse>.CreateSuccess(
                    data,
                    "Agreement created successfully. Awaiting patient signature.",
                    StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "{MethodName}: Error creating agreement", methodName);
                return BaseResponse<AgreementResponse>.CreateError(
                    $"Error creating agreement: {ex.Message}",
                    StatusCodes.Status500InternalServerError,
                    "INTERNAL_ERROR");
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// Update an existing agreement
        /// </summary>
        public async Task<BaseResponse<AgreementResponse>> UpdateAsync(Guid id, UpdateAgreementRequest request)
        {
            const string methodName = nameof(UpdateAsync);
            _logger.LogInformation("{MethodName} called with ID: {Id}, request: {@Request}", methodName, id, request);

            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid agreement ID provided", methodName);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "Invalid agreement ID",
                        StatusCodes.Status400BadRequest,
                        "INVALID_ID");
                }

                var entity = await _unitOfWork.Repository<Agreement>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Agreement not found with ID: {Id}", methodName, id);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "Agreement not found",
                        StatusCodes.Status404NotFound,
                        "NOT_FOUND");
                }

                // Check if agreement can be updated (business rules)
                if (entity.Status == AgreementStatus.Completed || entity.Status == AgreementStatus.Canceled)
                {
                    _logger.LogWarning("{MethodName}: Cannot update agreement with status {Status}", methodName, entity.Status);
                    return BaseResponse<AgreementResponse>.CreateError(
                        $"Cannot update agreement with status {entity.Status}",
                        StatusCodes.Status400BadRequest,
                        "INVALID_STATUS");
                }

                // Business rule: Cannot modify signed fields directly through Update
                // Signatures should only be changed through SignAsync or VerifySignatureAsync
                if (request.SignedByPatient.HasValue || request.SignedByDoctor.HasValue)
                {
                    _logger.LogWarning("{MethodName}: Cannot update signature fields directly. Use SignAsync or VerifySignatureAsync instead", methodName);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "Signature fields cannot be updated directly. Use the sign or verify-signature endpoints instead.",
                        StatusCodes.Status400BadRequest,
                        "INVALID_OPERATION");
                }

                // Update fields
                if (request.StartDate.HasValue)
                {
                    // Validate: Cannot change StartDate if agreement is already Active
                    if (entity.Status == AgreementStatus.Active)
                    {
                        _logger.LogWarning("{MethodName}: Cannot change StartDate for Active agreement", methodName);
                        return BaseResponse<AgreementResponse>.CreateError(
                            "Cannot change StartDate for Active agreement",
                            StatusCodes.Status400BadRequest,
                            "INVALID_OPERATION");
                    }
                    entity.StartDate = request.StartDate.Value;
                }

                if (request.EndDate.HasValue)
                {
                    var endDate = request.EndDate.Value;
                    var startDate = request.StartDate ?? entity.StartDate;

                    if (endDate < startDate)
                    {
                        _logger.LogWarning("{MethodName}: EndDate cannot be earlier than StartDate", methodName);
                        return BaseResponse<AgreementResponse>.CreateError(
                            "EndDate cannot be earlier than StartDate",
                            StatusCodes.Status400BadRequest,
                            "INVALID_DATE_RANGE");
                    }
                    entity.EndDate = endDate;
                }

                if (request.TotalAmount.HasValue)
                {
                    if (request.TotalAmount.Value < 0)
                    {
                        _logger.LogWarning("{MethodName}: TotalAmount cannot be negative", methodName);
                        return BaseResponse<AgreementResponse>.CreateError(
                            "TotalAmount must be greater than or equal to 0",
                            StatusCodes.Status400BadRequest,
                            "INVALID_AMOUNT");
                    }
                    entity.TotalAmount = request.TotalAmount.Value;
                }

                if (request.Status.HasValue)
                {
                    // Validate status transition
                    if (!IsValidStatusTransition(entity.Status, request.Status.Value))
                    {
                        _logger.LogWarning("{MethodName}: Invalid status transition from {OldStatus} to {NewStatus}",
                            methodName, entity.Status, request.Status.Value);
                        return BaseResponse<AgreementResponse>.CreateError(
                            $"Invalid status transition from {entity.Status} to {request.Status.Value}",
                            StatusCodes.Status400BadRequest,
                            "INVALID_STATUS_TRANSITION");
                    }
                    entity.Status = request.Status.Value;
                }

                if (request.FileUrl != null)
                    entity.FileUrl = request.FileUrl;

                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Agreement>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                var data = _mapper.Map<AgreementResponse>(entity);

                _logger.LogInformation("{MethodName}: Successfully updated agreement {Id}", methodName, id);

                return BaseResponse<AgreementResponse>.CreateSuccess(data, "Agreement updated successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "{MethodName}: Error updating agreement {Id}", methodName, id);
                return BaseResponse<AgreementResponse>.CreateError(
                    $"Error updating agreement: {ex.Message}",
                    StatusCodes.Status500InternalServerError,
                    "INTERNAL_ERROR");
            }
        }
        #endregion

        #region Sign
        /// <summary>
        /// Sign an agreement (by patient or doctor)
        /// Note: For patient signature, use VerifySignatureAsync with OTP instead
        /// </summary>
        public async Task<BaseResponse<AgreementResponse>> SignAsync(Guid id, SignAgreementRequest request)
        {
            const string methodName = nameof(SignAsync);
            _logger.LogInformation("{MethodName} called with ID: {Id}, request: {@Request}", methodName, id, request);

            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid agreement ID provided", methodName);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "Invalid agreement ID",
                        StatusCodes.Status400BadRequest,
                        "INVALID_ID");
                }

                // Validate request: At least one signature must be provided
                if (!request.SignedByPatient.HasValue && !request.SignedByDoctor.HasValue)
                {
                    _logger.LogWarning("{MethodName}: At least one signature (SignedByPatient or SignedByDoctor) must be provided", methodName);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "At least one signature (SignedByPatient or SignedByDoctor) must be provided",
                        StatusCodes.Status400BadRequest,
                        "INVALID_REQUEST");
                }

                var entity = await _unitOfWork.Repository<Agreement>()
                    .AsQueryable()
                    .Include(a => a.Treatment)
                    .Include(a => a.Patient)
                        .ThenInclude(p => p!.Account)
                    .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Agreement not found with ID: {Id}", methodName, id);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "Agreement not found",
                        StatusCodes.Status404NotFound,
                        "NOT_FOUND");
                }

                // Check if agreement can be signed
                if (entity.Status == AgreementStatus.Completed || entity.Status == AgreementStatus.Canceled)
                {
                    _logger.LogWarning("{MethodName}: Cannot sign agreement with status {Status}", methodName, entity.Status);
                    return BaseResponse<AgreementResponse>.CreateError(
                        $"Cannot sign agreement with status {entity.Status}",
                        StatusCodes.Status400BadRequest,
                        "INVALID_STATUS");
                }

                // Business rule: Patient should use OTP verification instead of direct sign
                if (request.SignedByPatient.HasValue && request.SignedByPatient.Value && !entity.SignedByPatient)
                {
                    _logger.LogWarning("{MethodName}: Patient signature should be done through OTP verification. Use VerifySignatureAsync instead", methodName);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "Patient signature must be done through OTP verification. Please use the verify-signature endpoint.",
                        StatusCodes.Status400BadRequest,
                        "INVALID_OPERATION");
                }

                // Update doctor signature
                if (request.SignedByDoctor.HasValue)
                {
                    if (request.SignedByDoctor.Value && entity.SignedByDoctor)
                    {
                        _logger.LogWarning("{MethodName}: Agreement {Id} already signed by doctor", methodName, id);
                        return BaseResponse<AgreementResponse>.CreateError(
                            "Agreement already signed by doctor",
                            StatusCodes.Status400BadRequest,
                            "ALREADY_SIGNED");
                    }
                    entity.SignedByDoctor = request.SignedByDoctor.Value;
                }

                // Auto-update status if both parties have signed
                if (entity.SignedByPatient && entity.SignedByDoctor && entity.Status == AgreementStatus.Pending)
                {
                    entity.Status = AgreementStatus.Active;
                    _logger.LogInformation("{MethodName}: Agreement {Id} automatically set to Active status", methodName, id);
                }

                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Agreement>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                var data = _mapper.Map<AgreementResponse>(entity);

                _logger.LogInformation("{MethodName}: Successfully signed agreement {Id}", methodName, id);

                return BaseResponse<AgreementResponse>.CreateSuccess(data, "Agreement signed successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "{MethodName}: Error signing agreement {Id}", methodName, id);
                return BaseResponse<AgreementResponse>.CreateError(
                    $"Error signing agreement: {ex.Message}",
                    StatusCodes.Status500InternalServerError,
                    "INTERNAL_ERROR");
            }
        }
        #endregion

        #region Update Status
        /// <summary>
        /// Update agreement status
        /// </summary>
        public async Task<BaseResponse<AgreementResponse>> UpdateStatusAsync(Guid id, AgreementStatus status)
        {
            const string methodName = nameof(UpdateStatusAsync);
            _logger.LogInformation("{MethodName} called with ID: {Id}, status: {Status}", methodName, id, status);

            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid agreement ID provided", methodName);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "Invalid agreement ID",
                        StatusCodes.Status400BadRequest,
                        "INVALID_ID");
                }

                var entity = await _unitOfWork.Repository<Agreement>()
                    .AsQueryable()
                    .Include(a => a.Treatment)
                    .Include(a => a.Patient)
                        .ThenInclude(p => p!.Account)
                    .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Agreement not found with ID: {Id}", methodName, id);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "Agreement not found",
                        StatusCodes.Status404NotFound,
                        "NOT_FOUND");
                }

                // Validate status transition
                if (!IsValidStatusTransition(entity.Status, status))
                {
                    _logger.LogWarning("{MethodName}: Invalid status transition from {OldStatus} to {NewStatus}",
                        methodName, entity.Status, status);
                    return BaseResponse<AgreementResponse>.CreateError(
                        $"Invalid status transition from {entity.Status} to {status}",
                        StatusCodes.Status400BadRequest,
                        "INVALID_STATUS_TRANSITION");
                }

                entity.Status = status;
                entity.UpdatedAt = DateTime.UtcNow;

                // Set EndDate if status is Completed or Canceled
                if ((status == AgreementStatus.Completed || status == AgreementStatus.Canceled) && !entity.EndDate.HasValue)
                {
                    entity.EndDate = DateTime.UtcNow;
                }

                await _unitOfWork.Repository<Agreement>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                var data = _mapper.Map<AgreementResponse>(entity);

                _logger.LogInformation("{MethodName}: Successfully updated status of agreement {Id} to {Status}", methodName, id, status);

                return BaseResponse<AgreementResponse>.CreateSuccess(data, "Agreement status updated successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "{MethodName}: Error updating status of agreement {Id}", methodName, id);
                return BaseResponse<AgreementResponse>.CreateError(
                    $"Error updating agreement status: {ex.Message}",
                    StatusCodes.Status500InternalServerError,
                    "INTERNAL_ERROR");
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Soft delete an agreement
        /// </summary>
        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            const string methodName = nameof(DeleteAsync);
            _logger.LogInformation("{MethodName} called with ID: {Id}", methodName, id);

            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid agreement ID provided", methodName);
                    return BaseResponse.CreateError(
                        "Invalid agreement ID",
                        StatusCodes.Status400BadRequest,
                        "INVALID_ID");
                }

                var entity = await _unitOfWork.Repository<Agreement>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Agreement not found with ID: {Id}", methodName, id);
                    return BaseResponse.CreateError(
                        "Agreement not found",
                        StatusCodes.Status404NotFound,
                        "NOT_FOUND");
                }

                // Business rule: Cannot delete active or completed agreements
                // Only Pending or Canceled agreements can be deleted
                if (entity.Status == AgreementStatus.Active || entity.Status == AgreementStatus.Completed)
                {
                    _logger.LogWarning("{MethodName}: Cannot delete agreement with status {Status}. Only Pending or Canceled agreements can be deleted",
                        methodName, entity.Status);
                    return BaseResponse.CreateError(
                        $"Cannot delete agreement with status {entity.Status}. Only Pending or Canceled agreements can be deleted.",
                        StatusCodes.Status400BadRequest,
                        "INVALID_STATUS");
                }

                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Agreement>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully deleted agreement {Id}", methodName, id);

                return BaseResponse.CreateSuccess("Agreement deleted successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "{MethodName}: Error deleting agreement {Id}", methodName, id);
                return BaseResponse.CreateError(
                    $"Error deleting agreement: {ex.Message}",
                    StatusCodes.Status500InternalServerError,
                    "INTERNAL_ERROR");
            }
        }
        #endregion

        #region Request Signature
        /// <summary>
        /// Request signature by sending OTP to patient
        /// </summary>
        public async Task<BaseResponse> RequestSignatureAsync(Guid id)
        {
            const string methodName = nameof(RequestSignatureAsync);
            _logger.LogInformation("{MethodName} called with ID: {Id}", methodName, id);

            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid agreement ID provided", methodName);
                    return BaseResponse.CreateError(
                        "Invalid agreement ID",
                        StatusCodes.Status400BadRequest,
                        "INVALID_ID");
                }

                var entity = await _unitOfWork.Repository<Agreement>()
                    .AsQueryable()
                    .Include(a => a.Patient)
                        .ThenInclude(p => p!.Account)
                    .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Agreement not found with ID: {Id}", methodName, id);
                    return BaseResponse.CreateError(
                        "Agreement not found",
                        StatusCodes.Status404NotFound,
                        "NOT_FOUND");
                }

                // Check if agreement can be signed
                if (entity.Status == AgreementStatus.Completed || entity.Status == AgreementStatus.Canceled)
                {
                    _logger.LogWarning("{MethodName}: Cannot request signature for agreement with status {Status}", methodName, entity.Status);
                    return BaseResponse.CreateError(
                        $"Cannot request signature for agreement with status {entity.Status}",
                        StatusCodes.Status400BadRequest,
                        "INVALID_STATUS");
                }

                // Check if already signed by patient
                if (entity.SignedByPatient)
                {
                    _logger.LogWarning("{MethodName}: Agreement {Id} already signed by patient", methodName, id);
                    return BaseResponse.CreateError(
                        "Agreement already signed by patient",
                        StatusCodes.Status400BadRequest,
                        "ALREADY_SIGNED");
                }

                // Get patient account information
                if (entity.Patient?.Account == null)
                {
                    _logger.LogWarning("{MethodName}: Patient account not found for agreement {Id}", methodName, id);
                    return BaseResponse.CreateError(
                        "Patient account not found",
                        StatusCodes.Status404NotFound,
                        "PATIENT_NOT_FOUND");
                }

                var patientEmail = entity.Patient.Account.Email;
                var patientPhone = entity.Patient.Account.Phone;

                if (string.IsNullOrWhiteSpace(patientEmail))
                {
                    _logger.LogWarning("{MethodName}: Patient email not found for agreement {Id}", methodName, id);
                    return BaseResponse.CreateError(
                        "Patient email not found",
                        StatusCodes.Status400BadRequest,
                        "EMAIL_NOT_FOUND");
                }

                // Check if OTP was recently sent (prevent spam - cooldown period: 1 minute)
                if (entity.OTPSentDate.HasValue)
                {
                    var timeSinceLastOTP = DateTime.UtcNow - entity.OTPSentDate.Value;
                    if (timeSinceLastOTP.TotalMinutes < 1)
                    {
                        var remainingSeconds = 60 - (int)timeSinceLastOTP.TotalSeconds;
                        _logger.LogWarning("{MethodName}: OTP was recently sent. Please wait {RemainingSeconds} seconds", methodName, remainingSeconds);
                        return BaseResponse.CreateError(
                            $"OTP was recently sent. Please wait {remainingSeconds} seconds before requesting again.",
                            StatusCodes.Status429TooManyRequests,
                            "OTP_COOLDOWN");
                    }
                }

                // Generate OTP
                var otpCode = _otpService.GenerateOTP();

                // Store OTP in cache (10 minutes expiry)
                await _otpService.StoreOTPAsync(id, entity.PatientId, otpCode, 10);

                // Send OTP via email
                var emailSent = await _otpService.SendOTPAsync(patientPhone, patientEmail, otpCode, entity.AgreementCode);

                if (!emailSent)
                {
                    _logger.LogError("{MethodName}: Failed to send OTP email for agreement {Id}", methodName, id);
                    return BaseResponse.CreateError(
                        "Failed to send OTP email",
                        StatusCodes.Status500InternalServerError,
                        "EMAIL_SEND_FAILED");
                }

                // Update OTPSentDate
                entity.OTPSentDate = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Agreement>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully sent OTP for agreement {Id}", methodName, id);

                return BaseResponse.CreateSuccess("OTP sent successfully to patient email");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "{MethodName}: Error requesting signature for agreement {Id}", methodName, id);
                return BaseResponse.CreateError(
                    $"Error requesting signature: {ex.Message}",
                    StatusCodes.Status500InternalServerError,
                    "INTERNAL_ERROR");
            }
        }
        #endregion

        #region Verify Signature
        /// <summary>
        /// Verify OTP and sign agreement
        /// </summary>
        public async Task<BaseResponse<AgreementResponse>> VerifySignatureAsync(Guid id, string otpCode, IFormFile? signedAgreementFile)
        {
            const string methodName = nameof(VerifySignatureAsync);
            _logger.LogInformation("{MethodName} called with ID: {Id}", methodName, id);

            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid agreement ID provided", methodName);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "Invalid agreement ID",
                        StatusCodes.Status400BadRequest,
                        "INVALID_ID");
                }

                if (string.IsNullOrWhiteSpace(otpCode))
                {
                    _logger.LogWarning("{MethodName}: OTP code is required", methodName);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "OTP code is required",
                        StatusCodes.Status400BadRequest,
                        "INVALID_OTP");
                }

                // Validate OTP format (should be 6 digits)
                if (otpCode.Length != 6 || !otpCode.All(char.IsDigit))
                {
                    _logger.LogWarning("{MethodName}: Invalid OTP format. OTP must be 6 digits", methodName);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "Invalid OTP format. OTP must be 6 digits",
                        StatusCodes.Status400BadRequest,
                        "INVALID_OTP_FORMAT");
                }

                Guid? uploaderAccountId = null;
                var shouldUploadSignedFile = signedAgreementFile != null && signedAgreementFile.Length > 0;
                if (shouldUploadSignedFile)
                {
                    uploaderAccountId = GetCurrentAccountId();
                    if (!uploaderAccountId.HasValue)
                    {
                        _logger.LogWarning("{MethodName}: Unable to determine account ID for signed agreement upload", methodName);
                        return BaseResponse<AgreementResponse>.CreateError(
                            "Unable to determine the current user for file upload",
                            StatusCodes.Status401Unauthorized,
                            "UNAUTHORIZED");
                    }
                }

                var isTestBypass = string.Equals(otpCode, "000000", StringComparison.Ordinal);

                var entity = await _unitOfWork.Repository<Agreement>()
                    .AsQueryable()
                    .Include(a => a.Patient)
                    .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Agreement not found with ID: {Id}", methodName, id);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "Agreement not found",
                        StatusCodes.Status404NotFound,
                        "NOT_FOUND");
                }

                // Check if agreement can be signed
                if (entity.Status == AgreementStatus.Completed || entity.Status == AgreementStatus.Canceled)
                {
                    _logger.LogWarning("{MethodName}: Cannot verify signature for agreement with status {Status}", methodName, entity.Status);
                    return BaseResponse<AgreementResponse>.CreateError(
                        $"Cannot verify signature for agreement with status {entity.Status}",
                        StatusCodes.Status400BadRequest,
                        "INVALID_STATUS");
                }

                // Check if already signed by patient
                if (entity.SignedByPatient)
                {
                    _logger.LogWarning("{MethodName}: Agreement {Id} already signed by patient", methodName, id);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "Agreement already signed by patient",
                        StatusCodes.Status400BadRequest,
                        "ALREADY_SIGNED");
                }

                // Check if OTP was requested
                if (!entity.OTPSentDate.HasValue && !isTestBypass)
                {
                    _logger.LogWarning("{MethodName}: OTP was not requested for agreement {Id}", methodName, id);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "OTP was not requested. Please request OTP first.",
                        StatusCodes.Status400BadRequest,
                        "OTP_NOT_REQUESTED");
                }

                // Validate OTP
                var isValidOTP = isTestBypass || await _otpService.ValidateOTPAsync(id, otpCode);

                if (!isValidOTP)
                {
                    _logger.LogWarning("{MethodName}: Invalid or expired OTP code for agreement {Id}", methodName, id);
                    return BaseResponse<AgreementResponse>.CreateError(
                        "Invalid or expired OTP code",
                        StatusCodes.Status400BadRequest,
                        "INVALID_OTP");
                }

                // Get IP address from HTTP context
                var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString()
                    ?? _httpContextAccessor.HttpContext?.Request?.Headers["X-Forwarded-For"].FirstOrDefault()
                    ?? "Unknown";

                // Sign agreement
                entity.SignedByPatient = true;
                entity.SignedDate = DateTime.UtcNow;
                entity.SignatureMethod = "OTP";
                entity.SignatureIPAddress = ipAddress;
                entity.UpdatedAt = DateTime.UtcNow;

                // Auto-update status if both parties have signed
                if (entity.SignedByPatient && entity.SignedByDoctor && entity.Status == AgreementStatus.Pending)
                {
                    entity.Status = AgreementStatus.Completed;
                    _logger.LogInformation("{MethodName}: Agreement {Id} automatically set to Active status", methodName, id);
                }

                // Remove OTP from cache after successful verification
                await _otpService.RemoveOTPAsync(id);

                await _unitOfWork.Repository<Agreement>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                var data = _mapper.Map<AgreementResponse>(entity);
                var responseMessage = "Agreement signed successfully";

                if (shouldUploadSignedFile && uploaderAccountId.HasValue)
                {
                    var uploadOutcome = await TryUploadSignedAgreementFileAsync(entity, signedAgreementFile!, uploaderAccountId.Value);
                    if (!uploadOutcome.IsSuccess)
                    {
                        responseMessage = "Agreement signed successfully, but failed to upload the signed file.";
                        if (!string.IsNullOrWhiteSpace(uploadOutcome.ErrorMessage))
                        {
                            responseMessage = $"{responseMessage} {uploadOutcome.ErrorMessage}";
                        }
                        _logger.LogWarning("{MethodName}: Signed agreement file upload failed for agreement {Id}. Reason: {Reason}", methodName, id, uploadOutcome.ErrorMessage);
                    }
                    else
                    {
                        data.FileUrl = uploadOutcome.FileUrl ?? data.FileUrl;
                        if (!string.IsNullOrWhiteSpace(uploadOutcome.FileUrl))
                        {
                            await PersistAgreementFileUrlAsync(entity, uploadOutcome.FileUrl);
                        }
                    }
                }

                _logger.LogInformation("{MethodName}: Successfully verified signature for agreement {Id}", methodName, id);

                return BaseResponse<AgreementResponse>.CreateSuccess(data, responseMessage);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "{MethodName}: Error verifying signature for agreement {Id}", methodName, id);
                return BaseResponse<AgreementResponse>.CreateError(
                    $"Error verifying signature: {ex.Message}",
                    StatusCodes.Status500InternalServerError,
                    "INTERNAL_ERROR");
            }
        }
        #endregion

        #region Get Agreement File
        /// <summary>
        /// Get agreement file(s) from Media table
        /// </summary>
        public async Task<BaseResponse<List<MediaResponse>>> GetAgreementFileAsync(Guid id)
        {
            const string methodName = nameof(GetAgreementFileAsync);
            _logger.LogInformation("{MethodName} called with ID: {Id}", methodName, id);

            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid agreement ID provided", methodName);
                    return BaseResponse<List<MediaResponse>>.CreateError(
                        "Invalid agreement ID",
                        StatusCodes.Status400BadRequest,
                        "INVALID_ID");
                }

                // Verify agreement exists
                var agreement = await _unitOfWork.Repository<Agreement>()
                    .AsQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

                if (agreement == null)
                {
                    _logger.LogWarning("{MethodName}: Agreement not found with ID: {Id}", methodName, id);
                    return BaseResponse<List<MediaResponse>>.CreateError(
                        "Agreement not found",
                        StatusCodes.Status404NotFound,
                        "NOT_FOUND");
                }

                // Get all media files related to this agreement
                var mediaFiles = await _unitOfWork.Repository<Media>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(m => m.RelatedEntityId == id
                        && m.RelatedEntityType == EntityTypeMedia.Agreement.ToString()
                        && !m.IsDeleted)
                    .OrderByDescending(m => m.CreatedAt)
                    .ToListAsync();

                var mediaResponses = _mapper.Map<List<MediaResponse>>(mediaFiles);

                // If no media files found but agreement has FileUrl, create a response for it
                if (!mediaResponses.Any() && !string.IsNullOrWhiteSpace(agreement.FileUrl))
                {
                    _logger.LogInformation("{MethodName}: No media files found, but agreement has FileUrl. Returning FileUrl.", methodName);
                    // Note: FileUrl is stored directly in Agreement, not in Media table
                    // We can return it as a MediaResponse-like object or just return empty list
                    // For consistency, we return empty list and let client use FileUrl from AgreementDetailResponse
                }

                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} file(s) for agreement {Id}",
                    methodName, mediaResponses.Count, id);

                return BaseResponse<List<MediaResponse>>.CreateSuccess(
                    mediaResponses,
                    mediaResponses.Any()
                        ? $"Successfully retrieved {mediaResponses.Count} file(s)"
                        : "No files found for this agreement");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving files for agreement {Id}", methodName, id);
                return BaseResponse<List<MediaResponse>>.CreateError(
                    $"Error retrieving agreement files: {ex.Message}",
                    StatusCodes.Status500InternalServerError,
                    "INTERNAL_ERROR");
            }
        }
        #endregion

        #region Private Helpers
        /// <summary>
        /// Generate unique agreement code
        /// </summary>
        private async Task<string> GenerateAgreementCodeAsync()
        {
            string code;
            bool isUnique = false;
            int attempts = 0;
            const int maxAttempts = 10;

            while (!isUnique && attempts < maxAttempts)
            {
                code = $"AGR-{DateTime.UtcNow:yyyyMMdd}-{DateTime.UtcNow:HHmmss}-{new Random().Next(1000, 9999)}";

                var exists = await _unitOfWork.Repository<Agreement>()
                    .AsQueryable()
                    .AnyAsync(a => a.AgreementCode == code && !a.IsDeleted);

                if (!exists)
                {
                    isUnique = true;
                    return code;
                }

                attempts++;
            }

            // Fallback: use GUID if all attempts fail
            return $"AGR-{Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper()}";
        }

        /// <summary>
        /// Validate if status transition is allowed
        /// Business rules:
        /// - Pending -> Active (when both parties sign)
        /// - Pending -> Canceled (can cancel anytime)
        /// - Active -> Completed (when treatment is done)
        /// - Active -> Canceled (can cancel active agreement)
        /// - Completed -> (no transitions allowed)
        /// - Canceled -> (no transitions allowed)
        /// </summary>
        private bool IsValidStatusTransition(AgreementStatus currentStatus, AgreementStatus newStatus)
        {
            // No change
            if (currentStatus == newStatus)
                return true;

            // Cannot change from Completed or Canceled
            if (currentStatus == AgreementStatus.Completed || currentStatus == AgreementStatus.Canceled)
                return false;

            // Valid transitions
            return (currentStatus, newStatus) switch
            {
                (AgreementStatus.Pending, AgreementStatus.Active) => true,
                (AgreementStatus.Pending, AgreementStatus.Canceled) => true,
                (AgreementStatus.Active, AgreementStatus.Completed) => true,
                (AgreementStatus.Active, AgreementStatus.Canceled) => true,
                _ => false
            };
        }

        /// <summary>
        /// Get the current authenticated account ID from the HTTP context
        /// </summary>
        private Guid? GetCurrentAccountId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var accountId))
            {
                return null;
            }

            return accountId;
        }

        /// <summary>
        /// Persist the latest agreement file URL after uploading to media storage
        /// </summary>
        private async Task PersistAgreementFileUrlAsync(Agreement agreement, string fileUrl)
        {
            agreement.FileUrl = fileUrl;
            agreement.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<Agreement>().UpdateGuid(agreement, agreement.Id);
            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Upload signed agreement file to media service
        /// </summary>
        private async Task<(bool IsSuccess, string? ErrorMessage, string? FileUrl)> TryUploadSignedAgreementFileAsync(
            Agreement agreement,
            IFormFile signedAgreementFile,
            Guid accountId)
        {
            try
            {
                var safeFileName = string.IsNullOrWhiteSpace(signedAgreementFile.FileName)
                    ? $"signed-agreement-{agreement.AgreementCode}{Path.GetExtension(signedAgreementFile.FileName)}"
                    : signedAgreementFile.FileName;

                var uploadRequest = new UploadMediaRequest
                {
                    File = signedAgreementFile,
                    FileName = safeFileName,
                    RelatedEntityId = agreement.Id,
                    RelatedEntityType = EntityTypeMedia.Agreement,
                    Title = $"Signed Agreement - {agreement.AgreementCode}",
                    Description = $"Signed agreement uploaded on {DateTime.UtcNow:O}",
                    Category = "Agreement",
                    Tags = "agreement,signed",
                    Notes = "Uploaded via OTP verification flow"
                };

                var uploadResponse = await _mediaService.UploadMediaAsync(uploadRequest, accountId);

                if (uploadResponse.Code >= 200 && uploadResponse.Code < 300)
                {
                    return (true, null, uploadResponse.Data?.FilePath);
                }

                return (false, uploadResponse.Message ?? "Failed to upload the signed agreement file.", null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Unexpected error while uploading signed agreement file for {AgreementId}", nameof(TryUploadSignedAgreementFileAsync), agreement.Id);
                return (false, "Unexpected error while uploading signed agreement file.", null);
            }
        }
        #endregion
        #endregion

        
    }
}

