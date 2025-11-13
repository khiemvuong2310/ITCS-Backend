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
    /// <summary>
    /// Doctor Schedule Management Controller - Handles doctor schedule CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/doctor-schedules")]
    [Produces("application/json")]
    public class DoctorScheduleController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorScheduleController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        #region Doctor Schedule CRUD Operations

        /// <summary>
        /// Get doctor schedule by ID
        /// </summary>
        /// <param name="id">Schedule ID</param>
        /// <returns>Doctor schedule information</returns>
        [HttpGet("{id}")]
        [Authorize]
        [ApiDefaultResponse(typeof(DoctorScheduleResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetDoctorScheduleById(Guid id)
        {
            var result = await _doctorService.GetDoctorScheduleByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get detailed doctor schedule by ID with slots
        /// </summary>
        /// <param name="id">Schedule ID</param>
        /// <returns>Detailed doctor schedule information</returns>
        [HttpGet("{id}/details")]
        [Authorize]
        [ApiDefaultResponse(typeof(DoctorScheduleDetailResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetDoctorScheduleDetails(Guid id)
        {
            var result = await _doctorService.GetDoctorScheduleDetailByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get all doctor schedules with pagination and filtering
        /// </summary>
        /// <param name="request">Filter and pagination parameters</param>
        /// <returns>Paginated list of doctor schedules</returns>
        [HttpGet]
        [Authorize]
        [ApiDefaultResponse(typeof(DoctorScheduleResponse))]
        public async Task<IActionResult> GetAllDoctorSchedules([FromQuery] GetDoctorSchedulesRequest request)
        {
            var result = await _doctorService.GetAllDoctorSchedulesAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get schedules for a specific doctor
        /// </summary>
        /// <param name="doctorId">Doctor ID</param>
        /// <param name="request">Filter and pagination parameters</param>
        /// <returns>Paginated list of doctor schedules</returns>
        [HttpGet("doctor/{doctorId}")]
        [Authorize]
        [ApiDefaultResponse(typeof(DoctorScheduleResponse))]
        public async Task<IActionResult> GetDoctorSchedulesByDoctorId(Guid doctorId, [FromQuery] GetDoctorSchedulesRequest request)
        {
            var result = await _doctorService.GetDoctorSchedulesByDoctorIdAsync(doctorId, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create new doctor schedule
        /// </summary>
        /// <param name="request">Schedule creation request</param>
        /// <returns>Created schedule information</returns>
        [HttpPost]
        [Authorize(Roles = "Doctor")]
        [ApiDefaultResponse(typeof(DoctorScheduleResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> CreateDoctorSchedule([FromBody] CreateDoctorScheduleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<DoctorScheduleResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    SystemCode = "INVALID_INPUT",
                    Message = "Invalid input data"
                });
            }

            // Check if user is trying to create schedule for themselves or is admin
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
            {
                return Unauthorized(new BaseResponse<DoctorScheduleResponse>
                {
                    Code = StatusCodes.Status401Unauthorized,
                    SystemCode = "INVALID_TOKEN",
                    Message = "Invalid user token"
                });
            }

            // Check if the doctor belongs to the current user
            var doctorResult = await _doctorService.GetDoctorByAccountIdAsync(userId);
            if (doctorResult.Data?.Id != request.DoctorId)
            {
                return Forbid();
            }

            var result = await _doctorService.CreateDoctorScheduleAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update existing doctor schedule
        /// </summary>
        /// <param name="id">Schedule ID</param>
        /// <param name="request">Schedule update request</param>
        /// <returns>Updated schedule information</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Doctor")]
        [ApiDefaultResponse(typeof(DoctorScheduleResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> UpdateDoctorSchedule(Guid id, [FromBody] UpdateDoctorScheduleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<DoctorScheduleResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    SystemCode = "INVALID_INPUT",
                    Message = "Invalid input data"
                });
            }

            // Check if user is trying to update their own schedule or is admin
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
            {
                return Unauthorized(new BaseResponse<DoctorScheduleResponse>
                {
                    Code = StatusCodes.Status401Unauthorized,
                    SystemCode = "INVALID_TOKEN",
                    Message = "Invalid user token"
                });
            }

            // Get the schedule to check ownership
            var scheduleResult = await _doctorService.GetDoctorScheduleByIdAsync(id);
            if (scheduleResult.Data == null)
            {
                return NotFound();
            }

            // Check if the doctor belongs to the current user
            var doctorResult = await _doctorService.GetDoctorByAccountIdAsync(userId);
            if (doctorResult.Data?.Id != scheduleResult.Data.DoctorId)
            {
                return Forbid();
            }

            var result = await _doctorService.UpdateDoctorScheduleAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete doctor schedule (soft delete)
        /// </summary>
        /// <param name="id">Schedule ID</param>
        /// <returns>Success or error response</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Doctor")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> DeleteDoctorSchedule(Guid id)
        {
            // Check if user is trying to delete their own schedule or is admin
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
            {
                return Unauthorized(new BaseResponse
                {
                    Code = StatusCodes.Status401Unauthorized,
                    SystemCode = "INVALID_TOKEN",
                    Message = "Invalid user token"
                });
            }

            // Get the schedule to check ownership
            var scheduleResult = await _doctorService.GetDoctorScheduleByIdAsync(id);
            if (scheduleResult.Data == null)
            {
                return NotFound();
            }

            // Check if the doctor belongs to the current user
            var doctorResult = await _doctorService.GetDoctorByAccountIdAsync(userId);
            if (doctorResult.Data?.Id != scheduleResult.Data.DoctorId)
            {
                return Forbid();
            }

            var result = await _doctorService.DeleteDoctorScheduleAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update schedule availability
        /// </summary>
        /// <param name="id">Schedule ID</param>
        /// <param name="isAvailable">New availability status</param>
        /// <returns>Success or error response</returns>
        [HttpPatch("{id}/availability")]
        [Authorize(Roles = "Doctor")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> UpdateScheduleAvailability(Guid id, [FromBody] bool isAvailable)
        {
            // Check if user is trying to update their own schedule or is admin
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
            {
                return Unauthorized(new BaseResponse
                {
                    Code = StatusCodes.Status401Unauthorized,
                    SystemCode = "INVALID_TOKEN",
                    Message = "Invalid user token"
                });
            }

            // Get the schedule to check ownership
            var scheduleResult = await _doctorService.GetDoctorScheduleByIdAsync(id);
            if (scheduleResult.Data == null)
            {
                return NotFound();
            }

            // Check if the doctor belongs to the current user
            var doctorResult = await _doctorService.GetDoctorByAccountIdAsync(userId);
            if (doctorResult.Data?.Id != scheduleResult.Data.DoctorId)
            {
                return Forbid();
            }

            var result = await _doctorService.UpdateScheduleAvailabilityAsync(id, isAvailable);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get busy schedule dates for a doctor
        /// </summary>
        /// <param name="request">Request containing doctor ID and optional date range</param>
        /// <returns>List of work dates for the doctor</returns>
        [HttpGet("busy-dates")]
        [Authorize]
        [ApiDefaultResponse(typeof(BusyScheduleDateResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetBusyScheduleDate([FromBody] GetBusyScheduleDateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<BusyScheduleDateResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    SystemCode = "INVALID_INPUT",
                    Message = "Invalid input data"
                });
            }

            var result = await _doctorService.GetBusyScheduleDateAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        #endregion
    }
}
