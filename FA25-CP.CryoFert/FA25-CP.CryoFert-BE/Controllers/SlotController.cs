using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using System.Security.Claims;
using FA25_CP.CryoFert_BE.AppStarts;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// Slot Management Controller - Handles slot CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/slots")]
    [Produces("application/json")]
    public class SlotController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public SlotController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        #region Slot CRUD Operations

        /// <summary>
        /// Get slot by ID
        /// </summary>
        /// <param name="id">Slot ID</param>
        /// <returns>Slot information</returns>
        [HttpGet("{id}")]
        [Authorize]
        [ApiDefaultResponse(typeof(SlotResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetSlotById(Guid id)
        {
            var result = await _doctorService.GetSlotByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get detailed slot by ID with appointment information
        /// </summary>
        /// <param name="id">Slot ID</param>
        /// <returns>Detailed slot information</returns>
        [HttpGet("{id}/details")]
        [Authorize]
        [ApiDefaultResponse(typeof(SlotDetailResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetSlotDetails(Guid id)
        {
            var result = await _doctorService.GetSlotDetailByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get all slots with pagination and filtering
        /// </summary>
        /// <param name="request">Filter and pagination parameters</param>
        /// <returns>Paginated list of slots</returns>
        [HttpGet]
        [Authorize]
        [ApiDefaultResponse(typeof(SlotResponse))]
        public async Task<IActionResult> GetAllSlots([FromQuery] GetSlotsRequest request)
        {
            var result = await _doctorService.GetAllSlotsAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get slots for a specific doctor schedule
        /// </summary>
        /// <param name="scheduleId">Doctor schedule ID</param>
        /// <param name="request">Filter and pagination parameters</param>
        /// <returns>Paginated list of slots</returns>
        [HttpGet("schedule/{scheduleId}")]
        [Authorize]
        [ApiDefaultResponse(typeof(SlotResponse))]
        public async Task<IActionResult> GetSlotsByScheduleId(Guid scheduleId, [FromQuery] GetSlotsRequest request)
        {
            var result = await _doctorService.GetSlotsByScheduleIdAsync(scheduleId, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get available slots for a specific doctor and date range
        /// </summary>
        /// <param name="doctorId">Doctor ID</param>
        /// <param name="dateFrom">Start date for search</param>
        /// <param name="dateTo">End date for search</param>
        /// <returns>Available slots</returns>
        [HttpGet("available/doctor/{doctorId}")]
        [Authorize]
        [ApiDefaultResponse(typeof(SlotResponse))]
        public async Task<IActionResult> GetAvailableSlots(Guid doctorId, [FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
        {
            if (dateFrom == default || dateTo == default)
            {
                return BadRequest(new DynamicResponse<SlotResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    SystemCode = "INVALID_DATE_RANGE",
                    Message = "Valid date range is required",
                    Data = new List<SlotResponse>()
                });
            }

            if (dateFrom > dateTo)
            {
                return BadRequest(new DynamicResponse<SlotResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    SystemCode = "INVALID_DATE_RANGE",
                    Message = "Start date must be before or equal to end date",
                    Data = new List<SlotResponse>()
                });
            }

            var result = await _doctorService.GetAvailableSlotsAsync(doctorId, dateFrom, dateTo);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create new slot
        /// </summary>
        /// <param name="request">Slot creation request</param>
        /// <returns>Created slot information</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Doctor")]
        [ApiDefaultResponse(typeof(SlotResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> CreateSlot([FromBody] CreateSlotRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<SlotResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    SystemCode = "INVALID_INPUT",
                    Message = "Invalid input data"
                });
            }

            // Check if user is trying to create slot for their own schedule or is admin
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
                {
                    return Unauthorized(new BaseResponse<SlotResponse>
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        SystemCode = "INVALID_TOKEN",
                        Message = "Invalid user token"
                    });
                }

                // Get the schedule to check ownership
                var scheduleResult = await _doctorService.GetDoctorScheduleByIdAsync(request.DoctorScheduleId);
                if (scheduleResult.Data == null)
                {
                    return BadRequest(new BaseResponse<SlotResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "SCHEDULE_NOT_FOUND",
                        Message = "Doctor schedule not found"
                    });
                }

                // Check if the doctor belongs to the current user
                var doctorResult = await _doctorService.GetDoctorByAccountIdAsync(userId);
                if (doctorResult.Data?.Id != scheduleResult.Data.DoctorId)
                {
                    return Forbid();
                }
            }

            var result = await _doctorService.CreateSlotAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create multiple slots for a schedule
        /// </summary>
        /// <param name="scheduleId">Doctor schedule ID</param>
        /// <param name="slotDuration">Duration of each slot in minutes (default: 30)</param>
        /// <returns>Number of created slots</returns>
        [HttpPost("schedule/{scheduleId}/generate")]
        [Authorize(Roles = "Admin,Doctor")]
        [ApiDefaultResponse(typeof(int), UseDynamicWrapper = false)]
        public async Task<IActionResult> CreateSlotsForSchedule(Guid scheduleId, [FromQuery] int slotDuration = 30)
        {
            if (slotDuration <= 0 || slotDuration > 480) // Max 8 hours
            {
                return BadRequest(new BaseResponse<int>
                {
                    Code = StatusCodes.Status400BadRequest,
                    SystemCode = "INVALID_SLOT_DURATION",
                    Message = "Slot duration must be between 1 and 480 minutes"
                });
            }

            // Check if user is trying to create slots for their own schedule or is admin
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
                {
                    return Unauthorized(new BaseResponse<int>
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        SystemCode = "INVALID_TOKEN",
                        Message = "Invalid user token"
                    });
                }

                // Get the schedule to check ownership
                var scheduleResult = await _doctorService.GetDoctorScheduleByIdAsync(scheduleId);
                if (scheduleResult.Data == null)
                {
                    return BadRequest(new BaseResponse<int>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "SCHEDULE_NOT_FOUND",
                        Message = "Doctor schedule not found"
                    });
                }

                // Check if the doctor belongs to the current user
                var doctorResult = await _doctorService.GetDoctorByAccountIdAsync(userId);
                if (doctorResult.Data?.Id != scheduleResult.Data.DoctorId)
                {
                    return Forbid();
                }
            }

            var result = await _doctorService.CreateSlotsForScheduleAsync(scheduleId, slotDuration);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update existing slot
        /// </summary>
        /// <param name="id">Slot ID</param>
        /// <param name="request">Slot update request</param>
        /// <returns>Updated slot information</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        [ApiDefaultResponse(typeof(SlotResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> UpdateSlot(Guid id, [FromBody] UpdateSlotRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<SlotResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    SystemCode = "INVALID_INPUT",
                    Message = "Invalid input data"
                });
            }

            // Check if user is trying to update their own slot or is admin
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
                {
                    return Unauthorized(new BaseResponse<SlotResponse>
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        SystemCode = "INVALID_TOKEN",
                        Message = "Invalid user token"
                    });
                }

                // Get the slot to check ownership
                var slotResult = await _doctorService.GetSlotByIdAsync(id);
                if (slotResult.Data?.Schedule?.Doctor == null)
                {
                    return NotFound();
                }

                // Check if the doctor belongs to the current user
                var doctorResult = await _doctorService.GetDoctorByAccountIdAsync(userId);
                if (doctorResult.Data?.Id != slotResult.Data.Schedule.Doctor.Id)
                {
                    return Forbid();
                }
            }

            var result = await _doctorService.UpdateSlotAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete slot (soft delete)
        /// </summary>
        /// <param name="id">Slot ID</param>
        /// <returns>Success or error response</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> DeleteSlot(Guid id)
        {
            // Check if user is trying to delete their own slot or is admin
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole != "Admin")
            {
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
                {
                    return Unauthorized(new BaseResponse
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        SystemCode = "INVALID_TOKEN",
                        Message = "Invalid user token"
                    });
                }

                // Get the slot to check ownership
                var slotResult = await _doctorService.GetSlotByIdAsync(id);
                if (slotResult.Data?.Schedule?.Doctor == null)
                {
                    return NotFound();
                }

                // Check if the doctor belongs to the current user
                var doctorResult = await _doctorService.GetDoctorByAccountIdAsync(userId);
                if (doctorResult.Data?.Id != slotResult.Data.Schedule.Doctor.Id)
                {
                    return Forbid();
                }
            }

            var result = await _doctorService.DeleteSlotAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update slot booking status
        /// </summary>
        /// <param name="id">Slot ID</param>
        /// <param name="isBooked">New booking status</param>
        /// <returns>Success or error response</returns>
        [HttpPatch("{id}/booking-status")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> UpdateSlotBookingStatus(Guid id, [FromBody] bool isBooked)
        {
            // Check if user has permission to update booking status
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == "Doctor")
            {
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
                {
                    return Unauthorized(new BaseResponse
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        SystemCode = "INVALID_TOKEN",
                        Message = "Invalid user token"
                    });
                }

                // Get the slot to check ownership
                var slotResult = await _doctorService.GetSlotByIdAsync(id);
                if (slotResult.Data?.Schedule?.Doctor == null)
                {
                    return NotFound();
                }

                // Check if the doctor belongs to the current user
                var doctorResult = await _doctorService.GetDoctorByAccountIdAsync(userId);
                if (doctorResult.Data?.Id != slotResult.Data.Schedule.Doctor.Id)
                {
                    return Forbid();
                }
            }

            var result = await _doctorService.UpdateSlotBookingStatusAsync(id, isBooked);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        #endregion
    }
}
