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
    public class TreatmentIUIService : ITreatmentIUIService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TreatmentIUIService> _logger;

        public TreatmentIUIService(IUnitOfWork unitOfWork, ILogger<TreatmentIUIService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BaseResponse<TreatmentIUIResponseModel>> GetByTreatmentIdAsync(Guid treatmentId)
        {
            const string methodName = nameof(GetByTreatmentIdAsync);
            _logger.LogInformation("{MethodName} called with treatmentId: {TreatmentId}", methodName, treatmentId);

            try
            {
                if (treatmentId == Guid.Empty)
                {
                    return BaseResponse<TreatmentIUIResponseModel>.CreateError("Treatment ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var entity = await _unitOfWork.Repository<TreatmentIUI>()
                    .GetQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == treatmentId && !x.IsDeleted);

                if (entity == null)
                {
                    return BaseResponse<TreatmentIUIResponseModel>.CreateError("IUI info not found", StatusCodes.Status404NotFound, "IUI_NOT_FOUND");
                }

                var response = entity.ToResponseModel();

                // Load agreements for this treatment
                var agreements = await _unitOfWork.Repository<Agreement>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(a => a.Treatment)
                    .Include(a => a.Patient)
                        .ThenInclude(p => p!.Account)
                    .Where(a => a.TreatmentId == treatmentId && !a.IsDeleted)
                    .OrderByDescending(a => a.CreatedAt)
                    .ToListAsync();

                if (agreements.Any())
                {
                    response.Agreements = agreements.Select(a => a.ToAgreementResponse()).ToList();
                }

                return BaseResponse<TreatmentIUIResponseModel>.CreateSuccess(response, "IUI info retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving IUI by treatmentId {TreatmentId}", methodName, treatmentId);
                return BaseResponse<TreatmentIUIResponseModel>.CreateError($"Error retrieving IUI: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<List<TreatmentIUIResponseModel>>> GetByPatientIdAsync(Guid patientId)
        {
            const string methodName = nameof(GetByPatientIdAsync);
            _logger.LogInformation("{MethodName} called with patientId: {PatientId}", methodName, patientId);

            try
            {
                if (patientId == Guid.Empty)
                {
                    return BaseResponse<List<TreatmentIUIResponseModel>>.CreateError("Patient ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var entities = await _unitOfWork.Repository<TreatmentIUI>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Join(
                        _unitOfWork.Repository<Treatment>().GetQueryable().Where(t => !t.IsDeleted && t.PatientId == patientId && t.TreatmentType == TreatmentType.IUI),
                        iui => iui.Id,
                        treatment => treatment.Id,
                        (iui, treatment) => iui
                    )
                    .Where(x => !x.IsDeleted)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();

                // Load agreements for all treatments in batch
                var treatmentIds = entities.Select(e => e.Id).ToList();
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

                var responseModels = entities.Select(entity =>
                {
                    var response = entity.ToResponseModel();
                    if (agreementsByTreatmentId.TryGetValue(entity.Id, out var treatmentAgreements))
                    {
                        response.Agreements = treatmentAgreements;
                    }
                    return response;
                }).ToList();

                _logger.LogInformation("{MethodName}: Retrieved {Count} IUI records for patient {PatientId}", methodName, responseModels.Count, patientId);

                return BaseResponse<List<TreatmentIUIResponseModel>>.CreateSuccess(responseModels, $"Retrieved {responseModels.Count} IUI record(s) successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving IUI records by patientId {PatientId}", methodName, patientId);
                return BaseResponse<List<TreatmentIUIResponseModel>>.CreateError($"Error retrieving IUI records: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<TreatmentIUIResponseModel>> CreateAsync(TreatmentIUICreateUpdateRequest request)
        {
            const string methodName = nameof(CreateAsync);
            _logger.LogInformation("{MethodName} called with request {@Request}", methodName, request);

            try
            {
                if (request == null)
                {
                    return BaseResponse<TreatmentIUIResponseModel>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                var treatment = await _unitOfWork.Repository<Treatment>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(t => t.Id == request.TreatmentId && !t.IsDeleted);
                if (treatment == null)
                {
                    return BaseResponse<TreatmentIUIResponseModel>.CreateError("Treatment not found", StatusCodes.Status404NotFound, "TREATMENT_NOT_FOUND");
                }

                if (treatment.TreatmentType != TreatmentType.IUI)
                {
                    return BaseResponse<TreatmentIUIResponseModel>.CreateError("Treatment type must be IUI to create IUI info", StatusCodes.Status400BadRequest, "INVALID_TREATMENT_TYPE");
                }

                var exists = await _unitOfWork.Repository<TreatmentIUI>()
                    .GetQueryable()
                    .AnyAsync(x => x.Id == request.TreatmentId && !x.IsDeleted);
                if (exists)
                {
                    return BaseResponse<TreatmentIUIResponseModel>.CreateError("IUI info already exists for this treatment", StatusCodes.Status400BadRequest, "IUI_EXISTS");
                }

                // Create entity with shared PK = TreatmentId, then map remaining fields
                var entity = new TreatmentIUI(request.TreatmentId, request.Protocol);
                entity.UpdateEntity(request);
                await _unitOfWork.Repository<TreatmentIUI>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                var created = await _unitOfWork.Repository<TreatmentIUI>()
                    .GetQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == entity.Id);

                return BaseResponse<TreatmentIUIResponseModel>.CreateSuccess(created!.ToResponseModel(), "IUI info created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating IUI", methodName);
                return BaseResponse<TreatmentIUIResponseModel>.CreateError($"Error creating IUI: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<TreatmentIUIResponseModel>> UpdateAsync(Guid id, TreatmentIUIUpdateRequest request)
        {
            const string methodName = nameof(UpdateAsync);
            _logger.LogInformation("{MethodName} called with id {Id} and request {@Request}", methodName, id, request);

            try
            {
                if (id == Guid.Empty)
                {
                    return BaseResponse<TreatmentIUIResponseModel>.CreateError("ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                if (request == null)
                {
                    return BaseResponse<TreatmentIUIResponseModel>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                var entity = await _unitOfWork.Repository<TreatmentIUI>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                if (entity == null)
                {
                    return BaseResponse<TreatmentIUIResponseModel>.CreateError("IUI info not found", StatusCodes.Status404NotFound, "IUI_NOT_FOUND");
                }

                var treatment = await _unitOfWork.Repository<Treatment>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(t => t.Id == entity.Id && !t.IsDeleted);
                if (treatment == null || treatment.TreatmentType != TreatmentType.IUI)
                {
                    return BaseResponse<TreatmentIUIResponseModel>.CreateError("Invalid treatment for IUI info", StatusCodes.Status400BadRequest, "INVALID_TREATMENT");
                }

                entity.UpdateEntity(request);
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<TreatmentIUI>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();

                var updated = await _unitOfWork.Repository<TreatmentIUI>()
                    .GetQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                return BaseResponse<TreatmentIUIResponseModel>.CreateSuccess(updated!.ToResponseModel(), "IUI info updated successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating IUI {Id}", methodName, id);
                return BaseResponse<TreatmentIUIResponseModel>.CreateError($"Error updating IUI: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Guid id)
        {
            const string methodName = nameof(DeleteAsync);
            _logger.LogInformation("{MethodName} called with id {Id}", methodName, id);

            try
            {
                if (id == Guid.Empty)
                {
                    return BaseResponse<bool>.CreateError("ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var entity = await _unitOfWork.Repository<TreatmentIUI>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                if (entity == null)
                {
                    return BaseResponse<bool>.CreateError("IUI info not found", StatusCodes.Status404NotFound, "IUI_NOT_FOUND");
                }

                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<TreatmentIUI>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();

                return BaseResponse<bool>.CreateSuccess(true, "IUI info deleted successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error deleting IUI {Id}", methodName, id);
                return BaseResponse<bool>.CreateError($"Error deleting IUI: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }
    }
}


