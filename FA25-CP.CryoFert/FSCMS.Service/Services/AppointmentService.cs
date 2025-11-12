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
using System.Threading.Tasks;

namespace FSCMS.Service.Services
{
    /// <summary>
    /// Service for managing appointments
    /// </summary>
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AppointmentService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Appointment CRUD Operations

        /// <summary>
        /// Get appointment by ID
        /// </summary>
        public async Task<BaseResponse<AppointmentResponse>> GetAppointmentByIdAsync(Guid appointmentId)
        {
            const string methodName = nameof(GetAppointmentByIdAsync);
            _logger.LogInformation("{MethodName} called with appointmentId: {AppointmentId}", methodName, appointmentId);

            try
            {
                if (appointmentId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid appointment ID provided - {AppointmentId}", methodName, appointmentId);
                    return BaseResponse<AppointmentResponse>.CreateError("Appointment ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_APPOINTMENT_ID");
                }

                var appointment = await _unitOfWork.Repository<Appointment>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(a => a.Patient)
                        .ThenInclude(p => p.Account)
                    .Include(a => a.TreatmentCycle)
                        .ThenInclude(tc => tc.Treatment)
                            .ThenInclude(t => t.Patient)
                                .ThenInclude(p => p.Account)
                    .Include(a => a.Slot)
                        .ThenInclude(s => s.DoctorSchedules)
                            .ThenInclude(ds => ds.Doctor)
                                .ThenInclude(d => d.Account)
                    .Include(a => a.AppointmentDoctors.Where(ad => !ad.IsDeleted))
                        .ThenInclude(ad => ad.Doctor)
                            .ThenInclude(d => d.Account)
                    .Where(a => a.Id == appointmentId && !a.IsDeleted)
                    .FirstOrDefaultAsync();

                if (appointment == null)
                {
                    _logger.LogWarning("{MethodName}: Appointment not found with ID: {AppointmentId}", methodName, appointmentId);
                    return BaseResponse<AppointmentResponse>.CreateError("Appointment not found", StatusCodes.Status404NotFound, "APPOINTMENT_NOT_FOUND");
                }

                var appointmentResponse = MapToAppointmentResponse(appointment);
                _logger.LogInformation("{MethodName}: Successfully retrieved appointment {AppointmentId}", methodName, appointmentId);
                return BaseResponse<AppointmentResponse>.CreateSuccess(appointmentResponse, "Appointment retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving appointment {AppointmentId}", methodName, appointmentId);
                return BaseResponse<AppointmentResponse>.CreateError($"Error retrieving appointment: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Get detailed appointment by ID with related data
        /// </summary>
        public async Task<BaseResponse<AppointmentDetailResponse>> GetAppointmentDetailByIdAsync(Guid appointmentId)
        {
            const string methodName = nameof(GetAppointmentDetailByIdAsync);
            _logger.LogInformation("{MethodName} called with appointmentId: {AppointmentId}", methodName, appointmentId);

            try
            {
                if (appointmentId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid appointment ID provided - {AppointmentId}", methodName, appointmentId);
                    return BaseResponse<AppointmentDetailResponse>.CreateError("Appointment ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_APPOINTMENT_ID");
                }

                var appointment = await _unitOfWork.Repository<Appointment>()
                    .AsQueryable()
                    .AsNoTrackingWithIdentityResolution()
                    .Include(a => a.Patient)
                        .ThenInclude(p => p.Account)
                    .Include(a => a.TreatmentCycle)
                        .ThenInclude(tc => tc.Treatment)
                            .ThenInclude(t => t.Patient)
                                .ThenInclude(p => p.Account)
                    .Include(a => a.Slot)
                        .ThenInclude(s => s.DoctorSchedules)
                            .ThenInclude(ds => ds.Doctor)
                                .ThenInclude(d => d.Account)
                    .Include(a => a.AppointmentDoctors.Where(ad => !ad.IsDeleted))
                        .ThenInclude(ad => ad.Doctor)
                            .ThenInclude(d => d.Account)
                    .Include(a => a.MedicalRecord)
                    .Include(a => a.TreatmentCycle)
                        .ThenInclude(tc => tc.Appointments.Where(ap => !ap.IsDeleted))
                    .Where(a => a.Id == appointmentId && !a.IsDeleted)
                    .FirstOrDefaultAsync();

                if (appointment == null)
                {
                    _logger.LogWarning("{MethodName}: Appointment not found with ID: {AppointmentId}", methodName, appointmentId);
                    return BaseResponse<AppointmentDetailResponse>.CreateError("Appointment not found", StatusCodes.Status404NotFound, "APPOINTMENT_NOT_FOUND");
                }

                var appointmentDetailResponse = MapToAppointmentDetailResponse(appointment);

                // Load service requests
                var serviceRequests = await _unitOfWork.Repository<ServiceRequest>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(sr => sr.AppointmentId == appointmentId && !sr.IsDeleted)
                    .ToListAsync();

                appointmentDetailResponse.ServiceRequests = serviceRequests.Select(sr => new AppointmentServiceRequestInfo
                {
                    Id = sr.Id,
                    RequestDate = sr.RequestDate,
                    Status = sr.Status.ToString(),
                    TotalAmount = sr.TotalAmount
                }).ToList();

                _logger.LogInformation("{MethodName}: Successfully retrieved appointment details {AppointmentId}", methodName, appointmentId);
                return BaseResponse<AppointmentDetailResponse>.CreateSuccess(appointmentDetailResponse, "Appointment details retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving appointment details {AppointmentId}", methodName, appointmentId);
                return BaseResponse<AppointmentDetailResponse>.CreateError($"Error retrieving appointment details: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Get all appointments with pagination and filtering
        /// </summary>
        public async Task<DynamicResponse<AppointmentResponse>> GetAllAppointmentsAsync(GetAppointmentsRequest request)
        {
            const string methodName = nameof(GetAllAppointmentsAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                if (request == null)
                    request = new GetAppointmentsRequest();

                request.Normalize();

                var query = _unitOfWork.Repository<Appointment>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(a => a.Patient)
                        .ThenInclude(p => p.Account)
                    .Include(a => a.TreatmentCycle)
                        .ThenInclude(tc => tc.Treatment)
                            .ThenInclude(t => t.Patient)
                                .ThenInclude(p => p.Account)
                    .Include(a => a.Slot)
                        .ThenInclude(s => s.DoctorSchedules)
                            .ThenInclude(ds => ds.Doctor)
                                .ThenInclude(d => d.Account)
                    .Include(a => a.AppointmentDoctors.Where(ad => !ad.IsDeleted))
                        .ThenInclude(ad => ad.Doctor)
                            .ThenInclude(d => d.Account)
                    .Where(a => !a.IsDeleted);

                // Apply filters
                if (request.TreatmentCycleId.HasValue)
                {
                    query = query.Where(a => a.TreatmentCycleId == request.TreatmentCycleId.Value);
                }

                if (request.DoctorId.HasValue)
                {
                    query = query.Where(a => a.AppointmentDoctors.Any(ad => ad.DoctorId == request.DoctorId.Value && !ad.IsDeleted));
                }

                if (request.SlotId.HasValue)
                {
                    query = query.Where(a => a.SlotId == request.SlotId.Value);
                }

                if (request.Type.HasValue)
                {
                    query = query.Where(a => a.Type == request.Type.Value);
                }

                if (request.Status.HasValue)
                {
                    query = query.Where(a => a.Status == request.Status.Value);
                }

                if (request.AppointmentDateFrom.HasValue)
                {
                    query = query.Where(a => a.AppointmentDate >= request.AppointmentDateFrom.Value);
                }

                if (request.AppointmentDateTo.HasValue)
                {
                    query = query.Where(a => a.AppointmentDate <= request.AppointmentDateTo.Value);
                }

                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    var searchTerm = request.SearchTerm.Trim().ToLowerInvariant();
                    query = query.Where(a =>
                        (a.Reason != null && a.Reason.ToLower().Contains(searchTerm)) ||
                        (a.Notes != null && a.Notes.ToLower().Contains(searchTerm)) ||
                        (a.TreatmentCycle != null && a.TreatmentCycle.CycleName.ToLower().Contains(searchTerm)) ||
                        // Search in Patient directly (for Booking appointments without TreatmentCycle)
                        (a.Patient != null && a.Patient.Account != null &&
                         (a.Patient.Account.FirstName.ToLower().Contains(searchTerm) ||
                          a.Patient.Account.LastName.ToLower().Contains(searchTerm))) ||
                        // Search in Patient through TreatmentCycle (for appointments with TreatmentCycle)
                        (a.TreatmentCycle != null && a.TreatmentCycle.Treatment != null &&
                         a.TreatmentCycle.Treatment.Patient != null &&
                         a.TreatmentCycle.Treatment.Patient.Account != null &&
                         (a.TreatmentCycle.Treatment.Patient.Account.FirstName.ToLower().Contains(searchTerm) ||
                          a.TreatmentCycle.Treatment.Patient.Account.LastName.ToLower().Contains(searchTerm))));
                }

                // Get total count
                var totalCount = await query.CountAsync();

                // Apply sorting
                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    var isDescending = request.Order?.ToLower() == "desc";
                    query = request.Sort.ToLower() switch
                    {
                        "appointmentdate" => isDescending ? query.OrderByDescending(a => a.AppointmentDate) : query.OrderBy(a => a.AppointmentDate),
                        "type" => isDescending ? query.OrderByDescending(a => a.Type) : query.OrderBy(a => a.Type),
                        "status" => isDescending ? query.OrderByDescending(a => a.Status) : query.OrderBy(a => a.Status),
                        "createdat" => isDescending ? query.OrderByDescending(a => a.CreatedAt) : query.OrderBy(a => a.CreatedAt),
                        _ => isDescending ? query.OrderByDescending(a => a.CreatedAt) : query.OrderBy(a => a.CreatedAt)
                    };
                }
                else
                {
                    query = query.OrderByDescending(a => a.AppointmentDate);
                }

                // Apply pagination
                var appointments = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                var appointmentResponses = appointments.Select(MapToAppointmentResponse).ToList();

                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} appointments", methodName, appointmentResponses.Count);

                return new DynamicResponse<AppointmentResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Appointments retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = totalCount,
                        CurrentPageSize = appointmentResponses.Count
                    },
                    Data = appointmentResponses
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving appointments", methodName);
                return new DynamicResponse<AppointmentResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = $"Error retrieving appointments: {ex.Message}",
                    MetaData = new PagingMetaData(),
                    Data = new List<AppointmentResponse>()
                };
            }
        }

        /// <summary>
        /// Get appointments for a specific treatment cycle
        /// </summary>
        public async Task<DynamicResponse<AppointmentResponse>> GetAppointmentsByTreatmentCycleIdAsync(Guid treatmentCycleId, GetAppointmentsRequest request)
        {
            const string methodName = nameof(GetAppointmentsByTreatmentCycleIdAsync);
            _logger.LogInformation("{MethodName} called with treatmentCycleId: {TreatmentCycleId}, request: {@Request}", methodName, treatmentCycleId, request);

            try
            {
                if (treatmentCycleId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid treatment cycle ID provided - {TreatmentCycleId}", methodName, treatmentCycleId);
                    return new DynamicResponse<AppointmentResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_TREATMENT_CYCLE_ID",
                        Message = "Treatment cycle ID cannot be empty",
                        MetaData = new PagingMetaData(),
                        Data = new List<AppointmentResponse>()
                    };
                }

                // Verify treatment cycle exists
                var treatmentCycleExists = await _unitOfWork.Repository<TreatmentCycle>()
                    .AsQueryable()
                    .AnyAsync(tc => tc.Id == treatmentCycleId && !tc.IsDeleted);

                if (!treatmentCycleExists)
                {
                    _logger.LogWarning("{MethodName}: Treatment cycle not found with ID: {TreatmentCycleId}", methodName, treatmentCycleId);
                    return new DynamicResponse<AppointmentResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "TREATMENT_CYCLE_NOT_FOUND",
                        Message = "Treatment cycle not found",
                        MetaData = new PagingMetaData(),
                        Data = new List<AppointmentResponse>()
                    };
                }

                if (request == null)
                    request = new GetAppointmentsRequest();

                request.Normalize();
                request.TreatmentCycleId = treatmentCycleId; // Override to ensure filtering by treatment cycle

                return await GetAllAppointmentsAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving appointments for treatment cycle {TreatmentCycleId}", methodName, treatmentCycleId);
                return new DynamicResponse<AppointmentResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = $"Error retrieving appointments: {ex.Message}",
                    MetaData = new PagingMetaData(),
                    Data = new List<AppointmentResponse>()
                };
            }
        }

        /// <summary>
        /// Get appointments for a specific doctor
        /// </summary>
        public async Task<DynamicResponse<AppointmentResponse>> GetAppointmentsByDoctorIdAsync(Guid doctorId, GetAppointmentsRequest request)
        {
            const string methodName = nameof(GetAppointmentsByDoctorIdAsync);
            _logger.LogInformation("{MethodName} called with doctorId: {DoctorId}, request: {@Request}", methodName, doctorId, request);

            try
            {
                if (doctorId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid doctor ID provided - {DoctorId}", methodName, doctorId);
                    return new DynamicResponse<AppointmentResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_DOCTOR_ID",
                        Message = "Doctor ID cannot be empty",
                        MetaData = new PagingMetaData(),
                        Data = new List<AppointmentResponse>()
                    };
                }

                // Verify doctor exists
                var doctorExists = await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .AnyAsync(d => d.Id == doctorId && !d.IsDeleted);

                if (!doctorExists)
                {
                    _logger.LogWarning("{MethodName}: Doctor not found with ID: {DoctorId}", methodName, doctorId);
                    return new DynamicResponse<AppointmentResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "DOCTOR_NOT_FOUND",
                        Message = "Doctor not found",
                        MetaData = new PagingMetaData(),
                        Data = new List<AppointmentResponse>()
                    };
                }

                if (request == null)
                    request = new GetAppointmentsRequest();

                request.Normalize();
                request.DoctorId = doctorId; // Override to ensure filtering by doctor

                return await GetAllAppointmentsAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving appointments for doctor {DoctorId}", methodName, doctorId);
                return new DynamicResponse<AppointmentResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = $"Error retrieving appointments: {ex.Message}",
                    MetaData = new PagingMetaData(),
                    Data = new List<AppointmentResponse>()
                };
            }
        }

        /// <summary>
        /// Get booking appointments for a specific patient
        /// </summary>
        public async Task<DynamicResponse<AppointmentResponse>> GetAppointmentsBookingByPatientIdAsync(Guid patientId, GetAppointmentsRequest request)
        {
            const string methodName = nameof(GetAppointmentsBookingByPatientIdAsync);
            _logger.LogInformation("{MethodName} called with patientId: {PatientId}, request: {@Request}", methodName, patientId, request);

            try
            {
                if (patientId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid patient ID provided - {PatientId}", methodName, patientId);
                    return new DynamicResponse<AppointmentResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_PATIENT_ID",
                        Message = "Patient ID cannot be empty",
                        MetaData = new PagingMetaData(),
                        Data = new List<AppointmentResponse>()
                    };
                }

                // Verify patient exists
                var patientExists = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .AnyAsync(p => p.Id == patientId && !p.IsDeleted);

                if (!patientExists)
                {
                    _logger.LogWarning("{MethodName}: Patient not found with ID: {PatientId}", methodName, patientId);
                    return new DynamicResponse<AppointmentResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "PATIENT_NOT_FOUND",
                        Message = "Patient not found",
                        MetaData = new PagingMetaData(),
                        Data = new List<AppointmentResponse>()
                    };
                }

                if (request == null)
                    request = new GetAppointmentsRequest();

                request.Normalize();

                // Build query for booking appointments of the patient
                var query = _unitOfWork.Repository<Appointment>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(a => a.Patient)
                        .ThenInclude(p => p.Account)
                    .Include(a => a.TreatmentCycle)
                        .ThenInclude(tc => tc.Treatment)
                            .ThenInclude(t => t.Patient)
                                .ThenInclude(p => p.Account)
                    .Include(a => a.Slot)
                        .ThenInclude(s => s.DoctorSchedules)
                            .ThenInclude(ds => ds.Doctor)
                                .ThenInclude(d => d.Account)
                    .Include(a => a.AppointmentDoctors.Where(ad => !ad.IsDeleted))
                        .ThenInclude(ad => ad.Doctor)
                            .ThenInclude(d => d.Account)
                    .Where(a => !a.IsDeleted 
                        && a.PatientId == patientId 
                        && a.Type == AppointmentType.Booking);

                // Apply additional filters from request
                if (request.Status.HasValue)
                {
                    query = query.Where(a => a.Status == request.Status.Value);
                }

                if (request.AppointmentDateFrom.HasValue)
                {
                    query = query.Where(a => a.AppointmentDate >= request.AppointmentDateFrom.Value);
                }

                if (request.AppointmentDateTo.HasValue)
                {
                    query = query.Where(a => a.AppointmentDate <= request.AppointmentDateTo.Value);
                }

                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    var searchTerm = request.SearchTerm.Trim().ToLowerInvariant();
                    query = query.Where(a =>
                        (a.Reason != null && a.Reason.ToLower().Contains(searchTerm)) ||
                        (a.Notes != null && a.Notes.ToLower().Contains(searchTerm)) ||
                        (a.Patient != null && a.Patient.Account != null &&
                         (a.Patient.Account.FirstName.ToLower().Contains(searchTerm) ||
                          a.Patient.Account.LastName.ToLower().Contains(searchTerm))));
                }

                // Get total count
                var totalCount = await query.CountAsync();

                // Apply sorting
                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    var isDescending = request.Order?.ToLower() == "desc";
                    query = request.Sort.ToLower() switch
                    {
                        "appointmentdate" => isDescending ? query.OrderByDescending(a => a.AppointmentDate) : query.OrderBy(a => a.AppointmentDate),
                        "status" => isDescending ? query.OrderByDescending(a => a.Status) : query.OrderBy(a => a.Status),
                        "createdat" => isDescending ? query.OrderByDescending(a => a.CreatedAt) : query.OrderBy(a => a.CreatedAt),
                        _ => isDescending ? query.OrderByDescending(a => a.CreatedAt) : query.OrderBy(a => a.CreatedAt)
                    };
                }
                else
                {
                    query = query.OrderByDescending(a => a.AppointmentDate);
                }

                // Apply pagination
                var appointments = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                var appointmentResponses = appointments.Select(MapToAppointmentResponse).ToList();

                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} booking appointments for patient {PatientId}", 
                    methodName, appointmentResponses.Count, patientId);

                return new DynamicResponse<AppointmentResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Booking appointments retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = totalCount,
                        CurrentPageSize = appointmentResponses.Count
                    },
                    Data = appointmentResponses
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving booking appointments for patient {PatientId}", methodName, patientId);
                return new DynamicResponse<AppointmentResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = $"Error retrieving booking appointments: {ex.Message}",
                    MetaData = new PagingMetaData(),
                    Data = new List<AppointmentResponse>()
                };
            }
        }

        /// <summary>
        /// Get appointment for a specific slot
        /// </summary>
        public async Task<BaseResponse<AppointmentResponse>> GetAppointmentBySlotIdAsync(Guid slotId)
        {
            const string methodName = nameof(GetAppointmentBySlotIdAsync);
            _logger.LogInformation("{MethodName} called with slotId: {SlotId}", methodName, slotId);

            try
            {
                if (slotId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid slot ID provided - {SlotId}", methodName, slotId);
                    return BaseResponse<AppointmentResponse>.CreateError("Slot ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_SLOT_ID");
                }

                var appointment = await _unitOfWork.Repository<Appointment>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(a => a.TreatmentCycle)
                        .ThenInclude(tc => tc.Treatment)
                            .ThenInclude(t => t.Patient)
                                .ThenInclude(p => p.Account)
                    .Include(a => a.Slot)
                        .ThenInclude(s => s.DoctorSchedules)
                            .ThenInclude(ds => ds.Doctor)
                                .ThenInclude(d => d.Account)
                    .Include(a => a.AppointmentDoctors.Where(ad => !ad.IsDeleted))
                        .ThenInclude(ad => ad.Doctor)
                            .ThenInclude(d => d.Account)
                    .Where(a => a.SlotId == slotId && !a.IsDeleted)
                    .FirstOrDefaultAsync();

                if (appointment == null)
                {
                    _logger.LogWarning("{MethodName}: Appointment not found for slot ID: {SlotId}", methodName, slotId);
                    return BaseResponse<AppointmentResponse>.CreateError("Appointment not found for the specified slot", StatusCodes.Status404NotFound, "APPOINTMENT_NOT_FOUND");
                }

                var appointmentResponse = MapToAppointmentResponse(appointment);
                _logger.LogInformation("{MethodName}: Successfully retrieved appointment for slot {SlotId}", methodName, slotId);
                return BaseResponse<AppointmentResponse>.CreateSuccess(appointmentResponse, "Appointment retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving appointment for slot {SlotId}", methodName, slotId);
                return BaseResponse<AppointmentResponse>.CreateError($"Error retrieving appointment: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Create new appointment
        /// </summary>
        public async Task<BaseResponse<AppointmentResponse>> CreateAppointmentAsync(CreateAppointmentRequest request)
        {
            const string methodName = nameof(CreateAppointmentAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<AppointmentResponse>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                // Validate patient exists
                var patient = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Where(p => p.Id == request.PatientId && !p.IsDeleted)
                    .FirstOrDefaultAsync();

                if (patient == null)
                {
                    _logger.LogWarning("{MethodName}: Patient not found with ID: {PatientId}", methodName, request.PatientId);
                    return BaseResponse<AppointmentResponse>.CreateError("Patient not found", StatusCodes.Status404NotFound, "PATIENT_NOT_FOUND");
                }

                // Validate treatment cycle exists if provided
                TreatmentCycle? treatmentCycle = null;
                if (request.TreatmentCycleId.HasValue)
                {
                    treatmentCycle = await _unitOfWork.Repository<TreatmentCycle>()
                        .AsQueryable()
                        .Where(tc => tc.Id == request.TreatmentCycleId.Value && !tc.IsDeleted)
                        .FirstOrDefaultAsync();

                    if (treatmentCycle == null)
                    {
                        _logger.LogWarning("{MethodName}: Treatment cycle not found with ID: {TreatmentCycleId}", methodName, request.TreatmentCycleId);
                        return BaseResponse<AppointmentResponse>.CreateError("Treatment cycle not found", StatusCodes.Status404NotFound, "TREATMENT_CYCLE_NOT_FOUND");
                    }
                }

                // Validate slot if provided
                if (request.SlotId.HasValue)
                {
                    var slot = await _unitOfWork.Repository<Slot>()
                        .AsQueryable()
                        .Where(s => s.Id == request.SlotId.Value && !s.IsDeleted)
                        .FirstOrDefaultAsync();

                    if (slot == null)
                    {
                        _logger.LogWarning("{MethodName}: Slot not found with ID: {SlotId}", methodName, request.SlotId);
                        return BaseResponse<AppointmentResponse>.CreateError("Slot not found", StatusCodes.Status404NotFound, "SLOT_NOT_FOUND");
                    }

                    // Check if slot is already booked (by existing appointment)
                    var existingAppointment = await _unitOfWork.Repository<Appointment>()
                        .AsQueryable()
                        .Where(a => a.SlotId == request.SlotId.Value && !a.IsDeleted)
                        .FirstOrDefaultAsync();

                    if (existingAppointment != null)
                    {
                        _logger.LogWarning("{MethodName}: Slot is already booked", methodName);
                        return BaseResponse<AppointmentResponse>.CreateError("Slot is already booked", StatusCodes.Status400BadRequest, "SLOT_ALREADY_BOOKED");
                    }

                    // Ensure there is a matching doctor schedule for the selected date/slot when a doctor is provided.
                    var primaryDoctorId = request.DoctorIds != null && request.DoctorIds.Any() ? (Guid?)request.DoctorIds.First() : null;
                    var workDate = DateOnly.FromDateTime(request.AppointmentDate.Date);
                    if (primaryDoctorId.HasValue)
                    {
                        var existingSchedule = await _unitOfWork.Repository<DoctorSchedule>()
                            .AsQueryable()
                            .FirstOrDefaultAsync(ds => ds.DoctorId == primaryDoctorId.Value && ds.SlotId == slot.Id && ds.WorkDate == workDate && !ds.IsDeleted);

                        if (existingSchedule == null)
                        {
                            // Create an available schedule for this doctor, date, and slot
                            var newSchedule = new DoctorSchedule(Guid.NewGuid(), primaryDoctorId.Value, slot.Id, workDate, true);
                            await _unitOfWork.Repository<DoctorSchedule>().InsertAsync(newSchedule);
                        }
                        else if (!existingSchedule.IsAvailable)
                        {
                            _logger.LogWarning("{MethodName}: Doctor not available for selected slot/date", methodName);
                            return BaseResponse<AppointmentResponse>.CreateError("Doctor is not available for the selected slot/date", StatusCodes.Status400BadRequest, "DOCTOR_NOT_AVAILABLE");
                        }
                    }
                }

                // Create appointment entity
                var appointment = new Appointment(
                    Guid.NewGuid(),
                    request.PatientId,
                    request.TreatmentCycleId,
                    request.AppointmentDate,
                    request.Type,
                    request.Status,
                    request.Reason,
                    request.Instructions,
                    request.Notes,
                    request.SlotId,
                    request.CheckInTime,
                    request.CheckOutTime,
                    false
                );

                await _unitOfWork.Repository<Appointment>().InsertAsync(appointment);

                // Add doctors to appointment if provided
                if (request.DoctorIds != null && request.DoctorIds.Any())
                {
                    var doctorRoles = request.DoctorRoles ?? new List<string>();
                    for (int i = 0; i < request.DoctorIds.Count; i++)
                    {
                        var doctorId = request.DoctorIds[i];
                        var role = i < doctorRoles.Count ? doctorRoles[i] : null;

                        // Verify doctor exists
                        var doctorExists = await _unitOfWork.Repository<Doctor>()
                            .AsQueryable()
                            .AnyAsync(d => d.Id == doctorId && !d.IsDeleted);

                        if (!doctorExists)
                        {
                            _logger.LogWarning("{MethodName}: Doctor not found with ID: {DoctorId}", methodName, doctorId);
                            return BaseResponse<AppointmentResponse>.CreateError($"Doctor not found with ID: {doctorId}", StatusCodes.Status404NotFound, "DOCTOR_NOT_FOUND");
                        }

                        // Check if doctor is already assigned
                        var existingAssignment = await _unitOfWork.Repository<AppointmentDoctor>()
                            .AsQueryable()
                            .AnyAsync(ad => ad.AppointmentId == appointment.Id && ad.DoctorId == doctorId && !ad.IsDeleted);

                        if (existingAssignment)
                        {
                            continue; // Skip if already assigned
                        }

                        var appointmentDoctor = new AppointmentDoctor(
                            Guid.NewGuid(),
                            appointment.Id,
                            doctorId,
                            role,
                            null
                        );

                        await _unitOfWork.Repository<AppointmentDoctor>().InsertAsync(appointmentDoctor);
                    }
                }

                // No direct slot booking flag; booking is inferred by Appointment presence

                await _unitOfWork.CommitAsync();

                // Reload with related data
                var createdAppointment = await _unitOfWork.Repository<Appointment>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(a => a.TreatmentCycle)
                        .ThenInclude(tc => tc.Treatment)
                            .ThenInclude(t => t.Patient)
                                .ThenInclude(p => p.Account)
                    .Include(a => a.Slot)
                        .ThenInclude(s => s.DoctorSchedules)
                            .ThenInclude(ds => ds.Doctor)
                                .ThenInclude(d => d.Account)
                    .Include(a => a.AppointmentDoctors.Where(ad => !ad.IsDeleted))
                        .ThenInclude(ad => ad.Doctor)
                            .ThenInclude(d => d.Account)
                    .FirstOrDefaultAsync(a => a.Id == appointment.Id);

                var appointmentResponse = MapToAppointmentResponse(createdAppointment!);
                _logger.LogInformation("{MethodName}: Successfully created appointment {AppointmentId}", methodName, appointment.Id);
                return BaseResponse<AppointmentResponse>.CreateSuccess(appointmentResponse, "Appointment created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating appointment", methodName);
                return BaseResponse<AppointmentResponse>.CreateError($"Error creating appointment: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Update existing appointment
        /// </summary>
        public async Task<BaseResponse<AppointmentResponse>> UpdateAppointmentAsync(Guid appointmentId, UpdateAppointmentRequest request)
        {
            const string methodName = nameof(UpdateAppointmentAsync);
            _logger.LogInformation("{MethodName} called with appointmentId: {AppointmentId}, request: {@Request}", methodName, appointmentId, request);

            try
            {
                if (appointmentId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid appointment ID provided - {AppointmentId}", methodName, appointmentId);
                    return BaseResponse<AppointmentResponse>.CreateError("Appointment ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_APPOINTMENT_ID");
                }

                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<AppointmentResponse>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                var appointment = await _unitOfWork.Repository<Appointment>()
                    .AsQueryable()
                    .Where(a => a.Id == appointmentId && !a.IsDeleted)
                    .FirstOrDefaultAsync();

                if (appointment == null)
                {
                    _logger.LogWarning("{MethodName}: Appointment not found with ID: {AppointmentId}", methodName, appointmentId);
                    return BaseResponse<AppointmentResponse>.CreateError("Appointment not found", StatusCodes.Status404NotFound, "APPOINTMENT_NOT_FOUND");
                }

                // Handle slot change
                if (request.SlotId.HasValue && request.SlotId.Value != appointment.SlotId)
                {
                    // No need to unbook old slot; booking is inferred by Appointment linkage

                    // Validate and book new slot
                    var newSlot = await _unitOfWork.Repository<Slot>()
                        .AsQueryable()
                        .Where(s => s.Id == request.SlotId.Value && !s.IsDeleted)
                        .FirstOrDefaultAsync();

                    if (newSlot == null)
                    {
                        _logger.LogWarning("{MethodName}: Slot not found with ID: {SlotId}", methodName, request.SlotId);
                        return BaseResponse<AppointmentResponse>.CreateError("Slot not found", StatusCodes.Status404NotFound, "SLOT_NOT_FOUND");
                    }

                    // Check if slot is already booked by another appointment
                    var existingAppointment2 = await _unitOfWork.Repository<Appointment>()
                        .AsQueryable()
                        .Where(a => a.SlotId == request.SlotId.Value && a.Id != appointmentId && !a.IsDeleted)
                        .FirstOrDefaultAsync();

                    if (existingAppointment2 != null)
                    {
                        _logger.LogWarning("{MethodName}: Slot is already booked", methodName);
                        return BaseResponse<AppointmentResponse>.CreateError("Slot is already booked", StatusCodes.Status400BadRequest, "SLOT_ALREADY_BOOKED");
                    }

                    // Validate slot date matches appointment date
                    var appointmentDate = request.AppointmentDate ?? appointment.AppointmentDate;
                    if (newSlot.DoctorSchedules == null || !newSlot.DoctorSchedules.Any(ds => ds.WorkDate == DateOnly.FromDateTime(appointmentDate.Date)))
                    {
                        _logger.LogWarning("{MethodName}: Slot date does not match appointment date", methodName);
                        return BaseResponse<AppointmentResponse>.CreateError("Slot date does not match appointment date", StatusCodes.Status400BadRequest, "SLOT_DATE_MISMATCH");
                    }

                    appointment.SlotId = request.SlotId.Value;
                }

                // Update appointment properties
                if (request.AppointmentDate.HasValue)
                    appointment.AppointmentDate = request.AppointmentDate.Value;
                if (request.Type.HasValue)
                    appointment.Type = request.Type.Value;
                if (request.Status.HasValue)
                    appointment.Status = request.Status.Value;
                if (request.Reason != null)
                    appointment.Reason = request.Reason;
                if (request.Instructions != null)
                    appointment.Instructions = request.Instructions;
                if (request.Notes != null)
                    appointment.Notes = request.Notes;
                if (request.CheckInTime.HasValue)
                    appointment.CheckInTime = request.CheckInTime;
                if (request.CheckOutTime.HasValue)
                    appointment.CheckOutTime = request.CheckOutTime;
                if (request.IsReminderSent.HasValue)
                    appointment.IsReminderSent = request.IsReminderSent.Value;

                appointment.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Appointment>().UpdateGuid(appointment, appointmentId);
                await _unitOfWork.CommitAsync();

                // Reload with related data
                var updatedAppointment = await _unitOfWork.Repository<Appointment>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(a => a.TreatmentCycle)
                        .ThenInclude(tc => tc.Treatment)
                            .ThenInclude(t => t.Patient)
                                .ThenInclude(p => p.Account)
                    .Include(a => a.Slot)
                        .ThenInclude(s => s.DoctorSchedules)
                            .ThenInclude(ds => ds.Doctor)
                                .ThenInclude(d => d.Account)
                    .Include(a => a.AppointmentDoctors.Where(ad => !ad.IsDeleted))
                        .ThenInclude(ad => ad.Doctor)
                            .ThenInclude(d => d.Account)
                    .FirstOrDefaultAsync(a => a.Id == appointmentId);

                var appointmentResponse = MapToAppointmentResponse(updatedAppointment!);
                _logger.LogInformation("{MethodName}: Successfully updated appointment {AppointmentId}", methodName, appointmentId);
                return BaseResponse<AppointmentResponse>.CreateSuccess(appointmentResponse, "Appointment updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating appointment {AppointmentId}", methodName, appointmentId);
                return BaseResponse<AppointmentResponse>.CreateError($"Error updating appointment: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Delete appointment (soft delete)
        /// </summary>
        public async Task<BaseResponse> DeleteAppointmentAsync(Guid appointmentId)
        {
            const string methodName = nameof(DeleteAppointmentAsync);
            _logger.LogInformation("{MethodName} called with appointmentId: {AppointmentId}", methodName, appointmentId);

            try
            {
                if (appointmentId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid appointment ID provided - {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Appointment ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_APPOINTMENT_ID");
                }

                var appointment = await _unitOfWork.Repository<Appointment>()
                    .AsQueryable()
                    .Where(a => a.Id == appointmentId && !a.IsDeleted)
                    .FirstOrDefaultAsync();

                if (appointment == null)
                {
                    _logger.LogWarning("{MethodName}: Appointment not found with ID: {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Appointment not found", StatusCodes.Status404NotFound, "APPOINTMENT_NOT_FOUND");
                }

                // Check if appointment can be deleted (e.g., not completed)
                if (appointment.Status == AppointmentStatus.Completed)
                {
                    _logger.LogWarning("{MethodName}: Cannot delete completed appointment: {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Cannot delete completed appointment", StatusCodes.Status400BadRequest, "CANNOT_DELETE_COMPLETED");
                }

                // No need to unbook slot; booking is inferred by Appointment linkage

                // Soft delete appointment
                appointment.IsDeleted = true;
                appointment.DeletedAt = DateTime.UtcNow;
                appointment.UpdatedAt = DateTime.UtcNow;

                // Soft delete appointment doctors
                var appointmentDoctors = await _unitOfWork.Repository<AppointmentDoctor>()
                    .AsQueryable()
                    .Where(ad => ad.AppointmentId == appointmentId && !ad.IsDeleted)
                    .ToListAsync();

                foreach (var appointmentDoctor in appointmentDoctors)
                {
                    appointmentDoctor.IsDeleted = true;
                    appointmentDoctor.DeletedAt = DateTime.UtcNow;
                    appointmentDoctor.UpdatedAt = DateTime.UtcNow;
                }

                await _unitOfWork.Repository<Appointment>().UpdateGuid(appointment, appointmentId);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully deleted appointment {AppointmentId}", methodName, appointmentId);
                return BaseResponse.CreateSuccess("Appointment deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error deleting appointment {AppointmentId}", methodName, appointmentId);
                return BaseResponse.CreateError($"Error deleting appointment: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        #endregion

        #region Appointment Status Operations

        /// <summary>
        /// Update appointment status
        /// </summary>
        public async Task<BaseResponse> UpdateAppointmentStatusAsync(Guid appointmentId, AppointmentStatus status)
        {
            const string methodName = nameof(UpdateAppointmentStatusAsync);
            _logger.LogInformation("{MethodName} called with appointmentId: {AppointmentId}, status: {Status}", methodName, appointmentId, status);

            try
            {
                if (appointmentId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid appointment ID provided - {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Appointment ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_APPOINTMENT_ID");
                }

                var appointment = await _unitOfWork.Repository<Appointment>()
                    .AsQueryable()
                    .Where(a => a.Id == appointmentId && !a.IsDeleted)
                    .FirstOrDefaultAsync();

                if (appointment == null)
                {
                    _logger.LogWarning("{MethodName}: Appointment not found with ID: {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Appointment not found", StatusCodes.Status404NotFound, "APPOINTMENT_NOT_FOUND");
                }

                appointment.Status = status;
                appointment.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Appointment>().UpdateGuid(appointment, appointmentId);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully updated appointment status {AppointmentId}", methodName, appointmentId);
                return BaseResponse.CreateSuccess($"Appointment status updated to {status}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating appointment status {AppointmentId}", methodName, appointmentId);
                return BaseResponse.CreateError($"Error updating appointment status: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Check in for an appointment
        /// </summary>
        public async Task<BaseResponse> CheckInAppointmentAsync(Guid appointmentId)
        {
            const string methodName = nameof(CheckInAppointmentAsync);
            _logger.LogInformation("{MethodName} called with appointmentId: {AppointmentId}", methodName, appointmentId);

            try
            {
                if (appointmentId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid appointment ID provided - {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Appointment ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_APPOINTMENT_ID");
                }

                var appointment = await _unitOfWork.Repository<Appointment>()
                    .AsQueryable()
                    .Where(a => a.Id == appointmentId && !a.IsDeleted)
                    .FirstOrDefaultAsync();

                if (appointment == null)
                {
                    _logger.LogWarning("{MethodName}: Appointment not found with ID: {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Appointment not found", StatusCodes.Status404NotFound, "APPOINTMENT_NOT_FOUND");
                }

                appointment.CheckInTime = DateTime.UtcNow;
                appointment.Status = AppointmentStatus.CheckedIn;
                appointment.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Appointment>().UpdateGuid(appointment, appointmentId);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully checked in appointment {AppointmentId}", methodName, appointmentId);
                return BaseResponse.CreateSuccess("Appointment checked in successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error checking in appointment {AppointmentId}", methodName, appointmentId);
                return BaseResponse.CreateError($"Error checking in appointment: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Check out for an appointment
        /// </summary>
        public async Task<BaseResponse> CheckOutAppointmentAsync(Guid appointmentId)
        {
            const string methodName = nameof(CheckOutAppointmentAsync);
            _logger.LogInformation("{MethodName} called with appointmentId: {AppointmentId}", methodName, appointmentId);

            try
            {
                if (appointmentId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid appointment ID provided - {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Appointment ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_APPOINTMENT_ID");
                }

                var appointment = await _unitOfWork.Repository<Appointment>()
                    .AsQueryable()
                    .Where(a => a.Id == appointmentId && !a.IsDeleted)
                    .FirstOrDefaultAsync();

                if (appointment == null)
                {
                    _logger.LogWarning("{MethodName}: Appointment not found with ID: {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Appointment not found", StatusCodes.Status404NotFound, "APPOINTMENT_NOT_FOUND");
                }

                if (!appointment.CheckInTime.HasValue)
                {
                    _logger.LogWarning("{MethodName}: Cannot check out without checking in first: {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Cannot check out without checking in first", StatusCodes.Status400BadRequest, "NOT_CHECKED_IN");
                }

                appointment.CheckOutTime = DateTime.UtcNow;
                appointment.Status = AppointmentStatus.Completed;
                appointment.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Appointment>().UpdateGuid(appointment, appointmentId);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully checked out appointment {AppointmentId}", methodName, appointmentId);
                return BaseResponse.CreateSuccess("Appointment checked out successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error checking out appointment {AppointmentId}", methodName, appointmentId);
                return BaseResponse.CreateError($"Error checking out appointment: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Cancel an appointment
        /// </summary>
        public async Task<BaseResponse> CancelAppointmentAsync(Guid appointmentId, string? cancellationReason = null)
        {
            const string methodName = nameof(CancelAppointmentAsync);
            _logger.LogInformation("{MethodName} called with appointmentId: {AppointmentId}, reason: {Reason}", methodName, appointmentId, cancellationReason);

            try
            {
                if (appointmentId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid appointment ID provided - {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Appointment ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_APPOINTMENT_ID");
                }

                var appointment = await _unitOfWork.Repository<Appointment>()
                    .AsQueryable()
                    .Where(a => a.Id == appointmentId && !a.IsDeleted)
                    .FirstOrDefaultAsync();

                if (appointment == null)
                {
                    _logger.LogWarning("{MethodName}: Appointment not found with ID: {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Appointment not found", StatusCodes.Status404NotFound, "APPOINTMENT_NOT_FOUND");
                }

                // Check if appointment can be cancelled based on status
                if (appointment.Status == AppointmentStatus.Completed)
                {
                    _logger.LogWarning("{MethodName}: Cannot cancel completed appointment: {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Cannot cancel completed appointment", StatusCodes.Status400BadRequest, "CANNOT_CANCEL_COMPLETED");
                }
                
                if (appointment.Status == AppointmentStatus.Cancelled)
                {
                    _logger.LogWarning("{MethodName}: Appointment is already cancelled: {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Appointment is already cancelled", StatusCodes.Status400BadRequest, "ALREADY_CANCELLED");
                }

                // No need to unbook slot; booking is inferred by Appointment linkage

                appointment.Status = AppointmentStatus.Cancelled;
                if (!string.IsNullOrWhiteSpace(cancellationReason))
                {
                    appointment.Notes = string.IsNullOrWhiteSpace(appointment.Notes)
                        ? $"Cancelled: {cancellationReason}"
                        : $"{appointment.Notes}\nCancelled: {cancellationReason}";
                }
                appointment.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Appointment>().UpdateGuid(appointment, appointmentId);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully cancelled appointment {AppointmentId}", methodName, appointmentId);
                return BaseResponse.CreateSuccess("Appointment cancelled successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error cancelling appointment {AppointmentId}", methodName, appointmentId);
                return BaseResponse.CreateError($"Error cancelling appointment: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        #endregion

        #region Appointment Doctor Management

        /// <summary>
        /// Add doctor to appointment
        /// </summary>
        public async Task<BaseResponse> AddDoctorToAppointmentAsync(Guid appointmentId, Guid doctorId, string? role = null, string? notes = null)
        {
            const string methodName = nameof(AddDoctorToAppointmentAsync);
            _logger.LogInformation("{MethodName} called with appointmentId: {AppointmentId}, doctorId: {DoctorId}, role: {Role}", methodName, appointmentId, doctorId, role);

            try
            {
                if (appointmentId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid appointment ID provided - {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Appointment ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_APPOINTMENT_ID");
                }

                if (doctorId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid doctor ID provided - {DoctorId}", methodName, doctorId);
                    return BaseResponse.CreateError("Doctor ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_DOCTOR_ID");
                }

                // Verify appointment exists
                var appointment = await _unitOfWork.Repository<Appointment>()
                    .AsQueryable()
                    .Where(a => a.Id == appointmentId && !a.IsDeleted)
                    .FirstOrDefaultAsync();

                if (appointment == null)
                {
                    _logger.LogWarning("{MethodName}: Appointment not found with ID: {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Appointment not found", StatusCodes.Status404NotFound, "APPOINTMENT_NOT_FOUND");
                }

                // Verify doctor exists
                var doctor = await _unitOfWork.Repository<Doctor>()
                    .AsQueryable()
                    .Where(d => d.Id == doctorId && !d.IsDeleted)
                    .FirstOrDefaultAsync();

                if (doctor == null)
                {
                    _logger.LogWarning("{MethodName}: Doctor not found with ID: {DoctorId}", methodName, doctorId);
                    return BaseResponse.CreateError("Doctor not found", StatusCodes.Status404NotFound, "DOCTOR_NOT_FOUND");
                }

                // Check if doctor is already assigned
                var existingAssignment = await _unitOfWork.Repository<AppointmentDoctor>()
                    .AsQueryable()
                    .Where(ad => ad.AppointmentId == appointmentId && ad.DoctorId == doctorId && !ad.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingAssignment != null)
                {
                    _logger.LogWarning("{MethodName}: Doctor is already assigned to appointment: {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Doctor is already assigned to this appointment", StatusCodes.Status400BadRequest, "DOCTOR_ALREADY_ASSIGNED");
                }

                var appointmentDoctor = new AppointmentDoctor(
                    Guid.NewGuid(),
                    appointmentId,
                    doctorId,
                    role,
                    notes
                );

                await _unitOfWork.Repository<AppointmentDoctor>().InsertAsync(appointmentDoctor);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully added doctor {DoctorId} to appointment {AppointmentId}", methodName, doctorId, appointmentId);
                return BaseResponse.CreateSuccess("Doctor added to appointment successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error adding doctor to appointment {AppointmentId}", methodName, appointmentId);
                return BaseResponse.CreateError($"Error adding doctor to appointment: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Remove doctor from appointment
        /// </summary>
        public async Task<BaseResponse> RemoveDoctorFromAppointmentAsync(Guid appointmentId, Guid doctorId)
        {
            const string methodName = nameof(RemoveDoctorFromAppointmentAsync);
            _logger.LogInformation("{MethodName} called with appointmentId: {AppointmentId}, doctorId: {DoctorId}", methodName, appointmentId, doctorId);

            try
            {
                if (appointmentId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid appointment ID provided - {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Appointment ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_APPOINTMENT_ID");
                }

                if (doctorId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid doctor ID provided - {DoctorId}", methodName, doctorId);
                    return BaseResponse.CreateError("Doctor ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_DOCTOR_ID");
                }

                var appointmentDoctor = await _unitOfWork.Repository<AppointmentDoctor>()
                    .AsQueryable()
                    .Where(ad => ad.AppointmentId == appointmentId && ad.DoctorId == doctorId && !ad.IsDeleted)
                    .FirstOrDefaultAsync();

                if (appointmentDoctor == null)
                {
                    _logger.LogWarning("{MethodName}: Doctor assignment not found", methodName);
                    return BaseResponse.CreateError("Doctor is not assigned to this appointment", StatusCodes.Status404NotFound, "DOCTOR_NOT_ASSIGNED");
                }

                // Soft delete
                appointmentDoctor.IsDeleted = true;
                appointmentDoctor.DeletedAt = DateTime.UtcNow;
                appointmentDoctor.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<AppointmentDoctor>().UpdateGuid(appointmentDoctor, appointmentDoctor.Id);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully removed doctor {DoctorId} from appointment {AppointmentId}", methodName, doctorId, appointmentId);
                return BaseResponse.CreateSuccess("Doctor removed from appointment successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error removing doctor from appointment {AppointmentId}", methodName, appointmentId);
                return BaseResponse.CreateError($"Error removing doctor from appointment: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        /// <summary>
        /// Update doctor role in appointment
        /// </summary>
        public async Task<BaseResponse> UpdateDoctorRoleInAppointmentAsync(Guid appointmentId, Guid doctorId, string? role, string? notes = null)
        {
            const string methodName = nameof(UpdateDoctorRoleInAppointmentAsync);
            _logger.LogInformation("{MethodName} called with appointmentId: {AppointmentId}, doctorId: {DoctorId}, role: {Role}", methodName, appointmentId, doctorId, role);

            try
            {
                if (appointmentId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid appointment ID provided - {AppointmentId}", methodName, appointmentId);
                    return BaseResponse.CreateError("Appointment ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_APPOINTMENT_ID");
                }

                if (doctorId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid doctor ID provided - {DoctorId}", methodName, doctorId);
                    return BaseResponse.CreateError("Doctor ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_DOCTOR_ID");
                }

                var appointmentDoctor = await _unitOfWork.Repository<AppointmentDoctor>()
                    .AsQueryable()
                    .Where(ad => ad.AppointmentId == appointmentId && ad.DoctorId == doctorId && !ad.IsDeleted)
                    .FirstOrDefaultAsync();

                if (appointmentDoctor == null)
                {
                    _logger.LogWarning("{MethodName}: Doctor assignment not found", methodName);
                    return BaseResponse.CreateError("Doctor is not assigned to this appointment", StatusCodes.Status404NotFound, "DOCTOR_NOT_ASSIGNED");
                }

                appointmentDoctor.Role = role;
                if (notes != null)
                    appointmentDoctor.Notes = notes;
                appointmentDoctor.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<AppointmentDoctor>().UpdateGuid(appointmentDoctor, appointmentDoctor.Id);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully updated doctor role in appointment {AppointmentId}", methodName, appointmentId);
                return BaseResponse.CreateSuccess("Doctor role updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating doctor role in appointment {AppointmentId}", methodName, appointmentId);
                return BaseResponse.CreateError($"Error updating doctor role: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Map Appointment entity to AppointmentResponse
        /// </summary>
        private AppointmentResponse MapToAppointmentResponse(Appointment appointment)
        {
            var response = new AppointmentResponse
            {
                Id = appointment.Id,
                TreatmentCycleId = appointment.TreatmentCycleId,
                SlotId = appointment.SlotId,
                Type = appointment.Type,
                TypeName = appointment.Type.ToString(),
                Status = appointment.Status,
                StatusName = appointment.Status.ToString(),
                AppointmentDate = appointment.AppointmentDate,
                Reason = appointment.Reason,
                Instructions = appointment.Instructions,
                Notes = appointment.Notes,
                CheckInTime = appointment.CheckInTime,
                CheckOutTime = appointment.CheckOutTime,
                IsReminderSent = appointment.IsReminderSent,
                CreatedAt = appointment.CreatedAt,
                UpdatedAt = appointment.UpdatedAt
            };

            // Map TreatmentCycle
            if (appointment.TreatmentCycle != null)
            {
                response.TreatmentCycle = new AppointmentTreatmentCycleInfo
                {
                    Id = appointment.TreatmentCycle.Id,
                    CycleName = appointment.TreatmentCycle.CycleName,
                    CycleNumber = appointment.TreatmentCycle.CycleNumber,
                    StartDate = appointment.TreatmentCycle.StartDate,
                    EndDate = appointment.TreatmentCycle.EndDate,
                    Status = appointment.TreatmentCycle.Status.ToString()
                };

                // Map Treatment
                if (appointment.TreatmentCycle.Treatment != null)
                {
                    response.TreatmentCycle.Treatment = new AppointmentTreatmentInfo
                    {
                        Id = appointment.TreatmentCycle.Treatment.Id,
                        TreatmentType = appointment.TreatmentCycle.Treatment.TreatmentType.ToString()
                    };

                    // Map Patient
                    if (appointment.TreatmentCycle.Treatment.Patient != null && appointment.TreatmentCycle.Treatment.Patient.Account != null)
                    {
                        var account = appointment.TreatmentCycle.Treatment.Patient.Account;
                        response.TreatmentCycle.Treatment.Patient = new AppointmentPatientInfo
                        {
                            Id = appointment.TreatmentCycle.Treatment.Patient.Id,
                            PatientCode = appointment.TreatmentCycle.Treatment.Patient.PatientCode,
                            FullName = $"{account.FirstName} {account.LastName}".Trim(),
                            Phone = account.Phone,
                            Email = account.Email
                        };
                    }
                }
            }

            // Map Patient (prefer direct relation; fallback to patient from treatment)
            if (appointment.Patient != null && appointment.Patient.Account != null)
            {
                var p = appointment.Patient;
                var pa = p.Account;
                response.Patient = new AppointmentPatientInfo
                {
                    Id = p.Id,
                    PatientCode = p.PatientCode,
                    FullName = $"{pa.FirstName} {pa.LastName}".Trim(),
                    Phone = pa.Phone,
                    Email = pa.Email
                };
            }
            else if (appointment.TreatmentCycle?.Treatment?.Patient != null && appointment.TreatmentCycle.Treatment.Patient.Account != null)
            {
                var p = appointment.TreatmentCycle.Treatment.Patient;
                var pa = p.Account;
                response.Patient = new AppointmentPatientInfo
                {
                    Id = p.Id,
                    PatientCode = p.PatientCode,
                    FullName = $"{pa.FirstName} {pa.LastName}".Trim(),
                    Phone = pa.Phone,
                    Email = pa.Email
                };
            }

            // Map Slot
            if (appointment.Slot != null)
            {
                response.Slot = new AppointmentSlotInfo
                {
                    Id = appointment.Slot.Id,
                    StartTime = appointment.Slot.StartTime,
                    EndTime = appointment.Slot.EndTime,
                    IsBooked = appointment.Slot.Appointment != null
                };

                // Map Schedule: pick schedule for the appointment date if available, otherwise first by date
                if (appointment.Slot != null && appointment.Slot.DoctorSchedules != null && appointment.Slot.DoctorSchedules.Any())
                {
                    var apptDateOnly = DateOnly.FromDateTime(appointment.AppointmentDate.Date);
                    var matchedSchedule = appointment.Slot.DoctorSchedules
                        .FirstOrDefault(ds => ds.WorkDate == apptDateOnly);
                    var schedule = matchedSchedule ?? appointment.Slot.DoctorSchedules.OrderBy(ds => ds.WorkDate).FirstOrDefault();
                    if (schedule != null)
                    {
                        response.Slot.Schedule = new AppointmentScheduleInfo
                        {
                            Id = schedule.Id,
                            WorkDate = schedule.WorkDate.ToDateTime(TimeOnly.MinValue),
                            Location = schedule.Location
                        };

                        // Map Doctor from Schedule
                        if (schedule.Doctor != null && schedule.Doctor.Account != null)
                        {
                            var account = schedule.Doctor.Account;
                            response.Slot.Schedule.Doctor = new AppointmentDoctorBasicInfo
                            {
                                Id = schedule.Doctor.Id,
                                BadgeId = schedule.Doctor.BadgeId,
                                Specialty = schedule.Doctor.Specialty,
                                FullName = $"{account.FirstName} {account.LastName}".Trim()
                            };
                        }
                    }
                }
            }

            // Map Doctors
            if (appointment.AppointmentDoctors != null && appointment.AppointmentDoctors.Any())
            {
                response.Doctors = appointment.AppointmentDoctors
                    .Where(ad => !ad.IsDeleted && ad.Doctor != null && ad.Doctor.Account != null)
                    .Select(ad =>
                    {
                        var account = ad.Doctor!.Account!;
                        return new AppointmentDoctorInfo
                        {
                            Id = ad.Id,
                            DoctorId = ad.DoctorId,
                            BadgeId = ad.Doctor.BadgeId,
                            Specialty = ad.Doctor.Specialty,
                            FullName = $"{account.FirstName} {account.LastName}".Trim(),
                            Role = ad.Role,
                            Notes = ad.Notes
                        };
                    })
                    .ToList();
            }

            response.DoctorCount = response.Doctors.Count;

            return response;
        }

        /// <summary>
        /// Map Appointment entity to AppointmentDetailResponse
        /// </summary>
        private AppointmentDetailResponse MapToAppointmentDetailResponse(Appointment appointment)
        {
            var baseResponse = MapToAppointmentResponse(appointment);
            var detailResponse = new AppointmentDetailResponse
            {
                Id = baseResponse.Id,
                TreatmentCycleId = baseResponse.TreatmentCycleId,
                SlotId = baseResponse.SlotId,
                Type = baseResponse.Type,
                TypeName = baseResponse.TypeName,
                Status = baseResponse.Status,
                StatusName = baseResponse.StatusName,
                AppointmentDate = baseResponse.AppointmentDate,
                Reason = baseResponse.Reason,
                Instructions = baseResponse.Instructions,
                Notes = baseResponse.Notes,
                CheckInTime = baseResponse.CheckInTime,
                CheckOutTime = baseResponse.CheckOutTime,
                IsReminderSent = baseResponse.IsReminderSent,
                CreatedAt = baseResponse.CreatedAt,
                UpdatedAt = baseResponse.UpdatedAt,
                TreatmentCycle = baseResponse.TreatmentCycle,
                Slot = baseResponse.Slot,
                Doctors = baseResponse.Doctors,
                DoctorCount = baseResponse.DoctorCount
            };

            // Map MedicalRecord
            if (appointment.MedicalRecord != null)
            {
                detailResponse.MedicalRecord = new AppointmentMedicalRecordInfo
                {
                    Id = appointment.MedicalRecord.Id,
                    RecordDate = appointment.MedicalRecord.CreatedAt,
                    ChiefComplaint = null, // Add if MedicalRecord has these properties
                    Diagnosis = null // Add if MedicalRecord has these properties
                };
            }

            return detailResponse;
        }

        #endregion
    }
}

