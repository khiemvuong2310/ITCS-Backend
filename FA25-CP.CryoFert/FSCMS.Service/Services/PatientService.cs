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

        public PatientService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PatientService> logger,
            IHttpContextAccessor httpContextAccessor,
            IMailService mailService,
            IEmailTemplateService emailTemplateService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _emailTemplateService = emailTemplateService ?? throw new ArgumentNullException(nameof(emailTemplateService));
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

                // Check if national ID already exists
                var existingPatientByNationalId = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Where(p => p.NationalID == request.NationalID && !p.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingPatientByNationalId != null)
                {
                    return BaseResponse<PatientResponse>.CreateError("National ID already exists", StatusCodes.Status409Conflict, "PATIENT_003");
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
                existingPatient.UpdatedAt = DateTime.UtcNow.AddHours(7);

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
                existingPatient.UpdatedAt = DateTime.UtcNow.AddHours(7);
                    
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
                existingPatient.UpdatedAt = DateTime.UtcNow.AddHours(7);

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

                if (request.Patient1Id == request.Patient2Id)
                {
                    _logger.LogWarning("{MethodName}: Patient cannot have relationship with themselves", methodName);
                    return BaseResponse<RelationshipResponse>.CreateError("Patient cannot have relationship with themselves", StatusCodes.Status400BadRequest, "RELATIONSHIP_002");
                }

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

                // Verify that Patient1Id matches current patient (only patients can create requests for themselves)
                if (request.Patient1Id != currentPatientId)
                {
                    _logger.LogWarning("{MethodName}: Patient {CurrentPatientId} attempted to create relationship for another patient {Patient1Id}", methodName, currentPatientId, request.Patient1Id);
                    return BaseResponse<RelationshipResponse>.CreateError("Unauthorized: You can only create relationship requests for yourself", StatusCodes.Status403Forbidden, "RELATIONSHIP_403_FORBIDDEN");
                }

                // 3. Verify both patients exist with account information
                var patient1 = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Include(p => p.Account)
                    .Where(p => p.Id == request.Patient1Id && !p.IsDeleted)
                    .FirstOrDefaultAsync();

                var patient2 = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Include(p => p.Account)
                    .Where(p => p.Id == request.Patient2Id && !p.IsDeleted)
                    .FirstOrDefaultAsync();

                if (patient1 == null || patient2 == null)
                {
                    _logger.LogWarning("{MethodName}: One or both patients not found - Patient1: {Patient1Id}, Patient2: {Patient2Id}", methodName, request.Patient1Id, request.Patient2Id);
                    return BaseResponse<RelationshipResponse>.CreateError("One or both patients not found", StatusCodes.Status404NotFound, "RELATIONSHIP_003");
                }

                if (patient1.Account == null || patient2.Account == null)
                {
                    _logger.LogWarning("{MethodName}: One or both patients do not have account information", methodName);
                    return BaseResponse<RelationshipResponse>.CreateError("Patient account information is missing", StatusCodes.Status404NotFound, "RELATIONSHIP_003_ACCOUNT");
                }

                // 4. Business rules validation
                var validationResult = await ValidateRelationshipBusinessRulesAsync(patient1, patient2, request.RelationshipType);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("{MethodName}: Business rule validation failed - {Error}", methodName, validationResult.ErrorMessage);
                    return BaseResponse<RelationshipResponse>.CreateError(validationResult.ErrorMessage, StatusCodes.Status400BadRequest, validationResult.ErrorCode);
                }

                // 5. Check for existing pending/approved relationship
                var existingRelationship = await _unitOfWork.Repository<Relationship>()
                    .AsQueryable()
                    .Where(r => ((r.Patient1Id == request.Patient1Id && r.Patient2Id == request.Patient2Id) ||
                                (r.Patient1Id == request.Patient2Id && r.Patient2Id == request.Patient1Id)) &&
                               r.RelationshipType == request.RelationshipType && !r.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingRelationship != null)
                {
                    if (existingRelationship.Status == RelationshipStatus.Pending)
                    {
                        _logger.LogWarning("{MethodName}: Relationship request already pending", methodName);
                        return BaseResponse<RelationshipResponse>.CreateError("A pending relationship request already exists", StatusCodes.Status409Conflict, "RELATIONSHIP_004_PENDING");
                    }
                    else if (existingRelationship.Status == RelationshipStatus.Approved)
                    {
                        _logger.LogWarning("{MethodName}: Relationship already approved", methodName);
                        return BaseResponse<RelationshipResponse>.CreateError("Relationship already exists and is approved", StatusCodes.Status409Conflict, "RELATIONSHIP_004_APPROVED");
                    }
                }

                // 6. Create relationship with Pending status
                var relationship = new Relationship(
                    Guid.NewGuid(),
                    request.Patient1Id,
                    request.Patient2Id,
                    request.RelationshipType
                )
                {
                    EstablishedDate = request.EstablishedDate,
                    Notes = request.Notes,
                    IsActive = request.IsActive,
                    Status = RelationshipStatus.Pending,
                    RequestedBy = currentPatientId,
                    ExpiresAt = DateTime.UtcNow.AddDays(7) // 7-day expiration for pending requests
                };

                await _unitOfWork.Repository<Relationship>().InsertAsync(relationship);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Relationship request created successfully with ID: {RelationshipId}", methodName, relationship.Id);

                // 7. Send email confirmation to Patient2
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

                // 8. Return response
                var createdRelationship = await GetRelationshipWithPatientsAsync(relationship.Id);
                var response = _mapper.Map<RelationshipResponse>(createdRelationship);

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

                var response = _mapper.Map<RelationshipResponse>(relationship);
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
                existingRelationship.UpdatedAt = DateTime.UtcNow.AddHours(7);

                await _unitOfWork.Repository<Relationship>().UpdateGuid(existingRelationship, relationshipId);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName} - Relationship updated successfully with ID: {RelationshipId}", methodName, relationshipId);

                // Get updated relationship with patient info
                var updatedRelationship = await GetRelationshipWithPatientsAsync(relationshipId);
                var response = _mapper.Map<RelationshipResponse>(updatedRelationship);

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
                if (relationshipId == Guid.Empty)
                {
                    return BaseResponse.CreateError("Relationship ID cannot be empty", StatusCodes.Status400BadRequest, "RELATIONSHIP_005");
                }

                var existingRelationship = await _unitOfWork.Repository<Relationship>()
                    .AsQueryable()
                    .Where(r => r.Id == relationshipId && !r.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingRelationship == null)
                {
                    return BaseResponse.CreateError("Relationship not found", StatusCodes.Status404NotFound, "RELATIONSHIP_006");
                }

                existingRelationship.IsDeleted = true;
                existingRelationship.UpdatedAt = DateTime.UtcNow.AddHours(7);

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
                if (string.IsNullOrWhiteSpace(nationalId))
                {
                    return BaseResponse<bool>.CreateError("National ID cannot be empty", StatusCodes.Status400BadRequest, "PATIENT_009");
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
                    patient.UpdatedAt = DateTime.UtcNow.AddHours(7);

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
                    patient.UpdatedAt = DateTime.UtcNow.AddHours(7);

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

                return BaseResponse<RelationshipResponse>.CreateSuccess(response, "Relationship rejected successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error rejecting relationship: {ErrorMessage}", methodName, ex.Message);
                return BaseResponse<RelationshipResponse>.CreateError($"An error occurred while rejecting relationship: {ex.Message}", StatusCodes.Status500InternalServerError, "RELATIONSHIP_REJECT_500");
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
        /// Validates business rules for relationship creation
        /// </summary>
        private async Task<(bool IsValid, string ErrorMessage, string ErrorCode)> ValidateRelationshipBusinessRulesAsync(
            Patient patient1, Patient patient2, RelationshipType relationshipType)
        {
            // Spouse relationship validation (1-1 rule)
            if (relationshipType == RelationshipType.Wife || relationshipType == RelationshipType.Husband)
            {
                // Check if Patient1 already has an approved spouse
                var existingSpouse1 = await _unitOfWork.Repository<Relationship>()
                    .AsQueryable()
                    .Where(r => (r.Patient1Id == patient1.Id || r.Patient2Id == patient1.Id) &&
                               (r.RelationshipType == RelationshipType.Wife || r.RelationshipType == RelationshipType.Husband) &&
                               r.Status == RelationshipStatus.Approved &&
                               !r.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingSpouse1 != null)
                {
                    return (false, "Patient already has an approved spouse relationship. Only one spouse relationship is allowed per patient.", "RELATIONSHIP_VALIDATION_001_SPOUSE_1");
                }

                // Check if Patient2 already has an approved spouse
                var existingSpouse2 = await _unitOfWork.Repository<Relationship>()
                    .AsQueryable()
                    .Where(r => (r.Patient1Id == patient2.Id || r.Patient2Id == patient2.Id) &&
                               (r.RelationshipType == RelationshipType.Wife || r.RelationshipType == RelationshipType.Husband) &&
                               r.Status == RelationshipStatus.Approved &&
                               !r.IsDeleted)
                    .FirstOrDefaultAsync();

                if (existingSpouse2 != null)
                {
                    return (false, "The other patient already has an approved spouse relationship. Only one spouse relationship is allowed per patient.", "RELATIONSHIP_VALIDATION_001_SPOUSE_2");
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
            var approvalUrl = $"{baseUrl}/api/relationship/approve/{relationship.Id}";
            var rejectionUrl = $"{baseUrl}/api/relationship/reject/{relationship.Id}";
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

        #endregion
    }
}
