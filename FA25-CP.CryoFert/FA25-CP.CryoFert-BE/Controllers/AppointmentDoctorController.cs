using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using System;
using System.Threading.Tasks;
using FA25_CP.CryoFert_BE.AppStarts;
using FA25_CP.CryoFert_BE.Common.Attributes;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// AppointmentDoctor Controller - Manages assignments between Appointments and Doctors
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class AppointmentDoctorController : ControllerBase
    {
        private readonly IAppointmentDoctorService _appointmentDoctorService;

        public AppointmentDoctorController(IAppointmentDoctorService appointmentDoctorService)
        {
            _appointmentDoctorService = appointmentDoctorService ?? throw new ArgumentNullException(nameof(appointmentDoctorService));
        }

        /// <summary>
        /// Get an AppointmentDoctor assignment by ID
        /// </summary>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(AppointmentDoctorResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _appointmentDoctorService.GetByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get all AppointmentDoctor assignments with filters and pagination
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(AppointmentDoctorResponse))]
        public async Task<IActionResult> GetAll([FromQuery] GetAppointmentDoctorsRequest request)
        {
            var result = await _appointmentDoctorService.GetAllAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get AppointmentDoctor assignments by Appointment ID
        /// </summary>
        [HttpGet("appointment/{appointmentId:guid}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(AppointmentDoctorResponse))]
        public async Task<IActionResult> GetByAppointmentId(Guid appointmentId, [FromQuery] GetAppointmentDoctorsRequest request)
        {
            var result = await _appointmentDoctorService.GetByAppointmentIdAsync(appointmentId, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get AppointmentDoctor assignments by Doctor ID
        /// </summary>
        [HttpGet("doctor/{doctorId:guid}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(AppointmentDoctorResponse))]
        public async Task<IActionResult> GetByDoctorId(Guid doctorId, [FromQuery] GetAppointmentDoctorsRequest request)
        {
            var result = await _appointmentDoctorService.GetByDoctorIdAsync(doctorId, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create a new AppointmentDoctor assignment
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(AppointmentDoctorResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDoctorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<AppointmentDoctorResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data",
                    SystemCode = "INVALID_REQUEST"
                });
            }

            var result = await _appointmentDoctorService.CreateAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update an AppointmentDoctor assignment
        /// </summary>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(AppointmentDoctorResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAppointmentDoctorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<AppointmentDoctorResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data",
                    SystemCode = "INVALID_REQUEST"
                });
            }

            var result = await _appointmentDoctorService.UpdateAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete (soft delete) an AppointmentDoctor assignment
        /// </summary>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _appointmentDoctorService.DeleteAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}


