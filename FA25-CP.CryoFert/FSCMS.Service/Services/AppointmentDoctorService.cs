using FSCMS.Core.Entities;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSCMS.Service.Services
{
    /// <summary>
    /// Service for managing AppointmentDoctor relationships.
    /// </summary>
    public class AppointmentDoctorService : IAppointmentDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AppointmentDoctorService> _logger;

        public AppointmentDoctorService(IUnitOfWork unitOfWork, ILogger<AppointmentDoctorService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BaseResponse<AppointmentDoctorResponse>> GetByIdAsync(Guid appointmentDoctorId)
        {
            const string methodName = nameof(GetByIdAsync);
            _logger.LogInformation("{MethodName} called with appointmentDoctorId: {AppointmentDoctorId}", methodName, appointmentDoctorId);

            try
            {
                if (appointmentDoctorId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID - {AppointmentDoctorId}", methodName, appointmentDoctorId);
                    return BaseResponse<AppointmentDoctorResponse>.CreateError("ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var entity = await _unitOfWork.Repository<AppointmentDoctor>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(ad => ad.Doctor)
                        .ThenInclude(d => d!.Account)
                    .Include(ad => ad.Appointment)
                    .Where(ad => ad.Id == appointmentDoctorId && !ad.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Not found - {AppointmentDoctorId}", methodName, appointmentDoctorId);
                    return BaseResponse<AppointmentDoctorResponse>.CreateError("AppointmentDoctor not found", StatusCodes.Status404NotFound, "NOT_FOUND");
                }

                var response = MapToResponse(entity);
                return BaseResponse<AppointmentDoctorResponse>.CreateSuccess(response, "Retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error", methodName);
                return BaseResponse<AppointmentDoctorResponse>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<DynamicResponse<AppointmentDoctorResponse>> GetAllAsync(GetAppointmentDoctorsRequest request)
        {
            const string methodName = nameof(GetAllAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                request ??= new GetAppointmentDoctorsRequest();
                request.Normalize();

                var query = _unitOfWork.Repository<AppointmentDoctor>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(ad => ad.Doctor)
                        .ThenInclude(d => d!.Account)
                    .Include(ad => ad.Appointment)
                    .Where(ad => !ad.IsDeleted);

                // Filters
                if (request.AppointmentId.HasValue)
                {
                    query = query.Where(ad => ad.AppointmentId == request.AppointmentId.Value);
                }

                if (request.DoctorId.HasValue)
                {
                    query = query.Where(ad => ad.DoctorId == request.DoctorId.Value);
                }

                if (!string.IsNullOrWhiteSpace(request.Role))
                {
                    var role = request.Role.Trim().ToLowerInvariant();
                    query = query.Where(ad => ad.Role != null && ad.Role.ToLower().Contains(role));
                }

                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    var st = request.SearchTerm.Trim().ToLowerInvariant();
                    query = query.Where(ad =>
                        (ad.Role != null && ad.Role.ToLower().Contains(st)) ||
                        (ad.Notes != null && ad.Notes.ToLower().Contains(st)) ||
                        (ad.Doctor != null && ad.Doctor.Account != null &&
                         (($"{ad.Doctor.Account.FirstName} {ad.Doctor.Account.LastName}").ToLower().Contains(st) ||
                          ad.Doctor.BadgeId!.ToLower().Contains(st)))
                    );
                }

                var total = await query.CountAsync();

                // Sorting
                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    var isDesc = request.Order?.ToLower() == "desc";
                    query = request.Sort.ToLower() switch
                    {
                        "createdat" => isDesc ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt),
                        "role" => isDesc ? query.OrderByDescending(x => x.Role) : query.OrderBy(x => x.Role),
                        _ => isDesc ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt)
                    };
                }
                else
                {
                    query = query.OrderByDescending(x => x.CreatedAt);
                }

                var items = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                var data = items.Select(MapToResponse).ToList();

                return new DynamicResponse<AppointmentDoctorResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Retrieved successfully",
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
                _logger.LogError(ex, "{MethodName}: Error", methodName);
                return new DynamicResponse<AppointmentDoctorResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = $"Error: {ex.Message}",
                    MetaData = new PagingMetaData(),
                    Data = new List<AppointmentDoctorResponse>()
                };
            }
        }

        public async Task<DynamicResponse<AppointmentDoctorResponse>> GetByAppointmentIdAsync(Guid appointmentId, GetAppointmentDoctorsRequest request)
        {
            const string methodName = nameof(GetByAppointmentIdAsync);
            _logger.LogInformation("{MethodName} called with appointmentId: {AppointmentId}, request: {@Request}", methodName, appointmentId, request);

            try
            {
                if (appointmentId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid appointment ID - {AppointmentId}", methodName, appointmentId);
                    return new DynamicResponse<AppointmentDoctorResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_APPOINTMENT_ID",
                        Message = "Appointment ID cannot be empty",
                        MetaData = new PagingMetaData(),
                        Data = new List<AppointmentDoctorResponse>()
                    };
                }

                var exists = await _unitOfWork.Repository<Appointment>()
                    .AsQueryable()
                    .AnyAsync(a => a.Id == appointmentId && !a.IsDeleted);

                if (!exists)
                {
                    _logger.LogWarning("{MethodName}: Appointment not found - {AppointmentId}", methodName, appointmentId);
                    return new DynamicResponse<AppointmentDoctorResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "APPOINTMENT_NOT_FOUND",
                        Message = "Appointment not found",
                        MetaData = new PagingMetaData(),
                        Data = new List<AppointmentDoctorResponse>()
                    };
                }

                request ??= new GetAppointmentDoctorsRequest();
                request.Normalize();
                request.AppointmentId = appointmentId;
                return await GetAllAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error", methodName);
                return new DynamicResponse<AppointmentDoctorResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = $"Error: {ex.Message}",
                    MetaData = new PagingMetaData(),
                    Data = new List<AppointmentDoctorResponse>()
                };
            }
        }

        public async Task<DynamicResponse<AppointmentDoctorResponse>> GetByDoctorIdAsync(Guid doctorId, GetAppointmentDoctorsRequest request)
        {
            const string methodName = nameof(GetByDoctorIdAsync);
            _logger.LogInformation("{MethodName} called with doctorId: {DoctorId}, request: {@Request}", methodName, doctorId, request);

            try
            {
                if (doctorId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid doctor ID - {DoctorId}", methodName, doctorId);
                    return new DynamicResponse<AppointmentDoctorResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_DOCTOR_ID",
                        Message = "Doctor ID cannot be empty",
                        MetaData = new PagingMetaData(),
                        Data = new List<AppointmentDoctorResponse>()
                    };
                }

                var exists = await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .AnyAsync(d => d.Id == doctorId && !d.IsDeleted);

                if (!exists)
                {
                    _logger.LogWarning("{MethodName}: Doctor not found - {DoctorId}", methodName, doctorId);
                    return new DynamicResponse<AppointmentDoctorResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "DOCTOR_NOT_FOUND",
                        Message = "Doctor not found",
                        MetaData = new PagingMetaData(),
                        Data = new List<AppointmentDoctorResponse>()
                    };
                }

                request ??= new GetAppointmentDoctorsRequest();
                request.Normalize();
                request.DoctorId = doctorId;
                return await GetAllAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error", methodName);
                return new DynamicResponse<AppointmentDoctorResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = $"Error: {ex.Message}",
                    MetaData = new PagingMetaData(),
                    Data = new List<AppointmentDoctorResponse>()
                };
            }
        }

        public async Task<BaseResponse<AppointmentDoctorResponse>> CreateAsync(CreateAppointmentDoctorRequest request)
        {
            const string methodName = nameof(CreateAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<AppointmentDoctorResponse>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                // Validate appointment
                var appointmentExists = await _unitOfWork.Repository<Appointment>()
                    .AsQueryable()
                    .AnyAsync(a => a.Id == request.AppointmentId && !a.IsDeleted);
                if (!appointmentExists)
                {
                    _logger.LogWarning("{MethodName}: Appointment not found - {AppointmentId}", methodName, request.AppointmentId);
                    return BaseResponse<AppointmentDoctorResponse>.CreateError("Appointment not found", StatusCodes.Status404NotFound, "APPOINTMENT_NOT_FOUND");
                }

                // Validate doctor
                var doctorExists = await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .AnyAsync(d => d.Id == request.DoctorId && !d.IsDeleted);
                if (!doctorExists)
                {
                    _logger.LogWarning("{MethodName}: Doctor not found - {DoctorId}", methodName, request.DoctorId);
                    return BaseResponse<AppointmentDoctorResponse>.CreateError("Doctor not found", StatusCodes.Status404NotFound, "DOCTOR_NOT_FOUND");
                }

                // Prevent duplicate assignment
                var duplicate = await _unitOfWork.Repository<AppointmentDoctor>()
                    .AsQueryable()
                    .AnyAsync(ad => ad.AppointmentId == request.AppointmentId && ad.DoctorId == request.DoctorId && !ad.IsDeleted);
                if (duplicate)
                {
                    _logger.LogWarning("{MethodName}: Duplicate assignment for appointment {AppointmentId} and doctor {DoctorId}", methodName, request.AppointmentId, request.DoctorId);
                    return BaseResponse<AppointmentDoctorResponse>.CreateError("Doctor is already assigned to this appointment", StatusCodes.Status400BadRequest, "DOCTOR_ALREADY_ASSIGNED");
                }

                var entity = new AppointmentDoctor(
                    Guid.NewGuid(),
                    request.AppointmentId,
                    request.DoctorId,
                    request.Role,
                    request.Notes
                );

                await _unitOfWork.Repository<AppointmentDoctor>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                // Reload with relations
                var created = await _unitOfWork.Repository<AppointmentDoctor>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(ad => ad.Doctor)
                        .ThenInclude(d => d!.Account)
                    .Include(ad => ad.Appointment)
                    .FirstAsync(ad => ad.Id == entity.Id);

                var response = MapToResponse(created);
                return BaseResponse<AppointmentDoctorResponse>.CreateSuccess(response, "Created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error", methodName);
                return BaseResponse<AppointmentDoctorResponse>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<AppointmentDoctorResponse>> UpdateAsync(Guid appointmentDoctorId, UpdateAppointmentDoctorRequest request)
        {
            const string methodName = nameof(UpdateAsync);
            _logger.LogInformation("{MethodName} called with appointmentDoctorId: {AppointmentDoctorId}, request: {@Request}", methodName, appointmentDoctorId, request);

            try
            {
                if (appointmentDoctorId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID - {AppointmentDoctorId}", methodName, appointmentDoctorId);
                    return BaseResponse<AppointmentDoctorResponse>.CreateError("ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<AppointmentDoctorResponse>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                var entity = await _unitOfWork.Repository<AppointmentDoctor>()
                    .AsQueryable()
                    .Where(ad => ad.Id == appointmentDoctorId && !ad.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Not found - {AppointmentDoctorId}", methodName, appointmentDoctorId);
                    return BaseResponse<AppointmentDoctorResponse>.CreateError("AppointmentDoctor not found", StatusCodes.Status404NotFound, "NOT_FOUND");
                }

                if (request.Role != null)
                    entity.Role = request.Role;
                if (request.Notes != null)
                    entity.Notes = request.Notes;

                entity.UpdatedAt = DateTime.UtcNow.AddHours(7);
                await _unitOfWork.Repository<AppointmentDoctor>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();

                // Reload with relations
                var updated = await _unitOfWork.Repository<AppointmentDoctor>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(ad => ad.Doctor)
                        .ThenInclude(d => d!.Account)
                    .Include(ad => ad.Appointment)
                    .FirstAsync(ad => ad.Id == entity.Id);

                var response = MapToResponse(updated);
                return BaseResponse<AppointmentDoctorResponse>.CreateSuccess(response, "Updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error", methodName);
                return BaseResponse<AppointmentDoctorResponse>.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse> DeleteAsync(Guid appointmentDoctorId)
        {
            const string methodName = nameof(DeleteAsync);
            _logger.LogInformation("{MethodName} called with appointmentDoctorId: {AppointmentDoctorId}", methodName, appointmentDoctorId);

            try
            {
                if (appointmentDoctorId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID - {AppointmentDoctorId}", methodName, appointmentDoctorId);
                    return BaseResponse.CreateError("ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var entity = await _unitOfWork.Repository<AppointmentDoctor>()
                    .AsQueryable()
                    .Where(ad => ad.Id == appointmentDoctorId && !ad.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Not found - {AppointmentDoctorId}", methodName, appointmentDoctorId);
                    return BaseResponse.CreateError("AppointmentDoctor not found", StatusCodes.Status404NotFound, "NOT_FOUND");
                }

                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow.AddHours(7);
                entity.UpdatedAt = DateTime.UtcNow.AddHours(7);

                await _unitOfWork.Repository<AppointmentDoctor>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();

                return BaseResponse.CreateSuccess("Deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error", methodName);
                return BaseResponse.CreateError($"Error: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        private AppointmentDoctorResponse MapToResponse(AppointmentDoctor ad)
        {
            var response = new AppointmentDoctorResponse
            {
                Id = ad.Id,
                AppointmentId = ad.AppointmentId,
                DoctorId = ad.DoctorId,
                Role = ad.Role,
                Notes = ad.Notes,
                CreatedAt = ad.CreatedAt,
                UpdatedAt = ad.UpdatedAt
            };

            if (ad.Doctor != null && ad.Doctor.Account != null)
            {
                var account = ad.Doctor.Account;
                response.Doctor = new AppointmentDoctorDoctorInfo
                {
                    Id = ad.Doctor.Id,
                    BadgeId = ad.Doctor.BadgeId,
                    Specialty = ad.Doctor.Specialty,
                    FullName = $"{account.FirstName} {account.LastName}".Trim()
                };
            }

            if (ad.Appointment != null)
            {
                response.Appointment = new AppointmentDoctorAppointmentInfo
                {
                    Id = ad.Appointment.Id,
                    AppointmentDate = ad.Appointment.AppointmentDate,
                    Status = ad.Appointment.Status.ToString()
                };
            }

            return response;
        }
    }
}


