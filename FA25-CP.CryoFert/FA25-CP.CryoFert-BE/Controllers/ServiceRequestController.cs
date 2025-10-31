using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using FSCMS.Core.Enum;

namespace FA25_CP.CryoFert_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IServiceRequestService _serviceRequestService;

        public ServiceRequestController(IServiceRequestService serviceRequestService)
        {
            _serviceRequestService = serviceRequestService;
        }

        /// <summary>
        /// Get all service requests with pagination and filtering
        /// </summary>
        /// <param name="request">Pagination and filtering request</param>
        /// <returns>Paginated service requests</returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        [ProducesResponseType(typeof(DynamicResponse<ServiceRequestResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DynamicResponse<ServiceRequestResponseModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] GetServiceRequestsRequest request)
        {
            if (request == null)
                request = new GetServiceRequestsRequest();

            var result = await _serviceRequestService.GetAllAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get service request by ID
        /// </summary>
        /// <param name="id">Service request ID</param>
        /// <returns>Service request response</returns>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _serviceRequestService.GetByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get service requests by status
        /// </summary>
        /// <param name="status">Service request status</param>
        /// <returns>List of service requests with the specified status</returns>
        [HttpGet("status/{status}")]
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        [ProducesResponseType(typeof(BaseResponse<List<ServiceRequestResponseModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<List<ServiceRequestResponseModel>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<List<ServiceRequestResponseModel>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByStatus(ServiceRequestStatus status)
        {
            var result = await _serviceRequestService.GetByStatusAsync(status);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get service requests by appointment
        /// </summary>
        /// <param name="appointmentId">Appointment ID</param>
        /// <returns>List of service requests for the appointment</returns>
        [HttpGet("appointment/{appointmentId:guid}")]
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        [ProducesResponseType(typeof(BaseResponse<List<ServiceRequestResponseModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<List<ServiceRequestResponseModel>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<List<ServiceRequestResponseModel>>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<List<ServiceRequestResponseModel>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByAppointment(Guid appointmentId)
        {
            var result = await _serviceRequestService.GetByAppointmentAsync(appointmentId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create a new service request
        /// </summary>
        /// <param name="request">Service request creation request</param>
        /// <returns>Created service request response</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ServiceRequestCreateRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<ServiceRequestResponseModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data"
                });
            }

            var result = await _serviceRequestService.CreateAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update an existing service request
        /// </summary>
        /// <param name="id">Service request ID</param>
        /// <param name="request">Service request update request</param>
        /// <returns>Updated service request response</returns>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromBody] ServiceRequestUpdateRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<ServiceRequestResponseModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data"
                });
            }

            var result = await _serviceRequestService.UpdateAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete a service request
        /// </summary>
        /// <param name="id">Service request ID</param>
        /// <returns>Operation result</returns>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _serviceRequestService.DeleteAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Approve a service request
        /// </summary>
        /// <param name="id">Service request ID</param>
        /// <param name="approvedBy">User who approves the request</param>
        /// <returns>Updated service request response</returns>
        [HttpPost("{id:guid}/approve")]
        [Authorize(Roles = "Admin,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Approve(Guid id, [FromBody] string approvedBy)
        {
            if (string.IsNullOrWhiteSpace(approvedBy))
            {
                return BadRequest(new BaseResponse<ServiceRequestResponseModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "ApprovedBy is required"
                });
            }

            var result = await _serviceRequestService.ApproveAsync(id, approvedBy);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Reject a service request
        /// </summary>
        /// <param name="id">Service request ID</param>
        /// <param name="rejectedBy">User who rejects the request</param>
        /// <returns>Updated service request response</returns>
        [HttpPost("{id:guid}/reject")]
        [Authorize(Roles = "Admin,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Reject(Guid id, [FromBody] string rejectedBy)
        {
            if (string.IsNullOrWhiteSpace(rejectedBy))
            {
                return BadRequest(new BaseResponse<ServiceRequestResponseModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "RejectedBy is required"
                });
            }

            var result = await _serviceRequestService.RejectAsync(id, rejectedBy);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Complete a service request
        /// </summary>
        /// <param name="id">Service request ID</param>
        /// <returns>Updated service request response</returns>
        [HttpPost("{id:guid}/complete")]
        [Authorize(Roles = "Admin,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Complete(Guid id)
        {
            var result = await _serviceRequestService.CompleteAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Cancel a service request
        /// </summary>
        /// <param name="id">Service request ID</param>
        /// <returns>Updated service request response</returns>
        [HttpPost("{id:guid}/cancel")]
        [Authorize(Roles = "Admin,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseResponse<ServiceRequestResponseModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var result = await _serviceRequestService.CancelAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}
