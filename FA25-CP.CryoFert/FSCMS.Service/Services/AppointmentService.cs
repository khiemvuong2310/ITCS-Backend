using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.Mapping;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FSCMS.Core.Interfaces;

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
        private readonly ITransactionService _transactionService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AppointmentService> logger, ITransactionService transactionService, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
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

                // Try to get entity from repository cache first
                var appointmentEntity = await _unitOfWork.Repository<Appointment>().GetByIdAsync(appointmentId);
                
                // Load with includes for response mapping
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

                var appointmentResponse = appointment.ToAppointmentResponse();

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
                    .Include(a => a.MedicalRecords.Where(mr => !mr.IsDeleted))
                    .Include(a => a.TreatmentCycle)
                        .ThenInclude(tc => tc.Appointments.Where(ap => !ap.IsDeleted))
                    .Where(a => a.Id == appointmentId && !a.IsDeleted)
                    .FirstOrDefaultAsync();

                if (appointment == null)
                {
                    _logger.LogWarning("{MethodName}: Appointment not found with ID: {AppointmentId}", methodName, appointmentId);
                    return BaseResponse<AppointmentDetailResponse>.CreateError("Appointment not found", StatusCodes.Status404NotFound, "APPOINTMENT_NOT_FOUND");
                }

                // Authorization check: If user is Patient, verify they can only access their own appointments
                if (IsCurrentUserInRole("Patient"))
                {
                    var currentAccountId = GetCurrentAccountId();
                    if (!currentAccountId.HasValue)
                    {
                        _logger.LogWarning("{MethodName}: Unable to get current user account ID", methodName);
                        return BaseResponse<AppointmentDetailResponse>.CreateError("Unable to identify current user", StatusCodes.Status401Unauthorized, "UNAUTHORIZED");
                    }

                    // Patient and Account share the same ID, so PatientId == AccountId
                    if (appointment.PatientId != currentAccountId.Value)
                    {
                        _logger.LogWarning("{MethodName}: Patient {AccountId} attempted to access appointment {AppointmentId} belonging to patient {PatientId}",
                            methodName, currentAccountId.Value, appointmentId, appointment.PatientId);
                        return BaseResponse<AppointmentDetailResponse>.CreateError("You do not have permission to access this appointment", StatusCodes.Status403Forbidden, "FORBIDDEN");
                    }
                }

                // Load Patient separately if it's null but PatientId exists (e.g., if Patient is soft deleted)
                if (appointment.Patient == null && appointment.PatientId != Guid.Empty)
                {
                    appointment.Patient = await _unitOfWork.Repository<Patient>()
                        .AsQueryable()
                        .AsNoTracking()
                        .Include(p => p.Account)
                        .FirstOrDefaultAsync(p => p.Id == appointment.PatientId);
                }

                var appointmentDetailResponse = appointment.ToAppointmentDetailResponse();
                await AttachTransactionsAsync(new[] { appointmentDetailResponse });

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

                // Build base query
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
                    var fromDate = request.AppointmentDateFrom.Value;
                    query = query.Where(a => a.AppointmentDate >= fromDate);
                }

                if (request.AppointmentDateTo.HasValue)
                {
                    var toDate = request.AppointmentDateTo.Value;
                    query = query.Where(a => a.AppointmentDate <= toDate);
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

                var appointmentResponses = appointments
                    .Select(a => a.ToAppointmentResponse())
                    .ToList();
                await AttachTransactionsAsync(appointmentResponses);

                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} appointments", methodName, appointmentResponses.Count);

                var finalResponse = new DynamicResponse<AppointmentResponse>
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

                return finalResponse;
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
        public async Task<DynamicResponse<AppointmentResponse>> GetAppointmentsHistoryByPatientIdAsync(Guid patientId, GetAppointmentsRequest request)
        {
            const string methodName = nameof(GetAppointmentsHistoryByPatientIdAsync);
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
                    .Include(a => a.Slot)
                    .Include(a => a.AppointmentDoctors.Where(ad => !ad.IsDeleted))
                        .ThenInclude(ad => ad.Doctor)
                            .ThenInclude(d => d.Account)
                    .Where(a => !a.IsDeleted
                        && a.PatientId == patientId);

                // Apply additional filters from request
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

                var appointmentResponses = appointments
                    .Select(a => a.ToAppointmentResponse())
                    .ToList();
                await AttachTransactionsAsync(appointmentResponses);

                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} appointments (with booking transactions) for patient {PatientId}",
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
                    .Include(a => a.Slot)
                        .ThenInclude(s => s.Appointments)
                            .ThenInclude(ap => ap.Patient)
                                .ThenInclude(p => p.Account)
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

                var appointmentResponse = appointment.ToAppointmentResponse();
                await AttachTransactionsAsync(new[] { appointmentResponse });
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

                    // Validate all doctors if provided
                    if (request.DoctorIds != null && request.DoctorIds.Any())
                    {
                        foreach (var doctorId in request.DoctorIds)
                        {
                            // Check doctor availability via DoctorSchedule + active appointments
                            var isAvailable = await IsDoctorAvailableForSlotAsync(doctorId, request.SlotId.Value, request.AppointmentDate);
                            if (!isAvailable)
                            {
                                _logger.LogWarning("{MethodName}: Doctor {DoctorId} is busy for slot {SlotId} on date {AppointmentDate}",
                                    methodName, doctorId, request.SlotId.Value, request.AppointmentDate);
                                return BaseResponse<AppointmentResponse>.CreateError(
                                    $"Doctor is busy for the selected slot/date",
                                    StatusCodes.Status400BadRequest,
                                    "DOCTOR_NOT_AVAILABLE");
                            }
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
                var noti = new Notification(Guid.NewGuid(), "New Appointment", $"New Appointment {request.AppointmentDate.ToString()}", NotificationType.Appointment);
                noti.Status = NotificationStatus.Sent;
                noti.PatientId = request.PatientId;
                noti.SentTime = DateTime.UtcNow;
                noti.Channel = "Appointment";
                noti.RelatedEntityType = "Appointment";
                noti.RelatedEntityId = appointment.Id;
                await _unitOfWork.Repository<Notification>().InsertAsync(noti);
                // Entity will be cached automatically after SaveChanges

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
                CreateTransactionRequest createTransactionRequest = new CreateTransactionRequest
                {
                    Amount = 100000,
                    RelatedEntityType = EntityTypeTransaction.Appointment,
                    RelatedEntityId = appointment.Id
                };
                await _transactionService.CreateTransactionAsync(createTransactionRequest, patient.Id);
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

                var appointmentResponse = createdAppointment!.ToAppointmentResponse();
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
                    if (newSlot.DoctorSchedules == null || !newSlot.DoctorSchedules.Any(ds => ds.WorkDate == appointmentDate))
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

                var appointmentResponse = updatedAppointment!.ToAppointmentResponse();
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
                // Cache is automatically invalidated by repository UpdateGuid method

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
                // Cache is automatically invalidated by repository UpdateGuid method

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
                // Cache is automatically invalidated by repository UpdateGuid method

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
                // Cache is automatically invalidated by repository UpdateGuid method

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
                // Cache is automatically invalidated by repository UpdateGuid method

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
        /// Determine if a doctor is free for the specified slot and appointment date.
        /// If DoctorSchedule exists, doctor is busy (not available).
        /// If DoctorSchedule is empty (no record), doctor is available.
        /// </summary>
        private async Task<bool> IsDoctorAvailableForSlotAsync(Guid doctorId, Guid slotId, DateOnly appointmentDate)
        {
            // Check if doctor has schedule (if exists, doctor is busy)
            var hasSchedule = await _unitOfWork.Repository<DoctorSchedule>()
                .AsQueryable()
                .AnyAsync(ds =>
                    !ds.IsDeleted &&
                    ds.DoctorId == doctorId &&
                    ds.SlotId == slotId &&
                    ds.WorkDate == appointmentDate);

            if (hasSchedule)
            {
                return false; // Doctor is busy (has schedule)
            }

            // Check if doctor already has appointment for same slot, same date
            var hasConflictingAppointment = await _unitOfWork.Repository<Appointment>()
                .AsQueryable()
                .AnyAsync(a =>
                    !a.IsDeleted &&
                    a.SlotId == slotId &&
                    a.AppointmentDate == appointmentDate &&
                    a.AppointmentDoctors.Any(ad => ad.DoctorId == doctorId && !ad.IsDeleted) &&
                    a.Status != AppointmentStatus.Cancelled &&
                    a.Status != AppointmentStatus.Completed);

            return !hasConflictingAppointment;
        }

        /// <summary>
        /// Map Transaction entity to TransactionResponseModel for appointment responses
        /// </summary>
        private static TransactionResponseModel MapToTransactionResponse(Transaction transaction)
        {
            return new TransactionResponseModel
            {
                Id = transaction.Id,
                TransactionCode = transaction.TransactionCode,
                PaymentUrl = string.Empty,
                TransactionType = transaction.TransactionType,
                Amount = transaction.Amount,
                Currency = transaction.Currency,
                TransactionDate = transaction.TransactionDate,
                Status = transaction.Status,
                PaymentMethod = transaction.PaymentMethod,
                PaymentGateway = transaction.PaymentGateway,
                ReferenceNumber = transaction.ReferenceNumber,
                Description = transaction.Description,
                Notes = transaction.Notes,
                PatientId = transaction.PatientId,
                PatientName = transaction.PatientName,
                ProcessedDate = transaction.ProcessedDate,
                ProcessedBy = transaction.ProcessedBy,
                RelatedEntityType = transaction.RelatedEntityType,
                RelatedEntityId = transaction.RelatedEntityId
            };
        }

        /// <summary>
        /// Gets current account ID from JWT token
        /// </summary>
        private Guid? GetCurrentAccountId()
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid accountId))
                {
                    return null;
                }
                return accountId;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error getting current account ID from token");
                return null;
            }
        }

        /// <summary>
        /// Checks if current user has the specified role
        /// </summary>
        private bool IsCurrentUserInRole(string roleName)
        {
            try
            {
                return _httpContextAccessor.HttpContext?.User?.IsInRole(roleName) ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error checking user role: {RoleName}", roleName);
                return false;
            }
        }

        /// <summary>
        /// Attach transaction history to appointment responses (all GET endpoints share this logic)
        /// </summary>
        private async Task AttachTransactionsAsync(IEnumerable<AppointmentResponse> appointmentResponses)
        {
            if (appointmentResponses == null)
            {
                return;
            }

            var responses = appointmentResponses.Where(r => r != null).ToList();
            if (!responses.Any())
            {
                return;
            }

            var appointmentIds = responses.Select(r => r.Id).Distinct().ToList();
            if (!appointmentIds.Any())
            {
                return;
            }

            var appointmentTransactions = await _unitOfWork.Repository<Transaction>()
                .AsQueryable()
                .AsNoTracking()
                .Where(t =>
                    !t.IsDeleted &&
                    t.RelatedEntityType == EntityTypeTransaction.Appointment.ToString() &&
                    appointmentIds.Contains(t.RelatedEntityId))
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();

            if (!appointmentTransactions.Any())
            {
                return;
            }

            var transactionLookup = appointmentTransactions
                .GroupBy(t => t.RelatedEntityId)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(MapToTransactionResponse).ToList());

            foreach (var response in responses)
            {
                if (transactionLookup.TryGetValue(response.Id, out var transactions))
                {
                    response.Transactions = transactions;
                }
            }
        }

        #endregion
    }
}

