using AutoMapper;
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
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorService> _logger;

        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DoctorService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Doctor CRUD Operations

        /// <summary>
        /// Get doctor by ID
        /// </summary>
        public async Task<BaseResponse<DoctorResponse>> GetDoctorByIdAsync(Guid doctorId)
        {
            const string methodName = nameof(GetDoctorByIdAsync);
            _logger.LogInformation("{MethodName} called with doctorId: {DoctorId}", methodName, doctorId);

            try
            {
                // Input validation
                if (doctorId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid doctor ID provided - {DoctorId}", methodName, doctorId);
                    return new BaseResponse<DoctorResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_DOCTOR_ID",
                        Message = "Doctor ID cannot be empty or invalid",
                        Data = null
                    };
                }

                var doctor = await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(d => d.Account)
                    .Where(d => d.Id == doctorId && !d.IsDeleted)
                    .FirstOrDefaultAsync();

                if (doctor == null)
                {
                    _logger.LogWarning("{MethodName}: Doctor not found with ID: {DoctorId}", methodName, doctorId);
                    return new BaseResponse<DoctorResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "DOCTOR_NOT_FOUND",
                        Message = "Doctor not found",
                        Data = null
                    };
                }

                var doctorResponse = _mapper.Map<DoctorResponse>(doctor);
                _logger.LogInformation("{MethodName}: Successfully retrieved doctor {DoctorId}", methodName, doctorId);

                return new BaseResponse<DoctorResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Doctor retrieved successfully",
                    Data = doctorResponse
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving doctor {DoctorId}", methodName, doctorId);
                return new BaseResponse<DoctorResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred while retrieving the doctor",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Get detailed doctor information by ID
        /// </summary>
        public async Task<BaseResponse<DoctorDetailResponse>> GetDoctorDetailByIdAsync(Guid doctorId)
        {
            const string methodName = nameof(GetDoctorDetailByIdAsync);
            _logger.LogInformation("{MethodName} called with doctorId: {DoctorId}", methodName, doctorId);

            try
            {
                if (doctorId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid doctor ID provided - {DoctorId}", methodName, doctorId);
                    return new BaseResponse<DoctorDetailResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_DOCTOR_ID",
                        Message = "Doctor ID cannot be empty or invalid",
                        Data = null
                    };
                }

                var doctor = await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(d => d.Account)
                    .Include(d => d.DoctorSchedules.Where(s => !s.IsDeleted))
                    .Include(d => d.Treatments.Where(t => !t.IsDeleted))
                    .Where(d => d.Id == doctorId && !d.IsDeleted)
                    .FirstOrDefaultAsync();

                if (doctor == null)
                {
                    _logger.LogWarning("{MethodName}: Doctor not found with ID: {DoctorId}", methodName, doctorId);
                    return new BaseResponse<DoctorDetailResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "DOCTOR_NOT_FOUND",
                        Message = "Doctor not found",
                        Data = null
                    };
                }

                var doctorDetailResponse = _mapper.Map<DoctorDetailResponse>(doctor);
                _logger.LogInformation("{MethodName}: Successfully retrieved doctor details {DoctorId}", methodName, doctorId);

                return new BaseResponse<DoctorDetailResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Doctor details retrieved successfully",
                    Data = doctorDetailResponse
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving doctor details {DoctorId}", methodName, doctorId);
                return new BaseResponse<DoctorDetailResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred while retrieving the doctor details",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Get doctor by account ID
        /// </summary>
        public async Task<BaseResponse<DoctorResponse>> GetDoctorByAccountIdAsync(Guid accountId)
        {
            const string methodName = nameof(GetDoctorByAccountIdAsync);
            _logger.LogInformation("{MethodName} called with accountId: {AccountId}", methodName, accountId);

            try
            {
                if (accountId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid account ID provided - {AccountId}", methodName, accountId);
                    return new BaseResponse<DoctorResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_ACCOUNT_ID",
                        Message = "Account ID cannot be empty or invalid",
                        Data = null
                    };
                }

                var doctor = await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(d => d.Account)
                    .Where(d => d.AccountId == accountId && !d.IsDeleted)
                    .FirstOrDefaultAsync();

                if (doctor == null)
                {
                    _logger.LogWarning("{MethodName}: Doctor not found with account ID: {AccountId}", methodName, accountId);
                    return new BaseResponse<DoctorResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "DOCTOR_NOT_FOUND",
                        Message = "Doctor not found for the specified account",
                        Data = null
                    };
                }

                var doctorResponse = _mapper.Map<DoctorResponse>(doctor);
                _logger.LogInformation("{MethodName}: Successfully retrieved doctor by account ID {AccountId}", methodName, accountId);

                return new BaseResponse<DoctorResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Doctor retrieved successfully",
                    Data = doctorResponse
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving doctor by account ID {AccountId}", methodName, accountId);
                return new BaseResponse<DoctorResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred while retrieving the doctor",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Get all doctors with pagination and filtering
        /// </summary>
        public async Task<DynamicResponse<DoctorResponse>> GetAllDoctorsAsync(GetDoctorsRequest request)
        {
            const string methodName = nameof(GetAllDoctorsAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                // Normalize pagination parameters
                request.Normalize();

                var query = _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(d => d.Account)
                    .Where(d => !d.IsDeleted);

                // Apply filters
                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    var searchTerm = request.SearchTerm.Trim().ToLowerInvariant();
                    query = query.Where(d =>
                        d.BadgeId.ToLower().Contains(searchTerm) ||
                        d.Specialty.ToLower().Contains(searchTerm) ||
                        (d.Account != null && (
                            d.Account.FirstName.ToLower().Contains(searchTerm) ||
                            d.Account.LastName.ToLower().Contains(searchTerm) ||
                            d.Account.Email.ToLower().Contains(searchTerm))));
                }

                if (!string.IsNullOrWhiteSpace(request.Specialty))
                {
                    query = query.Where(d => d.Specialty.ToLower().Contains(request.Specialty.ToLower()));
                }

                if (request.IsActive.HasValue)
                {
                    query = query.Where(d => d.IsActive == request.IsActive.Value);
                }

                if (request.MinExperience.HasValue)
                {
                    query = query.Where(d => d.YearsOfExperience >= request.MinExperience.Value);
                }

                if (request.MaxExperience.HasValue)
                {
                    query = query.Where(d => d.YearsOfExperience <= request.MaxExperience.Value);
                }

                if (request.JoinDateFrom.HasValue)
                {
                    query = query.Where(d => d.JoinDate >= request.JoinDateFrom.Value);
                }

                if (request.JoinDateTo.HasValue)
                {
                    query = query.Where(d => d.JoinDate <= request.JoinDateTo.Value);
                }

                // Get total count
                var totalCount = await query.CountAsync();

                // Apply sorting
                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    var isDescending = request.Order?.ToLower() == "desc";

                    query = request.Sort.ToLower() switch
                    {
                        "badgeid" => isDescending ? query.OrderByDescending(d => d.BadgeId) : query.OrderBy(d => d.BadgeId),
                        "specialty" => isDescending ? query.OrderByDescending(d => d.Specialty) : query.OrderBy(d => d.Specialty),
                        "experience" => isDescending ? query.OrderByDescending(d => d.YearsOfExperience) : query.OrderBy(d => d.YearsOfExperience),
                        "joindate" => isDescending ? query.OrderByDescending(d => d.JoinDate) : query.OrderBy(d => d.JoinDate),
                        "createdat" => isDescending ? query.OrderByDescending(d => d.CreatedAt) : query.OrderBy(d => d.CreatedAt),
                        _ => isDescending ? query.OrderByDescending(d => d.CreatedAt) : query.OrderBy(d => d.CreatedAt)
                    };
                }
                else
                {
                    query = query.OrderByDescending(d => d.CreatedAt);
                }

                // Apply pagination
                var doctors = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                var doctorResponses = _mapper.Map<List<DoctorResponse>>(doctors);
                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} doctors", methodName, doctorResponses.Count);

                return new DynamicResponse<DoctorResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Doctors retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = totalCount,
                        CurrentPageSize = doctorResponses.Count
                    },
                    Data = doctorResponses
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving doctors", methodName);
                return new DynamicResponse<DoctorResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred while retrieving doctors",
                    MetaData = new PagingMetaData(),
                    Data = new List<DoctorResponse>()
                };
            }
        }

        /// <summary>
        /// Create new doctor
        /// </summary>
        public async Task<BaseResponse<DoctorResponse>> CreateDoctorAsync(CreateDoctorRequest request)
        {
            const string methodName = nameof(CreateDoctorAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                // Check if account exists and is not already associated with a doctor
                var existingAccount = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Where(a => a.Id == request.AccountId && !a.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingAccount == null)
                {
                    _logger.LogWarning("{MethodName}: Account not found with ID: {AccountId}", methodName, request.AccountId);
                    return new BaseResponse<DoctorResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "ACCOUNT_NOT_FOUND",
                        Message = "Account not found",
                        Data = null
                    };
                }

                // Check if account is already associated with a doctor
                var existingDoctor = await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .Where(d => d.AccountId == request.AccountId && !d.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingDoctor != null)
                {
                    _logger.LogWarning("{MethodName}: Account already associated with a doctor: {AccountId}", methodName, request.AccountId);
                    return new BaseResponse<DoctorResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "ACCOUNT_ALREADY_DOCTOR",
                        Message = "Account is already associated with a doctor",
                        Data = null
                    };
                }

                // Check if badge ID is unique
                if (!await IsBadgeIdUniqueAsync(request.BadgeId))
                {
                    _logger.LogWarning("{MethodName}: Badge ID already exists: {BadgeId}", methodName, request.BadgeId);
                    return new BaseResponse<DoctorResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "BADGE_ID_EXISTS",
                        Message = "Badge ID already exists",
                        Data = null
                    };
                }

                // Create doctor entity
                var doctor = _mapper.Map<Doctor>(request);

                // Save to database
                await _unitOfWork.Repository<Doctor>().InsertAsync(doctor);
                await _unitOfWork.CommitAsync();

                // Reload with account information
                var createdDoctor = await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(d => d.Account)
                    .FirstOrDefaultAsync(d => d.Id == doctor.Id);

                var doctorResponse = _mapper.Map<DoctorResponse>(createdDoctor);
                _logger.LogInformation("{MethodName}: Successfully created doctor {DoctorId}", methodName, doctor.Id);

                return new BaseResponse<DoctorResponse>
                {
                    Code = StatusCodes.Status201Created,
                    SystemCode = "SUCCESS",
                    Message = "Doctor created successfully",
                    Data = doctorResponse
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating doctor", methodName);
                return new BaseResponse<DoctorResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred while creating the doctor",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Update existing doctor
        /// </summary>
        public async Task<BaseResponse<DoctorResponse>> UpdateDoctorAsync(Guid doctorId, UpdateDoctorRequest request)
        {
            const string methodName = nameof(UpdateDoctorAsync);
            _logger.LogInformation("{MethodName} called with doctorId: {DoctorId}, request: {@Request}", methodName, doctorId, request);

            try
            {
                if (doctorId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid doctor ID provided - {DoctorId}", methodName, doctorId);
                    return new BaseResponse<DoctorResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_DOCTOR_ID",
                        Message = "Doctor ID cannot be empty or invalid",
                        Data = null
                    };
                }

                var doctor = await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .Where(d => d.Id == doctorId && !d.IsDeleted)
                    .FirstOrDefaultAsync();

                if (doctor == null)
                {
                    _logger.LogWarning("{MethodName}: Doctor not found with ID: {DoctorId}", methodName, doctorId);
                    return new BaseResponse<DoctorResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "DOCTOR_NOT_FOUND",
                        Message = "Doctor not found",
                        Data = null
                    };
                }

                // Check if badge ID is unique (if being updated)
                if (!string.IsNullOrWhiteSpace(request.BadgeId) && request.BadgeId != doctor.BadgeId)
                {
                    if (!await IsBadgeIdUniqueAsync(request.BadgeId, doctorId))
                    {
                        _logger.LogWarning("{MethodName}: Badge ID already exists: {BadgeId}", methodName, request.BadgeId);
                        return new BaseResponse<DoctorResponse>
                        {
                            Code = StatusCodes.Status400BadRequest,
                            SystemCode = "BADGE_ID_EXISTS",
                            Message = "Badge ID already exists",
                            Data = null
                        };
                    }
                }

                // Update doctor
                _mapper.Map(request, doctor);

                // Save changes
                await _unitOfWork.Repository<Doctor>().UpdateGuid(doctor, doctor.Id);
                await _unitOfWork.CommitAsync();

                // Reload with account information
                var updatedDoctor = await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(d => d.Account)
                    .FirstOrDefaultAsync(d => d.Id == doctorId);

                var doctorResponse = _mapper.Map<DoctorResponse>(updatedDoctor);
                _logger.LogInformation("{MethodName}: Successfully updated doctor {DoctorId}", methodName, doctorId);

                return new BaseResponse<DoctorResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Doctor updated successfully",
                    Data = doctorResponse
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating doctor {DoctorId}", methodName, doctorId);
                return new BaseResponse<DoctorResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred while updating the doctor",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Delete doctor (soft delete)
        /// </summary>
        public async Task<BaseResponse> DeleteDoctorAsync(Guid doctorId)
        {
            const string methodName = nameof(DeleteDoctorAsync);
            _logger.LogInformation("{MethodName} called with doctorId: {DoctorId}", methodName, doctorId);

            try
            {
                if (doctorId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid doctor ID provided - {DoctorId}", methodName, doctorId);
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_DOCTOR_ID",
                        Message = "Doctor ID cannot be empty or invalid"
                    };
                }

                var doctor = await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .Where(d => d.Id == doctorId && !d.IsDeleted)
                    .FirstOrDefaultAsync();

                if (doctor == null)
                {
                    _logger.LogWarning("{MethodName}: Doctor not found with ID: {DoctorId}", methodName, doctorId);
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "DOCTOR_NOT_FOUND",
                        Message = "Doctor not found"
                    };
                }

                // Check if doctor has active schedules or treatments
                var hasActiveSchedules = await _unitOfWork.Repository<DoctorSchedule>()
                    .AsQueryable()
                    .AnyAsync(s => s.DoctorId == doctorId && s.WorkDate >= DateTime.Today && !s.IsDeleted);

                if (hasActiveSchedules)
                {
                    _logger.LogWarning("{MethodName}: Cannot delete doctor with active schedules: {DoctorId}", methodName, doctorId);
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "DOCTOR_HAS_ACTIVE_SCHEDULES",
                        Message = "Cannot delete doctor with active schedules"
                    };
                }

                // Soft delete
                doctor.IsDeleted = true;
                doctor.DeletedAt = DateTime.UtcNow;
                doctor.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Doctor>().UpdateGuid(doctor, doctor.Id);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully deleted doctor {DoctorId}", methodName, doctorId);

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Doctor deleted successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error deleting doctor {DoctorId}", methodName, doctorId);
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred while deleting the doctor"
                };
            }
        }

        /// <summary>
        /// Update doctor status (active/inactive)
        /// </summary>
        public async Task<BaseResponse> UpdateDoctorStatusAsync(Guid doctorId, bool isActive)
        {
            const string methodName = nameof(UpdateDoctorStatusAsync);
            _logger.LogInformation("{MethodName} called with doctorId: {DoctorId}, isActive: {IsActive}", methodName, doctorId, isActive);

            try
            {
                if (doctorId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid doctor ID provided - {DoctorId}", methodName, doctorId);
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_DOCTOR_ID",
                        Message = "Doctor ID cannot be empty or invalid"
                    };
                }

                var doctor = await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .Where(d => d.Id == doctorId && !d.IsDeleted)
                    .FirstOrDefaultAsync();

                if (doctor == null)
                {
                    _logger.LogWarning("{MethodName}: Doctor not found with ID: {DoctorId}", methodName, doctorId);
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "DOCTOR_NOT_FOUND",
                        Message = "Doctor not found"
                    };
                }

                doctor.IsActive = isActive;
                doctor.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Doctor>().UpdateGuid(doctor, doctor.Id);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully updated doctor status {DoctorId} to {IsActive}", methodName, doctorId, isActive);

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = $"Doctor status updated to {(isActive ? "active" : "inactive")} successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating doctor status {DoctorId}", methodName, doctorId);
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred while updating the doctor status"
                };
            }
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Check if doctor exists
        /// </summary>
        public async Task<bool> DoctorExistsAsync(Guid doctorId)
        {
            try
            {
                if (doctorId == Guid.Empty)
                    return false;

                return await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .AnyAsync(d => d.Id == doctorId && !d.IsDeleted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if doctor exists: {DoctorId}", doctorId);
                return false;
            }
        }

        /// <summary>
        /// Check if badge ID is unique
        /// </summary>
        public async Task<bool> IsBadgeIdUniqueAsync(string badgeId, Guid? excludeDoctorId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(badgeId))
                    return false;

                var query = _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .Where(d => d.BadgeId == badgeId && !d.IsDeleted);

                if (excludeDoctorId.HasValue)
                {
                    query = query.Where(d => d.Id != excludeDoctorId.Value);
                }

                return !await query.AnyAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking badge ID uniqueness: {BadgeId}", badgeId);
                return false;
            }
        }

        /// <summary>
        /// Get doctor statistics
        /// </summary>
        public async Task<BaseResponse<DoctorStatisticsResponse>> GetDoctorStatisticsAsync()
        {
            const string methodName = nameof(GetDoctorStatisticsAsync);
            _logger.LogInformation("{MethodName} called", methodName);

            try
            {
                var today = DateTime.Today;

                var totalDoctors = await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .CountAsync(d => !d.IsDeleted);

                var activeDoctors = await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .CountAsync(d => !d.IsDeleted && d.IsActive);

                var inactiveDoctors = totalDoctors - activeDoctors;

                var totalSchedulesToday = await _unitOfWork.Repository<DoctorSchedule>()
                    .AsQueryable()
                    .CountAsync(s => s.WorkDate == today && !s.IsDeleted);

                var totalSlotsToday = await _unitOfWork.Repository<Slot>()
                    .AsQueryable()
                    .Include(s => s.DoctorSchedule)
                    .CountAsync(s => s.DoctorSchedule.WorkDate == today && !s.IsDeleted && !s.DoctorSchedule.IsDeleted);

                var bookedSlotsToday = await _unitOfWork.Repository<Slot>()
                    .AsQueryable()
                    .Include(s => s.DoctorSchedule)
                    .CountAsync(s => s.DoctorSchedule.WorkDate == today && s.IsBooked && !s.IsDeleted && !s.DoctorSchedule.IsDeleted);

                var availableSlotsToday = totalSlotsToday - bookedSlotsToday;

                var specialties = await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .Where(d => !d.IsDeleted)
                    .GroupBy(d => d.Specialty)
                    .Select(g => new SpecialtyStatistic
                    {
                        Specialty = g.Key,
                        DoctorCount = g.Count(),
                        AverageExperience = g.Average(d => d.YearsOfExperience)
                    })
                    .OrderByDescending(s => s.DoctorCount)
                    .ToListAsync();

                var statistics = new DoctorStatisticsResponse
                {
                    TotalDoctors = totalDoctors,
                    ActiveDoctors = activeDoctors,
                    InactiveDoctors = inactiveDoctors,
                    TotalSchedulesToday = totalSchedulesToday,
                    TotalSlotsToday = totalSlotsToday,
                    BookedSlotsToday = bookedSlotsToday,
                    AvailableSlotsToday = availableSlotsToday,
                    Specialties = specialties
                };

                _logger.LogInformation("{MethodName}: Successfully retrieved doctor statistics", methodName);

                return new BaseResponse<DoctorStatisticsResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Doctor statistics retrieved successfully",
                    Data = statistics
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving doctor statistics", methodName);
                return new BaseResponse<DoctorStatisticsResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred while retrieving doctor statistics",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Get available specialties
        /// </summary>
        public async Task<BaseResponse<List<string>>> GetAvailableSpecialtiesAsync()
        {
            const string methodName = nameof(GetAvailableSpecialtiesAsync);
            _logger.LogInformation("{MethodName} called", methodName);

            try
            {
                var specialties = await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .Where(d => !d.IsDeleted)
                    .Select(d => d.Specialty)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToListAsync();

                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} specialties", methodName, specialties.Count);

                return new BaseResponse<List<string>>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Available specialties retrieved successfully",
                    Data = specialties
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving available specialties", methodName);
                return new BaseResponse<List<string>>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred while retrieving available specialties",
                    Data = new List<string>()
                };
            }
        }

        #endregion

        #region Doctor Schedule CRUD Operations - Placeholder implementations

        public async Task<BaseResponse<DoctorScheduleResponse>> GetDoctorScheduleByIdAsync(Guid scheduleId)
        {
            // Implementation will be added in the next part
            throw new NotImplementedException("This method will be implemented in the next part of the service.");
        }

        public async Task<BaseResponse<DoctorScheduleDetailResponse>> GetDoctorScheduleDetailByIdAsync(Guid scheduleId)
        {
            throw new NotImplementedException("This method will be implemented in the next part of the service.");
        }

        public async Task<DynamicResponse<DoctorScheduleResponse>> GetAllDoctorSchedulesAsync(GetDoctorSchedulesRequest request)
        {
            throw new NotImplementedException("This method will be implemented in the next part of the service.");
        }

        public async Task<DynamicResponse<DoctorScheduleResponse>> GetDoctorSchedulesByDoctorIdAsync(Guid doctorId, GetDoctorSchedulesRequest request)
        {
            throw new NotImplementedException("This method will be implemented in the next part of the service.");
        }

        public async Task<BaseResponse<DoctorScheduleResponse>> CreateDoctorScheduleAsync(CreateDoctorScheduleRequest request)
        {
            throw new NotImplementedException("This method will be implemented in the next part of the service.");
        }

        public async Task<BaseResponse<DoctorScheduleResponse>> UpdateDoctorScheduleAsync(Guid scheduleId, UpdateDoctorScheduleRequest request)
        {
            throw new NotImplementedException("This method will be implemented in the next part of the service.");
        }

        public async Task<BaseResponse> DeleteDoctorScheduleAsync(Guid scheduleId)
        {
            throw new NotImplementedException("This method will be implemented in the next part of the service.");
        }

        public async Task<BaseResponse> UpdateScheduleAvailabilityAsync(Guid scheduleId, bool isAvailable)
        {
            throw new NotImplementedException("This method will be implemented in the next part of the service.");
        }

        #endregion

        #region Slot CRUD Operations - Placeholder implementations

        public async Task<BaseResponse<SlotResponse>> GetSlotByIdAsync(Guid slotId)
        {
            throw new NotImplementedException("This method will be implemented in the next part of the service.");
        }

        public async Task<BaseResponse<SlotDetailResponse>> GetSlotDetailByIdAsync(Guid slotId)
        {
            throw new NotImplementedException("This method will be implemented in the next part of the service.");
        }

        public async Task<DynamicResponse<SlotResponse>> GetAllSlotsAsync(GetSlotsRequest request)
        {
            throw new NotImplementedException("This method will be implemented in the next part of the service.");
        }

        public async Task<DynamicResponse<SlotResponse>> GetSlotsByScheduleIdAsync(Guid scheduleId, GetSlotsRequest request)
        {
            throw new NotImplementedException("This method will be implemented in the next part of the service.");
        }

        public async Task<DynamicResponse<SlotResponse>> GetAvailableSlotsAsync(Guid doctorId, DateTime dateFrom, DateTime dateTo)
        {
            throw new NotImplementedException("This method will be implemented in the next part of the service.");
        }

        public async Task<BaseResponse<SlotResponse>> CreateSlotAsync(CreateSlotRequest request)
        {
            throw new NotImplementedException("This method will be implemented in the next part of the service.");
        }

        public async Task<BaseResponse<int>> CreateSlotsForScheduleAsync(Guid scheduleId, int slotDuration = 30)
        {
            throw new NotImplementedException("This method will be implemented in the next part of the service.");
        }

        public async Task<BaseResponse<SlotResponse>> UpdateSlotAsync(Guid slotId, UpdateSlotRequest request)
        {
            throw new NotImplementedException("This method will be implemented in the next part of the service.");
        }

        public async Task<BaseResponse> DeleteSlotAsync(Guid slotId)
        {
            throw new NotImplementedException("This method will be implemented in the next part of the service.");
        }

        public async Task<BaseResponse> UpdateSlotBookingStatusAsync(Guid slotId, bool isBooked)
        {
            throw new NotImplementedException("This method will be implemented in the next part of the service.");
        }

        #endregion
    }
}
