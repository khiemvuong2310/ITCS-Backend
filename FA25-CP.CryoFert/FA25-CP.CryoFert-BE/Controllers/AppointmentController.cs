using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using FSCMS.Core.Enum;
using System;
using System.Threading.Tasks;
using FA25_CP.CryoFert_BE.AppStarts;
using FA25_CP.CryoFert_BE.Common.Attributes;
using FSCMS.Core.Interfaces;

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
        private readonly IRedisService _redisService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
        }

        #region Appointment CRUD Operations

        /// <summary>
        /// Get appointment by ID
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiDefaultResponse(typeof(AppointmentResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            var result = await _appointmentService.GetAppointmentByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get detailed appointment by ID with related data
        /// </summary>
        [HttpGet("{id:guid}/details")]
        [ApiDefaultResponse(typeof(AppointmentDetailResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetAppointmentDetails(Guid id)
        {
            var result = await _appointmentService.GetAppointmentDetailByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get all appointments with pagination and filtering
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Doctor,Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(AppointmentResponse))]
        public async Task<IActionResult> GetAllAppointments([FromQuery] GetAppointmentsRequest request)
        {
            var result = await _appointmentService.GetAllAppointmentsAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get appointments by treatment cycle ID
        /// </summary>
        [HttpGet("treatment-cycle/{treatmentCycleId:guid}")]
        [ApiDefaultResponse(typeof(AppointmentResponse))]
        public async Task<IActionResult> GetAppointmentsByTreatmentCycleId(Guid treatmentCycleId, [FromQuery] GetAppointmentsRequest request)
        {
            var result = await _appointmentService.GetAppointmentsByTreatmentCycleIdAsync(treatmentCycleId, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get appointments by doctor ID
        /// </summary>
        [HttpGet("doctor/{doctorId:guid}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(AppointmentResponse))]
        public async Task<IActionResult> GetAppointmentsByDoctorId(Guid doctorId, [FromQuery] GetAppointmentsRequest request)
        {
            var result = await _appointmentService.GetAppointmentsByDoctorIdAsync(doctorId, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get history appointments by patient ID
        /// </summary>
        [HttpGet("patient/{patientId:guid}/history")]
        [ApiDefaultResponse(typeof(AppointmentResponse))]
        public async Task<IActionResult> GetAppointmentsHistory(Guid patientId, [FromQuery] GetAppointmentsRequest request)
        {
            var result = await _appointmentService.GetAppointmentsHistoryByPatientIdAsync(patientId, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get appointment by slot ID
        /// </summary>
        [HttpGet("slot/{slotId:guid}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(AppointmentResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetAppointmentBySlotId(Guid slotId)
        {
            var result = await _appointmentService.GetAppointmentBySlotIdAsync(slotId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create new appointment
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Doctor,Receptionist,Patient,Laboratory Technician")]
        [ApiDefaultResponse(typeof(AppointmentResponse), UseDynamicWrapper = false)]
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
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Doctor,Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(AppointmentResponse), UseDynamicWrapper = false)]
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
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Doctor,Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
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
        [Authorize(Roles = "Doctor,Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
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
        [Authorize(Roles = "Doctor,Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
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
        [Authorize(Roles = "Doctor,Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
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
        [Authorize(Roles = "Doctor,Receptionist,Patient,Laboratory Technician")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
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
        [Authorize(Roles = "Doctor,Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
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
        [Authorize(Roles = "Doctor,Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
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
        [Authorize(Roles = "Doctor,Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
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

