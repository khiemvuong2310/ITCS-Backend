using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using System.Security.Claims;
using FA25_CP.CryoFert_BE.AppStarts;
using FA25_CP.CryoFert_BE.Common.Attributes;


namespace FA25_CP.CryoFert_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        #region Doctor CRUD Operations

        /// <summary>
        /// Get doctor by ID
        /// </summary>
        /// <param name="id">Doctor ID</param>
        /// <returns>Doctor information</returns>
        [HttpGet("{id}")]
        [Authorize]
        [ApiDefaultResponse(typeof(DoctorResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetDoctorById(Guid id)
        {
            var result = await _doctorService.GetDoctorByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get detailed doctor information by ID
        /// </summary>
        /// <param name="id">Doctor ID</param>
        /// <returns>Detailed doctor information</returns>
        [HttpGet("{id}/details")]
        [Authorize]
        [ApiDefaultResponse(typeof(DoctorDetailResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetDoctorDetails(Guid id)
        {
            var result = await _doctorService.GetDoctorDetailByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get doctor by account ID
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <returns>Doctor information</returns>
        [HttpGet("account/{accountId}")]
        [Authorize]
        [ApiDefaultResponse(typeof(DoctorResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetDoctorByAccountId(Guid accountId)
        {
            var result = await _doctorService.GetDoctorByAccountIdAsync(accountId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get all doctors with pagination and filtering
        /// </summary>
        /// <param name="request">Filter and pagination parameters</param>
        /// <returns>Paginated list of doctors</returns>
        [HttpGet]
        [Authorize]
        [ApiDefaultResponse(typeof(DoctorResponse))]
        public async Task<IActionResult> GetAllDoctors([FromQuery] GetDoctorsRequest request)
        {
            var result = await _doctorService.GetAllDoctorsAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get doctors who are available based on optional filters
        /// </summary>
        /// <param name="request">Availability filter parameters</param>
        /// <returns>Paginated list of available doctors</returns>
        [HttpGet("available")]
        [Authorize]
        [ApiDefaultResponse(typeof(DoctorResponse))]
        public async Task<IActionResult> GetAvailableDoctors([FromQuery] GetAvailableDoctorsRequest request)
        {
            var result = await _doctorService.GetAvailableDoctorsAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create new doctor
        /// </summary>
        /// <param name="request">Doctor creation request</param>
        /// <returns>Created doctor information</returns>
        [HttpPost]
        [Authorize(Roles = "Receptionist")]
        [ApiDefaultResponse(typeof(DoctorResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<DoctorResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    SystemCode = "INVALID_INPUT",
                    Message = "Invalid input data"
                });
            }

            var result = await _doctorService.CreateDoctorAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update existing doctor
        /// </summary>
        /// <param name="id">Doctor ID</param>
        /// <param name="request">Doctor update request</param>
        /// <returns>Updated doctor information</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Doctor")]
        [ApiDefaultResponse(typeof(DoctorResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> UpdateDoctor(Guid id, [FromBody] UpdateDoctorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<DoctorResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    SystemCode = "INVALID_INPUT",
                    Message = "Invalid input data"
                });
            }

            // Check if user is trying to update their own profile or is admin
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
                {
                    return Unauthorized(new BaseResponse<DoctorResponse>
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        SystemCode = "INVALID_TOKEN",
                        Message = "Invalid user token"
                    });
                }

                // Check if the doctor belongs to the current user
                var doctorResult = await _doctorService.GetDoctorByAccountIdAsync(userId);
                if (doctorResult.Data?.Id != id)
                {
                    return Forbid();
                }
            }

            var result = await _doctorService.UpdateDoctorAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete doctor (soft delete)
        /// </summary>
        /// <param name="id">Doctor ID</param>
        /// <returns>Success or error response</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Receptionist")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            var result = await _doctorService.DeleteDoctorAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update doctor status (active/inactive)
        /// </summary>
        /// <param name="id">Doctor ID</param>
        /// <param name="isActive">New status</param>
        /// <returns>Success or error response</returns>
        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Receptionist")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> UpdateDoctorStatus(Guid id, [FromBody] bool isActive)
        {
            var result = await _doctorService.UpdateDoctorStatusAsync(id, isActive);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        #endregion

        #region Utility Endpoints

        /// <summary>
        /// Get doctor statistics
        /// </summary>
        /// <returns>Doctor statistics</returns>
        [HttpGet("statistics")]
        [Authorize(Roles = "Receptionist")]
        [ApiDefaultResponse(typeof(DoctorStatisticsResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetDoctorStatistics()
        {
            var result = await _doctorService.GetDoctorStatisticsAsync();
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get available specialties
        /// </summary>
        /// <returns>List of available specialties</returns>
        [HttpGet("specialties")]
        [Authorize]
        [ApiDefaultResponse(typeof(List<string>), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetAvailableSpecialties()
        {
            var result = await _doctorService.GetAvailableSpecialtiesAsync();
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Check if doctor exists
        /// </summary>
        /// <param name="id">Doctor ID</param>
        /// <returns>Boolean indicating if doctor exists</returns>
        [HttpGet("{id}/exists")]
        [Authorize]
        [ApiDefaultResponse(typeof(bool), UseDynamicWrapper = false)]
        public async Task<IActionResult> DoctorExists(Guid id)
        {
            try
            {
                var exists = await _doctorService.DoctorExistsAsync(id);
                return Ok(exists);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An error occurred while checking doctor existence"
                });
            }
        }

        /// <summary>
        /// Check if badge ID is unique
        /// </summary>
        /// <param name="badgeId">Badge ID to check</param>
        /// <param name="excludeDoctorId">Doctor ID to exclude from check (optional)</param>
        /// <returns>Boolean indicating if badge ID is unique</returns>
        [HttpGet("badge/{badgeId}/unique")]
        [Authorize(Roles = "Receptionist")]
        [ApiDefaultResponse(typeof(bool), UseDynamicWrapper = false)]
        public async Task<IActionResult> IsBadgeIdUnique(string badgeId, [FromQuery] Guid? excludeDoctorId = null)
        {
            try
            {
                var isUnique = await _doctorService.IsBadgeIdUniqueAsync(badgeId, excludeDoctorId);
                return Ok(isUnique);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An error occurred while checking badge ID uniqueness"
                });
            }
        }

        #endregion
    }
}
