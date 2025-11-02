using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using FSCMS.Core.Enum;
using System;
using System.Threading.Tasks;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// Appointment Controller - Handles appointment management operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
        }

        #region Appointment CRUD Operations

        /// <summary>
        /// Get appointment by ID
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <returns>Appointment information</returns>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist,Patient")]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            var result = await _appointmentService.GetAppointmentByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get detailed appointment by ID with related data
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <returns>Detailed appointment information</returns>
        [HttpGet("{id:guid}/details")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse<AppointmentDetailResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentDetailResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentDetailResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentDetailResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentDetailResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAppointmentDetails(Guid id)
        {
            var result = await _appointmentService.GetAppointmentDetailByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get all appointments with pagination and filtering
        /// </summary>
        /// <param name="request">Filter and pagination parameters</param>
        /// <returns>Paginated list of appointments</returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ProducesResponseType(typeof(DynamicResponse<AppointmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DynamicResponse<AppointmentResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(DynamicResponse<AppointmentResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAppointments([FromQuery] GetAppointmentsRequest request)
        {
            var result = await _appointmentService.GetAllAppointmentsAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get appointments by treatment cycle ID
        /// </summary>
        /// <param name="treatmentCycleId">Treatment cycle ID</param>
        /// <param name="request">Filter and pagination parameters</param>
        /// <returns>Paginated list of appointments</returns>
        [HttpGet("treatment-cycle/{treatmentCycleId:guid}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist,Patient")]
        [ProducesResponseType(typeof(DynamicResponse<AppointmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DynamicResponse<AppointmentResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DynamicResponse<AppointmentResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DynamicResponse<AppointmentResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(DynamicResponse<AppointmentResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAppointmentsByTreatmentCycleId(Guid treatmentCycleId, [FromQuery] GetAppointmentsRequest request)
        {
            var result = await _appointmentService.GetAppointmentsByTreatmentCycleIdAsync(treatmentCycleId, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get appointments by doctor ID
        /// </summary>
        /// <param name="doctorId">Doctor ID</param>
        /// <param name="request">Filter and pagination parameters</param>
        /// <returns>Paginated list of appointments</returns>
        [HttpGet("doctor/{doctorId:guid}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ProducesResponseType(typeof(DynamicResponse<AppointmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DynamicResponse<AppointmentResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DynamicResponse<AppointmentResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DynamicResponse<AppointmentResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(DynamicResponse<AppointmentResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAppointmentsByDoctorId(Guid doctorId, [FromQuery] GetAppointmentsRequest request)
        {
            var result = await _appointmentService.GetAppointmentsByDoctorIdAsync(doctorId, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get appointment by slot ID
        /// </summary>
        /// <param name="slotId">Slot ID</param>
        /// <returns>Appointment information</returns>
        [HttpGet("slot/{slotId:guid}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAppointmentBySlotId(Guid slotId)
        {
            var result = await _appointmentService.GetAppointmentBySlotIdAsync(slotId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create new appointment
        /// </summary>
        /// <param name="request">Appointment creation request</param>
        /// <returns>Created appointment information</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Doctor,Receptionist,Patient")]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<AppointmentResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data",
                    SystemCode = "INVALID_REQUEST"
                });
            }

            var result = await _appointmentService.CreateAppointmentAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update existing appointment
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <param name="request">Appointment update request</param>
        /// <returns>Updated appointment information</returns>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<AppointmentResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAppointment(Guid id, [FromBody] UpdateAppointmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<AppointmentResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data",
                    SystemCode = "INVALID_REQUEST"
                });
            }

            var result = await _appointmentService.UpdateAppointmentAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete appointment (soft delete)
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <returns>Success or error response</returns>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            var result = await _appointmentService.DeleteAppointmentAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        #endregion

        #region Appointment Status Operations

        /// <summary>
        /// Update appointment status
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <param name="status">New appointment status</param>
        /// <returns>Success or error response</returns>
        [HttpPut("{id:guid}/status")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAppointmentStatus(Guid id, [FromBody] UpdateAppointmentStatusRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data",
                    SystemCode = "INVALID_REQUEST"
                });
            }

            var result = await _appointmentService.UpdateAppointmentStatusAsync(id, request.Status);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Check in for an appointment
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <returns>Success or error response</returns>
        [HttpPost("{id:guid}/check-in")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CheckInAppointment(Guid id)
        {
            var result = await _appointmentService.CheckInAppointmentAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Check out for an appointment
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <returns>Success or error response</returns>
        [HttpPost("{id:guid}/check-out")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CheckOutAppointment(Guid id)
        {
            var result = await _appointmentService.CheckOutAppointmentAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Cancel an appointment
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <param name="request">Cancellation request with optional reason</param>
        /// <returns>Success or error response</returns>
        [HttpPost("{id:guid}/cancel")]
        [Authorize(Roles = "Admin,Doctor,Receptionist,Patient")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CancelAppointment(Guid id, [FromBody] CancelAppointmentRequest? request = null)
        {
            var cancellationReason = request?.CancellationReason;
            var result = await _appointmentService.CancelAppointmentAsync(id, cancellationReason);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        #endregion

        #region Appointment Doctor Management

        /// <summary>
        /// Add doctor to appointment
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <param name="request">Request containing doctor ID, role, and notes</param>
        /// <returns>Success or error response</returns>
        [HttpPost("{id:guid}/doctors")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddDoctorToAppointment(Guid id, [FromBody] AddDoctorToAppointmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data",
                    SystemCode = "INVALID_REQUEST"
                });
            }

            var result = await _appointmentService.AddDoctorToAppointmentAsync(id, request.DoctorId, request.Role, request.Notes);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Remove doctor from appointment
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <param name="doctorId">Doctor ID</param>
        /// <returns>Success or error response</returns>
        [HttpDelete("{id:guid}/doctors/{doctorId:guid}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveDoctorFromAppointment(Guid id, Guid doctorId)
        {
            var result = await _appointmentService.RemoveDoctorFromAppointmentAsync(id, doctorId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update doctor role in appointment
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <param name="doctorId">Doctor ID</param>
        /// <param name="request">Request containing new role and notes</param>
        /// <returns>Success or error response</returns>
        [HttpPut("{id:guid}/doctors/{doctorId:guid}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDoctorRoleInAppointment(Guid id, Guid doctorId, [FromBody] UpdateDoctorRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data",
                    SystemCode = "INVALID_REQUEST"
                });
            }

            var result = await _appointmentService.UpdateDoctorRoleInAppointmentAsync(id, doctorId, request.Role, request.Notes);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        #endregion
    }

    #region Request Models

    /// <summary>
    /// Request model for updating appointment status
    /// </summary>
    public class UpdateAppointmentStatusRequest
    {
        public AppointmentStatus Status { get; set; }
    }

    /// <summary>
    /// Request model for cancelling an appointment
    /// </summary>
    public class CancelAppointmentRequest
    {
        public string? CancellationReason { get; set; }
    }

    /// <summary>
    /// Request model for adding doctor to appointment
    /// </summary>
    public class AddDoctorToAppointmentRequest
    {
        public Guid DoctorId { get; set; }
        public string? Role { get; set; }
        public string? Notes { get; set; }
    }

    /// <summary>
    /// Request model for updating doctor role in appointment
    /// </summary>
    public class UpdateDoctorRoleRequest
    {
        public string? Role { get; set; }
        public string? Notes { get; set; }
    }

    #endregion
}

