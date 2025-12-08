using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.Mapping;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Services
{
    public class TreatmentService : ITreatmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TreatmentService> _logger;
        private readonly ITreatmentIUIService _treatmentIUIService;
        private readonly ITreatmentIVFService _treatmentIVFService;

        public TreatmentService(
            IUnitOfWork unitOfWork,
            ILogger<TreatmentService> logger,
            ITreatmentIUIService treatmentIUIService,
            ITreatmentIVFService treatmentIVFService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _treatmentIUIService = treatmentIUIService ?? throw new ArgumentNullException(nameof(treatmentIUIService));
            _treatmentIVFService = treatmentIVFService ?? throw new ArgumentNullException(nameof(treatmentIVFService));
        }

        public async Task<DynamicResponse<TreatmentResponseModel>> GetAllAsync(GetTreatmentsRequest request)
        {
            const string methodName = nameof(GetAllAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                if (request == null)
                {
                    return new DynamicResponse<TreatmentResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_REQUEST",
                        Message = "Request cannot be null",
                        MetaData = new PagingMetaData(),
                        Data = new List<TreatmentResponseModel>()
                    };
                }

                request.Normalize();

                var query = _unitOfWork.Repository<Treatment>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(t => t.Patient)
                    .Include(t => t.Doctor)
                    .Include(t => t.TreatmentIVF)
                    .Where(t => !t.IsDeleted);

                if (request.PatientId.HasValue)
                {
                    query = query.Where(t => t.PatientId == request.PatientId.Value);
                }

                if (request.DoctorId.HasValue)
                {
                    query = query.Where(t => t.DoctorId == request.DoctorId.Value);
                }

                if (request.TreatmentType.HasValue)
                {
                    query = query.Where(t => t.TreatmentType == request.TreatmentType.Value);
                }

                if (request.Status.HasValue)
                {
                    query = query.Where(t => t.Status == request.Status.Value);
                }

                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    var term = request.SearchTerm.ToLowerInvariant();
                    query = query.Where(t => t.TreatmentName.ToLower().Contains(term) ||
                                              (t.Diagnosis != null && t.Diagnosis.ToLower().Contains(term)) ||
                                              (t.Notes != null && t.Notes.ToLower().Contains(term)));
                }

                if (request.StartDateFrom.HasValue)
                {
                    query = query.Where(t => t.StartDate >= request.StartDateFrom.Value);
                }

                if (request.StartDateTo.HasValue)
                {
                    query = query.Where(t => t.StartDate <= request.StartDateTo.Value);
                }

                var total = await query.CountAsync();

                var isDesc = request.Order?.ToLower() == "desc";
                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    switch (request.Sort.ToLower())
                    {
                        case "startdate":
                            query = isDesc ? query.OrderByDescending(t => t.StartDate) : query.OrderBy(t => t.StartDate);
                            break;
                        case "enddate":
                            query = isDesc ? query.OrderByDescending(t => t.EndDate) : query.OrderBy(t => t.EndDate);
                            break;
                        case "status":
                            query = isDesc ? query.OrderByDescending(t => t.Status) : query.OrderBy(t => t.Status);
                            break;
                        case "name":
                            query = isDesc ? query.OrderByDescending(t => t.TreatmentName) : query.OrderBy(t => t.TreatmentName);
                            break;
                        default:
                            query = isDesc ? query.OrderByDescending(t => t.CreatedAt) : query.OrderBy(t => t.CreatedAt);
                            break;
                    }
                }
                else
                {
                    query = query.OrderByDescending(t => t.StartDate);
                }

                var items = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                // Load agreements for all treatments in batch
                var treatmentIds = items.Select(t => t.Id).ToList();
                var agreements = await _unitOfWork.Repository<Agreement>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(a => a.Treatment)
                    .Include(a => a.Patient)
                        .ThenInclude(p => p!.Account)
                    .Where(a => treatmentIds.Contains(a.TreatmentId) && !a.IsDeleted)
                    .ToListAsync();

                var agreementsByTreatmentId = agreements
                    .GroupBy(a => a.TreatmentId)
                    .ToDictionary(g => g.Key, g => g.Select(a => a.ToAgreementResponse()).ToList());

                var data = items.Select(treatment =>
                {
                    var response = treatment.ToResponseModel();
                    if (agreementsByTreatmentId.TryGetValue(treatment.Id, out var treatmentAgreements))
                    {
                        response.Agreements = treatmentAgreements;
                    }
                    return response;
                }).ToList();

                return new DynamicResponse<TreatmentResponseModel>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Treatments retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = total,
                        CurrentPageSize = data.Count
                    },
                    Data = data
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving treatments", methodName);
                return new DynamicResponse<TreatmentResponseModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred",
                    MetaData = new PagingMetaData(),
                    Data = new List<TreatmentResponseModel>()
                };
            }
        }

        /// <summary>
        /// Retrieves a treatment by its unique identifier with all related data (IVF/IUI, Agreements)
        /// </summary>
        /// <param name="id">The unique identifier of the treatment</param>
        /// <returns>BaseResponse containing TreatmentDetailResponseModel with all related information</returns>
        public async Task<BaseResponse<TreatmentDetailResponseModel>> GetByIdAsync(Guid id)
        {
            const string methodName = nameof(GetByIdAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

            try
            {
                // Validate input parameter
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid treatment ID provided - {TreatmentId}", methodName, id);
                    return BaseResponse<TreatmentDetailResponseModel>.CreateError(
                        "Treatment ID cannot be empty", 
                        StatusCodes.Status400BadRequest, 
                        "INVALID_ID");
                }

                // Retrieve treatment entity with related data in a single optimized query
                var treatment = await GetTreatmentWithRelatedDataAsync(id);
                
                if (treatment == null)
                {
                    _logger.LogWarning("{MethodName}: Treatment not found with ID: {TreatmentId}", methodName, id);
                    return BaseResponse<TreatmentDetailResponseModel>.CreateError(
                        "Treatment not found", 
                        StatusCodes.Status404NotFound, 
                        "TREATMENT_NOT_FOUND");
                }

                // Map entity to response model
                var responseModel = await MapToDetailResponseModelAsync(treatment, id);

                _logger.LogInformation(
                    "{MethodName}: Successfully retrieved treatment {TreatmentId} with Type: {TreatmentType}", 
                    methodName, id, treatment.TreatmentType);

                return BaseResponse<TreatmentDetailResponseModel>.CreateSuccess(
                    responseModel, 
                    "Treatment retrieved successfully", 
                    StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Unexpected error retrieving treatment {Id}", methodName, id);
                return BaseResponse<TreatmentDetailResponseModel>.CreateError(
                    "An error occurred while retrieving the treatment. Please try again later.", 
                    StatusCodes.Status500InternalServerError, 
                    "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Retrieves treatment entity with all related navigation properties
        /// </summary>
        private async Task<Treatment?> GetTreatmentWithRelatedDataAsync(Guid id)
        {
            return await _unitOfWork.Repository<Treatment>()
                .GetQueryable()
                .AsNoTracking()
                .Include(t => t.Patient)
                .Include(t => t.Doctor)
                .Include(t => t.TreatmentIVF)
                .Include(t => t.TreatmentIUI)
                .Where(t => t.Id == id && !t.IsDeleted)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Maps Treatment entity to TreatmentDetailResponseModel with all related data
        /// </summary>
        private async Task<TreatmentDetailResponseModel> MapToDetailResponseModelAsync(Treatment treatment, Guid treatmentId)
        {
            // Map base treatment data
            var responseModel = treatment.ToDetailResponseModel();

            // Load and map IVF data if treatment type is IVF
            if (treatment.TreatmentType == TreatmentType.IVF && 
                treatment.TreatmentIVF != null && 
                !treatment.TreatmentIVF.IsDeleted)
            {
                responseModel.IVF = treatment.TreatmentIVF.ToResponseModel();
            }

            // Load and map IUI data if treatment type is IUI
            if (treatment.TreatmentType == TreatmentType.IUI)
            {
                var iui = treatment.TreatmentIUI;
                if (iui == null || iui.IsDeleted)
                {
                    iui = await _unitOfWork.Repository<TreatmentIUI>()
                        .GetQueryable()
                        .AsNoTracking()
                        .Where(x => x.Id == treatmentId && !x.IsDeleted)
                        .FirstOrDefaultAsync();
                }

                if (iui != null)
                {
                    responseModel.IUI = iui.ToResponseModel();
                }
            }

            // Load and map agreements
            var agreements = await LoadAgreementsAsync(treatmentId);
            
            if (agreements.Any())
            {
                var agreementList = agreements.Select(a => a.ToAgreementResponse()).ToList();
                responseModel.Agreements = agreementList;

                // Attach agreements to IVF/IUI if they exist
                if (responseModel.IVF != null)
                {
                    responseModel.IVF.Agreements = agreementList;
                }

                if (responseModel.IUI != null)
                {
                    responseModel.IUI.Agreements = agreementList;
                }
            }

            return responseModel;
        }

        /// <summary>
        /// Loads all agreements associated with a treatment
        /// </summary>
        private async Task<List<Agreement>> LoadAgreementsAsync(Guid treatmentId)
        {
            return await _unitOfWork.Repository<Agreement>()
                .GetQueryable()
                .AsNoTracking()
                .Include(a => a.Treatment)
                .Include(a => a.Patient)
                    .ThenInclude(p => p!.Account)
                .Where(a => a.TreatmentId == treatmentId && !a.IsDeleted)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<BaseResponse<TreatmentResponseModel>> CreateAsync(TreatmentCreateUpdateRequest request)
        {
            const string methodName = nameof(CreateAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (request == null)
                {
                    return BaseResponse<TreatmentResponseModel>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                // Validate foreign keys
                var patientExists = await _unitOfWork.Repository<Patient>().GetQueryable().AnyAsync(p => p.Id == request.PatientId && !p.IsDeleted);
                if (!patientExists)
                {
                    return BaseResponse<TreatmentResponseModel>.CreateError("Patient not found", StatusCodes.Status404NotFound, "PATIENT_NOT_FOUND");
                }

                var doctorExists = await _unitOfWork.Repository<Doctor>().GetQueryable().AnyAsync(d => d.Id == request.DoctorId && !d.IsDeleted);
                if (!doctorExists)
                {
                    return BaseResponse<TreatmentResponseModel>.CreateError("Doctor not found", StatusCodes.Status404NotFound, "DOCTOR_NOT_FOUND");
                }

                // Validate IUI/IVF data consistency with TreatmentType
                if (request.TreatmentType == TreatmentType.IUI && request.IVF != null)
                {
                    return BaseResponse<TreatmentResponseModel>.CreateError("Cannot provide IVF data when TreatmentType is IUI", StatusCodes.Status400BadRequest, "INVALID_TREATMENT_DATA");
                }

                if (request.TreatmentType == TreatmentType.IVF && request.IUI != null)
                {
                    return BaseResponse<TreatmentResponseModel>.CreateError("Cannot provide IUI data when TreatmentType is IVF", StatusCodes.Status400BadRequest, "INVALID_TREATMENT_DATA");
                }

                // Create Treatment entity
                var entity = request.ToEntity();
                await _unitOfWork.Repository<Treatment>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Treatment created with Id: {TreatmentId}, Type: {TreatmentType}", methodName, entity.Id, entity.TreatmentType);

                // Automatically create IUI or IVF record based on TreatmentType
                if (request.TreatmentType == TreatmentType.IUI)
                {
                    // Validate that Protocol is provided if IUI data is provided
                    if (request.IUI != null && string.IsNullOrWhiteSpace(request.IUI.Protocol))
                    {
                        await transaction.RollbackAsync();
                        return BaseResponse<TreatmentResponseModel>.CreateError("Protocol is required when providing IUI data", StatusCodes.Status400BadRequest, "INVALID_IUI_DATA");
                    }

                    var iuiRequest = request.IUI ?? new TreatmentIUICreateUpdateRequest
                    {
                        TreatmentId = entity.Id,
                        Protocol = "Default IUI Protocol" // Default protocol if not provided
                    };

                    // Ensure TreatmentId matches the created Treatment
                    iuiRequest.TreatmentId = entity.Id;

                    var iuiResult = await _treatmentIUIService.CreateAsync(iuiRequest);
                    if (!iuiResult.Success)
                    {
                        await transaction.RollbackAsync();
                        _logger.LogError("{MethodName}: Failed to create IUI record for Treatment {TreatmentId}: {Error}", 
                            methodName, entity.Id, iuiResult.Message);
                        return BaseResponse<TreatmentResponseModel>.CreateError(
                            $"Failed to create IUI record: {iuiResult.Message}", 
                            iuiResult.Code ?? StatusCodes.Status500InternalServerError, 
                            iuiResult.SystemCode ?? "IUI_CREATE_FAILED");
                    }

                    _logger.LogInformation("{MethodName}: IUI record created successfully for Treatment {TreatmentId}", methodName, entity.Id);
                }
                else if (request.TreatmentType == TreatmentType.IVF)
                {
                    // Validate that Protocol is provided if IVF data is provided
                    if (request.IVF != null && string.IsNullOrWhiteSpace(request.IVF.Protocol))
                    {
                        await transaction.RollbackAsync();
                        return BaseResponse<TreatmentResponseModel>.CreateError("Protocol is required when providing IVF data", StatusCodes.Status400BadRequest, "INVALID_IVF_DATA");
                    }

                    var ivfRequest = request.IVF ?? new TreatmentIVFCreateUpdateRequest
                    {
                        TreatmentId = entity.Id,
                        Protocol = "Default IVF Protocol" // Default protocol if not provided
                    };

                    // Ensure TreatmentId matches the created Treatment
                    ivfRequest.TreatmentId = entity.Id;

                    var ivfResult = await _treatmentIVFService.CreateAsync(ivfRequest);
                    if (!ivfResult.Success)
                    {
                        await transaction.RollbackAsync();
                        _logger.LogError("{MethodName}: Failed to create IVF record for Treatment {TreatmentId}: {Error}", 
                            methodName, entity.Id, ivfResult.Message);
                        return BaseResponse<TreatmentResponseModel>.CreateError(
                            $"Failed to create IVF record: {ivfResult.Message}", 
                            ivfResult.Code ?? StatusCodes.Status500InternalServerError, 
                            ivfResult.SystemCode ?? "IVF_CREATE_FAILED");
                    }

                    _logger.LogInformation("{MethodName}: IVF record created successfully for Treatment {TreatmentId}", methodName, entity.Id);
                }

                // Auto-create treatment cycles if requested
                var createdCycles = new List<TreatmentCycle>();
                if (request.AutoCreate)
                {
                    if (request.TreatmentType == TreatmentType.IUI)
                    {
                        createdCycles = await CreateIUICyclesAsync(entity.Id, request.PreferredStartDate ?? entity.StartDate, request.IUI?.Protocol ?? "Default IUI Protocol", methodName);
                    }
                    else if (request.TreatmentType == TreatmentType.IVF)
                    {
                        createdCycles = await CreateIVFCyclesAsync(entity.Id, request.PreferredStartDate ?? entity.StartDate, request.IVF?.Protocol ?? "Default IVF Protocol", methodName);
                    }
                }

                // Commit all changes (Treatment + IUI/IVF + Cycles) in one transaction
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                // Retrieve the created treatment with related data
                var created = await _unitOfWork.Repository<Treatment>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(t => t.TreatmentIVF)
                    .FirstOrDefaultAsync(t => t.Id == entity.Id);

                // If cycles were auto-created, return extended response with cycles
                if (request.AutoCreate && createdCycles.Any())
                {
                    var baseResponse = created!.ToResponseModel();
                    var responseWithCycles = new TreatmentWithCyclesResponseModel
                    {
                        Id = baseResponse.Id,
                        PatientId = baseResponse.PatientId,
                        DoctorId = baseResponse.DoctorId,
                        TreatmentName = baseResponse.TreatmentName,
                        TreatmentType = baseResponse.TreatmentType,
                        StartDate = baseResponse.StartDate,
                        EndDate = baseResponse.EndDate,
                        Status = baseResponse.Status,
                        Diagnosis = baseResponse.Diagnosis,
                        Goals = baseResponse.Goals,
                        Notes = baseResponse.Notes,
                        EstimatedCost = baseResponse.EstimatedCost,
                        ActualCost = baseResponse.ActualCost,
                        CreatedAt = baseResponse.CreatedAt,
                        UpdatedAt = baseResponse.UpdatedAt,
                        AutoCreatedCycles = createdCycles.Select(c => c.ToResponseModel()).ToList()
                    };

                    _logger.LogInformation("{MethodName}: Treatment created with {CycleCount} auto-generated cycles", methodName, createdCycles.Count);
                    return BaseResponse<TreatmentResponseModel>.CreateSuccess(responseWithCycles, $"Treatment created successfully with {createdCycles.Count} auto-generated cycle(s)", StatusCodes.Status201Created);
                }

                return BaseResponse<TreatmentResponseModel>.CreateSuccess(created!.ToResponseModel(), "Treatment created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "{MethodName}: Error creating treatment", methodName);
                return BaseResponse<TreatmentResponseModel>.CreateError($"Error creating treatment: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<TreatmentResponseModel>> UpdateAsync(Guid id, TreatmentUpdateRequest request)
        {
            const string methodName = nameof(UpdateAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}, request: {@Request}", methodName, id, request);

            try
            {
                if (id == Guid.Empty)
                {
                    return BaseResponse<TreatmentResponseModel>.CreateError("Treatment ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                if (request == null)
                {
                    return BaseResponse<TreatmentResponseModel>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                var entity = await _unitOfWork.Repository<Treatment>()
                    .GetQueryable()
                    .Where(t => t.Id == id && !t.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    return BaseResponse<TreatmentResponseModel>.CreateError("Treatment not found", StatusCodes.Status404NotFound, "TREATMENT_NOT_FOUND");
                }

                // Validate foreign keys only if they are being updated
                if (request.PatientId.HasValue)
                {
                    var patientExists = await _unitOfWork.Repository<Patient>().GetQueryable().AnyAsync(p => p.Id == request.PatientId.Value && !p.IsDeleted);
                    if (!patientExists)
                    {
                        return BaseResponse<TreatmentResponseModel>.CreateError("Patient not found", StatusCodes.Status404NotFound, "PATIENT_NOT_FOUND");
                    }
                }

                if (request.DoctorId.HasValue)
                {
                    var doctorExists = await _unitOfWork.Repository<Doctor>().GetQueryable().AnyAsync(d => d.Id == request.DoctorId.Value && !d.IsDeleted);
                    if (!doctorExists)
                    {
                        return BaseResponse<TreatmentResponseModel>.CreateError("Doctor not found", StatusCodes.Status404NotFound, "DOCTOR_NOT_FOUND");
                    }
                }

                // Validate IUI/IVF data consistency with TreatmentType if TreatmentType is being updated
                if (request.TreatmentType.HasValue)
                {
                    if (request.TreatmentType.Value == TreatmentType.IUI && request.IVF != null)
                    {
                        return BaseResponse<TreatmentResponseModel>.CreateError("Cannot provide IVF data when TreatmentType is IUI", StatusCodes.Status400BadRequest, "INVALID_TREATMENT_DATA");
                    }

                    if (request.TreatmentType.Value == TreatmentType.IVF && request.IUI != null)
                    {
                        return BaseResponse<TreatmentResponseModel>.CreateError("Cannot provide IUI data when TreatmentType is IVF", StatusCodes.Status400BadRequest, "INVALID_TREATMENT_DATA");
                    }
                }
                else
                {
                    // If TreatmentType is not being updated, validate against current TreatmentType
                    if (entity.TreatmentType == TreatmentType.IUI && request.IVF != null)
                    {
                        return BaseResponse<TreatmentResponseModel>.CreateError("Cannot provide IVF data when TreatmentType is IUI", StatusCodes.Status400BadRequest, "INVALID_TREATMENT_DATA");
                    }

                    if (entity.TreatmentType == TreatmentType.IVF && request.IUI != null)
                    {
                        return BaseResponse<TreatmentResponseModel>.CreateError("Cannot provide IUI data when TreatmentType is IVF", StatusCodes.Status400BadRequest, "INVALID_TREATMENT_DATA");
                    }
                }

                // Update Treatment entity (partial update)
                entity.UpdateEntity(request);
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<Treatment>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();

                // Handle IUI/IVF updates if provided
                var currentTreatmentType = request.TreatmentType ?? entity.TreatmentType;
                if (currentTreatmentType == TreatmentType.IUI && request.IUI != null)
                {
                    request.IUI.TreatmentId = id;
                    var existingIUI = await _unitOfWork.Repository<TreatmentIUI>()
                        .GetQueryable()
                        .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                    if (existingIUI != null)
                    {
                        existingIUI.UpdateEntity(request.IUI);
                        existingIUI.UpdatedAt = DateTime.UtcNow;
                        await _unitOfWork.Repository<TreatmentIUI>().UpdateGuid(existingIUI, id);
                    }
                    else
                    {
                        var iuiResult = await _treatmentIUIService.CreateAsync(request.IUI);
                        if (!iuiResult.Success)
                        {
                            _logger.LogWarning("{MethodName}: Failed to create IUI record for Treatment {TreatmentId}: {Error}",
                                methodName, id, iuiResult.Message);
                        }
                    }
                    await _unitOfWork.CommitAsync();
                }
                else if (currentTreatmentType == TreatmentType.IVF && request.IVF != null)
                {
                    request.IVF.TreatmentId = id;
                    var existingIVF = await _unitOfWork.Repository<TreatmentIVF>()
                        .GetQueryable()
                        .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                    if (existingIVF != null)
                    {
                        existingIVF.UpdateEntity(request.IVF);
                        existingIVF.UpdatedAt = DateTime.UtcNow;
                        await _unitOfWork.Repository<TreatmentIVF>().UpdateGuid(existingIVF, id);
                    }
                    else
                    {
                        var ivfResult = await _treatmentIVFService.CreateAsync(request.IVF);
                        if (!ivfResult.Success)
                        {
                            _logger.LogWarning("{MethodName}: Failed to create IVF record for Treatment {TreatmentId}: {Error}",
                                methodName, id, ivfResult.Message);
                        }
                    }
                    await _unitOfWork.CommitAsync();
                }

                // Retrieve the updated treatment with related data
                var updated = await _unitOfWork.Repository<Treatment>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(t => t.TreatmentIVF)
                    .FirstOrDefaultAsync(t => t.Id == id);

                return BaseResponse<TreatmentResponseModel>.CreateSuccess(updated!.ToResponseModel(), "Treatment updated successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating treatment {Id}", methodName, id);
                return BaseResponse<TreatmentResponseModel>.CreateError($"Error updating treatment: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Update treatment status only
        /// </summary>
        public async Task<BaseResponse> UpdateStatusAsync(Guid id, TreatmentStatus status)
        {
            const string methodName = nameof(UpdateStatusAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}, status: {Status}", methodName, id, status);

            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid treatment ID provided - {TreatmentId}", methodName, id);
                    return BaseResponse.CreateError("Treatment ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_TREATMENT_ID");
                }

                var treatment = await _unitOfWork.Repository<Treatment>()
                    .GetQueryable()
                    .Where(t => t.Id == id && !t.IsDeleted)
                    .FirstOrDefaultAsync();

                if (treatment == null)
                {
                    _logger.LogWarning("{MethodName}: Treatment not found with ID: {TreatmentId}", methodName, id);
                    return BaseResponse.CreateError("Treatment not found", StatusCodes.Status404NotFound, "TREATMENT_NOT_FOUND");
                }

                treatment.Status = status;
                treatment.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Treatment>().UpdateGuid(treatment, id);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully updated treatment status {TreatmentId} to {Status}", methodName, id, status);
                return BaseResponse.CreateSuccess($"Treatment status updated to {status}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating treatment status {TreatmentId}", methodName, id);
                return BaseResponse.CreateError($"Error updating treatment status: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Guid id)
        {
            const string methodName = nameof(DeleteAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

            try
            {
                if (id == Guid.Empty)
                {
                    return BaseResponse<bool>.CreateError("Treatment ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var entity = await _unitOfWork.Repository<Treatment>()
                    .GetQueryable()
                    .Include(t => t.TreatmentIVF)
                    .Where(t => t.Id == id && !t.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    return BaseResponse<bool>.CreateError("Treatment not found", StatusCodes.Status404NotFound, "TREATMENT_NOT_FOUND");
                }

                // For safety, also soft delete linked IVF and IUI if present
                var iui = await _unitOfWork.Repository<TreatmentIUI>()
                    .GetQueryable()
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .FirstOrDefaultAsync();
                if (iui != null)
                {
                    iui.IsDeleted = true;
                    iui.DeletedAt = DateTime.UtcNow;
                    iui.UpdatedAt = DateTime.UtcNow;
                    await _unitOfWork.Repository<TreatmentIUI>().UpdateGuid(iui, iui.Id);
                }

                if (entity.TreatmentIVF != null && !entity.TreatmentIVF.IsDeleted)
                {
                    entity.TreatmentIVF.IsDeleted = true;
                    entity.TreatmentIVF.DeletedAt = DateTime.UtcNow;
                    entity.TreatmentIVF.UpdatedAt = DateTime.UtcNow;
                    await _unitOfWork.Repository<TreatmentIVF>().UpdateGuid(entity.TreatmentIVF, entity.TreatmentIVF.Id);
                }

                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<Treatment>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();

                return BaseResponse<bool>.CreateSuccess(true, "Treatment deleted successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error deleting treatment {Id}", methodName, id);
                return BaseResponse<bool>.CreateError($"Error deleting treatment: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Cancels all remaining Planned cycles for a treatment when pregnancy is detected.
        /// This method should be called when any cycle results in pregnancy (e.g., when TreatmentIVF.Status == PregnancyPositive).
        /// </summary>
        /// <param name="treatmentId">The treatment ID</param>
        /// <param name="excludeCycleId">Optional cycle ID to exclude from cancellation (the cycle that resulted in pregnancy)</param>
        /// <returns>Number of cycles cancelled</returns>
        public async Task<int> CancelRemainingPlannedCyclesAsync(Guid treatmentId, Guid? excludeCycleId = null)
        {
            const string methodName = nameof(CancelRemainingPlannedCyclesAsync);
            _logger.LogInformation("{MethodName}: Cancelling remaining Planned cycles for Treatment {TreatmentId}", methodName, treatmentId);

            try
            {
                var plannedCycles = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .Where(tc => tc.TreatmentId == treatmentId 
                        && tc.Status == TreatmentStatus.Planned 
                        && !tc.IsDeleted
                        && (excludeCycleId == null || tc.Id != excludeCycleId.Value))
                    .ToListAsync();

                if (!plannedCycles.Any())
                {
                    _logger.LogInformation("{MethodName}: No Planned cycles found to cancel for Treatment {TreatmentId}", methodName, treatmentId);
                    return 0;
                }

                var now = DateTime.UtcNow;
                foreach (var cycle in plannedCycles)
                {
                    cycle.Status = TreatmentStatus.Cancelled;
                    cycle.UpdatedAt = now;
                    await _unitOfWork.Repository<TreatmentCycle>().UpdateGuid(cycle, cycle.Id);
                    _logger.LogInformation("{MethodName}: Cancelled cycle {CycleId} (Cycle #{CycleNumber}) for Treatment {TreatmentId}", 
                        methodName, cycle.Id, cycle.CycleNumber, treatmentId);
                }

                await _unitOfWork.CommitAsync();
                _logger.LogInformation("{MethodName}: Successfully cancelled {Count} Planned cycle(s) for Treatment {TreatmentId}", 
                    methodName, plannedCycles.Count, treatmentId);

                return plannedCycles.Count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error cancelling Planned cycles for Treatment {TreatmentId}", methodName, treatmentId);
                throw;
            }
        }

        /// <summary>
        /// Determines the status of a treatment cycle based on its scheduled date and position in the sequence.
        /// Unified logic for both IUI and IVF treatments:
        /// - Completed: steps with ScheduledDate < now (past steps)
        /// - Scheduled: the first step with ScheduledDate >= now (next upcoming step)
        /// - Planned: all remaining steps after the Scheduled step
        /// </summary>
        /// <param name="scheduledDate">The scheduled date of the cycle step</param>
        /// <param name="currentIndex">The current index in the step plan</param>
        /// <param name="scheduledIndex">The index of the first upcoming step (Scheduled status)</param>
        /// <param name="now">The current UTC time</param>
        /// <returns>The appropriate TreatmentStatus for the cycle</returns>
        private TreatmentStatus DetermineCycleStatus(DateTime scheduledDate, int currentIndex, int scheduledIndex, DateTime now)
        {
            // If the scheduled date is in the past, mark as Completed
            if (scheduledDate < now)
            {
                return TreatmentStatus.Completed;
            }

            // If this is the first upcoming step (scheduledIndex), mark as Scheduled
            if (currentIndex == scheduledIndex)
            {
                return TreatmentStatus.Scheduled;
            }

            // All other future steps are Planned
            return TreatmentStatus.Planned;
        }
    }
}


