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
    public class TreatmentIVFService : ITreatmentIVFService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TreatmentIVFService> _logger;

        public TreatmentIVFService(IUnitOfWork unitOfWork, ILogger<TreatmentIVFService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BaseResponse<TreatmentIVFResponseModel>> GetByTreatmentIdAsync(Guid treatmentId)
        {
            const string methodName = nameof(GetByTreatmentIdAsync);
            _logger.LogInformation("{MethodName} called with treatmentId: {TreatmentId}", methodName, treatmentId);

            try
            {
                if (treatmentId == Guid.Empty)
                {
                    return BaseResponse<TreatmentIVFResponseModel>.CreateError("Treatment ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var entity = await _unitOfWork.Repository<TreatmentIVF>()
                    .GetQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == treatmentId && !x.IsDeleted);

                if (entity == null)
                {
                    return BaseResponse<TreatmentIVFResponseModel>.CreateError("IVF info not found", StatusCodes.Status404NotFound, "IVF_NOT_FOUND");
                }

                return BaseResponse<TreatmentIVFResponseModel>.CreateSuccess(entity.ToResponseModel(), "IVF info retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving IVF by treatmentId {TreatmentId}", methodName, treatmentId);
                return BaseResponse<TreatmentIVFResponseModel>.CreateError($"Error retrieving IVF: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<List<TreatmentIVFResponseModel>>> GetByPatientIdAsync(Guid patientId)
        {
            const string methodName = nameof(GetByPatientIdAsync);
            _logger.LogInformation("{MethodName} called with patientId: {PatientId}", methodName, patientId);

            try
            {
                if (patientId == Guid.Empty)
                {
                    return BaseResponse<List<TreatmentIVFResponseModel>>.CreateError("Patient ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var entities = await _unitOfWork.Repository<TreatmentIVF>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Join(
                        _unitOfWork.Repository<Treatment>().GetQueryable().Where(t => !t.IsDeleted && t.PatientId == patientId && t.TreatmentType == TreatmentType.IVF),
                        ivf => ivf.Id,
                        treatment => treatment.Id,
                        (ivf, treatment) => ivf
                    )
                    .Where(x => !x.IsDeleted)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();

                var responseModels = entities.Select(e => e.ToResponseModel()).ToList();

                _logger.LogInformation("{MethodName}: Retrieved {Count} IVF records for patient {PatientId}", methodName, responseModels.Count, patientId);

                return BaseResponse<List<TreatmentIVFResponseModel>>.CreateSuccess(responseModels, $"Retrieved {responseModels.Count} IVF record(s) successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving IVF records by patientId {PatientId}", methodName, patientId);
                return BaseResponse<List<TreatmentIVFResponseModel>>.CreateError($"Error retrieving IVF records: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<TreatmentIVFResponseModel>> CreateAsync(TreatmentIVFCreateUpdateRequest request)
        {
            const string methodName = nameof(CreateAsync);
            _logger.LogInformation("{MethodName} called with request {@Request}", methodName, request);

            try
            {
                if (request == null)
                {
                    return BaseResponse<TreatmentIVFResponseModel>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                var treatment = await _unitOfWork.Repository<Treatment>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(t => t.Id == request.TreatmentId && !t.IsDeleted);
                if (treatment == null)
                {
                    return BaseResponse<TreatmentIVFResponseModel>.CreateError("Treatment not found", StatusCodes.Status404NotFound, "TREATMENT_NOT_FOUND");
                }

                if (treatment.TreatmentType != TreatmentType.IVF)
                {
                    return BaseResponse<TreatmentIVFResponseModel>.CreateError("Treatment type must be IVF to create IVF info", StatusCodes.Status400BadRequest, "INVALID_TREATMENT_TYPE");
                }

                var exists = await _unitOfWork.Repository<TreatmentIVF>()
                    .GetQueryable()
                    .AnyAsync(x => x.Id == request.TreatmentId && !x.IsDeleted);
                if (exists)
                {
                    return BaseResponse<TreatmentIVFResponseModel>.CreateError("IVF info already exists for this treatment", StatusCodes.Status400BadRequest, "IVF_EXISTS");
                }

                var entity = new TreatmentIVF(request.TreatmentId, request.Protocol);
                entity.UpdateEntity(request);
                await _unitOfWork.Repository<TreatmentIVF>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                var created = await _unitOfWork.Repository<TreatmentIVF>()
                    .GetQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == entity.Id);

                return BaseResponse<TreatmentIVFResponseModel>.CreateSuccess(created!.ToResponseModel(), "IVF info created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating IVF", methodName);
                return BaseResponse<TreatmentIVFResponseModel>.CreateError($"Error creating IVF: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<TreatmentIVFResponseModel>> UpdateAsync(Guid id, TreatmentIVFUpdateRequest request)
        {
            const string methodName = nameof(UpdateAsync);
            _logger.LogInformation("{MethodName} called with id {Id} and request {@Request}", methodName, id, request);

            try
            {
                if (id == Guid.Empty)
                {
                    return BaseResponse<TreatmentIVFResponseModel>.CreateError("ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                if (request == null)
                {
                    return BaseResponse<TreatmentIVFResponseModel>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                var entity = await _unitOfWork.Repository<TreatmentIVF>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                if (entity == null)
                {
                    return BaseResponse<TreatmentIVFResponseModel>.CreateError("IVF info not found", StatusCodes.Status404NotFound, "IVF_NOT_FOUND");
                }

                // Ensure treatment still valid and type IVF
                var treatment = await _unitOfWork.Repository<Treatment>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(t => t.Id == entity.Id && !t.IsDeleted);
                if (treatment == null || treatment.TreatmentType != TreatmentType.IVF)
                {
                    return BaseResponse<TreatmentIVFResponseModel>.CreateError("Invalid treatment for IVF info", StatusCodes.Status400BadRequest, "INVALID_TREATMENT");
                }

                entity.UpdateEntity(request);
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<TreatmentIVF>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();

                var updated = await _unitOfWork.Repository<TreatmentIVF>()
                    .GetQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                return BaseResponse<TreatmentIVFResponseModel>.CreateSuccess(updated!.ToResponseModel(), "IVF info updated successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating IVF {Id}", methodName, id);
                return BaseResponse<TreatmentIVFResponseModel>.CreateError($"Error updating IVF: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
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

                var entity = await _unitOfWork.Repository<TreatmentIVF>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                if (entity == null)
                {
                    return BaseResponse<bool>.CreateError("IVF info not found", StatusCodes.Status404NotFound, "IVF_NOT_FOUND");
                }

                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<TreatmentIVF>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();

                return BaseResponse<bool>.CreateSuccess(true, "IVF info deleted successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error deleting IVF {Id}", methodName, id);
                return BaseResponse<bool>.CreateError($"Error deleting IVF: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }
    }
}


