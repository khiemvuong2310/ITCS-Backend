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

        public TreatmentService(IUnitOfWork unitOfWork, ILogger<TreatmentService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

                var data = items.Select(x => x.ToResponseModel()).ToList();

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

        public async Task<BaseResponse<TreatmentDetailResponseModel>> GetByIdAsync(Guid id)
        {
            const string methodName = nameof(GetByIdAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

            try
            {
                if (id == Guid.Empty)
                {
                    return BaseResponse<TreatmentDetailResponseModel>.CreateError("Treatment ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var entity = await _unitOfWork.Repository<Treatment>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(t => t.Patient)
                    .Include(t => t.Doctor)
                    .Include(t => t.TreatmentIVF)
                    .Where(t => t.Id == id && !t.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    return BaseResponse<TreatmentDetailResponseModel>.CreateError("Treatment not found", StatusCodes.Status404NotFound, "TREATMENT_NOT_FOUND");
                }

                var detail = entity.ToDetailResponseModel();

                // Attach IUI if exists (1-1 by shared PK: IUI.Id == Treatment.Id)
                var iui = await _unitOfWork.Repository<TreatmentIUI>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .FirstOrDefaultAsync();
                if (iui != null)
                {
                    detail.IUI = iui.ToResponseModel();
                }

                return BaseResponse<TreatmentDetailResponseModel>.CreateSuccess(detail, "Treatment retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving treatment {Id}", methodName, id);
                return BaseResponse<TreatmentDetailResponseModel>.CreateError($"Error retrieving treatment: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<TreatmentResponseModel>> CreateAsync(TreatmentCreateUpdateRequest request)
        {
            const string methodName = nameof(CreateAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

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

                var entity = request.ToEntity();
                await _unitOfWork.Repository<Treatment>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                var created = await _unitOfWork.Repository<Treatment>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(t => t.TreatmentIVF)
                    .FirstOrDefaultAsync(t => t.Id == entity.Id);

                return BaseResponse<TreatmentResponseModel>.CreateSuccess(created!.ToResponseModel(), "Treatment created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating treatment", methodName);
                return BaseResponse<TreatmentResponseModel>.CreateError($"Error creating treatment: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<TreatmentResponseModel>> UpdateAsync(Guid id, TreatmentCreateUpdateRequest request)
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

                // Validate FKs
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

                entity.UpdateEntity(request);
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<Treatment>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();

                var updated = await _unitOfWork.Repository<Treatment>()
                    .GetQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == id);

                return BaseResponse<TreatmentResponseModel>.CreateSuccess(updated!.ToResponseModel(), "Treatment updated successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating treatment {Id}", methodName, id);
                return BaseResponse<TreatmentResponseModel>.CreateError($"Error updating treatment: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
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
    }
}


