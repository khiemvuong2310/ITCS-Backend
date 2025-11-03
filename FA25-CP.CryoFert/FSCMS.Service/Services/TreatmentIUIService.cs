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
                    .FirstOrDefaultAsync(x => x.TreatmentId == treatmentId && !x.IsDeleted);

                if (entity == null)
                {
                    return BaseResponse<TreatmentIUIResponseModel>.CreateError("IUI info not found", StatusCodes.Status404NotFound, "IUI_NOT_FOUND");
                }

                return BaseResponse<TreatmentIUIResponseModel>.CreateSuccess(entity.ToResponseModel(), "IUI info retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving IUI by treatmentId {TreatmentId}", methodName, treatmentId);
                return BaseResponse<TreatmentIUIResponseModel>.CreateError($"Error retrieving IUI: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
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
                    .AnyAsync(x => x.TreatmentId == request.TreatmentId && !x.IsDeleted);
                if (exists)
                {
                    return BaseResponse<TreatmentIUIResponseModel>.CreateError("IUI info already exists for this treatment", StatusCodes.Status400BadRequest, "IUI_EXISTS");
                }

                var entity = request.ToEntity();
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

        public async Task<BaseResponse<TreatmentIUIResponseModel>> UpdateAsync(Guid id, TreatmentIUICreateUpdateRequest request)
        {
            const string methodName = nameof(UpdateAsync);
            _logger.LogInformation("{MethodName} called with id {Id} and request {@Request}", methodName, id, request);

            try
            {
                if (id == Guid.Empty)
                {
                    return BaseResponse<TreatmentIUIResponseModel>.CreateError("ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
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
                    .FirstOrDefaultAsync(t => t.Id == entity.TreatmentId && !t.IsDeleted);
                if (treatment == null || treatment.TreatmentType != TreatmentType.IUI)
                {
                    return BaseResponse<TreatmentIUIResponseModel>.CreateError("Invalid treatment for IUI info", StatusCodes.Status400BadRequest, "INVALID_TREATMENT");
                }

                entity.UpdateEntity(request);
                entity.UpdatedAt = DateTime.UtcNow.AddHours(7);
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
                entity.DeletedAt = DateTime.UtcNow.AddHours(7);
                entity.UpdatedAt = DateTime.UtcNow.AddHours(7);
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


