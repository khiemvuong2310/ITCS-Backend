using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FA25_CP.CryoFert_BE.AppStarts;

namespace FA25_CP.CryoFert_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
        }

        #region Patient CRUD Operations

        /// <summary>
        /// Creates a new patient
        /// </summary>
        /// <param name="request">Patient creation request</param>
        /// <returns>Created patient response</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Receptionist")]
        [ApiDefaultResponse(typeof(PatientResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<PatientResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data"
                });
            }

            var result = await _patientService.CreatePatientAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Gets a patient by ID
        /// </summary>
        /// <param name="id">Patient ID</param>
        /// <returns>Patient response</returns>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(PatientResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetPatientById(Guid id)
        {
            var result = await _patientService.GetPatientByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Gets detailed patient information by ID
        /// </summary>
        /// <param name="id">Patient ID</param>
        /// <returns>Detailed patient response</returns>
        [HttpGet("{id:guid}/details")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(PatientDetailResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetPatientDetails(Guid id)
        {
            var result = await _patientService.GetPatientDetailsByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Gets a patient by patient code
        /// </summary>
        /// <param name="code">Patient code</param>
        /// <returns>Patient response</returns>
        [HttpGet("by-code/{code}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(PatientResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetPatientByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return BadRequest(new BaseResponse<PatientResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Patient code is required"
                });
            }

            var result = await _patientService.GetPatientByCodeAsync(code);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Gets a patient by national ID
        /// </summary>
        /// <param name="nationalId">National ID</param>
        /// <returns>Patient response</returns>
        [HttpGet("by-national-id/{nationalId}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(PatientResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetPatientByNationalId(string nationalId)
        {
            if (string.IsNullOrWhiteSpace(nationalId))
            {
                return BadRequest(new BaseResponse<PatientResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "National ID is required"
                });
            }

            var result = await _patientService.GetPatientByNationalIdAsync(nationalId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Gets a patient by account ID
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <returns>Patient response</returns>
        [HttpGet("by-account/{accountId:guid}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(PatientResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetPatientByAccountId(Guid accountId)
        {
            var result = await _patientService.GetPatientByAccountIdAsync(accountId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Gets all patients with pagination and filtering
        /// </summary>
        /// <param name="request">Get patients request</param>
        /// <returns>Paginated patient responses</returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(PatientResponse))]
        public async Task<IActionResult> GetAllPatients([FromQuery] GetPatientsRequest request)
        {
            var result = await _patientService.GetAllPatientsAsync(request ?? new GetPatientsRequest());
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Updates an existing patient
        /// </summary>
        /// <param name="id">Patient ID</param>
        /// <param name="request">Patient update request</param>
        /// <returns>Updated patient response</returns>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,Receptionist")]
        [ApiDefaultResponse(typeof(PatientResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> UpdatePatient(Guid id, [FromBody] UpdatePatientRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<PatientResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data"
                });
            }

            var result = await _patientService.UpdatePatientAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Updates patient status (active/inactive)
        /// </summary>
        /// <param name="id">Patient ID</param>
        /// <param name="request">Status update request</param>
        /// <returns>Updated patient response</returns>
        [HttpPatch("{id:guid}/status")]
        [Authorize(Roles = "Admin,Receptionist")]
        [ApiDefaultResponse(typeof(PatientResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> UpdatePatientStatus(Guid id, [FromBody] UpdatePatientStatusRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<PatientResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data"
                });
            }

            var result = await _patientService.UpdatePatientStatusAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Soft deletes a patient
        /// </summary>
        /// <param name="id">Patient ID</param>
        /// <returns>Operation result</returns>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            var result = await _patientService.DeletePatientAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        #endregion

        #region Search and Utility Operations

        /// <summary>
        /// Searches patients by various criteria
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        /// <param name="request">Pagination request</param>
        /// <returns>Paginated patient search results</returns>
        [HttpGet("search")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(PatientSearchResult))]
        public async Task<IActionResult> SearchPatients([FromQuery] string searchTerm, [FromQuery] GetPatientsRequest request)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest(new DynamicResponse<PatientSearchResult>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Search term is required"
                });
            }

            var result = await _patientService.SearchPatientsAsync(searchTerm, request ?? new GetPatientsRequest());
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Gets patient statistics
        /// </summary>
        /// <returns>Patient statistics</returns>
        [HttpGet("statistics")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(PatientStatisticsResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetPatientStatistics()
        {
            var result = await _patientService.GetPatientStatisticsAsync();
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Validates if a patient code is unique
        /// </summary>
        /// <param name="patientCode">Patient code to validate</param>
        /// <param name="excludePatientId">Patient ID to exclude from validation (for updates)</param>
        /// <returns>Validation result</returns>
        [HttpGet("validate-patient-code")]
        [Authorize(Roles = "Admin,Receptionist")]
        [ApiDefaultResponse(typeof(bool), UseDynamicWrapper = false)]
        public async Task<IActionResult> ValidatePatientCode([FromQuery] string patientCode, [FromQuery] Guid? excludePatientId = null)
        {
            if (string.IsNullOrWhiteSpace(patientCode))
            {
                return BadRequest(new BaseResponse<bool>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Patient code is required"
                });
            }

            var result = await _patientService.ValidatePatientCodeAsync(patientCode, excludePatientId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Validates if a national ID is unique
        /// </summary>
        /// <param name="nationalId">National ID to validate</param>
        /// <param name="excludePatientId">Patient ID to exclude from validation (for updates)</param>
        /// <returns>Validation result</returns>
        [HttpGet("validate-national-id")]
        [Authorize(Roles = "Admin,Receptionist")]
        [ApiDefaultResponse(typeof(bool), UseDynamicWrapper = false)]
        public async Task<IActionResult> ValidateNationalId([FromQuery] string nationalId, [FromQuery] Guid? excludePatientId = null)
        {
            if (string.IsNullOrWhiteSpace(nationalId))
            {
                return BadRequest(new BaseResponse<bool>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "National ID is required"
                });
            }

            var result = await _patientService.ValidateNationalIdAsync(nationalId, excludePatientId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Gets available blood types
        /// </summary>
        /// <returns>List of blood types</returns>
        [HttpGet("blood-types")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(List<string>), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetAvailableBloodTypes()
        {
            var result = await _patientService.GetAvailableBloodTypesAsync();
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Gets related patients by relationship type
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <param name="relationshipType">Relationship type</param>
        /// <returns>List of related patients</returns>
        [HttpGet("{patientId:guid}/related")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(List<RelatedPatientInfo>), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetRelatedPatients(Guid patientId, [FromQuery] FSCMS.Core.Enum.RelationshipType relationshipType)
        {
            var result = await _patientService.GetRelatedPatientsAsync(patientId, relationshipType);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        #endregion

        #region Bulk Operations

        /// <summary>
        /// Bulk updates patient status
        /// </summary>
        /// <param name="request">Bulk status update request</param>
        /// <returns>Operation result</returns>
        [HttpPatch("bulk/status")]
        [Authorize(Roles = "Admin")]
        [ApiDefaultResponse(typeof(int), UseDynamicWrapper = false)]
        public async Task<IActionResult> BulkUpdatePatientStatus([FromBody] BulkUpdatePatientStatusRequest request)
        {
            if (!ModelState.IsValid || request?.PatientIds == null || !request.PatientIds.Any())
            {
                return BadRequest(new BaseResponse<int>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data or empty patient IDs list"
                });
            }

            var result = await _patientService.BulkUpdatePatientStatusAsync(request.PatientIds, request.IsActive, request.Reason);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Bulk deletes patients
        /// </summary>
        /// <param name="request">Bulk delete request</param>
        /// <returns>Operation result</returns>
        [HttpDelete("bulk")]
        [Authorize(Roles = "Admin")]
        [ApiDefaultResponse(typeof(int), UseDynamicWrapper = false)]
        public async Task<IActionResult> BulkDeletePatients([FromBody] BulkDeletePatientsRequest request)
        {
            if (!ModelState.IsValid || request?.PatientIds == null || !request.PatientIds.Any())
            {
                return BadRequest(new BaseResponse<int>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data or empty patient IDs list"
                });
            }

            var result = await _patientService.BulkDeletePatientsAsync(request.PatientIds);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        #endregion
    }

    /// <summary>
    /// Request model for bulk status update
    /// </summary>
    public class BulkUpdatePatientStatusRequest
    {
        public List<Guid> PatientIds { get; set; } = new();
        public bool IsActive { get; set; }
        public string? Reason { get; set; }
    }

    /// <summary>
    /// Request model for bulk delete
    /// </summary>
    public class BulkDeletePatientsRequest
    {
        public List<Guid> PatientIds { get; set; } = new();
    }
}
