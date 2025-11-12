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
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TreatmentCycleService> _logger;

        public TreatmentCycleService(IUnitOfWork unitOfWork, ILogger<TreatmentCycleService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

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

                entity.UpdateEntity(request);
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<TreatmentCycle>().UpdateGuid(entity, id);

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

                var oldValues = JsonSerializer.Serialize(entity.ToResponseModel());

                entity.Status = TreatmentStatus.Completed;
                if (request?.EndDate.HasValue == true) entity.EndDate = request.EndDate.Value; else entity.EndDate = DateTime.UtcNow;
                if (!string.IsNullOrWhiteSpace(request?.Notes)) entity.Notes = (entity.Notes == null ? request!.Notes : entity.Notes + "\n" + request!.Notes);
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<TreatmentCycle>().UpdateGuid(entity, id);

                await AddAuditLog("TreatmentCycle", id, "Complete", oldValues, JsonSerializer.Serialize(new { entity, request?.Outcome }));
                await _unitOfWork.CommitAsync();

                return BaseResponse<TreatmentCycleResponseModel>.CreateSuccess(entity.ToResponseModel(), "Treatment cycle completed", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error completing cycle {Id}", methodName, id);
                return BaseResponse<TreatmentCycleResponseModel>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

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

                var oldValues = JsonSerializer.Serialize(entity.ToResponseModel());

                entity.Status = TreatmentStatus.Cancelled;
                entity.Notes = (entity.Notes == null ? $"Cancelled: {request?.Reason}" : entity.Notes + $"\nCancelled: {request?.Reason}");
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<TreatmentCycle>().UpdateGuid(entity, id);

                await AddAuditLog("TreatmentCycle", id, "Cancel", oldValues, JsonSerializer.Serialize(entity.ToResponseModel()));
                await _unitOfWork.CommitAsync();

                return BaseResponse<TreatmentCycleResponseModel>.CreateSuccess(entity.ToResponseModel(), "Treatment cycle cancelled", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error cancelling cycle {Id}", methodName, id);
                return BaseResponse<TreatmentCycleResponseModel>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<List<object>>> GetSamplesAsync(Guid id)
        {
            try
            {
                var cycle = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .Include(tc => tc.Treatment)
                    .FirstOrDefaultAsync(tc => tc.Id == id && !tc.IsDeleted);
                if (cycle == null)
                    return BaseResponse<List<object>>.CreateError("Treatment cycle not found", StatusCodes.Status404NotFound, "NOT_FOUND");

                var samples = await _unitOfWork.Repository<LabSample>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(s => s.PatientId == cycle.Treatment!.PatientId && !s.IsDeleted)
                    .OrderByDescending(s => s.CollectionDate)
                    .Select(s => new { s.Id, s.SampleCode, s.SampleType, s.Status, s.CollectionDate })
                    .ToListAsync();

                return BaseResponse<List<object>>.CreateSuccess(samples.Cast<object>().ToList(), "Samples retrieved", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting samples for cycle {Id}", id);
                return BaseResponse<List<object>>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

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

        private async Task AddAuditLog(string entityType, Guid entityId, string action, string? oldValues, string? newValues)
        {
            var log = new AuditLog(Guid.NewGuid(), Guid.Empty, entityType, entityId, action, oldValues, newValues, null, null, DateTime.UtcNow.AddHours(7));
            await _unitOfWork.Repository<AuditLog>().InsertAsync(log);
        }
    }
}


