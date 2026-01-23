using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
using FSCMS.Core.Interfaces;
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
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMailService _mailService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly INotificationService _notificationService;

        public PatientService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PatientService> logger,
            IHttpContextAccessor httpContextAccessor,
            IMailService mailService,
            IEmailTemplateService emailTemplateService,
            INotificationService notificationService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _emailTemplateService = emailTemplateService ?? throw new ArgumentNullException(nameof(emailTemplateService));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        #region Patient CRUD Operations

        /// <summary>
        /// Creates a new patient
        /// </summary>
        public async Task<BaseResponse<PatientResponse>> CreatePatientAsync(CreatePatientRequest request)
        {
            const string methodName = nameof(CreatePatientAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                // Validate input
                if (request == null)
                {
                    return BaseResponse<PatientResponse>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "PATIENT_001");
                }

                // Check if patient code already exists
                var existingPatientByCode = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Where(p => p.PatientCode == request.PatientCode && !p.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingPatientByCode != null)
                {
                    return BaseResponse<PatientResponse>.CreateError("Patient code already exists", StatusCodes.Status409Conflict, "PATIENT_002");
                }

                // Check if national ID already exists (skip check for empty strings - multiple patients can have empty NationalID)
                if (!string.IsNullOrWhiteSpace(request.NationalID))
                {
                    var existingPatientByNationalId = await _unitOfWork.Repository<Patient>()
                        .AsQueryable()
                        .Where(p => p.NationalID == request.NationalID && !p.IsDeleted)
                        .FirstOrDefaultAsync();

                    if (existingPatientByNationalId != null)
                    {
                        return BaseResponse<PatientResponse>.CreateError("National ID already exists", StatusCodes.Status409Conflict, "PATIENT_003");
                    }
                }

                // Check if account exists and is not already associated with another patient
                var account = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .Where(a => a.Id == request.AccountId && !a.IsDeleted)
                    .FirstOrDefaultAsync();

                if (account == null)
                {
                    return BaseResponse<PatientResponse>.CreateError("Account not found", StatusCodes.Status404NotFound, "PATIENT_004");
                }

                var existingPatientByAccount = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Where(p => p.Id == request.AccountId && !p.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingPatientByAccount != null)
                {
                    return BaseResponse<PatientResponse>.CreateError("Account is already associated with another patient", StatusCodes.Status409Conflict, "PATIENT_005");
                }

                // Create patient with shared PK = AccountId, then map other fields
                var patient = new Patient(request.AccountId, request.PatientCode, request.NationalID);
                _mapper.Map(request, patient);
                await _unitOfWork.Repository<Patient>().InsertAsync(patient);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName} - Patient created successfully with ID: {PatientId}", methodName, patient.Id);

                // Get created patient with account info
                var createdPatient = await GetPatientWithAccountAsync(patient.Id);
                var response = _mapper.Map<PatientResponse>(createdPatient);

                return BaseResponse<PatientResponse>.CreateSuccess(response, "Patient created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error creating patient: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<PatientResponse>.CreateError($"An error occurred while creating patient: {ex.Message}", StatusCodes.Status500InternalServerError, "PATIENT_500");
            }
        }

        /// <summary>
        /// Gets a patient by ID
        /// </summary>
        public async Task<BaseResponse<PatientResponse>> GetPatientByIdAsync(Guid patientId)
        {
            const string methodName = nameof(GetPatientByIdAsync);
            _logger.LogInformation("{MethodName} called with patientId: {PatientId}", methodName, patientId);

            try
            {
                // Authorization: Patients can only view their own information
                if (IsCurrentUserInRole("Patient"))
                {
                    var currentAccountId = GetCurrentAccountId();
                    if (currentAccountId == null)
                    {
                        _logger.LogWarning("{MethodName}: Unauthorized - No account ID found in token", methodName);
                        return BaseResponse<PatientResponse>.CreateError(
                            "Unauthorized: You must be logged in",
                            StatusCodes.Status401Unauthorized,
                            "PATIENT_401");
                    }

                    var currentPatientResult = await GetPatientByAccountIdAsync(currentAccountId.Value);
                    if (!currentPatientResult.Success || currentPatientResult.Data == null)
                    {
                        _logger.LogWarning("{MethodName}: Current user is not a patient", methodName);
                        return BaseResponse<PatientResponse>.CreateError(
                            "Unauthorized: Only patients can access this resource",
                            StatusCodes.Status403Forbidden,
                            "PATIENT_403");
                    }

                    var currentPatientId = currentPatientResult.Data.Id;
                    if (patientId != currentPatientId)
                    {
                        _logger.LogWarning("{MethodName}: Patient {CurrentPatientId} attempted to access patient {PatientId}", methodName, currentPatientId, patientId);
                        return BaseResponse<PatientResponse>.CreateError(
                            "Unauthorized: You can only view your own patient information",
                            StatusCodes.Status403Forbidden,
                            "PATIENT_403_FORBIDDEN");
                    }
                }

                if (patientId == Guid.Empty)
                {
                    return BaseResponse<PatientResponse>.CreateError("Patient ID cannot be empty", StatusCodes.Status400BadRequest, "PATIENT_006");
                }

                var patient = await GetPatientWithAccountAsync(patientId);
                if (patient == null)
                {
                    return BaseResponse<PatientResponse>.CreateError("Patient not found", StatusCodes.Status404NotFound, "PATIENT_007");
                }

                var response = _mapper.Map<PatientResponse>(patient);
                return BaseResponse<PatientResponse>.CreateSuccess(response, "Patient retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error retrieving patient: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<PatientResponse>.CreateError($"An error occurred while retrieving patient: {ex.Message}", StatusCodes.Status500InternalServerError, "PATIENT_500");
            }
        }

        /// <summary>
        /// Gets detailed patient information by ID including related data
        /// </summary>
        public async Task<BaseResponse<PatientDetailResponse>> GetPatientDetailsByIdAsync(Guid patientId)
        {
            const string methodName = nameof(GetPatientDetailsByIdAsync);
            _logger.LogInformation("{MethodName} called with patientId: {PatientId}", methodName, patientId);

            try
            {
                // Authorization: Patients can only view their own detailed information
                if (IsCurrentUserInRole("Patient"))
                {
                    var currentAccountId = GetCurrentAccountId();
                    if (currentAccountId == null)
                    {
                        _logger.LogWarning("{MethodName}: Unauthorized - No account ID found in token", methodName);
                        return BaseResponse<PatientDetailResponse>.CreateError(
                            "Unauthorized: You must be logged in",
                            StatusCodes.Status401Unauthorized,
                            "PATIENT_401");
                    }

                    var currentPatientResult = await GetPatientByAccountIdAsync(currentAccountId.Value);
                    if (!currentPatientResult.Success || currentPatientResult.Data == null)
                    {
                        _logger.LogWarning("{MethodName}: Current user is not a patient", methodName);
                        return BaseResponse<PatientDetailResponse>.CreateError(
                            "Unauthorized: Only patients can access this resource",
                            StatusCodes.Status403Forbidden,
                            "PATIENT_403");
                    }

                    var currentPatientId = currentPatientResult.Data.Id;
                    if (patientId != currentPatientId)
                    {
                        _logger.LogWarning("{MethodName}: Patient {CurrentPatientId} attempted to access details of patient {PatientId}", methodName, currentPatientId, patientId);
                        return BaseResponse<PatientDetailResponse>.CreateError(
                            "Unauthorized: You can only view your own patient information",
                            StatusCodes.Status403Forbidden,
                            "PATIENT_403_FORBIDDEN");
                    }
                }

                if (patientId == Guid.Empty)
                {
                    return BaseResponse<PatientDetailResponse>.CreateError("Patient ID cannot be empty", StatusCodes.Status400BadRequest, "PATIENT_006");
                }

                var patient = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(p => p.Account)
                    .Include(p => p.Treatments)
                    .Include(p => p.LabSamples)
                    .Include(p => p.RelationshipsAsPatient1)
                        .ThenInclude(r => r.Patient2)
                            .ThenInclude(p => p.Account)
                    .Include(p => p.RelationshipsAsPatient2)
                        .ThenInclude(r => r.Patient1)
                            .ThenInclude(p => p.Account)
                    .Where(p => p.Id == patientId && !p.IsDeleted)
                    .FirstOrDefaultAsync();

                if (patient == null)
                {
                    return BaseResponse<PatientDetailResponse>.CreateError("Patient not found", StatusCodes.Status404NotFound, "PATIENT_007");
                }

                var response = _mapper.Map<PatientDetailResponse>(patient);
                return BaseResponse<PatientDetailResponse>.CreateSuccess(response, "Patient details retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error retrieving patient details: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<PatientDetailResponse>.CreateError($"An error occurred while retrieving patient details: {ex.Message}", StatusCodes.Status500InternalServerError, "PATIENT_500");
            }
        }

        /// <summary>
        /// Gets a patient by patient code
        /// </summary>
        public async Task<BaseResponse<PatientResponse>> GetPatientByCodeAsync(string patientCode)
        {
            const string methodName = nameof(GetPatientByCodeAsync);
            _logger.LogInformation("{MethodName} called with patientCode: {PatientCode}", methodName, patientCode);

            try
            {
                if (string.IsNullOrWhiteSpace(patientCode))
                {
                    return BaseResponse<PatientResponse>.CreateError("Patient code cannot be empty", StatusCodes.Status400BadRequest, "PATIENT_008");
                }

                var patient = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(p => p.Account)
                    .Where(p => p.PatientCode == patientCode && !p.IsDeleted)
                    .FirstOrDefaultAsync();

                if (patient == null)
                {
                    return BaseResponse<PatientResponse>.CreateError("Patient not found", StatusCodes.Status404NotFound, "PATIENT_007");
                }

                // Authorization: Patients can only retrieve their own record by code
                if (IsCurrentUserInRole("Patient"))
                {
                    var currentAccountId = GetCurrentAccountId();
                    if (currentAccountId == null)
                    {
                        _logger.LogWarning("{MethodName}: Unauthorized - No account ID found in token", methodName);
                        return BaseResponse<PatientResponse>.CreateError(
                            "Unauthorized: You must be logged in",
                            StatusCodes.Status401Unauthorized,
                            "PATIENT_401");
                    }

                    var currentPatientResult = await GetPatientByAccountIdAsync(currentAccountId.Value);
                    if (!currentPatientResult.Success || currentPatientResult.Data == null)
                    {
                        _logger.LogWarning("{MethodName}: Current user is not a patient", methodName);
                        return BaseResponse<PatientResponse>.CreateError(
                            "Unauthorized: Only patients can access this resource",
                            StatusCodes.Status403Forbidden,
                            "PATIENT_403");
                    }

                    var currentPatientId = currentPatientResult.Data.Id;
                    if (patient.Id != currentPatientId)
                    {
                        _logger.LogWarning("{MethodName}: Patient {CurrentPatientId} attempted to access patient by code for patient {PatientId}", methodName, currentPatientId, patient.Id);
                        return BaseResponse<PatientResponse>.CreateError(
                            "Unauthorized: You can only view your own patient information",
                            StatusCodes.Status403Forbidden,
                            "PATIENT_403_FORBIDDEN");
                    }
                }

                var response = _mapper.Map<PatientResponse>(patient);
                return BaseResponse<PatientResponse>.CreateSuccess(response, "Patient retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error retrieving patient by code: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<PatientResponse>.CreateError($"An error occurred while retrieving patient: {ex.Message}", StatusCodes.Status500InternalServerError, "PATIENT_500");
            }
        }

        /// <summary>
        /// Gets a patient by national ID
        /// </summary>
        public async Task<BaseResponse<PatientResponse>> GetPatientByNationalIdAsync(string nationalId)
        {
            const string methodName = nameof(GetPatientByNationalIdAsync);
            _logger.LogInformation("{MethodName} called with nationalId: {NationalId}", methodName, nationalId);

            try
            {
                if (string.IsNullOrWhiteSpace(nationalId))
                {
                    return BaseResponse<PatientResponse>.CreateError("National ID cannot be empty", StatusCodes.Status400BadRequest, "PATIENT_009");
                }

                var patient = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(p => p.Account)
                    .Where(p => p.NationalID == nationalId && !p.IsDeleted)
                    .FirstOrDefaultAsync();

                if (patient == null)
                {
                    return BaseResponse<PatientResponse>.CreateError("Patient not found", StatusCodes.Status404NotFound, "PATIENT_007");
                }

                // Authorization: Patients can only retrieve their own record by national ID
                if (IsCurrentUserInRole("Patient"))
                {
                    var currentAccountId = GetCurrentAccountId();
                    if (currentAccountId == null)
                    {
                        _logger.LogWarning("{MethodName}: Unauthorized - No account ID found in token", methodName);
                        return BaseResponse<PatientResponse>.CreateError(
                            "Unauthorized: You must be logged in",
                            StatusCodes.Status401Unauthorized,
                            "PATIENT_401");
                    }

                    var currentPatientResult = await GetPatientByAccountIdAsync(currentAccountId.Value);
                    if (!currentPatientResult.Success || currentPatientResult.Data == null)
                    {
                        _logger.LogWarning("{MethodName}: Current user is not a patient", methodName);
                        return BaseResponse<PatientResponse>.CreateError(
                            "Unauthorized: Only patients can access this resource",
                            StatusCodes.Status403Forbidden,
                            "PATIENT_403");
                    }

                    var currentPatientId = currentPatientResult.Data.Id;
                    if (patient.Id != currentPatientId)
                    {
                        _logger.LogWarning("{MethodName}: Patient {CurrentPatientId} attempted to access patient by national ID for patient {PatientId}", methodName, currentPatientId, patient.Id);
                        return BaseResponse<PatientResponse>.CreateError(
                            "Unauthorized: You can only view your own patient information",
                            StatusCodes.Status403Forbidden,
                            "PATIENT_403_FORBIDDEN");
                    }
                }

                var response = _mapper.Map<PatientResponse>(patient);
                return BaseResponse<PatientResponse>.CreateSuccess(response, "Patient retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error retrieving patient by national ID: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<PatientResponse>.CreateError($"An error occurred while retrieving patient: {ex.Message}", StatusCodes.Status500InternalServerError, "PATIENT_500");
            }
        }

        /// <summary>
        /// Gets a patient by account ID
        /// </summary>
        public async Task<BaseResponse<PatientResponse>> GetPatientByAccountIdAsync(Guid accountId)
        {
            const string methodName = nameof(GetPatientByAccountIdAsync);
            _logger.LogInformation("{MethodName} called with accountId: {AccountId}", methodName, accountId);

            try
            {
                if (accountId == Guid.Empty)
                {
                    return BaseResponse<PatientResponse>.CreateError("Account ID cannot be empty", StatusCodes.Status400BadRequest, "PATIENT_010");
                }

                // Authorization: Patients can only retrieve their own record by account ID
                if (IsCurrentUserInRole("Patient"))
                {
                    var currentAccountId = GetCurrentAccountId();
                    if (currentAccountId == null)
                    {
                        _logger.LogWarning("{MethodName}: Unauthorized - No account ID found in token", methodName);
                        return BaseResponse<PatientResponse>.CreateError(
                            "Unauthorized: You must be logged in",
                            StatusCodes.Status401Unauthorized,
                            "PATIENT_401");
                    }

                    if (accountId != currentAccountId.Value)
                    {
                        _logger.LogWarning("{MethodName}: Patient with account {CurrentAccountId} attempted to access patient by account {AccountId}", methodName, currentAccountId, accountId);
                        return BaseResponse<PatientResponse>.CreateError(
                            "Unauthorized: You can only view your own patient information",
                            StatusCodes.Status403Forbidden,
                            "PATIENT_403_FORBIDDEN");
                    }
                }

                var patient = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(p => p.Account)
                    .Where(p => p.Id == accountId && !p.IsDeleted)
                    .FirstOrDefaultAsync();

                if (patient == null)
                {
                    return BaseResponse<PatientResponse>.CreateError("Patient not found", StatusCodes.Status404NotFound, "PATIENT_007");
                }

                var response = _mapper.Map<PatientResponse>(patient);
                return BaseResponse<PatientResponse>.CreateSuccess(response, "Patient retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error retrieving patient by account ID: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<PatientResponse>.CreateError($"An error occurred while retrieving patient: {ex.Message}", StatusCodes.Status500InternalServerError, "PATIENT_500");
            }
        }

        /// <summary>
        /// Gets all patients with pagination and filtering
        /// </summary>
        public async Task<DynamicResponse<PatientResponse>> GetAllPatientsAsync(GetPatientsRequest request)
        {
            const string methodName = nameof(GetAllPatientsAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                // Normalize pagination parameters
                request.Normalize();

                var query = _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(p => p.Account)
                    .Where(p => !p.IsDeleted);

                // Apply filters
                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    var searchTerm = request.SearchTerm.ToLower();
                    query = query.Where(p =>
                        p.PatientCode.ToLower().Contains(searchTerm) ||
                        p.NationalID.ToLower().Contains(searchTerm) ||
                        (p.Account != null && p.Account.Username != null && p.Account.Username.ToLower().Contains(searchTerm)) ||
                        (p.Account != null && p.Account.Email.ToLower().Contains(searchTerm)) ||
                        (p.EmergencyContact != null && p.EmergencyContact.ToLower().Contains(searchTerm)) ||
                        (p.Occupation != null && p.Occupation.ToLower().Contains(searchTerm)));
                }

                if (!string.IsNullOrWhiteSpace(request.PatientCode))
                {
                    query = query.Where(p => p.PatientCode.Contains(request.PatientCode));
                }

                if (!string.IsNullOrWhiteSpace(request.NationalID))
                {
                    query = query.Where(p => p.NationalID.Contains(request.NationalID));
                }

                if (!string.IsNullOrWhiteSpace(request.BloodType))
                {
                    query = query.Where(p => p.BloodType == request.BloodType);
                }

                if (request.IsActive.HasValue)
                {
                    query = query.Where(p => p.IsActive == request.IsActive.Value);
                }

                if (request.HasInsurance.HasValue)
                {
                    if (request.HasInsurance.Value)
                    {
                        query = query.Where(p => !string.IsNullOrEmpty(p.Insurance));
                    }
                    else
                    {
                        query = query.Where(p => string.IsNullOrEmpty(p.Insurance));
                    }
                }

                if (request.MinHeight.HasValue)
                {
                    query = query.Where(p => p.Height >= request.MinHeight.Value);
                }

                if (request.MaxHeight.HasValue)
                {
                    query = query.Where(p => p.Height <= request.MaxHeight.Value);
                }

                if (request.MinWeight.HasValue)
                {
                    query = query.Where(p => p.Weight >= request.MinWeight.Value);
                }

                if (request.MaxWeight.HasValue)
                {
                    query = query.Where(p => p.Weight <= request.MaxWeight.Value);
                }

                if (request.CreatedFrom.HasValue)
                {
                    query = query.Where(p => p.CreatedAt >= request.CreatedFrom.Value);
                }

                if (request.CreatedTo.HasValue)
                {
                    query = query.Where(p => p.CreatedAt <= request.CreatedTo.Value);
                }

                // Apply sorting
                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    switch (request.Sort.ToLower())
                    {
                        case "patientcode":
                            query = request.Order?.ToLower() == "desc"
                                ? query.OrderByDescending(p => p.PatientCode)
                                : query.OrderBy(p => p.PatientCode);
                            break;
                        case "nationalid":
                            query = request.Order?.ToLower() == "desc"
                                ? query.OrderByDescending(p => p.NationalID)
                                : query.OrderBy(p => p.NationalID);
                            break;
                        case "createdat":
                            query = request.Order?.ToLower() == "desc"
                                ? query.OrderByDescending(p => p.CreatedAt)
                                : query.OrderBy(p => p.CreatedAt);
                            break;
                        case "updatedat":
                            query = request.Order?.ToLower() == "desc"
                                ? query.OrderByDescending(p => p.UpdatedAt)
                                : query.OrderBy(p => p.UpdatedAt);
                            break;
                        default:
                            query = query.OrderByDescending(p => p.CreatedAt);
                            break;
                    }
                }
                else
                {
                    query = query.OrderByDescending(p => p.CreatedAt);
                }

                // Get total count
                var totalCount = await query.CountAsync();

                // Apply pagination
                var patients = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                var responses = _mapper.Map<List<PatientResponse>>(patients);

                var pagingMetaData = new PagingMetaData
                {
                    Page = request.Page,
                    Size = request.Size,
                    Total = totalCount,
                    CurrentPageSize = responses.Count
                };

                return new DynamicResponse<PatientResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Patients retrieved successfully",
                    Data = responses,
                    MetaData = pagingMetaData
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error retrieving patients: {ErrorMessage}", methodName, ex.Message);
                return new DynamicResponse<PatientResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred while retrieving patients: {ex.Message}",
                    SystemCode = "PATIENT_500"
                };
            }
        }

        /// <summary>
        /// Updates an existing patient
        /// </summary>
        public async Task<BaseResponse<PatientResponse>> UpdatePatientAsync(Guid patientId, UpdatePatientRequest request)
        {
            const string methodName = nameof(UpdatePatientAsync);
            _logger.LogInformation("{MethodName} called with patientId: {PatientId}, request: {@Request}", methodName, patientId, request);

            try
            {
                // Authorization: Patients can only update their own information
                if (IsCurrentUserInRole("Patient"))
                {
                    var currentAccountId = GetCurrentAccountId();
                    if (currentAccountId == null)
                    {
                        _logger.LogWarning("{MethodName}: Unauthorized - No account ID found in token", methodName);
                        return BaseResponse<PatientResponse>.CreateError(
                            "Unauthorized: You must be logged in",
                            StatusCodes.Status401Unauthorized,
                            "PATIENT_401");
                    }

                    var currentPatientResult = await GetPatientByAccountIdAsync(currentAccountId.Value);
                    if (!currentPatientResult.Success || currentPatientResult.Data == null)
                    {
                        _logger.LogWarning("{MethodName}: Current user is not a patient", methodName);
                        return BaseResponse<PatientResponse>.CreateError(
                            "Unauthorized: Only patients can access this resource",
                            StatusCodes.Status403Forbidden,
                            "PATIENT_403");
                    }

                    var currentPatientId = currentPatientResult.Data.Id;
                    if (patientId != currentPatientId)
                    {
                        _logger.LogWarning("{MethodName}: Patient {CurrentPatientId} attempted to update patient {PatientId}", methodName, currentPatientId, patientId);
                        return BaseResponse<PatientResponse>.CreateError(
                            "Unauthorized: You can only update your own patient information",
                            StatusCodes.Status403Forbidden,
                            "PATIENT_403_FORBIDDEN");
                    }
                }

                if (patientId == Guid.Empty)
                {
                    return BaseResponse<PatientResponse>.CreateError("Patient ID cannot be empty", StatusCodes.Status400BadRequest, "PATIENT_006");
                }

                if (request == null)
                {
                    return BaseResponse<PatientResponse>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "PATIENT_001");
                }

                var existingPatient = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Where(p => p.Id == patientId && !p.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingPatient == null)
                {
                    return BaseResponse<PatientResponse>.CreateError("Patient not found", StatusCodes.Status404NotFound, "PATIENT_007");
                }

                // Check for unique constraints if values are being updated
                if (!string.IsNullOrWhiteSpace(request.PatientCode) && request.PatientCode != existingPatient.PatientCode)
                {
                    var existingByCode = await _unitOfWork.Repository<Patient>()
                        .AsQueryable()
                        .Where(p => p.PatientCode == request.PatientCode && p.Id != patientId && !p.IsDeleted)
                        .FirstOrDefaultAsync();

                    if (existingByCode != null)
                    {
                        return BaseResponse<PatientResponse>.CreateError("Patient code already exists", StatusCodes.Status409Conflict, "PATIENT_002");
                    }
                }

                if (!string.IsNullOrWhiteSpace(request.NationalID) && request.NationalID != existingPatient.NationalID)
                {
                    var existingByNationalId = await _unitOfWork.Repository<Patient>()
                        .AsQueryable()
                        .Where(p => p.NationalID == request.NationalID && p.Id != patientId && !p.IsDeleted)
                        .FirstOrDefaultAsync();

                    if (existingByNationalId != null)
                    {
                        return BaseResponse<PatientResponse>.CreateError("National ID already exists", StatusCodes.Status409Conflict, "PATIENT_003");
                    }
                }

                // Update patient properties
                _mapper.Map(request, existingPatient);
                existingPatient.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Patient>().UpdateGuid(existingPatient, patientId);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName} - Patient updated successfully with ID: {PatientId}", methodName, patientId);

                // Get updated patient with account info
                var updatedPatient = await GetPatientWithAccountAsync(patientId);
                var response = _mapper.Map<PatientResponse>(updatedPatient);

                return BaseResponse<PatientResponse>.CreateSuccess(response, "Patient updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error updating patient: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<PatientResponse>.CreateError($"An error occurred while updating patient: {ex.Message}", StatusCodes.Status500InternalServerError, "PATIENT_500");
            }
        }

        /// <summary>
        /// Replaces existing patients with the provided information (all fields at once, except secure properties)
        /// </summary>
        public async Task<BaseResponse<List<PatientResponse>>> UpdatePatientFullAsync(UpdatePatientFullRequest request)
        {
            const string methodName = nameof(UpdatePatientFullAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (request == null)
                {
                    return BaseResponse<List<PatientResponse>>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "PATIENT_001");
                }

                var requestedIds = request.PatientIds?
                    .Where(id => id != Guid.Empty)
                    .Distinct()
                    .ToList() ?? new List<Guid>();

                var updateAllPatients = !requestedIds.Any();

                if (IsCurrentUserInRole("Patient"))
                {
                    var currentAccountId = GetCurrentAccountId();
                    if (currentAccountId == null)
                    {
                        _logger.LogWarning("{MethodName}: Unauthorized - No account ID found in token", methodName);
                        return BaseResponse<List<PatientResponse>>.CreateError(
                            "Unauthorized: You must be logged in",
                            StatusCodes.Status401Unauthorized,
                            "PATIENT_401");
                    }

                    var currentPatientResult = await GetPatientByAccountIdAsync(currentAccountId.Value);
                    if (!currentPatientResult.Success || currentPatientResult.Data == null)
                    {
                        _logger.LogWarning("{MethodName}: Current user is not a patient", methodName);
                        return BaseResponse<List<PatientResponse>>.CreateError(
                            "Unauthorized: Only patients can access this resource",
                            StatusCodes.Status403Forbidden,
                            "PATIENT_403");
                    }

                    var currentPatientId = currentPatientResult.Data.Id;
                    if (!requestedIds.Any())
                    {
                        requestedIds.Add(currentPatientId);
                    }

                    if (requestedIds.Count != 1 || requestedIds[0] != currentPatientId)
                    {
                        _logger.LogWarning("{MethodName}: Patient {CurrentPatientId} attempted to fully update patients {PatientIds}", methodName, currentPatientId, requestedIds);
                        return BaseResponse<List<PatientResponse>>.CreateError(
                            "Unauthorized: Patients can only update their own information",
                            StatusCodes.Status403Forbidden,
                            "PATIENT_403_FORBIDDEN");
                    }

                    updateAllPatients = false;
                }

                var patientQuery = _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Where(p => !p.IsDeleted);

                if (!updateAllPatients)
                {
                    patientQuery = patientQuery.Where(p => requestedIds.Contains(p.Id));
                }

                var patientsToUpdate = await patientQuery.ToListAsync();

                if (!patientsToUpdate.Any())
                {
                    _logger.LogWarning("{MethodName}: No patients matched provided criteria", methodName);
                    return BaseResponse<List<PatientResponse>>.CreateError(
                        "No patients found for update",
                        StatusCodes.Status404NotFound,
                        "PATIENT_007");
                }

                if (!updateAllPatients)
                {
                    var missingIds = requestedIds.Except(patientsToUpdate.Select(p => p.Id)).ToList();
                    if (missingIds.Any())
                    {
                        _logger.LogWarning("{MethodName}: Some patient IDs were not found: {MissingIds}", methodName, missingIds);
                        return BaseResponse<List<PatientResponse>>.CreateError(
                            $"Patient(s) not found: {string.Join(", ", missingIds)}",
                            StatusCodes.Status404NotFound,
                            "PATIENT_007");
                    }
                }
                else
                {
                    requestedIds = patientsToUpdate.Select(p => p.Id).ToList();
                }

                foreach (var patient in patientsToUpdate)
                {
                    var patientCode = patient.PatientCode;
                    var nationalId = patient.NationalID;

                    _mapper.Map(request, patient);

                    patient.PatientCode = patientCode;
                    patient.NationalID = nationalId;
                    patient.UpdatedAt = DateTime.UtcNow;
                }

                _unitOfWork.Repository<Patient>().UpdateRange(patientsToUpdate.AsQueryable());
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                var updatedPatients = await GetPatientsWithAccountAsync(requestedIds);
                var responseData = _mapper.Map<List<PatientResponse>>(updatedPatients);

                _logger.LogInformation("{MethodName} - Fully updated {Count} patient(s): {PatientIds}", methodName, responseData.Count, requestedIds);

                var successMessage = responseData.Count == 1
                    ? "Patient updated successfully"
                    : $"Successfully updated {responseData.Count} patients";

                return BaseResponse<List<PatientResponse>>.CreateSuccess(responseData, successMessage);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "{MethodName} - Error fully updating patients: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<List<PatientResponse>>.CreateError($"An error occurred while updating patients: {ex.Message}", StatusCodes.Status500InternalServerError, "PATIENT_500");
            }
        }

        /// <summary>
        /// Updates patient status (active/inactive)
        /// </summary>
        public async Task<BaseResponse<PatientResponse>> UpdatePatientStatusAsync(Guid patientId, UpdatePatientStatusRequest request)
        {
            const string methodName = nameof(UpdatePatientStatusAsync);
            _logger.LogInformation("{MethodName} called with patientId: {PatientId}, request: {@Request}", methodName, patientId, request);

            try
            {
                if (patientId == Guid.Empty)
                {
                    return BaseResponse<PatientResponse>.CreateError("Patient ID cannot be empty", StatusCodes.Status400BadRequest, "PATIENT_006");
                }

                if (request == null)
                {
                    return BaseResponse<PatientResponse>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "PATIENT_001");
                }

                var existingPatient = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Where(p => p.Id == patientId && !p.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingPatient == null)
                {
                    return BaseResponse<PatientResponse>.CreateError("Patient not found", StatusCodes.Status404NotFound, "PATIENT_007");
                }

                existingPatient.IsActive = request.IsActive;
                existingPatient.UpdatedAt = DateTime.UtcNow;

                if (!string.IsNullOrWhiteSpace(request.Reason))
                {
                    existingPatient.Notes = string.IsNullOrWhiteSpace(existingPatient.Notes)
                        ? $"Status changed: {request.Reason}"
                        : $"{existingPatient.Notes}\nStatus changed: {request.Reason}";
                }

                await _unitOfWork.Repository<Patient>().UpdateGuid(existingPatient, patientId);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName} - Patient status updated successfully with ID: {PatientId}", methodName, patientId);

                // Get updated patient with account info
                var updatedPatient = await GetPatientWithAccountAsync(patientId);
                var response = _mapper.Map<PatientResponse>(updatedPatient);

                return BaseResponse<PatientResponse>.CreateSuccess(response, "Patient status updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error updating patient status: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<PatientResponse>.CreateError($"An error occurred while updating patient status: {ex.Message}", StatusCodes.Status500InternalServerError, "PATIENT_500");
            }
        }

        /// <summary>
        /// Soft deletes a patient
        /// </summary>
        public async Task<BaseResponse> DeletePatientAsync(Guid patientId)
        {
            const string methodName = nameof(DeletePatientAsync);
            _logger.LogInformation("{MethodName} called with patientId: {PatientId}", methodName, patientId);

            try
            {
                if (patientId == Guid.Empty)
                {
                    return BaseResponse.CreateError("Patient ID cannot be empty", StatusCodes.Status400BadRequest, "PATIENT_006");
                }

                var existingPatient = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Where(p => p.Id == patientId && !p.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingPatient == null)
                {
                    return BaseResponse.CreateError("Patient not found", StatusCodes.Status404NotFound, "PATIENT_007");
                }

                // Check for active relationships
                var activeRelationships = await _unitOfWork.Repository<Relationship>()
                    .AsQueryable()
                    .Where(r => (r.Patient1Id == patientId || r.Patient2Id == patientId) && r.IsActive && !r.IsDeleted)
                    .CountAsync();

                if (activeRelationships > 0)
                {
                    return BaseResponse.CreateError("Cannot delete patient with active relationships", StatusCodes.Status409Conflict, "PATIENT_011");
                }

                existingPatient.IsDeleted = true;
                existingPatient.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Patient>().UpdateGuid(existingPatient, patientId);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName} - Patient deleted successfully with ID: {PatientId}", methodName, patientId);

                return BaseResponse.CreateSuccess("Patient deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error deleting patient: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse.CreateError($"An error occurred while deleting patient: {ex.Message}", StatusCodes.Status500InternalServerError, "PATIENT_500");
            }
        }

        #endregion

        #region Relationship CRUD Operations

        /// <summary>
        /// Creates a new relationship between patients with email confirmation workflow
        /// Uses PatientCode for easier partner identification
        /// </summary>
        public async Task<BaseResponse<RelationshipResponse>> CreateRelationshipAsync(CreateRelationshipRequest request)
        {
            const string methodName = nameof(CreateRelationshipAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                // 1. Input validation
                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<RelationshipResponse>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "RELATIONSHIP_001");
                }

                // Validate and sanitize Patient2Code
                if (string.IsNullOrWhiteSpace(request.Patient2Code))
                {
                    _logger.LogWarning("{MethodName}: Patient2Code is empty", methodName);
                    return BaseResponse<RelationshipResponse>.CreateError("Partner's patient code is required", StatusCodes.Status400BadRequest, "RELATIONSHIP_001_CODE");
                }

                var patient2Code = request.Patient2Code.Trim();

                // 2. Authorization check - Only Patient can create their own relationship requests
                var currentAccountId = GetCurrentAccountId();
                if (currentAccountId == null)
                {
                    _logger.LogWarning("{MethodName}: Unauthorized - No account ID found in token", methodName);
                    return BaseResponse<RelationshipResponse>.CreateError("Unauthorized: You must be logged in to create relationship requests", StatusCodes.Status401Unauthorized, "RELATIONSHIP_401");
                }

                // Get current patient by account ID
                var currentPatientResult = await GetPatientByAccountIdAsync(currentAccountId.Value);
                if (!currentPatientResult.Success || currentPatientResult.Data == null)
                {
                    _logger.LogWarning("{MethodName}: Current user is not a patient", methodName);
                    return BaseResponse<RelationshipResponse>.CreateError("Unauthorized: Only patients can create relationship requests", StatusCodes.Status403Forbidden, "RELATIONSHIP_403");
                }

                var currentPatientId = currentPatientResult.Data.Id;
                var currentPatientCode = currentPatientResult.Data.PatientCode;

                // 3. Verify both patients exist with account information
                // Patient1 is the current logged-in patient
                var patient1 = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Include(p => p.Account)
                    .Where(p => p.Id == currentPatientId && !p.IsDeleted)
                    .FirstOrDefaultAsync();

                // Patient2 is found by PatientCode from request
                var patient2 = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Include(p => p.Account)
                    .Where(p => p.PatientCode == patient2Code && !p.IsDeleted)
                    .FirstOrDefaultAsync();

                if (patient1 == null)
                {
                    _logger.LogWarning("{MethodName}: Current patient not found with ID: {PatientId}", methodName, currentPatientId);
                    return BaseResponse<RelationshipResponse>.CreateError("Current patient not found", StatusCodes.Status404NotFound, "RELATIONSHIP_003_PATIENT1");
                }

                if (patient2 == null)
                {
                    _logger.LogWarning("{MethodName}: Partner patient not found with code: {PatientCode}", methodName, patient2Code);
                    return BaseResponse<RelationshipResponse>.CreateError($"Partner patient with code '{patient2Code}' not found", StatusCodes.Status404NotFound, "RELATIONSHIP_003_PATIENT2");
                }

                // Verify patient is not trying to create relationship with themselves
                if (patient1.Id == patient2.Id || currentPatientCode.Equals(patient2Code, StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogWarning("{MethodName}: Patient {PatientCode} attempted to create relationship with themselves", methodName, currentPatientCode);
                    return BaseResponse<RelationshipResponse>.CreateError("You cannot create a relationship with yourself", StatusCodes.Status400BadRequest, "RELATIONSHIP_002");
                }

                if (patient1.Account == null || patient2.Account == null)
                {
                    _logger.LogWarning("{MethodName}: One or both patients do not have account information", methodName);
                    return BaseResponse<RelationshipResponse>.CreateError("Patient account information is missing", StatusCodes.Status404NotFound, "RELATIONSHIP_003_ACCOUNT");
                }

                // 4. Check each patient can only have ONE relationship 
                var patient1ValidationResult = await ValidatePatientHasNoExistingRelationshipAsync(patient1.Id, patient1);
                if (!patient1ValidationResult.IsValid)
                {
                    _logger.LogWarning("{MethodName}: Patient1 ({PatientCode}) validation failed - {Error}", methodName, currentPatientCode, patient1ValidationResult.ErrorMessage);
                    return BaseResponse<RelationshipResponse>.CreateError(patient1ValidationResult.ErrorMessage, StatusCodes.Status409Conflict, patient1ValidationResult.ErrorCode);
                }

                var patient2ValidationResult = await ValidatePatientHasNoExistingRelationshipAsync(patient2.Id, patient2);
                if (!patient2ValidationResult.IsValid)
                {
                    _logger.LogWarning("{MethodName}: Patient2 ({PatientCode}) validation failed - {Error}", methodName, patient2Code, patient2ValidationResult.ErrorMessage);
                    return BaseResponse<RelationshipResponse>.CreateError(patient2ValidationResult.ErrorMessage, StatusCodes.Status409Conflict, patient2ValidationResult.ErrorCode);
                }

                // 5. Patient genders and relationship type
                var businessRulesValidationResult = await ValidateRelationshipBusinessRulesAsync(patient1, patient2, request.RelationshipType);
                if (!businessRulesValidationResult.IsValid)
                {
                    _logger.LogWarning("{MethodName}: Business rule validation failed - {Error}", methodName, businessRulesValidationResult.ErrorMessage);
                    return BaseResponse<RelationshipResponse>.CreateError(businessRulesValidationResult.ErrorMessage, StatusCodes.Status400BadRequest, businessRulesValidationResult.ErrorCode);
                }

                // 6. Check for existing pending/approved relationship between these two specific patients
                var existingRelationship = await _unitOfWork.Repository<Relationship>()
                    .AsQueryable()
                    .Where(r => ((r.Patient1Id == patient1.Id && r.Patient2Id == patient2.Id) ||
                                (r.Patient1Id == patient2.Id && r.Patient2Id == patient1.Id)) &&
                               r.RelationshipType == request.RelationshipType && !r.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingRelationship != null)
                {
                    if (existingRelationship.Status == RelationshipStatus.Pending)
                    {
                        _logger.LogWarning("{MethodName}: Relationship request already pending between {Patient1Code} and {Patient2Code}", methodName, currentPatientCode, patient2Code);
                        return BaseResponse<RelationshipResponse>.CreateError("A pending relationship request already exists between these patients", StatusCodes.Status409Conflict, "RELATIONSHIP_004_PENDING");
                    }
                    else if (existingRelationship.Status == RelationshipStatus.Approved)
                    {
                        _logger.LogWarning("{MethodName}: Relationship already approved between {Patient1Code} and {Patient2Code}", methodName, currentPatientCode, patient2Code);
                        return BaseResponse<RelationshipResponse>.CreateError("Relationship already exists and is approved between these patients", StatusCodes.Status409Conflict, "RELATIONSHIP_004_APPROVED");
                    }
                }

                // 7. Create relationship with Pending status
                // Generate secure approval token for email-based verification
                var approvalToken = GenerateSecureToken();

                var relationship = new Relationship(
                    Guid.NewGuid(),
                    patient1.Id,
                    patient2.Id,
                    request.RelationshipType
                )
                {
                    EstablishedDate = request.EstablishedDate,
                    Notes = request.Notes,
                    IsActive = request.IsActive,
                    Status = RelationshipStatus.Pending,
                    RequestedBy = currentPatientId,
                    ExpiresAt = DateTime.UtcNow.AddDays(7), // 7-day expiration for pending requests
                    ApprovalToken = approvalToken
                };

                await _unitOfWork.Repository<Relationship>().InsertAsync(relationship);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Relationship request created successfully between {Patient1Code} and {Patient2Code}, RelationshipId: {RelationshipId}", 
                    methodName, currentPatientCode, patient2Code, relationship.Id);

                // 8. Send email confirmation to Patient2
                try
                {
                    await SendRelationshipConfirmationEmailAsync(relationship, patient1, patient2);
                    _logger.LogInformation("{MethodName}: Confirmation email sent to {Email}", methodName, patient2.Account.Email);
                }
                catch (Exception emailEx)
                {
                    // Log email error but don't fail the request
                    _logger.LogError(emailEx, "{MethodName}: Failed to send confirmation email to {Email}", methodName, patient2.Account.Email);
                    // Continue - relationship is created, email can be retried later
                }

                await TryCreateRelationshipNotificationAsync(relationship, patient1, patient2);

                // 9. Return response
                var createdRelationship = await GetRelationshipWithPatientsAsync(relationship.Id);
                var response = _mapper.Map<RelationshipResponse>(createdRelationship);
                HydrateRelationshipResponseFullName(response, createdRelationship);

                return BaseResponse<RelationshipResponse>.CreateSuccess(response, "Relationship request created successfully. A confirmation email has been sent to the recipient.", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating relationship: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<RelationshipResponse>.CreateError($"An error occurred while creating relationship: {ex.Message}", StatusCodes.Status500InternalServerError, "RELATIONSHIP_500");
            }
        }

        /// <summary>
        /// Gets a relationship by ID
        /// </summary>
        public async Task<BaseResponse<RelationshipResponse>> GetRelationshipByIdAsync(Guid relationshipId)
        {
            const string methodName = nameof(GetRelationshipByIdAsync);
            _logger.LogInformation("{MethodName} called with relationshipId: {RelationshipId}", methodName, relationshipId);

            try
            {
                if (relationshipId == Guid.Empty)
                {
                    return BaseResponse<RelationshipResponse>.CreateError("Relationship ID cannot be empty", StatusCodes.Status400BadRequest, "RELATIONSHIP_005");
                }

                var relationship = await GetRelationshipWithPatientsAsync(relationshipId);
                if (relationship == null)
                {
                    return BaseResponse<RelationshipResponse>.CreateError("Relationship not found", StatusCodes.Status404NotFound, "RELATIONSHIP_006");
                }

                // Authorization check - If user is Patient, they can only view their own relationships
                if (IsCurrentUserInRole("Patient"))
                {
                    var currentAccountId = GetCurrentAccountId();
                    if (currentAccountId == null)
                    {
                        _logger.LogWarning("{MethodName}: Unauthorized - No account ID found in token", methodName);
                        return BaseResponse<RelationshipResponse>.CreateError("Unauthorized: You must be logged in", StatusCodes.Status401Unauthorized, "RELATIONSHIP_401");
                    }

                    var currentPatientResult = await GetPatientByAccountIdAsync(currentAccountId.Value);
                    if (!currentPatientResult.Success || currentPatientResult.Data == null)
                    {
                        _logger.LogWarning("{MethodName}: Current user is not a patient", methodName);
                        return BaseResponse<RelationshipResponse>.CreateError("Unauthorized: Only patients can access this resource", StatusCodes.Status403Forbidden, "RELATIONSHIP_403");
                    }

                    var currentPatientId = currentPatientResult.Data.Id;
                    // Check if the relationship belongs to the current patient
                    if (relationship.Patient1Id != currentPatientId && relationship.Patient2Id != currentPatientId)
                    {
                        _logger.LogWarning("{MethodName}: Patient {CurrentPatientId} attempted to access relationship {RelationshipId} that does not belong to them", methodName, currentPatientId, relationshipId);
                        return BaseResponse<RelationshipResponse>.CreateError("Unauthorized: You can only view your own relationships", StatusCodes.Status403Forbidden, "RELATIONSHIP_403_FORBIDDEN");
                    }
                }

                var response = _mapper.Map<RelationshipResponse>(relationship);
                HydrateRelationshipResponseFullName(response, relationship);
                return BaseResponse<RelationshipResponse>.CreateSuccess(response, "Relationship retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error retrieving relationship: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<RelationshipResponse>.CreateError($"An error occurred while retrieving relationship: {ex.Message}", StatusCodes.Status500InternalServerError, "RELATIONSHIP_500");
            }
        }

        /// <summary>
        /// Gets all relationships with pagination and filtering
        /// </summary>
        public async Task<DynamicResponse<RelationshipResponse>> GetAllRelationshipsAsync(GetRelationshipsRequest request)
        {
            const string methodName = nameof(GetAllRelationshipsAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                // Normalize pagination parameters
                request.Normalize();

                var query = _unitOfWork.Repository<Relationship>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(r => r.Patient1)
                        .ThenInclude(p => p.Account)
                    .Include(r => r.Patient2)
                        .ThenInclude(p => p.Account)
                    .Where(r => !r.IsDeleted);

                // Apply filters
                if (request.PatientId.HasValue)
                {
                    query = query.Where(r => r.Patient1Id == request.PatientId.Value || r.Patient2Id == request.PatientId.Value);
                }

                if (request.RelationshipType.HasValue)
                {
                    query = query.Where(r => r.RelationshipType == request.RelationshipType.Value);
                }

                if (request.IsActive.HasValue)
                {
                    query = query.Where(r => r.IsActive == request.IsActive.Value);
                }

                if (request.EstablishedFrom.HasValue)
                {
                    query = query.Where(r => r.EstablishedDate >= request.EstablishedFrom.Value);
                }

                if (request.EstablishedTo.HasValue)
                {
                    query = query.Where(r => r.EstablishedDate <= request.EstablishedTo.Value);
                }

                if (request.CreatedFrom.HasValue)
                {
                    query = query.Where(r => r.CreatedAt >= request.CreatedFrom.Value);
                }

                if (request.CreatedTo.HasValue)
                {
                    query = query.Where(r => r.CreatedAt <= request.CreatedTo.Value);
                }

                // Apply sorting
                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    switch (request.Sort.ToLower())
                    {
                        case "relationshiptype":
                            query = request.Order?.ToLower() == "desc"
                                ? query.OrderByDescending(r => r.RelationshipType)
                                : query.OrderBy(r => r.RelationshipType);
                            break;
                        case "establisheddate":
                            query = request.Order?.ToLower() == "desc"
                                ? query.OrderByDescending(r => r.EstablishedDate)
                                : query.OrderBy(r => r.EstablishedDate);
                            break;
                        case "createdat":
                            query = request.Order?.ToLower() == "desc"
                                ? query.OrderByDescending(r => r.CreatedAt)
                                : query.OrderBy(r => r.CreatedAt);
                            break;
                        default:
                            query = query.OrderByDescending(r => r.CreatedAt);
                            break;
                    }
                }
                else
                {
                    query = query.OrderByDescending(r => r.CreatedAt);
                }

                // Get total count
                var totalCount = await query.CountAsync();

                // Apply pagination
                var relationships = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                var responses = _mapper.Map<List<RelationshipResponse>>(relationships);

                // Hydrate FullName for all relationship responses
                for (int i = 0; i < responses.Count && i < relationships.Count; i++)
                {
                    HydrateRelationshipResponseFullName(responses[i], relationships[i]);
                }

                var pagingMetaData = new PagingMetaData
                {
                    Page = request.Page,
                    Size = request.Size,
                    Total = totalCount,
                    CurrentPageSize = responses.Count
                };

                return new DynamicResponse<RelationshipResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Relationships retrieved successfully",
                    Data = responses,
                    MetaData = pagingMetaData
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error retrieving relationships: {ErrorMessage}", methodName, ex.Message);
                return new DynamicResponse<RelationshipResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred while retrieving relationships: {ex.Message}",
                    SystemCode = "RELATIONSHIP_500"
                };
            }
        }

        /// <summary>
        /// Gets relationships for a specific patient
        /// </summary>
        public async Task<DynamicResponse<RelationshipResponse>> GetPatientRelationshipsAsync(Guid patientId, GetRelationshipsRequest request)
        {
            const string methodName = nameof(GetPatientRelationshipsAsync);
            _logger.LogInformation("{MethodName} called with patientId: {PatientId}, request: {@Request}", methodName, patientId, request);

            try
            {
                if (patientId == Guid.Empty)
                {
                    return new DynamicResponse<RelationshipResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Patient ID cannot be empty",
                        SystemCode = "RELATIONSHIP_007"
                    };
                }

                // Authorization check - If user is Patient, they can only view their own relationships
                if (IsCurrentUserInRole("Patient"))
                {
                    var currentAccountId = GetCurrentAccountId();
                    if (currentAccountId == null)
                    {
                        _logger.LogWarning("{MethodName}: Unauthorized - No account ID found in token", methodName);
                        return new DynamicResponse<RelationshipResponse>
                        {
                            Code = StatusCodes.Status401Unauthorized,
                            Message = "Unauthorized: You must be logged in",
                            SystemCode = "RELATIONSHIP_401"
                        };
                    }

                    var currentPatientResult = await GetPatientByAccountIdAsync(currentAccountId.Value);
                    if (!currentPatientResult.Success || currentPatientResult.Data == null)
                    {
                        _logger.LogWarning("{MethodName}: Current user is not a patient", methodName);
                        return new DynamicResponse<RelationshipResponse>
                        {
                            Code = StatusCodes.Status403Forbidden,
                            Message = "Unauthorized: Only patients can access this resource",
                            SystemCode = "RELATIONSHIP_403"
                        };
                    }

                    var currentPatientId = currentPatientResult.Data.Id;
                    // Check if the requested patientId matches the current patient
                    if (patientId != currentPatientId)
                    {
                        _logger.LogWarning("{MethodName}: Patient {CurrentPatientId} attempted to access relationships for another patient {PatientId}", methodName, currentPatientId, patientId);
                        return new DynamicResponse<RelationshipResponse>
                        {
                            Code = StatusCodes.Status403Forbidden,
                            Message = "Unauthorized: You can only view your own relationships",
                            SystemCode = "RELATIONSHIP_403_FORBIDDEN"
                        };
                    }
                }

                request.PatientId = patientId;
                return await GetAllRelationshipsAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error retrieving patient relationships: {ErrorMessage}", methodName, ex.Message);
                return new DynamicResponse<RelationshipResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred while retrieving patient relationships: {ex.Message}",
                    SystemCode = "RELATIONSHIP_500"
                };
            }
        }

        /// <summary>
        /// Updates an existing relationship
        /// </summary>
        public async Task<BaseResponse<RelationshipResponse>> UpdateRelationshipAsync(Guid relationshipId, UpdateRelationshipRequest request)
        {
            const string methodName = nameof(UpdateRelationshipAsync);
            _logger.LogInformation("{MethodName} called with relationshipId: {RelationshipId}, request: {@Request}", methodName, relationshipId, request);

            try
            {
                if (relationshipId == Guid.Empty)
                {
                    return BaseResponse<RelationshipResponse>.CreateError("Relationship ID cannot be empty", StatusCodes.Status400BadRequest, "RELATIONSHIP_005");
                }

                if (request == null)
                {
                    return BaseResponse<RelationshipResponse>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "RELATIONSHIP_001");
                }

                var existingRelationship = await _unitOfWork.Repository<Relationship>()
                    .AsQueryable()
                    .Where(r => r.Id == relationshipId && !r.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingRelationship == null)
                {
                    return BaseResponse<RelationshipResponse>.CreateError("Relationship not found", StatusCodes.Status404NotFound, "RELATIONSHIP_006");
                }

                // Update relationship properties
                _mapper.Map(request, existingRelationship);
                existingRelationship.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Relationship>().UpdateGuid(existingRelationship, relationshipId);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName} - Relationship updated successfully with ID: {RelationshipId}", methodName, relationshipId);

                // Get updated relationship with patient info
                var updatedRelationship = await GetRelationshipWithPatientsAsync(relationshipId);
                var response = _mapper.Map<RelationshipResponse>(updatedRelationship);
                HydrateRelationshipResponseFullName(response, updatedRelationship);

                return BaseResponse<RelationshipResponse>.CreateSuccess(response, "Relationship updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error updating relationship: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<RelationshipResponse>.CreateError($"An error occurred while updating relationship: {ex.Message}", StatusCodes.Status500InternalServerError, "RELATIONSHIP_500");
            }
        }

        /// <summary>
        /// Soft deletes a relationship
        /// </summary>
        public async Task<BaseResponse> DeleteRelationshipAsync(Guid relationshipId)
        {
            const string methodName = nameof(DeleteRelationshipAsync);
            _logger.LogInformation("{MethodName} called with relationshipId: {RelationshipId}", methodName, relationshipId);

            try
            {
                // 1. Input validation
                if (relationshipId == Guid.Empty)
                {
                    return BaseResponse.CreateError("Relationship ID cannot be empty", StatusCodes.Status400BadRequest, "RELATIONSHIP_005");
                }

                // 2. Authorization check - Only involved patients or staff,admin can delete relationships
                if (IsCurrentUserInRole("Patient"))
                {
                    var currentAccountId = GetCurrentAccountId();
                    if (currentAccountId == null)
                    {
                        _logger.LogWarning("{MethodName}: Unauthorized - No account ID found in token", methodName);
                        return BaseResponse.CreateError("Unauthorized: You must be logged in", StatusCodes.Status401Unauthorized, "RELATIONSHIP_401");
                    }
                    var currentPatientResult = await GetPatientByAccountIdAsync(currentAccountId.Value);
                    if (!currentPatientResult.Success || currentPatientResult.Data == null)
                    {
                        _logger.LogWarning("{MethodName}: Current user is not a patient", methodName);
                        return BaseResponse.CreateError("Unauthorized: Only patients can access this resource", StatusCodes.Status403Forbidden, "RELATIONSHIP_403");
                    }
                    var currentPatientId = currentPatientResult.Data.Id;
                    var relationshipToCheck = await _unitOfWork.Repository<Relationship>()
                        .AsQueryable()
                        .Where(r => r.Id == relationshipId && !r.IsDeleted)
                        .FirstOrDefaultAsync();
                    if (relationshipToCheck == null ||
                        (relationshipToCheck.Patient1Id != currentPatientId && relationshipToCheck.Patient2Id != currentPatientId))
                    {
                        _logger.LogWarning("{MethodName}: Patient {CurrentPatientId} attempted to delete relationship {RelationshipId} that does not belong to them", methodName, currentPatientId, relationshipId);
                        return BaseResponse.CreateError("Unauthorized: You can only delete your own relationships", StatusCodes.Status403Forbidden, "RELATIONSHIP_403_FORBIDDEN");
                    }
                }

                // 3. Soft delete relationship
                var existingRelationship = await _unitOfWork.Repository<Relationship>()
                    .AsQueryable()
                    .Where(r => r.Id == relationshipId && !r.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingRelationship == null)
                {
                    return BaseResponse.CreateError("Relationship not found", StatusCodes.Status404NotFound, "RELATIONSHIP_006");
                }

                existingRelationship.IsDeleted = true;
                existingRelationship.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Relationship>().UpdateGuid(existingRelationship, relationshipId);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName} - Relationship deleted successfully with ID: {RelationshipId}", methodName, relationshipId);

                return BaseResponse.CreateSuccess("Relationship deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error deleting relationship: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse.CreateError($"An error occurred while deleting relationship: {ex.Message}", StatusCodes.Status500InternalServerError, "RELATIONSHIP_500");
            }
        }

        #endregion

        #region Search and Utility Operations

        /// <summary>
        /// Searches patients by various criteria
        /// </summary>
        public async Task<DynamicResponse<PatientSearchResult>> SearchPatientsAsync(string searchTerm, GetPatientsRequest request)
        {
            const string methodName = nameof(SearchPatientsAsync);
            _logger.LogInformation("{MethodName} called with searchTerm: {SearchTerm}", methodName, searchTerm);

            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return new DynamicResponse<PatientSearchResult>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Search term cannot be empty",
                        SystemCode = "PATIENT_012"
                    };
                }

                request.SearchTerm = searchTerm;
                var patientsResult = await GetAllPatientsAsync(request);

                var searchResults = patientsResult.Data?.Select(p =>
                {
                    var searchResult = _mapper.Map<PatientSearchResult>(p);

                    // Calculate relevance score and matched fields
                    var matchedFields = new List<string>();
                    var relevanceScore = 0.0;
                    var lowerSearchTerm = searchTerm.ToLower();

                    if (p.PatientCode.ToLower().Contains(lowerSearchTerm))
                    {
                        matchedFields.Add("PatientCode");
                        relevanceScore += 10.0;
                    }

                    if (p.NationalID.ToLower().Contains(lowerSearchTerm))
                    {
                        matchedFields.Add("NationalID");
                        relevanceScore += 9.0;
                    }

                    if (p.AccountInfo?.Email?.ToLower().Contains(lowerSearchTerm) == true)
                    {
                        matchedFields.Add("Email");
                        relevanceScore += 8.0;
                    }

                    if (p.AccountInfo?.Username?.ToLower().Contains(lowerSearchTerm) == true)
                    {
                        matchedFields.Add("Username");
                        relevanceScore += 7.0;
                    }

                    if (p.EmergencyContact?.ToLower().Contains(lowerSearchTerm) == true)
                    {
                        matchedFields.Add("EmergencyContact");
                        relevanceScore += 5.0;
                    }

                    if (p.Occupation?.ToLower().Contains(lowerSearchTerm) == true)
                    {
                        matchedFields.Add("Occupation");
                        relevanceScore += 3.0;
                    }

                    searchResult.MatchedFields = matchedFields;
                    searchResult.RelevanceScore = relevanceScore;

                    return searchResult;
                }).OrderByDescending(s => s.RelevanceScore).ToList();

                return new DynamicResponse<PatientSearchResult>
                {
                    Code = patientsResult.Code,
                    Message = "Patient search completed successfully",
                    Data = searchResults,
                    MetaData = patientsResult.MetaData
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error searching patients: {ErrorMessage}", methodName, ex.Message);
                return new DynamicResponse<PatientSearchResult>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred while searching patients: {ex.Message}",
                    SystemCode = "PATIENT_500"
                };
            }
        }

        /// <summary>
        /// Tính thống kê (tổng số, phân bố nhóm máu, relationships by type, average BMI,...)
        /// </summary>
        public async Task<BaseResponse<PatientStatisticsResponse>> GetPatientStatisticsAsync()
        {
            const string methodName = nameof(GetPatientStatisticsAsync);
            _logger.LogInformation("{MethodName} called", methodName);

            try
            {
                var patients = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(p => !p.IsDeleted)
                    .ToListAsync();

                var relationships = await _unitOfWork.Repository<Relationship>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(r => !r.IsDeleted)
                    .ToListAsync();

                var currentMonth = DateTime.UtcNow.Month;
                var currentYear = DateTime.UtcNow.Year;

                var statistics = new PatientStatisticsResponse
                {
                    TotalPatients = patients.Count,
                    ActivePatients = patients.Count(p => p.IsActive),
                    InactivePatients = patients.Count(p => !p.IsActive),
                    PatientsWithInsurance = patients.Count(p => !string.IsNullOrEmpty(p.Insurance)),
                    PatientsWithoutInsurance = patients.Count(p => string.IsNullOrEmpty(p.Insurance)),
                    TotalRelationships = relationships.Count,
                    ActiveRelationships = relationships.Count(r => r.IsActive),
                    PatientsCreatedThisMonth = patients.Count(p => p.CreatedAt.Month == currentMonth && p.CreatedAt.Year == currentYear),
                    PatientsCreatedThisYear = patients.Count(p => p.CreatedAt.Year == currentYear)
                };

                // Blood type distribution
                statistics.BloodTypeDistribution = patients
                    .Where(p => !string.IsNullOrEmpty(p.BloodType))
                    .GroupBy(p => p.BloodType)
                    .ToDictionary(g => g.Key!, g => g.Count());

                // Relationship type distribution
                statistics.RelationshipsByType = relationships
                    .GroupBy(r => r.RelationshipType.ToString())
                    .ToDictionary(g => g.Key, g => g.Count());

                // Calculate average BMI
                var patientsWithBMI = patients
                    .Where(p => p.Height.HasValue && p.Weight.HasValue && p.Height > 0)
                    .Select(p => p.Weight!.Value / (p.Height!.Value / 100 * p.Height!.Value / 100))
                    .ToList();

                if (patientsWithBMI.Any())
                {
                    statistics.AverageBMI = (double)Math.Round(patientsWithBMI.Average(), 2);
                }

                return BaseResponse<PatientStatisticsResponse>.CreateSuccess(statistics, "Patient statistics retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error retrieving patient statistics: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<PatientStatisticsResponse>.CreateError($"An error occurred while retrieving patient statistics: {ex.Message}", StatusCodes.Status500InternalServerError, "PATIENT_500");
            }
        }

        /// <summary>
        /// Validates if a patient code is unique
        /// </summary>
        public async Task<BaseResponse<bool>> ValidatePatientCodeAsync(string patientCode, Guid? excludePatientId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(patientCode))
                {
                    return BaseResponse<bool>.CreateError("Patient code cannot be empty", StatusCodes.Status400BadRequest, "PATIENT_008");
                }

                var query = _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Where(p => p.PatientCode == patientCode && !p.IsDeleted);

                if (excludePatientId.HasValue)
                {
                    query = query.Where(p => p.Id != excludePatientId.Value);
                }

                var exists = await query.AnyAsync();
                return BaseResponse<bool>.CreateSuccess(!exists, exists ? "Patient code already exists" : "Patient code is available");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating patient code: {ErrorMessage}", ex.Message);
                return BaseResponse<bool>.CreateError($"An error occurred while validating patient code: {ex.Message}", StatusCodes.Status500InternalServerError, "PATIENT_500");
            }
        }

        /// <summary>
        /// Validates if a national ID is unique
        /// </summary>
        public async Task<BaseResponse<bool>> ValidateNationalIdAsync(string nationalId, Guid? excludePatientId = null)
        {
            try
            {
                // Empty strings are allowed - multiple patients can have empty NationalID
                if (string.IsNullOrWhiteSpace(nationalId))
                {
                    return BaseResponse<bool>.CreateSuccess(true, "Empty National ID is allowed");
                }

                var query = _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Where(p => p.NationalID == nationalId && !p.IsDeleted);

                if (excludePatientId.HasValue)
                {
                    query = query.Where(p => p.Id != excludePatientId.Value);
                }

                var exists = await query.AnyAsync();
                return BaseResponse<bool>.CreateSuccess(!exists, exists ? "National ID already exists" : "National ID is available");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating national ID: {ErrorMessage}", ex.Message);
                return BaseResponse<bool>.CreateError($"An error occurred while validating national ID: {ex.Message}", StatusCodes.Status500InternalServerError, "PATIENT_500");
            }
        }

        /// <summary>
        /// Gets available blood types
        /// </summary>
        public async Task<BaseResponse<List<string>>> GetAvailableBloodTypesAsync()
        {
            try
            {
                var bloodTypes = new List<string> { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
                return BaseResponse<List<string>>.CreateSuccess(bloodTypes, "Blood types retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving blood types: {ErrorMessage}", ex.Message);
                return BaseResponse<List<string>>.CreateError($"An error occurred while retrieving blood types: {ex.Message}", StatusCodes.Status500InternalServerError, "PATIENT_500");
            }
        }

        /// <summary>
        /// Gets relationship type options
        /// </summary>
        public async Task<BaseResponse<Dictionary<int, string>>> GetRelationshipTypesAsync()
        {
            try
            {
                var relationshipTypes = Enum.GetValues<RelationshipType>()
                    .ToDictionary(rt => (int)rt, rt => rt.ToString());

                return BaseResponse<Dictionary<int, string>>.CreateSuccess(relationshipTypes, "Relationship types retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving relationship types: {ErrorMessage}", ex.Message);
                return BaseResponse<Dictionary<int, string>>.CreateError($"An error occurred while retrieving relationship types: {ex.Message}", StatusCodes.Status500InternalServerError, "RELATIONSHIP_500");
            }
        }

        /// <summary>
        /// Checks if two patients can have a relationship
        /// </summary>
        public async Task<BaseResponse<bool>> CanCreateRelationshipAsync(Guid patient1Id, Guid patient2Id)
        {
            try
            {
                if (patient1Id == Guid.Empty || patient2Id == Guid.Empty)
                {
                    return BaseResponse<bool>.CreateError("Patient IDs cannot be empty", StatusCodes.Status400BadRequest, "RELATIONSHIP_008");
                }

                if (patient1Id == patient2Id)
                {
                    return BaseResponse<bool>.CreateError("Patient cannot have relationship with themselves", StatusCodes.Status400BadRequest, "RELATIONSHIP_002");
                }

                // Check if both patients exist
                var patientsExist = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Where(p => (p.Id == patient1Id || p.Id == patient2Id) && !p.IsDeleted)
                    .CountAsync();

                if (patientsExist != 2)
                {
                    return BaseResponse<bool>.CreateSuccess(false, "One or both patients not found");
                }

                return BaseResponse<bool>.CreateSuccess(true, "Patients can have a relationship");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking relationship eligibility: {ErrorMessage}", ex.Message);
                return BaseResponse<bool>.CreateError($"An error occurred while checking relationship eligibility: {ex.Message}", StatusCodes.Status500InternalServerError, "RELATIONSHIP_500");
            }
        }

        /// <summary>
        /// Gets patients by relationship type
        /// </summary>
        public async Task<BaseResponse<List<RelatedPatientInfo>>> GetRelatedPatientsAsync(Guid patientId, RelationshipType relationshipType)
        {
            try
            {
                if (patientId == Guid.Empty)
                {
                    return BaseResponse<List<RelatedPatientInfo>>.CreateError("Patient ID cannot be empty", StatusCodes.Status400BadRequest, "PATIENT_006");
                }

                var relatedPatients = await _unitOfWork.Repository<Relationship>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(r => r.Patient1)
                        .ThenInclude(p => p.Account)
                    .Include(r => r.Patient2)
                        .ThenInclude(p => p.Account)
                    .Where(r => (r.Patient1Id == patientId || r.Patient2Id == patientId) &&
                               r.RelationshipType == relationshipType &&
                               r.IsActive && !r.IsDeleted)
                    .Select(r => r.Patient1Id == patientId ? r.Patient2 : r.Patient1)
                    .ToListAsync();

                var response = _mapper.Map<List<RelatedPatientInfo>>(relatedPatients);
                return BaseResponse<List<RelatedPatientInfo>>.CreateSuccess(response, "Related patients retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving related patients: {ErrorMessage}", ex.Message);
                return BaseResponse<List<RelatedPatientInfo>>.CreateError($"An error occurred while retrieving related patients: {ex.Message}", StatusCodes.Status500InternalServerError, "PATIENT_500");
            }
        }

        #endregion

        #region Bulk Operations

        /// <summary>
        /// Cập nhật IsActive cho nhiều bệnh nhân
        /// </summary>
        public async Task<BaseResponse<int>> BulkUpdatePatientStatusAsync(List<Guid> patientIds, bool isActive, string? reason = null)
        {
            const string methodName = nameof(BulkUpdatePatientStatusAsync);
            _logger.LogInformation("{MethodName} called with {Count} patient IDs", methodName, patientIds?.Count ?? 0);

            try
            {
                if (patientIds == null || !patientIds.Any())
                {
                    return BaseResponse<int>.CreateError("Patient IDs list cannot be empty", StatusCodes.Status400BadRequest, "PATIENT_013");
                }

                var patients = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Where(p => patientIds.Contains(p.Id) && !p.IsDeleted)
                    .ToListAsync();

                if (!patients.Any())
                {
                    return BaseResponse<int>.CreateError("No patients found with the provided IDs", StatusCodes.Status404NotFound, "PATIENT_007");
                }

                var updateCount = 0;
                foreach (var patient in patients)
                {
                    patient.IsActive = isActive;
                    patient.UpdatedAt = DateTime.UtcNow;

                    if (!string.IsNullOrWhiteSpace(reason))
                    {
                        patient.Notes = string.IsNullOrWhiteSpace(patient.Notes)
                            ? $"Bulk status update: {reason}"
                            : $"{patient.Notes}\nBulk status update: {reason}";
                    }

                    await _unitOfWork.Repository<Patient>().UpdateGuid(patient, patient.Id);
                    updateCount++;
                }

                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName} - Successfully updated {Count} patients", methodName, updateCount);

                return BaseResponse<int>.CreateSuccess(updateCount, $"Successfully updated {updateCount} patients");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error bulk updating patient status: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<int>.CreateError($"An error occurred while bulk updating patient status: {ex.Message}", StatusCodes.Status500InternalServerError, "PATIENT_500");
            }
        }

        /// <summary>
        /// Soft delete nhiều bệnh nhân
        /// </summary>
        public async Task<BaseResponse<int>> BulkDeletePatientsAsync(List<Guid> patientIds)
        {
            const string methodName = nameof(BulkDeletePatientsAsync);
            _logger.LogInformation("{MethodName} called with {Count} patient IDs", methodName, patientIds?.Count ?? 0);

            try
            {
                if (patientIds == null || !patientIds.Any())
                {
                    return BaseResponse<int>.CreateError("Patient IDs list cannot be empty", StatusCodes.Status400BadRequest, "PATIENT_013");
                }

                var patients = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Where(p => patientIds.Contains(p.Id) && !p.IsDeleted)
                    .ToListAsync();

                if (!patients.Any())
                {
                    return BaseResponse<int>.CreateError("No patients found with the provided IDs", StatusCodes.Status404NotFound, "PATIENT_007");
                }

                // Check for active relationships
                //var patientsWithActiveRelationships = await _unitOfWork.Repository<Relationship>()
                //    .AsQueryable()
                //    .Where(r => (patientIds.Contains(r.Patient1Id) || patientIds.Contains(r.Patient2Id)) && 
                //               r.IsActive && !r.IsDeleted)
                //    .Select(r => r.Patient1Id == patientIds.FirstOrDefault(id => id == r.Patient1Id) ? r.Patient1Id : r.Patient2Id)
                //    .Distinct()
                //    .ToListAsync();
                var patientsWithActiveRelationships = await _unitOfWork.Repository<Relationship>()
    .AsQueryable()
    .Where(r => (patientIds.Contains(r.Patient1Id) || patientIds.Contains(r.Patient2Id))
                && r.IsActive && !r.IsDeleted)
    .SelectMany(r => new[] { r.Patient1Id, r.Patient2Id })
    .Where(id => patientIds.Contains(id))
    .Distinct()
    .ToListAsync();


                if (patientsWithActiveRelationships.Any())
                {
                    return BaseResponse<int>.CreateError($"Cannot delete {patientsWithActiveRelationships.Count} patients with active relationships", StatusCodes.Status409Conflict, "PATIENT_011");
                }

                var deleteCount = 0;
                foreach (var patient in patients)
                {
                    patient.IsDeleted = true;
                    patient.UpdatedAt = DateTime.UtcNow;

                    await _unitOfWork.Repository<Patient>().UpdateGuid(patient, patient.Id);
                    deleteCount++;
                }

                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName} - Successfully deleted {Count} patients", methodName, deleteCount);

                return BaseResponse<int>.CreateSuccess(deleteCount, $"Successfully deleted {deleteCount} patients");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} - Error bulk deleting patients: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<int>.CreateError($"An error occurred while bulk deleting patients: {ex.Message}", StatusCodes.Status500InternalServerError, "PATIENT_500");
            }
        }

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Gets multiple patients with account information
        /// </summary>
        private async Task<List<Patient>> GetPatientsWithAccountAsync(List<Guid> patientIds)
        {
            if (patientIds == null || !patientIds.Any())
            {
                return new List<Patient>();
            }

            return await _unitOfWork.Repository<Patient>()
                .AsQueryable()
                .AsNoTracking()
                .Include(p => p.Account)
                .Include(p => p.Treatments)
                .Include(p => p.LabSamples)
                .Include(p => p.RelationshipsAsPatient1)
                .Include(p => p.RelationshipsAsPatient2)
                .Where(p => patientIds.Contains(p.Id) && !p.IsDeleted)
                .ToListAsync();
        }

        /// <summary>
        /// Gets a patient with account information
        /// </summary>
        private async Task<Patient?> GetPatientWithAccountAsync(Guid patientId)
        {
            return await _unitOfWork.Repository<Patient>()
                .AsQueryable()
                .AsNoTracking()
                .Include(p => p.Account)
                .Include(p => p.Treatments)
                .Include(p => p.LabSamples)
                .Include(p => p.RelationshipsAsPatient1)
                .Include(p => p.RelationshipsAsPatient2)
                .Where(p => p.Id == patientId && !p.IsDeleted)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets a relationship with patient information
        /// </summary>
        private async Task<Relationship?> GetRelationshipWithPatientsAsync(Guid relationshipId)
        {
            return await _unitOfWork.Repository<Relationship>()
                .AsQueryable()
                .AsNoTracking()
                .Include(r => r.Patient1)
                    .ThenInclude(p => p.Account)
                .Include(r => r.Patient2)
                    .ThenInclude(p => p.Account)
                .Where(r => r.Id == relationshipId && !r.IsDeleted)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Hydrates FullName for Patient1Info and Patient2Info in RelationshipResponse from Account FirstName + LastName
        /// </summary>
        private static void HydrateRelationshipResponseFullName(RelationshipResponse? response, Relationship? relationship)
        {
            if (response == null || relationship == null)
            {
                return;
            }

            // Hydrate Patient1Info FullName
            if (response.Patient1Info != null && relationship.Patient1?.Account != null)
            {
                var account = relationship.Patient1.Account;
                response.Patient1Info.FullName = $"{account.FirstName} {account.LastName}".Trim();
                if (string.IsNullOrWhiteSpace(response.Patient1Info.FullName))
                {
                    response.Patient1Info.FullName = null;
                }
            }

            // Hydrate Patient2Info FullName
            if (response.Patient2Info != null && relationship.Patient2?.Account != null)
            {
                var account = relationship.Patient2.Account;
                response.Patient2Info.FullName = $"{account.FirstName} {account.LastName}".Trim();
                if (string.IsNullOrWhiteSpace(response.Patient2Info.FullName))
                {
                    response.Patient2Info.FullName = null;
                }
            }
        }

        private async Task TryCreateRelationshipNotificationAsync(Relationship relationship, Patient patient1, Patient patient2)
        {
            try
            {
                var accountId = GetCurrentAccountId() ?? patient1.Id;
                if (accountId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Unable to determine accountId for relationship notification {RelationshipId}", nameof(TryCreateRelationshipNotificationAsync), relationship.Id);
                    return;
                }

                var patient1Name = $"{patient1.Account?.FirstName} {patient1.Account?.LastName}".Trim();
                var patient2Name = $"{patient2.Account?.FirstName} {patient2.Account?.LastName}".Trim();

                var request = new CreateNotificationRequest
                {
                    Title = "Relationship request created",
                    Content = $"A relationship request from {patient1Name} to {patient2Name} has been created.",
                    Type = NotificationType.Relationship,
                    RelatedEntityId = relationship.Id,
                    RelatedEntityType = EntityTypeNotification.Relationship,
                    Channel = "System",
                    Notes = relationship.Notes
                };

                var result = await _notificationService.CreateNotificationAsync(request, accountId);
                if (!result.Success)
                {
                    _logger.LogWarning("{MethodName}: Failed to create notification for relationship {RelationshipId}. Message: {Message}", nameof(TryCreateRelationshipNotificationAsync), relationship.Id, result.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating notification for relationship {RelationshipId}", nameof(TryCreateRelationshipNotificationAsync), relationship.Id);
            }
        }

        #endregion

        #region Relationship Status Operations

        /// <summary>
        /// Approves a pending relationship request
        /// </summary>
        public async Task<BaseResponse<RelationshipResponse>> ApproveRelationshipAsync(ApproveRelationshipRequest request)
        {
            const string methodName = nameof(ApproveRelationshipAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                if (request == null || request.RelationshipId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid request or relationship ID", methodName);
                    return BaseResponse<RelationshipResponse>.CreateError("Invalid request or relationship ID", StatusCodes.Status400BadRequest, "RELATIONSHIP_APPROVE_001");
                }

                // Get relationship with patient information
                var relationship = await _unitOfWork.Repository<Relationship>()
                    .AsQueryable()
                    .Include(r => r.Patient1)
                        .ThenInclude(p => p.Account)
                    .Include(r => r.Patient2)
                        .ThenInclude(p => p.Account)
                    .Where(r => r.Id == request.RelationshipId && !r.IsDeleted)
                    .FirstOrDefaultAsync();

                if (relationship == null)
                {
                    _logger.LogWarning("{MethodName}: Relationship not found - {RelationshipId}", methodName, request.RelationshipId);
                    return BaseResponse<RelationshipResponse>.CreateError("Relationship not found", StatusCodes.Status404NotFound, "RELATIONSHIP_APPROVE_002");
                }

                // Authorization check - Only Patient2 can approve the request
                var currentAccountId = GetCurrentAccountId();
                if (currentAccountId == null)
                {
                    _logger.LogWarning("{MethodName}: Unauthorized - No account ID found", methodName);
                    return BaseResponse<RelationshipResponse>.CreateError("Unauthorized: You must be logged in", StatusCodes.Status401Unauthorized, "RELATIONSHIP_APPROVE_401");
                }

                var currentPatientResult = await GetPatientByAccountIdAsync(currentAccountId.Value);
                if (!currentPatientResult.Success || currentPatientResult.Data == null)
                {
                    _logger.LogWarning("{MethodName}: Current user is not a patient", methodName);
                    return BaseResponse<RelationshipResponse>.CreateError("Unauthorized: Only patients can approve relationship requests", StatusCodes.Status403Forbidden, "RELATIONSHIP_APPROVE_403");
                }

                var currentPatientId = currentPatientResult.Data.Id;

                // Verify that current patient is Patient2 (the recipient)
                if (relationship.Patient2Id != currentPatientId)
                {
                    _logger.LogWarning("{MethodName}: Patient {CurrentPatientId} attempted to approve relationship for another patient", methodName, currentPatientId);
                    return BaseResponse<RelationshipResponse>.CreateError("Unauthorized: You can only approve relationship requests sent to you", StatusCodes.Status403Forbidden, "RELATIONSHIP_APPROVE_403_FORBIDDEN");
                }

                // Status validation
                if (relationship.Status != RelationshipStatus.Pending)
                {
                    if (relationship.Status == RelationshipStatus.Approved)
                    {
                        _logger.LogWarning("{MethodName}: Relationship already approved - {RelationshipId}", methodName, request.RelationshipId);
                        return BaseResponse<RelationshipResponse>.CreateError("Relationship is already approved", StatusCodes.Status400BadRequest, "RELATIONSHIP_APPROVE_003_APPROVED");
                    }
                    else if (relationship.Status == RelationshipStatus.Rejected)
                    {
                        _logger.LogWarning("{MethodName}: Relationship was rejected - {RelationshipId}", methodName, request.RelationshipId);
                        return BaseResponse<RelationshipResponse>.CreateError("Relationship request was already rejected", StatusCodes.Status400BadRequest, "RELATIONSHIP_APPROVE_003_REJECTED");
                    }
                    else if (relationship.Status == RelationshipStatus.Expired)
                    {
                        _logger.LogWarning("{MethodName}: Relationship request expired - {RelationshipId}", methodName, request.RelationshipId);
                        return BaseResponse<RelationshipResponse>.CreateError("Relationship request has expired", StatusCodes.Status400BadRequest, "RELATIONSHIP_APPROVE_003_EXPIRED");
                    }
                    else if (relationship.Status == RelationshipStatus.Cancelled)
                    {
                        _logger.LogWarning("{MethodName}: Relationship request cancelled - {RelationshipId}", methodName, request.RelationshipId);
                        return BaseResponse<RelationshipResponse>.CreateError("Relationship request was cancelled by the requester", StatusCodes.Status400BadRequest, "RELATIONSHIP_APPROVE_003_CANCELLED");
                    }
                }

                // Check expiration
                if (relationship.ExpiresAt.HasValue && relationship.ExpiresAt.Value < DateTime.UtcNow)
                {
                    relationship.Status = RelationshipStatus.Expired;
                    relationship.UpdatedAt = DateTime.UtcNow;
                    await _unitOfWork.Repository<Relationship>().UpdateGuid(relationship, relationship.Id);
                    await _unitOfWork.CommitAsync();

                    _logger.LogWarning("{MethodName}: Relationship request expired - {RelationshipId}", methodName, request.RelationshipId);
                    return BaseResponse<RelationshipResponse>.CreateError("Relationship request has expired", StatusCodes.Status400BadRequest, "RELATIONSHIP_APPROVE_003_EXPIRED");
                }

                // Update relationship status
                relationship.Status = RelationshipStatus.Approved;
                relationship.RespondedBy = currentPatientId;
                relationship.RespondedAt = DateTime.UtcNow;
                relationship.IsActive = true;
                relationship.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Relationship>().UpdateGuid(relationship, relationship.Id);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Relationship approved successfully - {RelationshipId}", methodName, request.RelationshipId);

                // Send notification email to Patient1
                try
                {
                    if (relationship.Patient1?.Account != null)
                    {
                        await SendRelationshipApprovalEmailAsync(relationship, relationship.Patient1, relationship.Patient2!);
                        _logger.LogInformation("{MethodName}: Approval notification email sent to {Email}", methodName, relationship.Patient1.Account.Email);
                    }
                }
                catch (Exception emailEx)
                {
                    _logger.LogError(emailEx, "{MethodName}: Failed to send approval notification email", methodName);
                }

                // Return response
                var updatedRelationship = await GetRelationshipWithPatientsAsync(relationship.Id);
                var response = _mapper.Map<RelationshipResponse>(updatedRelationship);
                HydrateRelationshipResponseFullName(response, updatedRelationship);

                return BaseResponse<RelationshipResponse>.CreateSuccess(response, "Relationship approved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error approving relationship: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<RelationshipResponse>.CreateError($"An error occurred while approving relationship: {ex.Message}", StatusCodes.Status500InternalServerError, "RELATIONSHIP_APPROVE_500");
            }
        }

        /// <summary>
        /// Rejects a pending relationship request
        /// </summary>
        public async Task<BaseResponse<RelationshipResponse>> RejectRelationshipAsync(RejectRelationshipRequest request)
        {
            const string methodName = nameof(RejectRelationshipAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                if (request == null || request.RelationshipId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid request or relationship ID", methodName);
                    return BaseResponse<RelationshipResponse>.CreateError("Invalid request or relationship ID", StatusCodes.Status400BadRequest, "RELATIONSHIP_REJECT_001");
                }

                // Get relationship with patient information
                var relationship = await _unitOfWork.Repository<Relationship>()
                    .AsQueryable()
                    .Include(r => r.Patient1)
                        .ThenInclude(p => p.Account)
                    .Include(r => r.Patient2)
                        .ThenInclude(p => p.Account)
                    .Where(r => r.Id == request.RelationshipId && !r.IsDeleted)
                    .FirstOrDefaultAsync();

                if (relationship == null)
                {
                    _logger.LogWarning("{MethodName}: Relationship not found - {RelationshipId}", methodName, request.RelationshipId);
                    return BaseResponse<RelationshipResponse>.CreateError("Relationship not found", StatusCodes.Status404NotFound, "RELATIONSHIP_REJECT_002");
                }

                // Authorization check - Only Patient2 can reject the request
                var currentAccountId = GetCurrentAccountId();
                if (currentAccountId == null)
                {
                    _logger.LogWarning("{MethodName}: Unauthorized - No account ID found", methodName);
                    return BaseResponse<RelationshipResponse>.CreateError("Unauthorized: You must be logged in", StatusCodes.Status401Unauthorized, "RELATIONSHIP_REJECT_401");
                }

                var currentPatientResult = await GetPatientByAccountIdAsync(currentAccountId.Value);
                if (!currentPatientResult.Success || currentPatientResult.Data == null)
                {
                    _logger.LogWarning("{MethodName}: Current user is not a patient", methodName);
                    return BaseResponse<RelationshipResponse>.CreateError("Unauthorized: Only patients can reject relationship requests", StatusCodes.Status403Forbidden, "RELATIONSHIP_REJECT_403");
                }

                var currentPatientId = currentPatientResult.Data.Id;

                // Verify that current patient is Patient2 (the recipient)
                if (relationship.Patient2Id != currentPatientId)
                {
                    _logger.LogWarning("{MethodName}: Patient {CurrentPatientId} attempted to reject relationship for another patient", methodName, currentPatientId);
                    return BaseResponse<RelationshipResponse>.CreateError("Unauthorized: You can only reject relationship requests sent to you", StatusCodes.Status403Forbidden, "RELATIONSHIP_REJECT_403_FORBIDDEN");
                }

                // Status validation
                if (relationship.Status != RelationshipStatus.Pending)
                {
                    if (relationship.Status == RelationshipStatus.Approved)
                    {
                        _logger.LogWarning("{MethodName}: Cannot reject already approved relationship - {RelationshipId}", methodName, request.RelationshipId);
                        return BaseResponse<RelationshipResponse>.CreateError("Cannot reject an already approved relationship", StatusCodes.Status400BadRequest, "RELATIONSHIP_REJECT_003_APPROVED");
                    }
                    else if (relationship.Status == RelationshipStatus.Rejected)
                    {
                        _logger.LogWarning("{MethodName}: Relationship already rejected - {RelationshipId}", methodName, request.RelationshipId);
                        return BaseResponse<RelationshipResponse>.CreateError("Relationship request was already rejected", StatusCodes.Status400BadRequest, "RELATIONSHIP_REJECT_003_REJECTED");
                    }
                    else if (relationship.Status == RelationshipStatus.Expired)
                    {
                        _logger.LogWarning("{MethodName}: Relationship request expired - {RelationshipId}", methodName, request.RelationshipId);
                        return BaseResponse<RelationshipResponse>.CreateError("Relationship request has expired", StatusCodes.Status400BadRequest, "RELATIONSHIP_REJECT_003_EXPIRED");
                    }
                    else if (relationship.Status == RelationshipStatus.Cancelled)
                    {
                        _logger.LogWarning("{MethodName}: Relationship request cancelled - {RelationshipId}", methodName, request.RelationshipId);
                        return BaseResponse<RelationshipResponse>.CreateError("Relationship request was cancelled by the requester", StatusCodes.Status400BadRequest, "RELATIONSHIP_REJECT_003_CANCELLED");
                    }
                }

                // Update relationship status
                relationship.Status = RelationshipStatus.Rejected;
                relationship.RespondedBy = currentPatientId;
                relationship.RespondedAt = DateTime.UtcNow;
                relationship.RejectionReason = request.RejectionReason;
                relationship.IsActive = false;
                relationship.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Relationship>().UpdateGuid(relationship, relationship.Id);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Relationship rejected successfully - {RelationshipId}", methodName, request.RelationshipId);

                // Send notification email to Patient1 (optional - HIPAA compliance consideration)
                try
                {
                    if (relationship.Patient1?.Account != null)
                    {
                        await SendRelationshipRejectionEmailAsync(relationship, relationship.Patient1, relationship.Patient2!, request.RejectionReason);
                        _logger.LogInformation("{MethodName}: Rejection notification email sent to {Email}", methodName, relationship.Patient1.Account.Email);
                    }
                }
                catch (Exception emailEx)
                {
                    _logger.LogError(emailEx, "{MethodName}: Failed to send rejection notification email", methodName);
                }

                // Return response
                var updatedRelationship = await GetRelationshipWithPatientsAsync(relationship.Id);
                var response = _mapper.Map<RelationshipResponse>(updatedRelationship);
                HydrateRelationshipResponseFullName(response, updatedRelationship);

                return BaseResponse<RelationshipResponse>.CreateSuccess(response, "Relationship rejected successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error rejecting relationship: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<RelationshipResponse>.CreateError($"An error occurred while rejecting relationship: {ex.Message}", StatusCodes.Status500InternalServerError, "RELATIONSHIP_REJECT_500");
            }
        }

        /// <summary>
        /// Cancels a pending relationship request initiated by the current patient
        /// </summary>
        public async Task<BaseResponse<RelationshipResponse>> CancelRelationshipAsync(CancelRelationshipRequest request)
        {
            const string methodName = nameof(CancelRelationshipAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                if (request == null || request.RelationshipId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid request or relationship ID", methodName);
                    return BaseResponse<RelationshipResponse>.CreateError("Invalid request or relationship ID", StatusCodes.Status400BadRequest, "RELATIONSHIP_CANCEL_001");
                }

                var relationship = await _unitOfWork.Repository<Relationship>()
                    .AsQueryable()
                    .Include(r => r.Patient1)
                        .ThenInclude(p => p.Account)
                    .Include(r => r.Patient2)
                        .ThenInclude(p => p.Account)
                    .Where(r => r.Id == request.RelationshipId && !r.IsDeleted)
                    .FirstOrDefaultAsync();

                if (relationship == null)
                {
                    _logger.LogWarning("{MethodName}: Relationship not found - {RelationshipId}", methodName, request.RelationshipId);
                    return BaseResponse<RelationshipResponse>.CreateError("Relationship not found", StatusCodes.Status404NotFound, "RELATIONSHIP_CANCEL_002");
                }

                var currentAccountId = GetCurrentAccountId();
                if (currentAccountId == null)
                {
                    _logger.LogWarning("{MethodName}: Unauthorized - No account ID found", methodName);
                    return BaseResponse<RelationshipResponse>.CreateError("Unauthorized: You must be logged in", StatusCodes.Status401Unauthorized, "RELATIONSHIP_CANCEL_401");
                }

                var currentPatientResult = await GetPatientByAccountIdAsync(currentAccountId.Value);
                if (!currentPatientResult.Success || currentPatientResult.Data == null)
                {
                    _logger.LogWarning("{MethodName}: Current user is not a patient", methodName);
                    return BaseResponse<RelationshipResponse>.CreateError("Unauthorized: Only patients can cancel relationship requests", StatusCodes.Status403Forbidden, "RELATIONSHIP_CANCEL_403");
                }

                var currentPatientId = currentPatientResult.Data.Id;
                var requesterId = relationship.RequestedBy ?? relationship.Patient1Id;

                if (requesterId == Guid.Empty)
                {
                    requesterId = relationship.Patient1Id;
                }

                if (currentPatientId != requesterId)
                {
                    _logger.LogWarning("{MethodName}: Patient {CurrentPatientId} attempted to cancel relationship {RelationshipId} not initiated by them", methodName, currentPatientId, request.RelationshipId);
                    return BaseResponse<RelationshipResponse>.CreateError("Unauthorized: You can only cancel relationship requests you initiated", StatusCodes.Status403Forbidden, "RELATIONSHIP_CANCEL_403_FORBIDDEN");
                }

                if (relationship.Status == RelationshipStatus.Cancelled)
                {
                    _logger.LogWarning("{MethodName}: Relationship already cancelled - {RelationshipId}", methodName, request.RelationshipId);
                    return BaseResponse<RelationshipResponse>.CreateError("Relationship request is already cancelled", StatusCodes.Status400BadRequest, "RELATIONSHIP_CANCEL_003_ALREADY");
                }

                if (relationship.Status != RelationshipStatus.Pending)
                {
                    _logger.LogWarning("{MethodName}: Only pending relationships can be cancelled - {RelationshipId}", methodName, request.RelationshipId);
                    return BaseResponse<RelationshipResponse>.CreateError("Only pending relationship requests can be cancelled", StatusCodes.Status400BadRequest, "RELATIONSHIP_CANCEL_003_STATUS");
                }

                relationship.Status = RelationshipStatus.Cancelled;
                relationship.IsActive = false;
                relationship.RespondedBy = currentPatientId;
                relationship.RespondedAt = DateTime.UtcNow;
                relationship.RejectionReason = request.CancellationReason;
                relationship.ApprovalToken = null;
                relationship.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Relationship>().UpdateGuid(relationship, relationship.Id);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Relationship cancelled successfully - {RelationshipId}", methodName, request.RelationshipId);

                var updatedRelationship = await GetRelationshipWithPatientsAsync(relationship.Id) ?? relationship;
                var response = _mapper.Map<RelationshipResponse>(updatedRelationship);
                HydrateRelationshipResponseFullName(response, updatedRelationship);

                return BaseResponse<RelationshipResponse>.CreateSuccess(response, "Relationship request cancelled successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error cancelling relationship: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<RelationshipResponse>.CreateError($"An error occurred while cancelling relationship: {ex.Message}", StatusCodes.Status500InternalServerError, "RELATIONSHIP_CANCEL_500");
            }
        }

        #endregion

        #region Private Helper Methods

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
        /// Checks if the current user is in the specified role
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
        /// Validates that a patient does not have any existing approved relationship (1-1 constraint)
        /// Each patient can only have ONE relationship with another patient
        /// </summary>
        private async Task<(bool IsValid, string ErrorMessage, string ErrorCode)> ValidatePatientHasNoExistingRelationshipAsync(
            Guid patientId, Patient patient)
        {
            // Check if the patient already has ANY approved relationship
            var existingApprovedRelationship = await _unitOfWork.Repository<Relationship>()
                .AsQueryable()
                .Where(r => (r.Patient1Id == patientId || r.Patient2Id == patientId) &&
                           r.Status == RelationshipStatus.Approved &&
                           !r.IsDeleted)
                .FirstOrDefaultAsync();

            if (existingApprovedRelationship != null)
            {
                var patientName = patient.Account != null
                    ? $"{patient.Account.FirstName} {patient.Account.LastName}".Trim()
                    : patient.PatientCode;

                if (string.IsNullOrWhiteSpace(patientName))
                {
                    patientName = patient.PatientCode;
                }

                return (false,
                    $"Patient '{patientName}' already has an approved relationship. Each patient can only have one relationship.",
                    "RELATIONSHIP_VALIDATION_ONE_TO_ONE");
            }

            // Also check for pending relationships to prevent multiple simultaneous requests
            var existingPendingRelationship = await _unitOfWork.Repository<Relationship>()
                .AsQueryable()
                .Where(r => (r.Patient1Id == patientId || r.Patient2Id == patientId) &&
                           r.Status == RelationshipStatus.Pending &&
                           !r.IsDeleted)
                .FirstOrDefaultAsync();

            if (existingPendingRelationship != null)
            {
                var patientName = patient.Account != null
                    ? $"{patient.Account.FirstName} {patient.Account.LastName}".Trim()
                    : patient.PatientCode;

                if (string.IsNullOrWhiteSpace(patientName))
                {
                    patientName = patient.PatientCode;
                }

                return (false,
                    $"Patient '{patientName}' already has a pending relationship request. Please wait for it to be processed.",
                    "RELATIONSHIP_VALIDATION_PENDING_EXISTS");
            }

            return (true, string.Empty, string.Empty);
        }

        /// <summary>
        /// Validates business rules for relationship creation (gender compatibility)
        /// </summary>
        private async Task<(bool IsValid, string ErrorMessage, string ErrorCode)> ValidateRelationshipBusinessRulesAsync(
            Patient patient1, Patient patient2, RelationshipType relationshipType)
        {
            // Gender validation - Check that both patients have gender information
            if (patient1.Account == null || patient1.Account.Gender == null)
            {
                return (false, "Patient1 does not have gender information. Gender is required for relationship creation.", "RELATIONSHIP_VALIDATION_GENDER_001");
            }

            if (patient2.Account == null || patient2.Account.Gender == null)
            {
                return (false, "Patient2 does not have gender information. Gender is required for relationship creation.", "RELATIONSHIP_VALIDATION_GENDER_002");
            }

            // true = male, false = female
            var patient1Gender = patient1.Account.Gender.Value;
            var patient2Gender = patient2.Account.Gender.Value;

            // Check that genders are different
            if (patient1Gender == patient2Gender)
            {
                return (false, "Both patients must have different genders to create a relationship.", "RELATIONSHIP_VALIDATION_GENDER_003");
            }

            // Validate gender compatibility with RelationshipType
            if (relationshipType == RelationshipType.Married)
            {
                // Married relationship validation - both patients must have different genders
                if (patient1Gender == patient2Gender)
                {
                    return (false, "Invalid gender combination for Married relationship. Patients must have different genders.", "RELATIONSHIP_VALIDATION_GENDER_004");
                }
            }
            else if (relationshipType == RelationshipType.Unmarried)
            {
                // Unmarried relationship validation - both patients must have different genders
                if (patient1Gender == patient2Gender)
                {
                    return (false, "Invalid gender combination for Unmarried relationship. Patients must have different genders.", "RELATIONSHIP_VALIDATION_GENDER_005");
                }
            }

            // Age validation for parent-child relationships (if added in future)
            // This is a placeholder for future implementation
            // if (relationshipType == RelationshipType.Parent || relationshipType == RelationshipType.Child)
            // {
            //     var ageDifference = CalculateAgeDifference(patient1, patient2);
            //     if (ageDifference < MIN_PARENT_CHILD_AGE_DIFFERENCE)
            //     {
            //         return (false, "Age difference is insufficient for parent-child relationship", "RELATIONSHIP_VALIDATION_002_AGE");
            //     }
            // }

            return (true, string.Empty, string.Empty);
        }

        /// <summary>
        /// Sends relationship confirmation email to Patient2
        /// </summary>
        private async Task SendRelationshipConfirmationEmailAsync(Relationship relationship, Patient patient1, Patient patient2)
        {
            if (patient2.Account == null || string.IsNullOrWhiteSpace(patient2.Account.Email))
            {
                _logger.LogWarning("Cannot send email: Patient2 does not have a valid email address");
                return;
            }

            var patient1Name = $"{patient1.Account?.FirstName} {patient1.Account?.LastName}".Trim();
            var patient2Name = $"{patient2.Account?.FirstName} {patient2.Account?.LastName}".Trim();
            var relationshipTypeName = relationship.RelationshipType.ToString();

            var subject = $"Relationship Request from {patient1Name}";
            var baseUrl = _httpContextAccessor.HttpContext?.Request?.Scheme + "://" + _httpContextAccessor.HttpContext?.Request?.Host;
            // Include token in URL for email-based verification
            var approvalUrl = $"{baseUrl}/api/relationship/email-approve/{relationship.Id}?token={relationship.ApprovalToken}";
            var rejectionUrl = $"{baseUrl}/api/relationship/email-reject/{relationship.Id}?token={relationship.ApprovalToken}";
            var expiresAt = relationship.ExpiresAt?.ToString("yyyy-MM-dd HH:mm") ?? "N/A";

            var body = await _emailTemplateService.GetRelationshipConfirmationTemplateAsync(
                patient1Name,
                patient2Name,
                relationshipTypeName,
                expiresAt,
                approvalUrl,
                rejectionUrl,
                relationship.Notes
            );

            await _mailService.SendEmailAsync(
                patient2.Account.Email,
                subject,
                body
            );
        }

        /// <summary>
        /// Sends relationship approval notification email to Patient1
        /// </summary>
        private async Task SendRelationshipApprovalEmailAsync(Relationship relationship, Patient patient1, Patient patient2)
        {
            if (patient1.Account == null || string.IsNullOrWhiteSpace(patient1.Account.Email))
            {
                _logger.LogWarning("Cannot send email: Patient1 does not have a valid email address");
                return;
            }

            var patient1Name = $"{patient1.Account?.FirstName} {patient1.Account?.LastName}".Trim();
            var patient2Name = $"{patient2.Account?.FirstName} {patient2.Account?.LastName}".Trim();
            var relationshipTypeName = relationship.RelationshipType.ToString();

            var subject = $"Relationship Request Approved";
            var body = await _emailTemplateService.GetRelationshipApprovalTemplateAsync(
                patient1Name,
                patient2Name,
                relationshipTypeName
            );

            await _mailService.SendEmailAsync(
                patient1.Account.Email,
                subject,
                body
            );
        }

        /// <summary>
        /// Sends relationship rejection notification email to Patient1
        /// </summary>
        private async Task SendRelationshipRejectionEmailAsync(Relationship relationship, Patient patient1, Patient patient2, string? rejectionReason)
        {
            if (patient1.Account == null || string.IsNullOrWhiteSpace(patient1.Account.Email))
            {
                _logger.LogWarning("Cannot send email: Patient1 does not have a valid email address");
                return;
            }

            var patient1Name = $"{patient1.Account?.FirstName} {patient1.Account?.LastName}".Trim();
            var patient2Name = $"{patient2.Account?.FirstName} {patient2.Account?.LastName}".Trim();
            var relationshipTypeName = relationship.RelationshipType.ToString();

            var subject = $"Relationship Request Update";
            var body = await _emailTemplateService.GetRelationshipRejectionTemplateAsync(
                patient1Name,
                patient2Name,
                relationshipTypeName,
                rejectionReason
            );

            await _mailService.SendEmailAsync(
                patient1.Account.Email,
                subject,
                body
            );
        }

        /// <summary>
        /// Generates a secure random token for email-based verification
        /// </summary>
        private static string GenerateSecureToken()
        {
            var tokenBytes = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenBytes);
            }
            return Convert.ToBase64String(tokenBytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", ""); // URL-safe base64
        }

        #endregion

        #region Email-Based Relationship Operations (Token-based)

        /// <summary>
        /// Approves a relationship request via email link with token verification
        /// </summary>
        public async Task<BaseResponse<RelationshipResponse>> ApproveRelationshipByTokenAsync(Guid relationshipId, string token)
        {
            const string methodName = nameof(ApproveRelationshipByTokenAsync);
            _logger.LogInformation("{MethodName} called for RelationshipId: {RelationshipId}", methodName, relationshipId);

            try
            {
                // 1. Find relationship
                var relationship = await _unitOfWork.Repository<Relationship>()
                    .AsQueryable()
                    .Include(r => r.Patient1)
                        .ThenInclude(p => p!.Account)
                    .Include(r => r.Patient2)
                        .ThenInclude(p => p!.Account)
                    .Where(r => r.Id == relationshipId && !r.IsDeleted)
                    .FirstOrDefaultAsync();

                if (relationship == null)
                {
                    _logger.LogWarning("{MethodName}: Relationship not found - {RelationshipId}", methodName, relationshipId);
                    return BaseResponse<RelationshipResponse>.CreateError("Relationship request not found", StatusCodes.Status404NotFound, "RELATIONSHIP_EMAIL_001");
                }

                // 2. Validate token
                if (string.IsNullOrEmpty(relationship.ApprovalToken) || relationship.ApprovalToken != token)
                {
                    _logger.LogWarning("{MethodName}: Invalid token for RelationshipId: {RelationshipId}", methodName, relationshipId);
                    return BaseResponse<RelationshipResponse>.CreateError("Invalid or expired approval token", StatusCodes.Status401Unauthorized, "RELATIONSHIP_EMAIL_002");
                }

                // 3. Check if already processed
                if (relationship.Status == RelationshipStatus.Approved)
                {
                    _logger.LogWarning("{MethodName}: Relationship already approved - {RelationshipId}", methodName, relationshipId);
                    return BaseResponse<RelationshipResponse>.CreateError("Relationship is already approved", StatusCodes.Status400BadRequest, "RELATIONSHIP_EMAIL_003_APPROVED");
                }

                if (relationship.Status == RelationshipStatus.Rejected)
                {
                    _logger.LogWarning("{MethodName}: Relationship already rejected - {RelationshipId}", methodName, relationshipId);
                    return BaseResponse<RelationshipResponse>.CreateError("Relationship has been rejected and cannot be approved", StatusCodes.Status400BadRequest, "RELATIONSHIP_EMAIL_003_REJECTED");
                }

                if (relationship.Status == RelationshipStatus.Cancelled)
                {
                    _logger.LogWarning("{MethodName}: Relationship already cancelled - {RelationshipId}", methodName, relationshipId);
                    return BaseResponse<RelationshipResponse>.CreateError("Relationship request was cancelled by the requester", StatusCodes.Status400BadRequest, "RELATIONSHIP_EMAIL_003_CANCELLED");
                }

                // 4. Check expiration
                if (relationship.ExpiresAt.HasValue && relationship.ExpiresAt.Value < DateTime.UtcNow)
                {
                    _logger.LogWarning("{MethodName}: Relationship request has expired - {RelationshipId}", methodName, relationshipId);
                    return BaseResponse<RelationshipResponse>.CreateError("This relationship request has expired", StatusCodes.Status400BadRequest, "RELATIONSHIP_EMAIL_004_EXPIRED");
                }

                // 5. Update status
                relationship.Status = RelationshipStatus.Approved;
                relationship.RespondedBy = relationship.Patient2Id;
                relationship.RespondedAt = DateTime.UtcNow;
                relationship.ApprovalToken = null; // Clear token after use

                await _unitOfWork.Repository<Relationship>().UpdateAsync(relationship);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Relationship approved successfully via email - {RelationshipId}", methodName, relationshipId);

                // 6. Send notification email to requester
                try
                {
                    if (relationship.Patient1 != null && relationship.Patient2 != null)
                    {
                        await SendRelationshipApprovalEmailAsync(relationship, relationship.Patient1, relationship.Patient2);
                    }
                }
                catch (Exception emailEx)
                {
                    _logger.LogError(emailEx, "{MethodName}: Failed to send approval notification email", methodName);
                }

                var response = _mapper.Map<RelationshipResponse>(relationship);
                HydrateRelationshipResponseFullName(response, relationship);
                return BaseResponse<RelationshipResponse>.CreateSuccess(response, "Relationship approved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error approving relationship via email: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<RelationshipResponse>.CreateError($"An error occurred while approving relationship: {ex.Message}", StatusCodes.Status500InternalServerError, "RELATIONSHIP_EMAIL_500");
            }
        }

        /// <summary>
        /// Rejects a relationship request via email link with token verification
        /// </summary>
        public async Task<BaseResponse<RelationshipResponse>> RejectRelationshipByTokenAsync(Guid relationshipId, string token, string? rejectionReason = null)
        {
            const string methodName = nameof(RejectRelationshipByTokenAsync);
            _logger.LogInformation("{MethodName} called for RelationshipId: {RelationshipId}", methodName, relationshipId);

            try
            {
                // 1. Find relationship
                var relationship = await _unitOfWork.Repository<Relationship>()
                    .AsQueryable()
                    .Include(r => r.Patient1)
                        .ThenInclude(p => p!.Account)
                    .Include(r => r.Patient2)
                        .ThenInclude(p => p!.Account)
                    .Where(r => r.Id == relationshipId && !r.IsDeleted)
                    .FirstOrDefaultAsync();

                if (relationship == null)
                {
                    _logger.LogWarning("{MethodName}: Relationship not found - {RelationshipId}", methodName, relationshipId);
                    return BaseResponse<RelationshipResponse>.CreateError("Relationship request not found", StatusCodes.Status404NotFound, "RELATIONSHIP_EMAIL_001");
                }

                // 2. Validate token
                if (string.IsNullOrEmpty(relationship.ApprovalToken) || relationship.ApprovalToken != token)
                {
                    _logger.LogWarning("{MethodName}: Invalid token for RelationshipId: {RelationshipId}", methodName, relationshipId);
                    return BaseResponse<RelationshipResponse>.CreateError("Invalid or expired approval token", StatusCodes.Status401Unauthorized, "RELATIONSHIP_EMAIL_002");
                }

                // 3. Check if already processed
                if (relationship.Status == RelationshipStatus.Approved)
                {
                    _logger.LogWarning("{MethodName}: Cannot reject already approved relationship - {RelationshipId}", methodName, relationshipId);
                    return BaseResponse<RelationshipResponse>.CreateError("Cannot reject an already approved relationship", StatusCodes.Status400BadRequest, "RELATIONSHIP_EMAIL_003_APPROVED");
                }

                if (relationship.Status == RelationshipStatus.Rejected)
                {
                    _logger.LogWarning("{MethodName}: Relationship already rejected - {RelationshipId}", methodName, relationshipId);
                    return BaseResponse<RelationshipResponse>.CreateError("Relationship has already been rejected", StatusCodes.Status400BadRequest, "RELATIONSHIP_EMAIL_003_REJECTED");
                }

                if (relationship.Status == RelationshipStatus.Cancelled)
                {
                    _logger.LogWarning("{MethodName}: Relationship already cancelled - {RelationshipId}", methodName, relationshipId);
                    return BaseResponse<RelationshipResponse>.CreateError("Relationship request was cancelled and cannot be rejected", StatusCodes.Status400BadRequest, "RELATIONSHIP_EMAIL_003_CANCELLED");
                }

                // 4. Check expiration (allow rejection even after expiration for user experience)
                // Users should be able to explicitly reject even if expired

                // 5. Update status
                relationship.Status = RelationshipStatus.Rejected;
                relationship.RespondedBy = relationship.Patient2Id;
                relationship.RespondedAt = DateTime.UtcNow;
                relationship.RejectionReason = rejectionReason;
                relationship.ApprovalToken = null; // Clear token after use

                await _unitOfWork.Repository<Relationship>().UpdateAsync(relationship);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Relationship rejected successfully via email - {RelationshipId}", methodName, relationshipId);

                // 6. Send notification email to requester
                try
                {
                    if (relationship.Patient1 != null && relationship.Patient2 != null)
                    {
                        await SendRelationshipRejectionEmailAsync(relationship, relationship.Patient1, relationship.Patient2, rejectionReason);
                    }
                }
                catch (Exception emailEx)
                {
                    _logger.LogError(emailEx, "{MethodName}: Failed to send rejection notification email", methodName);
                }

                var response = _mapper.Map<RelationshipResponse>(relationship);
                HydrateRelationshipResponseFullName(response, relationship);
                return BaseResponse<RelationshipResponse>.CreateSuccess(response, "Relationship rejected successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error rejecting relationship via email: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<RelationshipResponse>.CreateError($"An error occurred while rejecting relationship: {ex.Message}", StatusCodes.Status500InternalServerError, "RELATIONSHIP_EMAIL_500");
            }
        }

        #endregion
    }
}
