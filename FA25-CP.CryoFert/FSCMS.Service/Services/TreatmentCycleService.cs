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
using System.Text.Json;

namespace FSCMS.Service.Services
{
    public class TreatmentCycleService : ITreatmentCycleService
    {
        #region Dependencies

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TreatmentCycleService> _logger;

        public TreatmentCycleService(IUnitOfWork unitOfWork, ILogger<TreatmentCycleService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Retrieval

        // Returns paginated treatment cycles filtered by request parameters.
        public async Task<DynamicResponse<TreatmentCycleResponseModel>> GetAllAsync(GetTreatmentCyclesRequest request)
        {
            const string methodName = nameof(GetAllAsync);
            _logger.LogInformation("{MethodName} called with request {@Request}", methodName, request);

            try
            {
                if (request == null)
                {
                    return new DynamicResponse<TreatmentCycleResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_REQUEST",
                        Message = "Request cannot be null",
                        MetaData = new PagingMetaData(),
                        Data = new List<TreatmentCycleResponseModel>()
                    };
                }

                request.Normalize();

                var query = _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(tc => tc.Treatment)
                    .Where(tc => !tc.IsDeleted);

                if (request.TreatmentId.HasValue)
                    query = query.Where(tc => tc.TreatmentId == request.TreatmentId.Value);
                if (request.PatientId.HasValue)
                    query = query.Where(tc => tc.Treatment!.PatientId == request.PatientId.Value);
                if (request.DoctorId.HasValue)
                    query = query.Where(tc => tc.Treatment!.DoctorId == request.DoctorId.Value);
                if (request.Status.HasValue)
                    query = query.Where(tc => tc.Status == request.Status.Value);
                if (request.FromDate.HasValue)
                    query = query.Where(tc => tc.StartDate >= request.FromDate.Value);
                if (request.ToDate.HasValue)
                    query = query.Where(tc => tc.StartDate <= request.ToDate.Value);
                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    var term = request.SearchTerm.ToLowerInvariant();
                    query = query.Where(tc => tc.CycleName.ToLower().Contains(term) || (tc.Notes != null && tc.Notes.ToLower().Contains(term)));
                }

                var total = await query.CountAsync();
                var isDesc = request.Order?.ToLower() == "desc";
                switch (request.Sort)
                {
                    case "startdate":
                        query = isDesc ? query.OrderByDescending(x => x.StartDate) : query.OrderBy(x => x.StartDate);
                        break;
                    case "status":
                        query = isDesc ? query.OrderByDescending(x => x.Status) : query.OrderBy(x => x.Status);
                        break;
                    case "cyclenumber":
                        query = isDesc ? query.OrderByDescending(x => x.CycleNumber) : query.OrderBy(x => x.CycleNumber);
                        break;
                    default:
                        query = isDesc ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt);
                        break;
                }

                var items = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                var data = items.Select(i => i.ToResponseModel()).ToList();

                return new DynamicResponse<TreatmentCycleResponseModel>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Treatment cycles retrieved successfully",
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
                _logger.LogError(ex, "{MethodName}: Error retrieving treatment cycles", methodName);
                return new DynamicResponse<TreatmentCycleResponseModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred",
                    MetaData = new PagingMetaData(),
                    Data = new List<TreatmentCycleResponseModel>()
                };
            }
        }

        // Returns detailed info (appointments + docs) for a specific cycle.
        public async Task<BaseResponse<TreatmentCycleDetailResponseModel>> GetByIdAsync(Guid id)
        {
            const string methodName = nameof(GetByIdAsync);
            _logger.LogInformation("{MethodName} called with id {Id}", methodName, id);

            try
            {
                if (id == Guid.Empty)
                    return BaseResponse<TreatmentCycleDetailResponseModel>.CreateError("ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");

                var entity = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(tc => tc.Treatment)
                    .Include(tc => tc.Appointments)
                    .FirstOrDefaultAsync(tc => tc.Id == id && !tc.IsDeleted);

                if (entity == null)
                    return BaseResponse<TreatmentCycleDetailResponseModel>.CreateError("Treatment cycle not found", StatusCodes.Status404NotFound, "NOT_FOUND");

                var detail = entity.ToDetailResponseModel();
                detail.Appointments = entity.Appointments.Select(a => new AppointmentSummary
                {
                    Id = a.Id,
                    AppointmentDate = a.AppointmentDate,
                    Type = a.Type.ToString(),
                    Status = a.Status.ToString()
                }).ToList();

                // Documents
                var docs = await _unitOfWork.Repository<Media>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(m => m.RelatedEntityType == "TreatmentCycle" && m.RelatedEntityId == id && !m.IsDeleted)
                    .ToListAsync();
                detail.Documents = docs.Select(d => new DocumentSummary
                {
                    Id = d.Id,
                    FileName = d.FileName,
                    FileType = d.FileType,
                    FileSize = d.FileSize,
                    Category = d.Category,
                    UploadDate = d.UploadDate
                }).ToList();

                return BaseResponse<TreatmentCycleDetailResponseModel>.CreateSuccess(detail, "Treatment cycle retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving treatment cycle {Id}", methodName, id);
                return BaseResponse<TreatmentCycleDetailResponseModel>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        // Returns status of Treatment by TreatmentId.
        public async Task<BaseResponse<TreatmentStatus>> GetTreatmentStatusAsync(Guid treatmentId)
        {
            const string methodName = nameof(GetTreatmentStatusAsync);
            _logger.LogInformation("{MethodName} called with TreatmentId {TreatmentId}", methodName, treatmentId);
            try
            {
                if (treatmentId == Guid.Empty)
                    return BaseResponse<TreatmentStatus>.CreateError("TreatmentId cannot be empty", StatusCodes.Status400BadRequest, "INVALID_TREATMENT_ID");
                var treatment = await _unitOfWork.Repository<Treatment>()
                    .GetQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == treatmentId && !t.IsDeleted);
                if (treatment == null)
                    return BaseResponse<TreatmentStatus>.CreateError("Treatment not found", StatusCodes.Status404NotFound, "TREATMENT_NOT_FOUND");
                return BaseResponse<TreatmentStatus>.CreateSuccess(treatment.Status, "Treatment status retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving treatment status for TreatmentId {TreatmentId}", methodName, treatmentId);
                return BaseResponse<TreatmentStatus>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        #endregion

        #region Lifecycle Management

        // Creates a cycle for an existing treatment.
        public async Task<BaseResponse<TreatmentCycleResponseModel>> CreateAsync(CreateTreatmentCycleRequest request)
        {
            const string methodName = nameof(CreateAsync);
            _logger.LogInformation("{MethodName} called with request {@Request}", methodName, request);

            try
            {
                if (request == null)
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");

                var trm = await _unitOfWork.Repository<Treatment>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(t => t.Id == request.TreatmentId && !t.IsDeleted);
                if (trm == null)
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError("Treatment not found", StatusCodes.Status404NotFound, "TREATMENT_NOT_FOUND");

                var entity = request.ToEntity();
                await _unitOfWork.Repository<TreatmentCycle>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                var created = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == entity.Id);

                return BaseResponse<TreatmentCycleResponseModel>.CreateSuccess(created!.ToResponseModel(), "Treatment cycle created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating treatment cycle", methodName);
                return BaseResponse<TreatmentCycleResponseModel>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        // Updates editable fields of a cycle and optionally its status.
        public async Task<BaseResponse<TreatmentCycleResponseModel>> UpdateAsync(Guid id, UpdateTreatmentCycleRequest request)
        {
            const string methodName = nameof(UpdateAsync);
            _logger.LogInformation("{MethodName} called with id {Id} and request {@Request}", methodName, id, request);

            try
            {
                if (id == Guid.Empty)
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError("ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                if (request == null)
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");

                var entity = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                if (entity == null)
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError("Treatment cycle not found", StatusCodes.Status404NotFound, "NOT_FOUND");

                if (entity.Status == TreatmentStatus.Completed && !request.IsAdminOverride)
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError("Cannot update a completed cycle", StatusCodes.Status400BadRequest, "CYCLE_COMPLETED");

                var oldValues = JsonSerializer.Serialize(entity.ToResponseModel());

                var isStatusChange = request.Status.HasValue && request.Status.Value != entity.Status;
                if (isStatusChange && !await IsPreviousCycleCompletedAsync(entity.TreatmentId, entity.CycleNumber, methodName))
                {
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError(
                        $"Cannot update cycle #{entity.CycleNumber} because the previous cycle has not been completed.",
                        StatusCodes.Status409Conflict,
                        "PREVIOUS_CYCLE_INCOMPLETE");
                }

                var previousStatus = entity.Status;
                var statusChangedToCompleted = request.Status.HasValue && request.Status.Value == TreatmentStatus.Completed;

                entity.UpdateEntity(request);
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<TreatmentCycle>().UpdateGuid(entity, id);

                // Cập nhật trạng thái Treatment và TreatmentIUI/IVF nếu Status của cycle thay đổi
                if (request.Status.HasValue && request.Status.Value != previousStatus)
                {
                    await UpdateTreatmentAggregateStatusAsync(entity.TreatmentId, request.Status.Value, methodName);
                }

                if (statusChangedToCompleted)
                {
                    await UpdateTreatmentCurrentStepAsync(entity.TreatmentId, entity.CycleNumber, methodName);
                }

                await AddAuditLog("TreatmentCycle", id, "Update", oldValues, JsonSerializer.Serialize(entity.ToResponseModel()));

                await _unitOfWork.CommitAsync();

                var updated = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                return BaseResponse<TreatmentCycleResponseModel>.CreateSuccess(updated!.ToResponseModel(), "Treatment cycle updated successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating treatment cycle {Id}", methodName, id);
                return BaseResponse<TreatmentCycleResponseModel>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        // Soft deletes a cycle and records an audit trail.
        public async Task<BaseResponse<bool>> DeleteAsync(Guid id)
        {
            const string methodName = nameof(DeleteAsync);
            _logger.LogInformation("{MethodName} called with id {Id}", methodName, id);

            try
            {
                if (id == Guid.Empty)
                    return BaseResponse<bool>.CreateError("ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");

                var entity = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .Include(tc => tc.Appointments)
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                if (entity == null)
                    return BaseResponse<bool>.CreateError("Treatment cycle not found", StatusCodes.Status404NotFound, "NOT_FOUND");

                // If there are appointments or (future) samples/transactions, we soft-delete (always here)
                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<TreatmentCycle>().UpdateGuid(entity, id);

                await AddAuditLog("TreatmentCycle", id, "Delete", JsonSerializer.Serialize(entity.ToResponseModel()), null);

                await _unitOfWork.CommitAsync();

                return BaseResponse<bool>.CreateSuccess(true, "Treatment cycle deleted successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error deleting treatment cycle {Id}", methodName, id);
                return BaseResponse<bool>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        // Moves a cycle into InProgress (optionally overriding start date).
        public async Task<BaseResponse<TreatmentCycleResponseModel>> StartAsync(Guid id, StartTreatmentCycleRequest request)
        {
            const string methodName = nameof(StartAsync);
            _logger.LogInformation("{MethodName} called with id {Id}", methodName, id);
            try
            {
                var entity = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                if (entity == null)
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError("Treatment cycle not found", StatusCodes.Status404NotFound, "NOT_FOUND");

                if (entity.Status == TreatmentStatus.Completed)
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError("Cannot start a completed cycle", StatusCodes.Status400BadRequest, "CYCLE_COMPLETED");

                var oldValues = JsonSerializer.Serialize(entity.ToResponseModel());

                if (request?.StartDate.HasValue == true) entity.StartDate = request.StartDate.Value;
                entity.Status = TreatmentStatus.InProgress;
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<TreatmentCycle>().UpdateGuid(entity, id);

                // Khi bắt đầu chu kỳ, cập nhật trạng thái tổng của Treatment
                await UpdateTreatmentAggregateStatusAsync(entity.TreatmentId, entity.Status, methodName);

                await AddAuditLog("TreatmentCycle", id, "Start", oldValues, JsonSerializer.Serialize(entity.ToResponseModel()));
                await _unitOfWork.CommitAsync();

                return BaseResponse<TreatmentCycleResponseModel>.CreateSuccess(entity.ToResponseModel(), "Treatment cycle started", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error starting cycle {Id}", methodName, id);
                return BaseResponse<TreatmentCycleResponseModel>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        // Completes a cycle, updates next cycle + treatment step tracking.
        public async Task<BaseResponse<TreatmentCycleResponseModel>> CompleteAsync(Guid id, CompleteTreatmentCycleRequest request)
        {
            const string methodName = nameof(CompleteAsync);
            _logger.LogInformation("{MethodName} called with id {Id}", methodName, id);
            try
            {
                var entity = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                if (entity == null)
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError("Treatment cycle not found", StatusCodes.Status404NotFound, "NOT_FOUND");

                if (entity.Status == TreatmentStatus.Completed)
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError("Treatment cycle is already completed", StatusCodes.Status400BadRequest, "ALREADY_COMPLETED");

                if (!await IsPreviousCycleCompletedAsync(entity.TreatmentId, entity.CycleNumber, methodName))
                {
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError(
                        $"Cannot complete cycle #{entity.CycleNumber} because the previous cycle has not been completed.",
                        StatusCodes.Status409Conflict,
                        "PREVIOUS_CYCLE_INCOMPLETE");
                }

                var oldValues = JsonSerializer.Serialize(entity.ToResponseModel());

                entity.Status = TreatmentStatus.Completed;
                if (request?.EndDate.HasValue == true) entity.EndDate = request.EndDate.Value; else entity.EndDate = DateTime.UtcNow;
                if (!string.IsNullOrWhiteSpace(request?.Notes)) entity.Notes = (entity.Notes == null ? request!.Notes : entity.Notes + "\n" + request!.Notes);
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<TreatmentCycle>().UpdateGuid(entity, id);

                await AddAuditLog("TreatmentCycle", id, "Complete", oldValues, JsonSerializer.Serialize(new { entity, request?.Outcome }));

                // Khi hoàn thành chu kỳ, cập nhật trạng thái tổng của Treatment
                await UpdateTreatmentAggregateStatusAsync(entity.TreatmentId, entity.Status, methodName);

                // Auto-update next cycle status from Planned to Scheduled
                await UpdateNextCycleStatusAsync(entity.TreatmentId, entity.CycleNumber, methodName);

                await UpdateTreatmentCurrentStepAsync(entity.TreatmentId, entity.CycleNumber, methodName);

                await _unitOfWork.CommitAsync();

                return BaseResponse<TreatmentCycleResponseModel>.CreateSuccess(entity.ToResponseModel(), "Treatment cycle completed", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error completing cycle {Id}", methodName, id);
                return BaseResponse<TreatmentCycleResponseModel>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        // Cancels a cycle and marks all subsequent cycles as Failed.
        public async Task<BaseResponse<TreatmentCycleResponseModel>> CancelAsync(Guid id, CancelTreatmentCycleRequest request)
        {
            const string methodName = nameof(CancelAsync);
            _logger.LogInformation("{MethodName} called with id {Id}", methodName, id);
            try
            {
                var entity = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                if (entity == null)
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError("Treatment cycle not found", StatusCodes.Status404NotFound, "NOT_FOUND");

                if (entity.Status == TreatmentStatus.Cancelled)
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError("Treatment cycle is already cancelled", StatusCodes.Status400BadRequest, "ALREADY_CANCELLED");

                if (entity.Status == TreatmentStatus.Completed)
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError("Cannot cancel a completed cycle", StatusCodes.Status400BadRequest, "CYCLE_COMPLETED");

                if (!await IsPreviousCycleCompletedAsync(entity.TreatmentId, entity.CycleNumber, methodName))
                {
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError(
                        $"Cannot cancel cycle #{entity.CycleNumber} because the previous cycle has not been completed.",
                        StatusCodes.Status409Conflict,
                        "PREVIOUS_CYCLE_INCOMPLETE");
                }

                var oldValues = JsonSerializer.Serialize(entity.ToResponseModel());

                entity.Status = TreatmentStatus.Cancelled;
                entity.Notes = (entity.Notes == null ? $"Cancelled: {request?.Reason}" : entity.Notes + $"\nCancelled: {request?.Reason}");
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<TreatmentCycle>().UpdateGuid(entity, id);

                await AddAuditLog("TreatmentCycle", id, "Cancel", oldValues, JsonSerializer.Serialize(entity.ToResponseModel()));

                // Khi hủy chu kỳ, cập nhật trạng thái tổng của Treatment
                await UpdateTreatmentAggregateStatusAsync(entity.TreatmentId, entity.Status, methodName);

                // Update all subsequent cycles in the same treatment to Failed status
                await UpdateAllSubsequentCyclesToFailedAsync(entity.TreatmentId, entity.CycleNumber, request?.Reason, methodName);

                await _unitOfWork.CommitAsync();

                return BaseResponse<TreatmentCycleResponseModel>.CreateSuccess(entity.ToResponseModel(), "Treatment cycle cancelled", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error cancelling cycle {Id}", methodName, id);
                return BaseResponse<TreatmentCycleResponseModel>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        // Updates a cycle status by treatmentId + cycle number lookup.
        public async Task<BaseResponse<TreatmentCycleResponseModel>> UpdateStatusByOrderAsync(UpdateTreatmentCycleStatusByOrderRequest request)
        {
            const string methodName = nameof(UpdateStatusByOrderAsync);
            _logger.LogInformation("{MethodName} called with TreatmentId {TreatmentId}, CycleNumber {CycleNumber}, Status {Status}",
                methodName, request.TreatmentId, request.CycleNumber, request.Status);

            try
            {
                if (request == null)
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");

                if (request.TreatmentId == Guid.Empty)
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError("TreatmentId cannot be empty", StatusCodes.Status400BadRequest, "INVALID_TREATMENT_ID");

                if (request.CycleNumber <= 0)
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError("CycleNumber must be greater than 0", StatusCodes.Status400BadRequest, "INVALID_CYCLE_NUMBER");

                // Find treatment cycle by TreatmentId and CycleNumber
                var entity = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(x => x.TreatmentId == request.TreatmentId
                        && x.CycleNumber == request.CycleNumber
                        && !x.IsDeleted);

                if (entity == null)
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError(
                        $"Treatment cycle not found for TreatmentId {request.TreatmentId} and CycleNumber {request.CycleNumber}",
                        StatusCodes.Status404NotFound,
                        "NOT_FOUND");

                // Validate status transition
                //if (entity.status == treatmentstatus.completed && !request.isadminoverride)
                //    return baseresponse<treatmentcycleresponsemodel>.createerror(
                //        "cannot update a completed cycle without admin override", 
                //        statuscodes.status400badrequest, 
                //        "cycle_completed");

                var oldStatus = entity.Status;
                var oldValues = JsonSerializer.Serialize(entity.ToResponseModel());

                var isStatusChange = request.Status != entity.Status;
                if (isStatusChange && !await IsPreviousCycleCompletedAsync(entity.TreatmentId, entity.CycleNumber, methodName))
                {
                    return BaseResponse<TreatmentCycleResponseModel>.CreateError(
                        $"Cannot update cycle #{entity.CycleNumber} because the previous cycle has not been completed.",
                        StatusCodes.Status409Conflict,
                        "PREVIOUS_CYCLE_INCOMPLETE");
                }

                entity.Status = request.Status;
                entity.UpdatedAt = DateTime.UtcNow;

                // Update notes if provided
                if (!string.IsNullOrWhiteSpace(request.Notes))
                {
                    entity.Notes = entity.Notes == null
                        ? request.Notes
                        : entity.Notes + $"\n{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}: {request.Notes}";
                }

                // Set EndDate if status is Completed or Failed
                if (request.Status == TreatmentStatus.Completed || request.Status == TreatmentStatus.Failed)
                {
                    if (entity.EndDate == null)
                        entity.EndDate = DateTime.UtcNow;
                }

                await _unitOfWork.Repository<TreatmentCycle>().UpdateGuid(entity, entity.Id);

                // Cập nhật trạng thái tổng của Treatment và TreatmentIUI/IVF khi status chu kỳ thay đổi
                if (isStatusChange)
                {
                    await UpdateTreatmentAggregateStatusAsync(entity.TreatmentId, entity.Status, methodName);
                }

                await AddAuditLog("TreatmentCycle", entity.Id, "UpdateStatusByOrder", oldValues, JsonSerializer.Serialize(entity.ToResponseModel()));

                // Auto-update next cycle status based on current status
                if (request.Status == TreatmentStatus.Completed)
                {
                    // Update next cycle from Planned to Scheduled
                    await UpdateNextCycleStatusAsync(entity.TreatmentId, entity.CycleNumber, methodName);
                    await UpdateTreatmentCurrentStepAsync(entity.TreatmentId, entity.CycleNumber, methodName);
                }
                else if (request.Status == TreatmentStatus.Failed)
                {
                    // Update next cycle from Planned to Failed
                    await UpdateNextCycleStatusToFailedAsync(entity.TreatmentId, entity.CycleNumber, methodName);
                }

                await _unitOfWork.CommitAsync();

                var updated = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == entity.Id);

                return BaseResponse<TreatmentCycleResponseModel>.CreateSuccess(
                    updated!.ToResponseModel(),
                    "Treatment cycle status updated successfully",
                    StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating treatment cycle status for TreatmentId {TreatmentId}, CycleNumber {CycleNumber}",
                    methodName, request?.TreatmentId, request?.CycleNumber);
                return BaseResponse<TreatmentCycleResponseModel>.CreateError(
                    $"Error: {ex.Message}",
                    StatusCodes.Status500InternalServerError,
                    "INTERNAL_ERROR");
            }
        }

        #endregion

        #region Samples & Appointments

        // Returns lab samples tied to the patient of a cycle and all related patients.
        public async Task<BaseResponse<List<object>>> GetSamplesAsync(Guid id)
        {
            const string methodName = nameof(GetSamplesAsync);
            _logger.LogInformation("{MethodName} called with cycle ID {Id}", methodName, id);

            try
            {
                if (id == Guid.Empty)
                    return BaseResponse<List<object>>.CreateError("ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");

                // Get the treatment cycle with its treatment
                var cycle = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .Include(tc => tc.Treatment)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(tc => tc.Id == id && !tc.IsDeleted);

                if (cycle == null)
                    return BaseResponse<List<object>>.CreateError("Treatment cycle not found", StatusCodes.Status404NotFound, "NOT_FOUND");

                if (cycle.Treatment == null)
                    return BaseResponse<List<object>>.CreateError("Treatment not found for the cycle", StatusCodes.Status404NotFound, "TREATMENT_NOT_FOUND");

                var primaryPatientId = cycle.Treatment.PatientId;

                // Get all related patient IDs from relationships
                var relatedPatientIds = await _unitOfWork.Repository<Relationship>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(r => !r.IsDeleted
                        && r.IsActive
                        && r.Status == RelationshipStatus.Approved
                        && (r.Patient1Id == primaryPatientId || r.Patient2Id == primaryPatientId))
                    .Select(r => r.Patient1Id == primaryPatientId ? r.Patient2Id : r.Patient1Id)
                    .Distinct()
                    .ToListAsync();

                // Combine primary patient ID with all related patient IDs
                var allPatientIds = new List<Guid> { primaryPatientId };
                allPatientIds.AddRange(relatedPatientIds);

                // Get all lab samples for the primary patient and all related patients
                var samples = await _unitOfWork.Repository<LabSample>()
                    .GetQueryable()
                    .AsNoTracking()
                       .Include(x => x.LabSampleSperm)
                    .Include(x => x.LabSampleOocyte)
                    .Where(s => allPatientIds.Contains(s.PatientId) && !s.IsDeleted)
                    .OrderByDescending(s => s.CollectionDate)
                    .Select(s => new { s.Id, s.SampleCode, s.SampleType, s.Status, s.CollectionDate })
                    .ToListAsync();

                _logger.LogInformation("{MethodName} retrieved {Count} samples for cycle {Id} (primary patient: {PatientId}, related patients: {RelatedCount})",
                    methodName, samples.Count, id, primaryPatientId, relatedPatientIds.Count);

                return BaseResponse<List<object>>.CreateSuccess(samples.Cast<object>().ToList(), "Samples retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting samples for cycle {Id}", id);
                return BaseResponse<List<object>>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        // Records a new sample for the cycle's patient.
        public async Task<BaseResponse<object>> AddSampleAsync(Guid id, AddCycleSampleRequest request)
        {
            try
            {
                var cycle = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .Include(tc => tc.Treatment)
                    .FirstOrDefaultAsync(tc => tc.Id == id && !tc.IsDeleted);
                if (cycle == null)
                    return BaseResponse<object>.CreateError("Treatment cycle not found", StatusCodes.Status404NotFound, "NOT_FOUND");

                var patientId = cycle.Treatment!.PatientId;

                // Parse SampleType string to enum if possible
                if (!Enum.TryParse(typeof(FSCMS.Core.Enum.SampleType), request.SampleType, true, out var sampleType))
                    return BaseResponse<object>.CreateError("Invalid sample type", StatusCodes.Status400BadRequest, "INVALID_SAMPLE_TYPE");

                var entity = new LabSample(Guid.NewGuid(), patientId, request.SampleCode, (FSCMS.Core.Enum.SampleType)sampleType!, request.CollectionDate, SpecimenStatus.Stored)
                {
                    Notes = request.Notes
                };
                await _unitOfWork.Repository<LabSample>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                return BaseResponse<object>.CreateSuccess(new { entity.Id, entity.SampleCode }, "Sample added to patient", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding sample for cycle {Id}", id);
                return BaseResponse<object>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        // Lists appointments linked to a cycle.
        public async Task<BaseResponse<List<AppointmentSummary>>> GetAppointmentsAsync(Guid id)
        {
            try
            {
                var apps = await _unitOfWork.Repository<Appointment>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(a => a.TreatmentCycleId == id && !a.IsDeleted)
                    .OrderByDescending(a => a.AppointmentDate)
                    .Select(a => new AppointmentSummary
                    {
                        Id = a.Id,
                        AppointmentDate = a.AppointmentDate,
                        Type = a.Type.ToString(),
                        Status = a.Status.ToString()
                    }).ToListAsync();

                return BaseResponse<List<AppointmentSummary>>.CreateSuccess(apps, "Appointments retrieved", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting appointments for cycle {Id}", id);
                return BaseResponse<List<AppointmentSummary>>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        // Creates a new appointment and associates it with the cycle.
        public async Task<BaseResponse<AppointmentSummary>> AddAppointmentAsync(Guid id, AddCycleAppointmentRequest request)
        {
            try
            {
                var cycle = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .Include(tc => tc.Treatment)
                    .FirstOrDefaultAsync(tc => tc.Id == id && !tc.IsDeleted);
                if (cycle == null)
                    return BaseResponse<AppointmentSummary>.CreateError("Treatment cycle not found", StatusCodes.Status404NotFound, "NOT_FOUND");

                if (!Enum.TryParse(typeof(AppointmentType), request.Type, true, out var typeObj))
                    return BaseResponse<AppointmentSummary>.CreateError("Invalid appointment type", StatusCodes.Status400BadRequest, "INVALID_TYPE");

                var patientId = cycle.Treatment?.PatientId ?? Guid.Empty;
                if (patientId == Guid.Empty)
                    return BaseResponse<AppointmentSummary>.CreateError("Associated patient not found for this treatment cycle", StatusCodes.Status400BadRequest, "PATIENT_NOT_FOUND");

                var entity = new Appointment(
                    Guid.NewGuid(),
                    patientId,
                    id, // treatmentCycleId (nullable)
                    request.AppointmentDate,
                    (AppointmentType)typeObj!,
                    AppointmentStatus.Scheduled,
                    request.Reason,
                    request.Instructions,
                    request.Notes,
                    request.SlotId
                );
                await _unitOfWork.Repository<Appointment>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                var dto = new AppointmentSummary { Id = entity.Id, AppointmentDate = entity.AppointmentDate, Type = entity.Type.ToString(), Status = entity.Status.ToString() };
                return BaseResponse<AppointmentSummary>.CreateSuccess(dto, "Appointment created", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding appointment for cycle {Id}", id);
                return BaseResponse<AppointmentSummary>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        #endregion

        #region Billing & Documents

        // Returns billing snapshot (estimated cost only for now).
        public async Task<BaseResponse<TreatmentCycleBillingResponse>> GetBillingAsync(Guid id)
        {
            try
            {
                var cycle = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(tc => tc.Id == id && !tc.IsDeleted);
                if (cycle == null)
                    return BaseResponse<TreatmentCycleBillingResponse>.CreateError("Treatment cycle not found", StatusCodes.Status404NotFound, "NOT_FOUND");

                // No direct transaction link; return estimated cost only
                var resp = new TreatmentCycleBillingResponse
                {
                    TreatmentCycleId = id,
                    EstimatedCost = cycle.Cost,
                    TotalPaid = 0,
                    Items = new List<BillingItem>()
                };

                return BaseResponse<TreatmentCycleBillingResponse>.CreateSuccess(resp, "Billing retrieved", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting billing for cycle {Id}", id);
                return BaseResponse<TreatmentCycleBillingResponse>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        // Lists uploaded documents for the given cycle.
        public async Task<BaseResponse<List<DocumentSummary>>> GetDocumentsAsync(Guid id)
        {
            try
            {
                var docs = await _unitOfWork.Repository<Media>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(m => m.RelatedEntityType == "TreatmentCycle" && m.RelatedEntityId == id && !m.IsDeleted)
                    .OrderByDescending(m => m.UploadDate)
                    .ToListAsync();

                var result = docs.Select(d => new DocumentSummary
                {
                    Id = d.Id,
                    FileName = d.FileName,
                    FileType = d.FileType,
                    FileSize = d.FileSize,
                    Category = d.Category,
                    UploadDate = d.UploadDate
                }).ToList();

                return BaseResponse<List<DocumentSummary>>.CreateSuccess(result, "Documents retrieved", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting documents for cycle {Id}", id);
                return BaseResponse<List<DocumentSummary>>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        // Uploads metadata for a new document tied to the cycle.
        public async Task<BaseResponse<DocumentSummary>> UploadDocumentAsync(Guid id, UploadCycleDocumentRequest request)
        {
            try
            {
                var entity = new Media(Guid.NewGuid(), request.FileName, request.FilePath, request.FileType, request.FileSize)
                {
                    RelatedEntityId = id,
                    RelatedEntityType = "TreatmentCycle",
                    Title = request.Title,
                    Description = request.Description,
                    Category = request.Category
                };
                await _unitOfWork.Repository<Media>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                var dto = new DocumentSummary { Id = entity.Id, FileName = entity.FileName, FileType = entity.FileType, FileSize = entity.FileSize, Category = entity.Category, UploadDate = entity.UploadDate };
                return BaseResponse<DocumentSummary>.CreateSuccess(dto, "Document uploaded", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading document for cycle {Id}", id);
                return BaseResponse<DocumentSummary>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Updates the next treatment cycle status from Planned to Scheduled when the current cycle is completed or cancelled.
        /// This maintains workflow continuity by automatically scheduling the next step in the treatment sequence.
        /// </summary>
        /// <param name="treatmentId">The treatment ID to find cycles for</param>
        /// <param name="currentCycleNumber">The CycleNumber of the current cycle</param>
        /// <param name="callingMethodName">The name of the calling method for logging purposes</param>
        private async Task UpdateNextCycleStatusAsync(Guid treatmentId, int currentCycleNumber, string callingMethodName)
        {
            try
            {
                var nextCycle = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(tc =>
                        tc.TreatmentId == treatmentId
                        && tc.CycleNumber == currentCycleNumber + 1
                        && !tc.IsDeleted
                        && tc.Status == TreatmentStatus.Planned);

                if (nextCycle != null)
                {
                    var oldStatus = nextCycle.Status;
                    nextCycle.Status = TreatmentStatus.Scheduled;
                    nextCycle.UpdatedAt = DateTime.UtcNow;
                    await _unitOfWork.Repository<TreatmentCycle>().UpdateGuid(nextCycle, nextCycle.Id);

                    _logger.LogInformation(
                        "{CallingMethodName}: Auto-updated next cycle {NextCycleId} (CycleNumber: {NextCycleNumber}) status from {OldStatus} to {NewStatus} for Treatment {TreatmentId}",
                        callingMethodName, nextCycle.Id, nextCycle.CycleNumber, oldStatus, TreatmentStatus.Scheduled, treatmentId);
                }
                else
                {
                    _logger.LogInformation(
                        "{CallingMethodName}: No next cycle found (CycleNumber: {NextCycleNumber}) or next cycle is not in Planned status for Treatment {TreatmentId}",
                        callingMethodName, currentCycleNumber + 1, treatmentId);
                }
            }
            catch (Exception ex)
            {
                // Log error but don't fail the main operation
                _logger.LogWarning(ex,
                    "{CallingMethodName}: Error updating next cycle status for Treatment {TreatmentId} after CycleNumber {CycleNumber}. Main operation will continue.",
                    callingMethodName, treatmentId, currentCycleNumber);
            }
        }

        /// <summary>
        /// Updates the next treatment cycle status from Planned to Failed when the current cycle fails.
        /// This maintains workflow continuity by automatically marking the next step as failed when the current step fails.
        /// </summary>
        /// <param name="treatmentId">The treatment ID to find cycles for</param>
        /// <param name="currentCycleNumber">The CycleNumber of the current cycle</param>
        /// <param name="callingMethodName">The name of the calling method for logging purposes</param>
        private async Task UpdateNextCycleStatusToFailedAsync(Guid treatmentId, int currentCycleNumber, string callingMethodName)
        {
            try
            {
                var nextCycle = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(tc =>
                        tc.TreatmentId == treatmentId
                        && tc.CycleNumber == currentCycleNumber + 1
                        && !tc.IsDeleted
                        && tc.Status == TreatmentStatus.Planned);

                if (nextCycle != null)
                {
                    var oldStatus = nextCycle.Status;
                    nextCycle.Status = TreatmentStatus.Failed;
                    nextCycle.UpdatedAt = DateTime.UtcNow;
                    if (nextCycle.EndDate == null)
                        nextCycle.EndDate = DateTime.UtcNow;
                    await _unitOfWork.Repository<TreatmentCycle>().UpdateGuid(nextCycle, nextCycle.Id);

                    _logger.LogInformation(
                        "{CallingMethodName}: Auto-updated next cycle {NextCycleId} (CycleNumber: {NextCycleNumber}) status from {OldStatus} to {NewStatus} for Treatment {TreatmentId}",
                        callingMethodName, nextCycle.Id, nextCycle.CycleNumber, oldStatus, TreatmentStatus.Failed, treatmentId);
                }
                else
                {
                    _logger.LogInformation(
                        "{CallingMethodName}: No next cycle found (CycleNumber: {NextCycleNumber}) or next cycle is not in Planned status for Treatment {TreatmentId}",
                        callingMethodName, currentCycleNumber + 1, treatmentId);
                }
            }
            catch (Exception ex)
            {
                // Log error but don't fail the main operation
                _logger.LogWarning(ex,
                    "{CallingMethodName}: Error updating next cycle status to Failed for Treatment {TreatmentId} after CycleNumber {CycleNumber}. Main operation will continue.",
                    callingMethodName, treatmentId, currentCycleNumber);
            }
        }

        /// <summary>
        /// Updates all subsequent treatment cycles (with higher CycleNumber) to Failed status when the current cycle is cancelled.
        /// This ensures that when a cycle fails/is cancelled, all remaining cycles in the treatment are also marked as Failed.
        /// </summary>
        /// <param name="treatmentId">The treatment ID to find cycles for</param>
        /// <param name="currentCycleNumber">The CycleNumber of the current cancelled cycle</param>
        /// <param name="reason">The reason for cancellation to include in notes</param>
        /// <param name="callingMethodName">The name of the calling method for logging purposes</param>
        private async Task UpdateAllSubsequentCyclesToFailedAsync(Guid treatmentId, int currentCycleNumber, string? reason, string callingMethodName)
        {
            try
            {
                var subsequentCycles = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .Where(tc =>
                        tc.TreatmentId == treatmentId
                        && tc.CycleNumber > currentCycleNumber
                        && !tc.IsDeleted
                        && tc.Status != TreatmentStatus.Completed
                        && tc.Status != TreatmentStatus.Failed
                        && tc.Status != TreatmentStatus.Cancelled)
                    .OrderBy(tc => tc.CycleNumber)
                    .ToListAsync();

                if (subsequentCycles.Any())
                {
                    foreach (var cycle in subsequentCycles)
                    {
                        var oldStatus = cycle.Status;
                        cycle.Status = TreatmentStatus.Failed;
                        cycle.UpdatedAt = DateTime.UtcNow;
                        cycle.EndDate ??= DateTime.UtcNow;
                        cycle.Notes = cycle.Notes == null
                            ? $"Failed due to cycle #{currentCycleNumber} cancelled: {reason}"
                            : cycle.Notes + $"\nFailed due to cycle #{currentCycleNumber} cancelled: {reason}";

                        await _unitOfWork.Repository<TreatmentCycle>().UpdateGuid(cycle, cycle.Id);

                        _logger.LogInformation(
                            "{CallingMethodName}: Auto-updated cycle {CycleId} (CycleNumber: {CycleNumber}) status from {OldStatus} to Failed for Treatment {TreatmentId}",
                            callingMethodName, cycle.Id, cycle.CycleNumber, oldStatus, treatmentId);
                    }

                    _logger.LogInformation(
                        "{CallingMethodName}: Updated {Count} subsequent cycles to Failed status for Treatment {TreatmentId}",
                        callingMethodName, subsequentCycles.Count, treatmentId);
                }
                else
                {
                    _logger.LogInformation(
                        "{CallingMethodName}: No subsequent cycles found to update for Treatment {TreatmentId} after CycleNumber {CycleNumber}",
                        callingMethodName, treatmentId, currentCycleNumber);
                }
            }
            catch (Exception ex)
            {
                // Log error but don't fail the main operation
                _logger.LogWarning(ex,
                    "{CallingMethodName}: Error updating subsequent cycles to Failed for Treatment {TreatmentId} after CycleNumber {CycleNumber}. Main operation will continue.",
                    callingMethodName, treatmentId, currentCycleNumber);
            }
        }

        /// <summary>
        /// Đồng bộ trạng thái tổng của Treatment và entity chi tiết (TreatmentIUI/TreatmentIVF)
        /// dựa trên trạng thái mới của một TreatmentCycle.
        /// </summary>
        /// <param name="treatmentId">Id của Treatment cần cập nhật</param>
        /// <param name="newStatus">Trạng thái mới của cycle (TreatmentStatus)</param>
        /// <param name="callingMethodName">Tên method gọi phục vụ logging</param>
        private async Task UpdateTreatmentAggregateStatusAsync(Guid treatmentId, TreatmentStatus newStatus, string callingMethodName)
        {
            try
            {
                var treatment = await _unitOfWork.Repository<Treatment>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(t => t.Id == treatmentId && !t.IsDeleted);

                if (treatment == null)
                {
                    _logger.LogWarning(
                        "{CallingMethodName}: Cannot update aggregate treatment status because Treatment {TreatmentId} was not found",
                        callingMethodName, treatmentId);
                    return;
                }

                // Cập nhật trạng thái tổng của Treatment nếu khác
                if (treatment.Status != newStatus)
                {
                    var oldStatus = treatment.Status;
                    treatment.Status = newStatus;
                    treatment.UpdatedAt = DateTime.UtcNow;
                    await _unitOfWork.Repository<Treatment>().UpdateGuid(treatment, treatment.Id);

                    _logger.LogInformation(
                        "{CallingMethodName}: Updated Treatment {TreatmentId} status from {OldStatus} to {NewStatus}",
                        callingMethodName, treatmentId, oldStatus, newStatus);
                }

                // Đồng bộ thêm trạng thái cho TreatmentIUI / TreatmentIVF (nếu có)
                if (treatment.TreatmentType == TreatmentType.IUI)
                {
                    var iui = await _unitOfWork.Repository<TreatmentIUI>()
                        .GetQueryable()
                        .FirstOrDefaultAsync(x => x.Id == treatmentId && !x.IsDeleted);

                    if (iui != null)
                    {
                        var mappedStatus = MapTreatmentStatusToIUICycleStatus(newStatus);
                        if (mappedStatus.HasValue && iui.Status != mappedStatus.Value)
                        {
                            var oldDetailStatus = iui.Status;
                            iui.Status = mappedStatus.Value;
                            iui.UpdatedAt = DateTime.UtcNow;
                            await _unitOfWork.Repository<TreatmentIUI>().UpdateGuid(iui, iui.Id);

                            _logger.LogInformation(
                                "{CallingMethodName}: Updated TreatmentIUI {TreatmentId} status from {OldStatus} to {NewStatus}",
                                callingMethodName, treatmentId, oldDetailStatus, mappedStatus.Value);
                        }
                    }
                }
                else if (treatment.TreatmentType == TreatmentType.IVF)
                {
                    var ivf = await _unitOfWork.Repository<TreatmentIVF>()
                        .GetQueryable()
                        .FirstOrDefaultAsync(x => x.Id == treatmentId && !x.IsDeleted);

                    if (ivf != null)
                    {
                        var mappedStatus = MapTreatmentStatusToIVFCycleStatus(newStatus);
                        if (mappedStatus.HasValue && ivf.Status != mappedStatus.Value)
                        {
                            var oldDetailStatus = ivf.Status;
                            ivf.Status = mappedStatus.Value;
                            ivf.UpdatedAt = DateTime.UtcNow;
                            await _unitOfWork.Repository<TreatmentIVF>().UpdateGuid(ivf, ivf.Id);

                            _logger.LogInformation(
                                "{CallingMethodName}: Updated TreatmentIVF {TreatmentId} status from {OldStatus} to {NewStatus}",
                                callingMethodName, treatmentId, oldDetailStatus, mappedStatus.Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Không làm fail flow chính, chỉ log cảnh báo
                _logger.LogWarning(ex,
                    "{CallingMethodName}: Failed to update aggregate treatment status for Treatment {TreatmentId}",
                    callingMethodName, treatmentId);
            }
        }

        /// <summary>
        /// Map TreatmentStatus chung sang IUICycleStatus cho phác đồ IUI.
        /// Chỉ map các trạng thái chính, các trạng thái chi tiết sẽ được cập nhật ở luồng riêng nếu cần.
        /// </summary>
        private IUICycleStatus? MapTreatmentStatusToIUICycleStatus(TreatmentStatus status)
        {
            return status switch
            {
                TreatmentStatus.Planned => IUICycleStatus.Planned,
                TreatmentStatus.Scheduled => IUICycleStatus.Planned,
                TreatmentStatus.InProgress => IUICycleStatus.Monitoring,
                TreatmentStatus.OnHold => IUICycleStatus.Monitoring,
                TreatmentStatus.Completed => IUICycleStatus.Closed,
                TreatmentStatus.Cancelled => IUICycleStatus.Closed,
                TreatmentStatus.Failed => IUICycleStatus.Closed,
                _ => null
            };
        }

        /// <summary>
        /// Map TreatmentStatus chung sang IVFCycleStatus cho phác đồ IVF.
        /// Chỉ map các trạng thái chính, các trạng thái chi tiết sẽ được cập nhật ở luồng riêng nếu cần.
        /// </summary>
        private IVFCycleStatus? MapTreatmentStatusToIVFCycleStatus(TreatmentStatus status)
        {
            return status switch
            {
                TreatmentStatus.Planned => IVFCycleStatus.Planned,
                TreatmentStatus.Scheduled => IVFCycleStatus.Planned,
                TreatmentStatus.InProgress => IVFCycleStatus.COS,
                TreatmentStatus.OnHold => IVFCycleStatus.COS,
                TreatmentStatus.Completed => IVFCycleStatus.Closed,
                TreatmentStatus.Cancelled => IVFCycleStatus.Closed,
                TreatmentStatus.Failed => IVFCycleStatus.Closed,
                _ => null
            };
        }

        private async Task UpdateTreatmentCurrentStepAsync(Guid treatmentId, int completedCycleNumber, string callingMethodName)
        {
            try
            {
                var treatment = await _unitOfWork.Repository<Treatment>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(t => t.Id == treatmentId && !t.IsDeleted);

                if (treatment == null)
                {
                    _logger.LogWarning("{CallingMethodName}: Unable to update current step because Treatment {TreatmentId} was not found", callingMethodName, treatmentId);
                    return;
                }

                if (treatment.TreatmentType == TreatmentType.IUI)
                {
                    var iui = await _unitOfWork.Repository<TreatmentIUI>()
                        .GetQueryable()
                        .FirstOrDefaultAsync(x => x.Id == treatmentId && !x.IsDeleted);

                    if (iui != null && iui.CurrentStep != completedCycleNumber)
                    {
                        iui.CurrentStep = completedCycleNumber;
                        iui.UpdatedAt = DateTime.UtcNow;
                        await _unitOfWork.Repository<TreatmentIUI>().UpdateGuid(iui, iui.Id);
                        _logger.LogInformation("{CallingMethodName}: Updated IUI current step to {CurrentStep} for Treatment {TreatmentId}", callingMethodName, completedCycleNumber, treatmentId);
                    }
                }
                else if (treatment.TreatmentType == TreatmentType.IVF)
                {
                    var ivf = await _unitOfWork.Repository<TreatmentIVF>()
                        .GetQueryable()
                        .FirstOrDefaultAsync(x => x.Id == treatmentId && !x.IsDeleted);

                    if (ivf != null && ivf.CurrentStep != completedCycleNumber)
                    {
                        ivf.CurrentStep = completedCycleNumber;
                        ivf.UpdatedAt = DateTime.UtcNow;
                        await _unitOfWork.Repository<TreatmentIVF>().UpdateGuid(ivf, ivf.Id);
                        _logger.LogInformation("{CallingMethodName}: Updated IVF current step to {CurrentStep} for Treatment {TreatmentId}", callingMethodName, completedCycleNumber, treatmentId);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "{CallingMethodName}: Failed to update treatment current step for Treatment {TreatmentId}", callingMethodName, treatmentId);
            }
        }

        private async Task<bool> IsPreviousCycleCompletedAsync(Guid treatmentId, int cycleNumber, string callingMethodName)
        {
            if (cycleNumber <= 1)
            {
                return true;
            }

            var previousCycle = await _unitOfWork.Repository<TreatmentCycle>()
                .GetQueryable()
                .AsNoTracking()
                .FirstOrDefaultAsync(tc =>
                    tc.TreatmentId == treatmentId &&
                    tc.CycleNumber == cycleNumber - 1 &&
                    !tc.IsDeleted);

            if (previousCycle == null)
            {
                _logger.LogWarning("{CallingMethodName}: Previous cycle #{PreviousCycleNumber} not found for Treatment {TreatmentId}", callingMethodName, cycleNumber - 1, treatmentId);
                return true;
            }

            var isCompleted = previousCycle.Status == TreatmentStatus.Completed;
            if (!isCompleted)
            {
                _logger.LogInformation("{CallingMethodName}: Previous cycle #{PreviousCycleNumber} for Treatment {TreatmentId} is not completed (Status: {Status})", callingMethodName, cycleNumber - 1, treatmentId, previousCycle.Status);
            }

            return isCompleted;
        }

        private async Task AddAuditLog(string entityType, Guid entityId, string action, string? oldValues, string? newValues)
        {
            var log = new AuditLog(Guid.NewGuid(), null, entityType, entityId, action, oldValues, newValues, null, null, DateTime.UtcNow.AddHours(7));
            await _unitOfWork.Repository<AuditLog>().InsertAsync(log);
        }

        #endregion
    }
}


