using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
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
                    .Where(d => d.Id == accountId && !d.IsDeleted)
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
        /// Get doctors who are available based on optional filters
        /// </summary>
        public async Task<DynamicResponse<DoctorResponse>> GetAvailableDoctorsAsync(GetAvailableDoctorsRequest request)
        {
            const string methodName = nameof(GetAvailableDoctorsAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                request ??= new GetAvailableDoctorsRequest();
                request.Normalize();

                if (request.SlotId.HasValue && request.SlotId.Value == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid slot ID provided", methodName);
                    return new DynamicResponse<DoctorResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_SLOT_ID",
                        Message = "Slot ID cannot be empty or invalid",
                        MetaData = new PagingMetaData
                        {
                            Page = request.Page,
                            Size = request.Size,
                            Total = 0,
                            CurrentPageSize = 0
                        },
                        Data = new List<DoctorResponse>()
                    };
                }

                if (request.SlotId.HasValue && !request.WorkDate.HasValue)
                {
                    _logger.LogWarning("{MethodName}: Slot filter requires work date", methodName);
                    return new DynamicResponse<DoctorResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_FILTER_COMBINATION",
                        Message = "Work date must be provided when filtering by slot",
                        MetaData = new PagingMetaData
                        {
                            Page = request.Page,
                            Size = request.Size,
                            Total = 0,
                            CurrentPageSize = 0
                        },
                        Data = new List<DoctorResponse>()
                    };
                }

                var query = _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(d => d.Account)
                    .Where(d => !d.IsDeleted);

                if (!request.IncludeInactive)
                {
                    query = query.Where(d => d.IsActive);
                }

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    var searchTerm = request.Name.Trim().ToLowerInvariant();
                    query = query.Where(d => d.Account != null &&
                        (
                            (d.Account.FirstName ?? string.Empty).ToLower().Contains(searchTerm) ||
                            (d.Account.LastName ?? string.Empty).ToLower().Contains(searchTerm) ||
                            ((d.Account.FirstName ?? string.Empty) + " " + (d.Account.LastName ?? string.Empty)).ToLower().Contains(searchTerm) ||
                            ((d.Account.LastName ?? string.Empty) + " " + (d.Account.FirstName ?? string.Empty)).ToLower().Contains(searchTerm)
                        ));
                }

                var blockingStatuses = new[]
                {
                    AppointmentStatus.Scheduled,
                    AppointmentStatus.Confirmed,
                    AppointmentStatus.CheckedIn,
                    AppointmentStatus.InProgress
                };

                if (request.WorkDate.HasValue)
                {
                    var workDate = request.WorkDate.Value;

                    query = query.Where(d => d.DoctorSchedules.Any(ds =>
                        !ds.IsDeleted &&
                        ds.IsAvailable &&
                        ds.WorkDate == workDate &&
                        ds.Slot != null &&
                        !ds.Slot.IsDeleted &&
                        (!request.SlotId.HasValue || ds.SlotId == request.SlotId.Value) &&
                        !d.AppointmentDoctors.Any(ad =>
                            !ad.IsDeleted &&
                            ad.Appointment != null &&
                            !ad.Appointment.IsDeleted &&
                            ad.Appointment.SlotId == ds.SlotId &&
                            ad.Appointment.AppointmentDate == workDate &&
                            blockingStatuses.Contains(ad.Appointment.Status)
                        )
                    ));
                }
                else
                {
                    query = query.Where(d => d.DoctorSchedules.Any(ds =>
                        !ds.IsDeleted &&
                        ds.IsAvailable &&
                        ds.Slot != null &&
                        !ds.Slot.IsDeleted));
                }

                var totalCount = await query.CountAsync();

                var isDescending = request.Order == "desc";
                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    switch (request.Sort.ToLower())
                    {
                        case "name":
                            query = isDescending
                                ? query
                                    .OrderByDescending(d => d.Account != null ? d.Account.LastName : string.Empty)
                                    .ThenByDescending(d => d.Account != null ? d.Account.FirstName : string.Empty)
                                : query
                                    .OrderBy(d => d.Account != null ? d.Account.LastName : string.Empty)
                                    .ThenBy(d => d.Account != null ? d.Account.FirstName : string.Empty);
                            break;
                        case "experience":
                            query = isDescending
                                ? query.OrderByDescending(d => d.YearsOfExperience)
                                : query.OrderBy(d => d.YearsOfExperience);
                            break;
                        case "joindate":
                            query = isDescending
                                ? query.OrderByDescending(d => d.JoinDate)
                                : query.OrderBy(d => d.JoinDate);
                            break;
                        case "createdat":
                            query = isDescending
                                ? query.OrderByDescending(d => d.CreatedAt)
                                : query.OrderBy(d => d.CreatedAt);
                            break;
                        default:
                            query = isDescending
                                ? query.OrderByDescending(d => d.CreatedAt)
                                : query.OrderBy(d => d.CreatedAt);
                            break;
                    }
                }
                else
                {
                    query = query
                        .OrderBy(d => d.Account != null ? d.Account.LastName : string.Empty)
                        .ThenBy(d => d.Account != null ? d.Account.FirstName : string.Empty);
                }

                var doctors = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                var doctorResponses = _mapper.Map<List<DoctorResponse>>(doctors);

                _logger.LogInformation("{MethodName}: Retrieved {Count} available doctors", methodName, doctorResponses.Count);

                return new DynamicResponse<DoctorResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Available doctors retrieved successfully",
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
                _logger.LogError(ex, "{MethodName}: Error retrieving available doctors", methodName);
                return new DynamicResponse<DoctorResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = $"An internal error occurred while retrieving available doctors: {ex.Message}",
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

                // Ensure account has Doctor role
                var doctorRoleId = new Guid("00000000-0000-0000-0000-000000000002");
                if (existingAccount.RoleId != doctorRoleId)
                {
                    _logger.LogWarning("{MethodName}: Account does not have Doctor role - {AccountId}", methodName, request.AccountId);
                    return new BaseResponse<DoctorResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "ACCOUNT_ROLE_INVALID",
                        Message = "Account role must be Doctor to create a Doctor profile",
                        Data = null
                    };
                }

                // Check if account is already associated with a doctor
                var existingDoctor = await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .Where(d => d.Id == request.AccountId && !d.IsDeleted)
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

                // Create doctor with shared PK = AccountId, then map other fields
                var doctor = new Doctor(
                    request.AccountId,
                    request.BadgeId,
                    request.Specialty,
                    request.YearsOfExperience,
                    request.JoinDate,
                    true
                );
                _mapper.Map(request, doctor);

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
                    .AnyAsync(s => s.DoctorId == doctorId && s.WorkDate >= DateOnly.FromDateTime(DateTime.Today) && !s.IsDeleted);

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
                var today = DateOnly.FromDateTime(DateTime.Today);

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
                    .Include(s => s.DoctorSchedules)
                    .CountAsync(s => s.DoctorSchedules.Any(ds => ds.WorkDate == today && !ds.IsDeleted) && !s.IsDeleted);

				var bookedSlotsToday = await _unitOfWork.Repository<Slot>()
                    .AsQueryable()
					.Include(s => s.DoctorSchedules)
					.CountAsync(s => s.DoctorSchedules.Any(ds => ds.WorkDate == today && !ds.IsDeleted) && s.Appointments != null && !s.IsDeleted);

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

        #region Doctor Schedule CRUD Operations

        /// <summary>
        /// Get doctor schedule by ID
        /// </summary>
        public async Task<BaseResponse<DoctorScheduleResponse>> GetDoctorScheduleByIdAsync(Guid scheduleId)
        {
            const string methodName = nameof(GetDoctorScheduleByIdAsync);
            _logger.LogInformation("{MethodName} called with scheduleId: {ScheduleId}", methodName, scheduleId);

            try
            {
                if (scheduleId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid schedule ID provided - {ScheduleId}", methodName, scheduleId);
                    return BaseResponse<DoctorScheduleResponse>.CreateError("Schedule ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_SCHEDULE_ID");
                }

                var schedule = await _unitOfWork.Repository<DoctorSchedule>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(ds => ds.Doctor)
                        .ThenInclude(d => d.Account)
					.Include(ds => ds.Slot)
						.ThenInclude(s => s.Appointments)
							.ThenInclude(a => a.Patient)
                    .Where(ds => ds.Id == scheduleId && !ds.IsDeleted)
                    .FirstOrDefaultAsync();

                if (schedule == null)
                {
                    _logger.LogWarning("{MethodName}: Schedule not found with ID: {ScheduleId}", methodName, scheduleId);
                    return BaseResponse<DoctorScheduleResponse>.CreateError("Schedule not found", StatusCodes.Status404NotFound, "SCHEDULE_NOT_FOUND");
                }

                var scheduleResponse = _mapper.Map<DoctorScheduleResponse>(schedule);
                
				// Calculate slot statistics (booked inferred by linked appointment)
				scheduleResponse.TotalSlots = schedule.Slot != null ? 1 : 0;
				scheduleResponse.BookedSlots = schedule.Slot != null && schedule.Slot.Appointments.Any(a =>
						!a.IsDeleted &&
						a.Status != AppointmentStatus.Cancelled &&
						a.Status != AppointmentStatus.Rescheduled &&
						a.Status != AppointmentStatus.Completed &&
						a.Status != AppointmentStatus.NoShow) ? 1 : 0;
				scheduleResponse.AvailableSlots = scheduleResponse.TotalSlots - scheduleResponse.BookedSlots;

                _logger.LogInformation("{MethodName}: Successfully retrieved schedule {ScheduleId}", methodName, scheduleId);
                return BaseResponse<DoctorScheduleResponse>.CreateSuccess(scheduleResponse, "Schedule retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving schedule {ScheduleId}", methodName, scheduleId);
                return BaseResponse<DoctorScheduleResponse>.CreateError($"Error retrieving schedule: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Get detailed doctor schedule by ID with slots
        /// </summary>
        public async Task<BaseResponse<DoctorScheduleDetailResponse>> GetDoctorScheduleDetailByIdAsync(Guid scheduleId)
        {
            const string methodName = nameof(GetDoctorScheduleDetailByIdAsync);
            _logger.LogInformation("{MethodName} called with scheduleId: {ScheduleId}", methodName, scheduleId);

            try
            {
                if (scheduleId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid schedule ID provided - {ScheduleId}", methodName, scheduleId);
                    return BaseResponse<DoctorScheduleDetailResponse>.CreateError("Schedule ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_SCHEDULE_ID");
                }

                var schedule = await _unitOfWork.Repository<DoctorSchedule>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(ds => ds.Doctor)
                        .ThenInclude(d => d.Account)
					.Include(ds => ds.Slot)
						.ThenInclude(s => s.Appointments)
							.ThenInclude(a => a.TreatmentCycle)
								.ThenInclude(tc => tc.Treatment)
									.ThenInclude(t => t.Patient)
										.ThenInclude(p => p.Account)
                    .Where(ds => ds.Id == scheduleId && !ds.IsDeleted)
                    .FirstOrDefaultAsync();

                if (schedule == null)
                {
                    _logger.LogWarning("{MethodName}: Schedule not found with ID: {ScheduleId}", methodName, scheduleId);
                    return BaseResponse<DoctorScheduleDetailResponse>.CreateError("Schedule not found", StatusCodes.Status404NotFound, "SCHEDULE_NOT_FOUND");
                }

                var scheduleDetailResponse = _mapper.Map<DoctorScheduleDetailResponse>(schedule);
                
				// Calculate slot statistics (booked inferred by linked appointment)
				scheduleDetailResponse.TotalSlots = schedule.Slot != null ? 1 : 0;
				scheduleDetailResponse.BookedSlots = schedule.Slot != null && schedule.Slot.Appointments.Any(a =>
						!a.IsDeleted &&
						a.Status != AppointmentStatus.Cancelled &&
						a.Status != AppointmentStatus.Rescheduled &&
						a.Status != AppointmentStatus.Completed &&
						a.Status != AppointmentStatus.NoShow) ? 1 : 0;
				scheduleDetailResponse.AvailableSlots = scheduleDetailResponse.TotalSlots - scheduleDetailResponse.BookedSlots;

                // Map slots
                scheduleDetailResponse.Slots = schedule.Slot != null ? _mapper.Map<List<SlotResponse>>(new List<Slot> { schedule.Slot }) : new List<SlotResponse>();

                _logger.LogInformation("{MethodName}: Successfully retrieved schedule details {ScheduleId}", methodName, scheduleId);
                return BaseResponse<DoctorScheduleDetailResponse>.CreateSuccess(scheduleDetailResponse, "Schedule details retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving schedule details {ScheduleId}", methodName, scheduleId);
                return BaseResponse<DoctorScheduleDetailResponse>.CreateError($"Error retrieving schedule details: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Get all doctor schedules with pagination and filtering
        /// </summary>
        public async Task<DynamicResponse<DoctorScheduleResponse>> GetAllDoctorSchedulesAsync(GetDoctorSchedulesRequest request)
        {
            const string methodName = nameof(GetAllDoctorSchedulesAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                if (request == null)
                    request = new GetDoctorSchedulesRequest();

                request.Normalize();

                var query = _unitOfWork.Repository<DoctorSchedule>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(ds => ds.Doctor)
                        .ThenInclude(d => d.Account)
                    .Include(ds => ds.Slot)
                        .ThenInclude(s => s.Appointments)
                    .Where(ds => !ds.IsDeleted);

                // Apply filters
                if (request.DoctorId.HasValue)
                {
                    query = query.Where(ds => ds.DoctorId == request.DoctorId.Value);
                }

                if (request.WorkDateFrom.HasValue)
                {
                    query = query.Where(ds => ds.WorkDate >= DateOnly.FromDateTime(request.WorkDateFrom.Value));
                }

                if (request.WorkDateTo.HasValue)
                {
                    query = query.Where(ds => ds.WorkDate <= DateOnly.FromDateTime(request.WorkDateTo.Value));
                }

                if (request.IsAvailable.HasValue)
                {
                    query = query.Where(ds => ds.IsAvailable == request.IsAvailable.Value);
                }

                if (!string.IsNullOrWhiteSpace(request.Location))
                {
                    query = query.Where(ds => ds.Location != null && ds.Location.Contains(request.Location));
                }

                // Get total count
                var totalCount = await query.CountAsync();

                // Apply sorting
                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    var isDescending = request.Order?.ToLower() == "desc";
                    query = request.Sort.ToLower() switch
                    {
                        "workdate" => isDescending ? query.OrderByDescending(ds => ds.WorkDate) : query.OrderBy(ds => ds.WorkDate),
                        "createdat" => isDescending ? query.OrderByDescending(ds => ds.CreatedAt) : query.OrderBy(ds => ds.CreatedAt),
                        _ => isDescending ? query.OrderByDescending(ds => ds.CreatedAt) : query.OrderBy(ds => ds.CreatedAt)
                    };
                }
                else
                {
                    query = query.OrderBy(ds => ds.WorkDate).ThenBy(ds => ds.CreatedAt);
                }

                // Apply pagination
                var schedules = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                var scheduleResponses = _mapper.Map<List<DoctorScheduleResponse>>(schedules);
                
				// Calculate slot statistics for each schedule (booked inferred by linked appointment)
                foreach (var scheduleResponse in scheduleResponses)
                {
                    var schedule = schedules.First(s => s.Id == scheduleResponse.Id);
					scheduleResponse.TotalSlots = schedule.Slot != null ? 1 : 0;
					scheduleResponse.BookedSlots = schedule.Slot != null && schedule.Slot.Appointments.Any(a =>
							!a.IsDeleted &&
							a.Status != AppointmentStatus.Cancelled &&
							a.Status != AppointmentStatus.Rescheduled &&
							a.Status != AppointmentStatus.Completed &&
							a.Status != AppointmentStatus.NoShow) ? 1 : 0;
					scheduleResponse.AvailableSlots = scheduleResponse.TotalSlots - scheduleResponse.BookedSlots;
                }

                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} schedules", methodName, scheduleResponses.Count);

                return new DynamicResponse<DoctorScheduleResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Schedules retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = totalCount,
                        CurrentPageSize = scheduleResponses.Count
                    },
                    Data = scheduleResponses
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving schedules", methodName);
                return new DynamicResponse<DoctorScheduleResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = $"Error retrieving schedules: {ex.Message}",
                    MetaData = new PagingMetaData(),
                    Data = new List<DoctorScheduleResponse>()
                };
            }
        }

        /// <summary>
        /// Get schedules for a specific doctor
        /// </summary>
        public async Task<DynamicResponse<DoctorScheduleResponse>> GetDoctorSchedulesByDoctorIdAsync(Guid doctorId, GetDoctorSchedulesRequest request)
        {
            const string methodName = nameof(GetDoctorSchedulesByDoctorIdAsync);
            _logger.LogInformation("{MethodName} called with doctorId: {DoctorId}, request: {@Request}", methodName, doctorId, request);

            try
            {
                if (doctorId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid doctor ID provided - {DoctorId}", methodName, doctorId);
                    return new DynamicResponse<DoctorScheduleResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_DOCTOR_ID",
                        Message = "Doctor ID cannot be empty",
                        MetaData = new PagingMetaData(),
                        Data = new List<DoctorScheduleResponse>()
                    };
                }

                // Verify doctor exists
                var doctorExists = await DoctorExistsAsync(doctorId);
                if (!doctorExists)
                {
                    _logger.LogWarning("{MethodName}: Doctor not found with ID: {DoctorId}", methodName, doctorId);
                    return new DynamicResponse<DoctorScheduleResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "DOCTOR_NOT_FOUND",
                        Message = "Doctor not found",
                        MetaData = new PagingMetaData(),
                        Data = new List<DoctorScheduleResponse>()
                    };
                }

                if (request == null)
                    request = new GetDoctorSchedulesRequest();

                request.Normalize();
                request.DoctorId = doctorId; // Override to ensure filtering by doctor

                return await GetAllDoctorSchedulesAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving schedules for doctor {DoctorId}", methodName, doctorId);
                return new DynamicResponse<DoctorScheduleResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = $"Error retrieving schedules: {ex.Message}",
                    MetaData = new PagingMetaData(),
                    Data = new List<DoctorScheduleResponse>()
                };
            }
        }

        /// <summary>
        /// Create new doctor schedule
        /// </summary>
        public async Task<BaseResponse<DoctorScheduleResponse>> CreateDoctorScheduleAsync(CreateDoctorScheduleRequest request)
        {
            const string methodName = nameof(CreateDoctorScheduleAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<DoctorScheduleResponse>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                // Verify doctor exists
                var doctorExists = await DoctorExistsAsync(request.DoctorId);
                if (!doctorExists)
                {
                    _logger.LogWarning("{MethodName}: Doctor not found with ID: {DoctorId}", methodName, request.DoctorId);
                    return BaseResponse<DoctorScheduleResponse>.CreateError("Doctor not found", StatusCodes.Status404NotFound, "DOCTOR_NOT_FOUND");
                }

                // Verify slot exists
                var slotExists = await _unitOfWork.Repository<Slot>()
                    .AsQueryable()
                    .AnyAsync(s => s.Id == request.SlotId && !s.IsDeleted);

                if (!slotExists)
                {
                    _logger.LogWarning("{MethodName}: Slot not found with ID: {SlotId}", methodName, request.SlotId);
                    return BaseResponse<DoctorScheduleResponse>.CreateError("Slot not found", StatusCodes.Status404NotFound, "SLOT_NOT_FOUND");
                }

                // Check for duplicate schedule (same doctor, same slot, same date)
                var hasDuplicate = await _unitOfWork.Repository<DoctorSchedule>()
                    .AsQueryable()
                    .Where(ds => ds.DoctorId == request.DoctorId &&
                                 ds.SlotId == request.SlotId &&
                                 ds.WorkDate == request.WorkDate &&
                                 !ds.IsDeleted)
                    .AnyAsync();

                if (hasDuplicate)
                {
                    _logger.LogWarning("{MethodName}: Schedule already exists for doctor {DoctorId}, slot {SlotId} on {WorkDate}", methodName, request.DoctorId, request.SlotId, request.WorkDate);
                    return BaseResponse<DoctorScheduleResponse>.CreateError("Schedule already exists for this doctor, slot, and date", StatusCodes.Status400BadRequest, "SCHEDULE_DUPLICATE");
                }

                // Create schedule entity
                var schedule = new DoctorSchedule(
                    Guid.NewGuid(),
                    request.DoctorId,
                    request.SlotId,
                    request.WorkDate,
                    request.IsAvailable
                )
                {
                    Location = request.Location,
                    Notes = request.Notes
                };

                await _unitOfWork.Repository<DoctorSchedule>().InsertAsync(schedule);
                await _unitOfWork.CommitAsync();

                // Reload with related data
                var createdSchedule = await _unitOfWork.Repository<DoctorSchedule>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(ds => ds.Doctor)
                        .ThenInclude(d => d.Account)
                    .Include(ds => ds.Slot)
                        .ThenInclude(s => s.Appointments)
                    .FirstOrDefaultAsync(ds => ds.Id == schedule.Id);

				var scheduleResponse = _mapper.Map<DoctorScheduleResponse>(createdSchedule);
				scheduleResponse.TotalSlots = createdSchedule!.Slot != null ? 1 : 0;
				scheduleResponse.BookedSlots = createdSchedule.Slot != null && createdSchedule.Slot.Appointments.Any(a =>
						!a.IsDeleted &&
						a.Status != AppointmentStatus.Cancelled &&
						a.Status != AppointmentStatus.Rescheduled &&
						a.Status != AppointmentStatus.Completed &&
						a.Status != AppointmentStatus.NoShow) ? 1 : 0;
				scheduleResponse.AvailableSlots = scheduleResponse.TotalSlots - scheduleResponse.BookedSlots;

                _logger.LogInformation("{MethodName}: Successfully created schedule {ScheduleId}", methodName, schedule.Id);
                return BaseResponse<DoctorScheduleResponse>.CreateSuccess(scheduleResponse, "Schedule created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating schedule", methodName);
                return BaseResponse<DoctorScheduleResponse>.CreateError($"Error creating schedule: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Update existing doctor schedule
        /// </summary>
        public async Task<BaseResponse<DoctorScheduleResponse>> UpdateDoctorScheduleAsync(Guid scheduleId, UpdateDoctorScheduleRequest request)
        {
            const string methodName = nameof(UpdateDoctorScheduleAsync);
            _logger.LogInformation("{MethodName} called with scheduleId: {ScheduleId}, request: {@Request}", methodName, scheduleId, request);

            try
            {
                if (scheduleId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid schedule ID provided - {ScheduleId}", methodName, scheduleId);
                    return BaseResponse<DoctorScheduleResponse>.CreateError("Schedule ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_SCHEDULE_ID");
                }

                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<DoctorScheduleResponse>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                var schedule = await _unitOfWork.Repository<DoctorSchedule>()
                    .AsQueryable()
                    .Where(ds => ds.Id == scheduleId && !ds.IsDeleted)
                    .FirstOrDefaultAsync();

                if (schedule == null)
                {
                    _logger.LogWarning("{MethodName}: Schedule not found with ID: {ScheduleId}", methodName, scheduleId);
                    return BaseResponse<DoctorScheduleResponse>.CreateError("Schedule not found", StatusCodes.Status404NotFound, "SCHEDULE_NOT_FOUND");
                }

                // Verify slot exists if being updated
                if (request.SlotId.HasValue)
                {
                    var slotExists = await _unitOfWork.Repository<Slot>()
                        .AsQueryable()
                        .AnyAsync(s => s.Id == request.SlotId.Value && !s.IsDeleted);

                    if (!slotExists)
                    {
                        _logger.LogWarning("{MethodName}: Slot not found with ID: {SlotId}", methodName, request.SlotId);
                        return BaseResponse<DoctorScheduleResponse>.CreateError("Slot not found", StatusCodes.Status404NotFound, "SLOT_NOT_FOUND");
                    }
                }

                // Check for duplicate schedule if slot or date is being updated
                var workDate = request.WorkDate.HasValue ? DateOnly.FromDateTime(request.WorkDate.Value) : schedule.WorkDate;
                var slotId = request.SlotId ?? schedule.SlotId;

                var hasDuplicate = await _unitOfWork.Repository<DoctorSchedule>()
                    .AsQueryable()
                    .Where(ds => ds.DoctorId == schedule.DoctorId &&
                                 ds.Id != scheduleId &&
                                 ds.SlotId == slotId &&
                                 ds.WorkDate == workDate &&
                                 !ds.IsDeleted)
                    .AnyAsync();

                if (hasDuplicate)
                {
                    _logger.LogWarning("{MethodName}: Updated schedule would create duplicate", methodName);
                    return BaseResponse<DoctorScheduleResponse>.CreateError("Schedule already exists for this doctor, slot, and date", StatusCodes.Status400BadRequest, "SCHEDULE_DUPLICATE");
                }

                // Update schedule
                if (request.SlotId.HasValue)
                    schedule.SlotId = request.SlotId.Value;
                if (request.WorkDate.HasValue)
                    schedule.WorkDate = DateOnly.FromDateTime(request.WorkDate.Value);
                if (request.Location != null)
                    schedule.Location = request.Location;
                if (request.Notes != null)
                    schedule.Notes = request.Notes;
                if (request.IsAvailable.HasValue)
                    schedule.IsAvailable = request.IsAvailable.Value;
                schedule.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<DoctorSchedule>().UpdateGuid(schedule, scheduleId);
                await _unitOfWork.CommitAsync();

                // Reload with related data
                var updatedSchedule = await _unitOfWork.Repository<DoctorSchedule>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(ds => ds.Doctor)
                        .ThenInclude(d => d.Account)
                    .Include(ds => ds.Slot)
                        .ThenInclude(s => s.Appointments)
                    .FirstOrDefaultAsync(ds => ds.Id == scheduleId);

				var scheduleResponse = _mapper.Map<DoctorScheduleResponse>(updatedSchedule);
				scheduleResponse.TotalSlots = updatedSchedule!.Slot != null ? 1 : 0;
				scheduleResponse.BookedSlots = updatedSchedule.Slot != null && updatedSchedule.Slot.Appointments.Any(a =>
						!a.IsDeleted &&
						a.Status != AppointmentStatus.Cancelled &&
						a.Status != AppointmentStatus.Rescheduled &&
						a.Status != AppointmentStatus.Completed &&
						a.Status != AppointmentStatus.NoShow) ? 1 : 0;
				scheduleResponse.AvailableSlots = scheduleResponse.TotalSlots - scheduleResponse.BookedSlots;

                _logger.LogInformation("{MethodName}: Successfully updated schedule {ScheduleId}", methodName, scheduleId);
                return BaseResponse<DoctorScheduleResponse>.CreateSuccess(scheduleResponse, "Schedule updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating schedule {ScheduleId}", methodName, scheduleId);
                return BaseResponse<DoctorScheduleResponse>.CreateError($"Error updating schedule: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Delete doctor schedule (soft delete)
        /// </summary>
        public async Task<BaseResponse> DeleteDoctorScheduleAsync(Guid scheduleId)
        {
            const string methodName = nameof(DeleteDoctorScheduleAsync);
            _logger.LogInformation("{MethodName} called with scheduleId: {ScheduleId}", methodName, scheduleId);

            try
            {
                if (scheduleId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid schedule ID provided - {ScheduleId}", methodName, scheduleId);
                    return BaseResponse.CreateError("Schedule ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_SCHEDULE_ID");
                }

				var schedule = await _unitOfWork.Repository<DoctorSchedule>()
                    .AsQueryable()
					.Include(ds => ds.Slot)
						.ThenInclude(s => s.Appointments)
                    .Where(ds => ds.Id == scheduleId && !ds.IsDeleted)
                    .FirstOrDefaultAsync();

                if (schedule == null)
                {
                    _logger.LogWarning("{MethodName}: Schedule not found with ID: {ScheduleId}", methodName, scheduleId);
                    return BaseResponse.CreateError("Schedule not found", StatusCodes.Status404NotFound, "SCHEDULE_NOT_FOUND");
                }

				// Check if schedule has booked slots
				var hasBookedSlots = schedule.Slot != null && schedule.Slot.Appointments.Any(a =>
						!a.IsDeleted &&
						a.Status != AppointmentStatus.Cancelled &&
						a.Status != AppointmentStatus.Rescheduled &&
						a.Status != AppointmentStatus.Completed &&
						a.Status != AppointmentStatus.NoShow);
                if (hasBookedSlots)
                {
                    _logger.LogWarning("{MethodName}: Cannot delete schedule with booked slots: {ScheduleId}", methodName, scheduleId);
                    return BaseResponse.CreateError("Cannot delete schedule with booked slots", StatusCodes.Status400BadRequest, "SCHEDULE_HAS_BOOKED_SLOTS");
                }

                // Soft delete
                schedule.IsDeleted = true;
                schedule.DeletedAt = DateTime.UtcNow;
                schedule.UpdatedAt = DateTime.UtcNow;

                // Note: Slots are global and not deleted when a schedule is deleted

                await _unitOfWork.Repository<DoctorSchedule>().UpdateGuid(schedule, scheduleId);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully deleted schedule {ScheduleId}", methodName, scheduleId);
                return BaseResponse.CreateSuccess("Schedule deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error deleting schedule {ScheduleId}", methodName, scheduleId);
                return BaseResponse.CreateError($"Error deleting schedule: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Update schedule availability
        /// </summary>
        public async Task<BaseResponse> UpdateScheduleAvailabilityAsync(Guid scheduleId, bool isAvailable)
        {
            const string methodName = nameof(UpdateScheduleAvailabilityAsync);
            _logger.LogInformation("{MethodName} called with scheduleId: {ScheduleId}, isAvailable: {IsAvailable}", methodName, scheduleId, isAvailable);

            try
            {
                if (scheduleId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid schedule ID provided - {ScheduleId}", methodName, scheduleId);
                    return BaseResponse.CreateError("Schedule ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_SCHEDULE_ID");
                }

                var schedule = await _unitOfWork.Repository<DoctorSchedule>()
                    .AsQueryable()
                    .Where(ds => ds.Id == scheduleId && !ds.IsDeleted)
                    .FirstOrDefaultAsync();

                if (schedule == null)
                {
                    _logger.LogWarning("{MethodName}: Schedule not found with ID: {ScheduleId}", methodName, scheduleId);
                    return BaseResponse.CreateError("Schedule not found", StatusCodes.Status404NotFound, "SCHEDULE_NOT_FOUND");
                }

                schedule.IsAvailable = isAvailable;
                schedule.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<DoctorSchedule>().UpdateGuid(schedule, scheduleId);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully updated schedule availability {ScheduleId}", methodName, scheduleId);
                return BaseResponse.CreateSuccess($"Schedule availability updated to {(isAvailable ? "available" : "unavailable")}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating schedule availability {ScheduleId}", methodName, scheduleId);
                return BaseResponse.CreateError($"Error updating schedule availability: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Get busy schedule dates for a doctor
        /// </summary>
        public async Task<BaseResponse<BusyScheduleDateResponse>> GetBusyScheduleDateAsync(GetBusyScheduleDateRequest request)
        {
            const string methodName = nameof(GetBusyScheduleDateAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<BusyScheduleDateResponse>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                if (request.DoctorId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid doctor ID provided - {DoctorId}", methodName, request.DoctorId);
                    return BaseResponse<BusyScheduleDateResponse>.CreateError("Doctor ID cannot be empty or invalid", StatusCodes.Status400BadRequest, "INVALID_DOCTOR_ID");
                }

                // Validate date range if both dates are provided
                if (request.FromDate.HasValue && request.ToDate.HasValue)
                {
                    if (request.FromDate.Value > request.ToDate.Value)
                    {
                        _logger.LogWarning("{MethodName}: FromDate cannot be greater than ToDate", methodName);
                        return BaseResponse<BusyScheduleDateResponse>.CreateError("FromDate cannot be greater than ToDate", StatusCodes.Status400BadRequest, "INVALID_DATE_RANGE");
                    }
                }

                // Verify doctor exists
                var doctorExists = await DoctorExistsAsync(request.DoctorId);
                if (!doctorExists)
                {
                    _logger.LogWarning("{MethodName}: Doctor not found with ID: {DoctorId}", methodName, request.DoctorId);
                    return BaseResponse<BusyScheduleDateResponse>.CreateError("Doctor not found", StatusCodes.Status404NotFound, "DOCTOR_NOT_FOUND");
                }

                // Build query to get distinct work dates
                var query = _unitOfWork.Repository<DoctorSchedule>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(ds => ds.DoctorId == request.DoctorId && !ds.IsDeleted);

                // Apply date filters based on request
                if (request.FromDate.HasValue && request.ToDate.HasValue)
                {
                    // Both dates provided: filter by date range
                    var fromDate = DateOnly.FromDateTime(request.FromDate.Value.Date);
                    var toDate = DateOnly.FromDateTime(request.ToDate.Value.Date);
                    query = query.Where(ds => ds.WorkDate >= fromDate && ds.WorkDate <= toDate);
                }
                else if (request.FromDate.HasValue)
                {
                    // Only FromDate provided: get all dates from FromDate onwards
                    var fromDate = DateOnly.FromDateTime(request.FromDate.Value.Date);
                    query = query.Where(ds => ds.WorkDate >= fromDate);
                }
                // If both are null, get all work dates (no additional filter)

                // Get distinct work dates
                var workDates = await query
                    .Select(ds => ds.WorkDate)
                    .Distinct()
                    .OrderBy(d => d)
                    .ToListAsync();

                // Convert DateOnly to DateTime for response
                var workDateTimes = workDates
                    .Select(d => d.ToDateTime(TimeOnly.MinValue))
                    .ToList();

                var response = new BusyScheduleDateResponse
                {
                    DoctorId = request.DoctorId,
                    WorkDates = workDateTimes
                };

                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} work dates for doctor {DoctorId}", methodName, workDateTimes.Count, request.DoctorId);

                return BaseResponse<BusyScheduleDateResponse>.CreateSuccess(response, "Busy schedule dates retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving busy schedule dates", methodName);
                return BaseResponse<BusyScheduleDateResponse>.CreateError($"Error retrieving busy schedule dates: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        #endregion

        #region Slot CRUD Operations

        /// <summary>
        /// Get slot by ID
        /// </summary>
        public async Task<BaseResponse<SlotResponse>> GetSlotByIdAsync(Guid slotId)
        {
            const string methodName = nameof(GetSlotByIdAsync);
            _logger.LogInformation("{MethodName} called with slotId: {SlotId}", methodName, slotId);

            try
            {
                if (slotId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid slot ID provided - {SlotId}", methodName, slotId);
                    return BaseResponse<SlotResponse>.CreateError("Slot ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_SLOT_ID");
                }

				var slot = await _unitOfWork.Repository<Slot>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(s => s.DoctorSchedules)
                        .ThenInclude(ds => ds.Doctor)
                            .ThenInclude(d => d.Account)
					.Include(s => s.Appointments)
                    .Where(s => s.Id == slotId && !s.IsDeleted)
                    .FirstOrDefaultAsync();

                if (slot == null)
                {
                    _logger.LogWarning("{MethodName}: Slot not found with ID: {SlotId}", methodName, slotId);
                    return BaseResponse<SlotResponse>.CreateError("Slot not found", StatusCodes.Status404NotFound, "SLOT_NOT_FOUND");
                }

                var slotResponse = _mapper.Map<SlotResponse>(slot);
                _logger.LogInformation("{MethodName}: Successfully retrieved slot {SlotId}", methodName, slotId);
                return BaseResponse<SlotResponse>.CreateSuccess(slotResponse, "Slot retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving slot {SlotId}", methodName, slotId);
                return BaseResponse<SlotResponse>.CreateError($"Error retrieving slot: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Get detailed slot by ID with appointment information
        /// </summary>
        public async Task<BaseResponse<SlotDetailResponse>> GetSlotDetailByIdAsync(Guid slotId)
        {
            const string methodName = nameof(GetSlotDetailByIdAsync);
            _logger.LogInformation("{MethodName} called with slotId: {SlotId}", methodName, slotId);

            try
            {
                if (slotId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid slot ID provided - {SlotId}", methodName, slotId);
                    return BaseResponse<SlotDetailResponse>.CreateError("Slot ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_SLOT_ID");
                }

				var slot = await _unitOfWork.Repository<Slot>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(s => s.DoctorSchedules)
                        .ThenInclude(ds => ds.Doctor)
                            .ThenInclude(d => d.Account)
					.Include(s => s.Appointments)
					.Include(s => s.Appointments)
                        .ThenInclude(a => a.TreatmentCycle)
                            .ThenInclude(tc => tc.Treatment)
                                .ThenInclude(t => t.Patient)
                                    .ThenInclude(p => p.Account)
                    .Where(s => s.Id == slotId && !s.IsDeleted)
                    .FirstOrDefaultAsync();

                if (slot == null)
                {
                    _logger.LogWarning("{MethodName}: Slot not found with ID: {SlotId}", methodName, slotId);
                    return BaseResponse<SlotDetailResponse>.CreateError("Slot not found", StatusCodes.Status404NotFound, "SLOT_NOT_FOUND");
                }

                var slotDetailResponse = _mapper.Map<SlotDetailResponse>(slot);
                _logger.LogInformation("{MethodName}: Successfully retrieved slot details {SlotId}", methodName, slotId);
                return BaseResponse<SlotDetailResponse>.CreateSuccess(slotDetailResponse, "Slot details retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving slot details {SlotId}", methodName, slotId);
                return BaseResponse<SlotDetailResponse>.CreateError($"Error retrieving slot details: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Get all slots with pagination and filtering
        /// </summary>
        public async Task<DynamicResponse<SlotResponse>> GetAllSlotsAsync(GetSlotsRequest request)
        {
            const string methodName = nameof(GetAllSlotsAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                if (request == null)
                    request = new GetSlotsRequest();

                request.Normalize();

				var query = _unitOfWork.Repository<Slot>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(s => s.DoctorSchedules)
                        .ThenInclude(ds => ds.Doctor)
                            .ThenInclude(d => d.Account)
					.Include(s => s.Appointments)
                    .Where(s => !s.IsDeleted);

                // Apply filters
                if (request.DoctorScheduleId.HasValue)
                {
                    query = query.Where(s => s.DoctorSchedules.Any(ds => ds.Id == request.DoctorScheduleId.Value));
                }

                if (request.DoctorId.HasValue)
                {
                    query = query.Where(s => s.DoctorSchedules.Any(ds => ds.DoctorId == request.DoctorId.Value));
                }

				if (request.IsBooked.HasValue)
				{
					if (request.IsBooked.Value)
						query = query.Where(s => s.Appointments != null);
					else
						query = query.Where(s => s.Appointments == null);
				}

                if (request.DateFrom.HasValue || request.DateTo.HasValue)
                {
                    if (request.DateFrom.HasValue)
                    {
                        var from = DateOnly.FromDateTime(request.DateFrom.Value);
                        query = query.Where(s => s.DoctorSchedules.Any(ds => ds.WorkDate >= from));
                    }
                    if (request.DateTo.HasValue)
                    {
                        var to = DateOnly.FromDateTime(request.DateTo.Value);
                        query = query.Where(s => s.DoctorSchedules.Any(ds => ds.WorkDate <= to));
                    }
                }

                if (request.TimeFrom.HasValue)
                {
                    query = query.Where(s => s.StartTime >= request.TimeFrom.Value);
                }

                if (request.TimeTo.HasValue)
                {
                    query = query.Where(s => s.EndTime <= request.TimeTo.Value);
                }

                // Get total count
                var totalCount = await query.CountAsync();

                // Apply sorting
                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    var isDescending = request.Order?.ToLower() == "desc";
                    query = request.Sort.ToLower() switch
                    {
                        "starttime" => isDescending ? query.OrderByDescending(s => s.StartTime) : query.OrderBy(s => s.StartTime),
                        "endtime" => isDescending ? query.OrderByDescending(s => s.EndTime) : query.OrderBy(s => s.EndTime),
                        "createdat" => isDescending ? query.OrderByDescending(s => s.CreatedAt) : query.OrderBy(s => s.CreatedAt),
                        _ => isDescending ? query.OrderByDescending(s => s.CreatedAt) : query.OrderBy(s => s.CreatedAt)
                    };
                }
                else
                {
                    query = query.OrderBy(s => s.DoctorSchedules.Any() ? s.DoctorSchedules.Min(ds => ds.WorkDate) : DateOnly.MinValue)
                                 .ThenBy(s => s.StartTime);
                }

                // Apply pagination
                var slots = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                var slotResponses = _mapper.Map<List<SlotResponse>>(slots);

                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} slots", methodName, slotResponses.Count);

                return new DynamicResponse<SlotResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Slots retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = totalCount,
                        CurrentPageSize = slotResponses.Count
                    },
                    Data = slotResponses
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving slots", methodName);
                return new DynamicResponse<SlotResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = $"Error retrieving slots: {ex.Message}",
                    MetaData = new PagingMetaData(),
                    Data = new List<SlotResponse>()
                };
            }
        }

        /// <summary>
        /// Get slots for a specific doctor schedule
        /// </summary>
        public async Task<DynamicResponse<SlotResponse>> GetSlotsByScheduleIdAsync(Guid scheduleId, GetSlotsRequest request)
        {
            const string methodName = nameof(GetSlotsByScheduleIdAsync);
            _logger.LogInformation("{MethodName} called with scheduleId: {ScheduleId}, request: {@Request}", methodName, scheduleId, request);

            try
            {
                if (scheduleId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid schedule ID provided - {ScheduleId}", methodName, scheduleId);
                    return new DynamicResponse<SlotResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_SCHEDULE_ID",
                        Message = "Schedule ID cannot be empty",
                        MetaData = new PagingMetaData(),
                        Data = new List<SlotResponse>()
                    };
                }

                // Verify schedule exists
                var scheduleExists = await _unitOfWork.Repository<DoctorSchedule>()
                    .AsQueryable()
                    .AnyAsync(ds => ds.Id == scheduleId && !ds.IsDeleted);

                if (!scheduleExists)
                {
                    _logger.LogWarning("{MethodName}: Schedule not found with ID: {ScheduleId}", methodName, scheduleId);
                    return new DynamicResponse<SlotResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "SCHEDULE_NOT_FOUND",
                        Message = "Schedule not found",
                        MetaData = new PagingMetaData(),
                        Data = new List<SlotResponse>()
                    };
                }

                if (request == null)
                    request = new GetSlotsRequest();

                request.Normalize();
                request.DoctorScheduleId = scheduleId; // Override to ensure filtering by schedule

                return await GetAllSlotsAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving slots for schedule {ScheduleId}", methodName, scheduleId);
                return new DynamicResponse<SlotResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = $"Error retrieving slots: {ex.Message}",
                    MetaData = new PagingMetaData(),
                    Data = new List<SlotResponse>()
                };
            }
        }

        /// <summary>
        /// Get available slots for a specific doctor and date range
        /// </summary>
        public async Task<DynamicResponse<SlotResponse>> GetAvailableSlotsAsync(Guid doctorId, DateTime dateFrom, DateTime dateTo)
        {
            const string methodName = nameof(GetAvailableSlotsAsync);
            _logger.LogInformation("{MethodName} called with doctorId: {DoctorId}, dateFrom: {DateFrom}, dateTo: {DateTo}", methodName, doctorId, dateFrom, dateTo);

            try
            {
                if (doctorId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid doctor ID provided - {DoctorId}", methodName, doctorId);
                    return new DynamicResponse<SlotResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_DOCTOR_ID",
                        Message = "Doctor ID cannot be empty",
                        MetaData = new PagingMetaData(),
                        Data = new List<SlotResponse>()
                    };
                }

                if (dateFrom > dateTo)
                {
                    _logger.LogWarning("{MethodName}: DateFrom must be before or equal to DateTo", methodName);
                    return new DynamicResponse<SlotResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_DATE_RANGE",
                        Message = "DateFrom must be before or equal to DateTo",
                        MetaData = new PagingMetaData(),
                        Data = new List<SlotResponse>()
                    };
                }

                // Verify doctor exists
                var doctorExists = await DoctorExistsAsync(doctorId);
                if (!doctorExists)
                {
                    _logger.LogWarning("{MethodName}: Doctor not found with ID: {DoctorId}", methodName, doctorId);
                    return new DynamicResponse<SlotResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "DOCTOR_NOT_FOUND",
                        Message = "Doctor not found",
                        MetaData = new PagingMetaData(),
                        Data = new List<SlotResponse>()
                    };
                }

				var slots = await _unitOfWork.Repository<Slot>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(s => s.DoctorSchedules)
                        .ThenInclude(ds => ds.Doctor)
                            .ThenInclude(d => d.Account)
					.Include(s => s.Appointments)
					.Where(s => !s.IsDeleted &&
							   s.Appointments == null &&
                               s.DoctorSchedules.Any(ds => ds.DoctorId == doctorId &&
                                                           ds.WorkDate >= DateOnly.FromDateTime(dateFrom) &&
                                                           ds.WorkDate <= DateOnly.FromDateTime(dateTo) &&
                                                           ds.IsAvailable &&
                                                           !ds.IsDeleted))
                    .OrderBy(s => s.DoctorSchedules.Min(ds => ds.WorkDate))
                    .ThenBy(s => s.StartTime)
                    .ToListAsync();

                var slotResponses = _mapper.Map<List<SlotResponse>>(slots);

                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} available slots", methodName, slotResponses.Count);

                return new DynamicResponse<SlotResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Available slots retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = 1,
                        Size = slotResponses.Count,
                        Total = slotResponses.Count,
                        CurrentPageSize = slotResponses.Count
                    },
                    Data = slotResponses
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving available slots", methodName);
                return new DynamicResponse<SlotResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = $"Error retrieving available slots: {ex.Message}",
                    MetaData = new PagingMetaData(),
                    Data = new List<SlotResponse>()
                };
            }
        }

        /// <summary>
        /// Create new slot
        /// </summary>
        public async Task<BaseResponse<SlotResponse>> CreateSlotAsync(CreateSlotRequest request)
        {
            const string methodName = nameof(CreateSlotAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<SlotResponse>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                // Validate time
                if (request.StartTime >= request.EndTime)
                {
                    _logger.LogWarning("{MethodName}: Start time must be before end time", methodName);
                    return BaseResponse<SlotResponse>.CreateError("Start time must be before end time", StatusCodes.Status400BadRequest, "INVALID_TIME_RANGE");
                }

                // With fixed global slots, creation per schedule is not supported
                _logger.LogWarning("{MethodName}: Creating slots per schedule is not supported with fixed Slot design", methodName);
                return BaseResponse<SlotResponse>.CreateError("Creating slots per schedule is not supported.", StatusCodes.Status400BadRequest, "SLOT_CREATION_NOT_SUPPORTED");

                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating slot", methodName);
                return BaseResponse<SlotResponse>.CreateError($"Error creating slot: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Create multiple slots for a schedule
        /// </summary>
        public async Task<BaseResponse<int>> CreateSlotsForScheduleAsync(Guid scheduleId, int slotDuration = 30)
        {
            const string methodName = nameof(CreateSlotsForScheduleAsync);
            _logger.LogInformation("{MethodName} called with scheduleId: {ScheduleId}, slotDuration: {SlotDuration}", methodName, scheduleId, slotDuration);

            // Not supported with fixed global slots
            _logger.LogWarning("{MethodName}: Creating generated slots per schedule is not supported", methodName);
            return BaseResponse<int>.CreateError("Auto-generating slots per schedule is not supported.", StatusCodes.Status400BadRequest, "SLOT_GENERATION_NOT_SUPPORTED");
        }

        /// <summary>
        /// Update existing slot
        /// </summary>
        public async Task<BaseResponse<SlotResponse>> UpdateSlotAsync(Guid slotId, UpdateSlotRequest request)
        {
            const string methodName = nameof(UpdateSlotAsync);
            _logger.LogInformation("{MethodName} called with slotId: {SlotId}, request: {@Request}", methodName, slotId, request);

            try
            {
                if (slotId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid slot ID provided - {SlotId}", methodName, slotId);
                    return BaseResponse<SlotResponse>.CreateError("Slot ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_SLOT_ID");
                }

                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<SlotResponse>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                var slot = await _unitOfWork.Repository<Slot>()
                    .AsQueryable()
                    .Include(s => s.DoctorSchedules)
                    .Where(s => s.Id == slotId && !s.IsDeleted)
                    .FirstOrDefaultAsync();

                if (slot == null)
                {
                    _logger.LogWarning("{MethodName}: Slot not found with ID: {SlotId}", methodName, slotId);
                    return BaseResponse<SlotResponse>.CreateError("Slot not found", StatusCodes.Status404NotFound, "SLOT_NOT_FOUND");
                }

				// Check if slot has an appointment and trying to change time
				if (slot.Appointments != null && (request.StartTime.HasValue || request.EndTime.HasValue))
                {
                    _logger.LogWarning("{MethodName}: Cannot modify time of booked slot: {SlotId}", methodName, slotId);
                    return BaseResponse<SlotResponse>.CreateError("Cannot modify time of booked slot", StatusCodes.Status400BadRequest, "SLOT_IS_BOOKED");
                }

                // Validate time if both times are being updated
                if (request.StartTime.HasValue && request.EndTime.HasValue)
                {
                    if (request.StartTime.Value >= request.EndTime.Value)
                    {
                        _logger.LogWarning("{MethodName}: Start time must be before end time", methodName);
                        return BaseResponse<SlotResponse>.CreateError("Start time must be before end time", StatusCodes.Status400BadRequest, "INVALID_TIME_RANGE");
                    }
                }
                else if (request.StartTime.HasValue && request.StartTime.Value >= slot.EndTime)
                {
                    _logger.LogWarning("{MethodName}: Start time must be before end time", methodName);
                    return BaseResponse<SlotResponse>.CreateError("Start time must be before end time", StatusCodes.Status400BadRequest, "INVALID_TIME_RANGE");
                }
                else if (request.EndTime.HasValue && slot.StartTime >= request.EndTime.Value)
                {
                    _logger.LogWarning("{MethodName}: Start time must be before end time", methodName);
                    return BaseResponse<SlotResponse>.CreateError("Start time must be before end time", StatusCodes.Status400BadRequest, "INVALID_TIME_RANGE");
                }

                // Check for overlapping slots globally if time is being updated
                if (request.StartTime.HasValue || request.EndTime.HasValue)
                {
                    var startTime = request.StartTime ?? slot.StartTime;
                    var endTime = request.EndTime ?? slot.EndTime;

                    var hasOverlap = await _unitOfWork.Repository<Slot>()
                        .AsQueryable()
                        .Where(s => s.Id != slotId &&
                                   !s.IsDeleted &&
                                   ((s.StartTime < endTime && s.EndTime > startTime)))
                        .AnyAsync();

                    if (hasOverlap)
                    {
                        _logger.LogWarning("{MethodName}: Updated slot overlaps with existing slot", methodName);
                        return BaseResponse<SlotResponse>.CreateError("Slot overlaps with an existing slot", StatusCodes.Status400BadRequest, "SLOT_OVERLAP");
                    }
                }

                // Update slot
                if (request.StartTime.HasValue)
                    slot.StartTime = request.StartTime.Value;
                if (request.EndTime.HasValue)
                    slot.EndTime = request.EndTime.Value;
				// Booking status is inferred from Appointment, cannot be set directly
                if (request.Notes != null)
                    slot.Notes = request.Notes;

                slot.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Slot>().UpdateGuid(slot, slotId);
                await _unitOfWork.CommitAsync();

                // Reload with related data
                var updatedSlot = await _unitOfWork.Repository<Slot>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(s => s.DoctorSchedules)
                        .ThenInclude(ds => ds.Doctor)
                            .ThenInclude(d => d.Account)
                    .FirstOrDefaultAsync(s => s.Id == slotId);

                var slotResponse = _mapper.Map<SlotResponse>(updatedSlot);
                _logger.LogInformation("{MethodName}: Successfully updated slot {SlotId}", methodName, slotId);
                return BaseResponse<SlotResponse>.CreateSuccess(slotResponse, "Slot updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating slot {SlotId}", methodName, slotId);
                return BaseResponse<SlotResponse>.CreateError($"Error updating slot: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Delete slot (soft delete)
        /// </summary>
        public async Task<BaseResponse> DeleteSlotAsync(Guid slotId)
        {
            const string methodName = nameof(DeleteSlotAsync);
            _logger.LogInformation("{MethodName} called with slotId: {SlotId}", methodName, slotId);

            try
            {
                if (slotId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid slot ID provided - {SlotId}", methodName, slotId);
                    return BaseResponse.CreateError("Slot ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_SLOT_ID");
                }

				var slot = await _unitOfWork.Repository<Slot>()
                    .AsQueryable()
                    .Where(s => s.Id == slotId && !s.IsDeleted)
                    .FirstOrDefaultAsync();

                if (slot == null)
                {
                    _logger.LogWarning("{MethodName}: Slot not found with ID: {SlotId}", methodName, slotId);
                    return BaseResponse.CreateError("Slot not found", StatusCodes.Status404NotFound, "SLOT_NOT_FOUND");
                }

				// Check if slot has an appointment
				if (slot.Appointments != null)
                {
                    _logger.LogWarning("{MethodName}: Cannot delete booked slot: {SlotId}", methodName, slotId);
                    return BaseResponse.CreateError("Cannot delete booked slot", StatusCodes.Status400BadRequest, "SLOT_IS_BOOKED");
                }

                // Soft delete
                slot.IsDeleted = true;
                slot.DeletedAt = DateTime.UtcNow;
                slot.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Slot>().UpdateGuid(slot, slotId);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully deleted slot {SlotId}", methodName, slotId);
                return BaseResponse.CreateSuccess("Slot deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error deleting slot {SlotId}", methodName, slotId);
                return BaseResponse.CreateError($"Error deleting slot: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Update slot booking status
        /// </summary>
        public async Task<BaseResponse> UpdateSlotBookingStatusAsync(Guid slotId, bool isBooked)
        {
            const string methodName = nameof(UpdateSlotBookingStatusAsync);
            _logger.LogInformation("{MethodName} called with slotId: {SlotId}, isBooked: {IsBooked}", methodName, slotId, isBooked);

            try
            {
                if (slotId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid slot ID provided - {SlotId}", methodName, slotId);
                    return BaseResponse.CreateError("Slot ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_SLOT_ID");
                }

				var slot = await _unitOfWork.Repository<Slot>()
                    .AsQueryable()
                    .Where(s => s.Id == slotId && !s.IsDeleted)
                    .FirstOrDefaultAsync();

                if (slot == null)
                {
                    _logger.LogWarning("{MethodName}: Slot not found with ID: {SlotId}", methodName, slotId);
                    return BaseResponse.CreateError("Slot not found", StatusCodes.Status404NotFound, "SLOT_NOT_FOUND");
                }

				// Booking status is inferred by Appointment and cannot be updated directly
				_logger.LogWarning("{MethodName}: Updating slot booking status is not supported for fixed slots", methodName);
				return BaseResponse.CreateError("Updating slot booking status is not supported.", StatusCodes.Status400BadRequest, "SLOT_BOOKING_STATUS_NOT_SUPPORTED");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating slot booking status {SlotId}", methodName, slotId);
                return BaseResponse.CreateError($"Error updating slot booking status: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        #endregion
    }
}
