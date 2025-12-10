using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using FA25_CP.CryoFert_BE.AppStarts;
using FA25_CP.CryoFert_BE.Common.Attributes;

namespace FA25_CP.CryoFert_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServiceRequestDetailsController : ControllerBase
    {
        private readonly IServiceRequestDetailsService _serviceRequestDetailsService;

        public ServiceRequestDetailsController(IServiceRequestDetailsService serviceRequestDetailsService)
        {
            _serviceRequestDetailsService = serviceRequestDetailsService;
        }

        /// <summary>
        /// Get service request detail by ID
        /// </summary>
        /// <param name="id">Service request detail ID</param>
        /// <returns>Service request detail response</returns>
        [HttpGet("{id:guid}")]
        [Authorize]
        [ApiDefaultResponse(typeof(ServiceRequestDetailResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _serviceRequestDetailsService.GetByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get service request details by service request ID
        /// </summary>
        /// <param name="serviceRequestId">Service request ID</param>
        /// <returns>List of service request details</returns>
        [HttpGet("service-request/{serviceRequestId:guid}")]
        [Authorize]
        [ApiDefaultResponse(typeof(List<ServiceRequestDetailResponseModel>), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetByServiceRequest(Guid serviceRequestId)
        {
            var result = await _serviceRequestDetailsService.GetByServiceRequestAsync(serviceRequestId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get service request details by service ID
        /// </summary>
        /// <param name="serviceId">Service ID</param>
        /// <returns>List of service request details</returns>
        [HttpGet("service/{serviceId:guid}")]
        [Authorize]
        [ApiDefaultResponse(typeof(List<ServiceRequestDetailResponseModel>), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetByService(Guid serviceId)
        {
            var result = await _serviceRequestDetailsService.GetByServiceAsync(serviceId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Add a service detail to an existing service request
        /// </summary>
        /// <param name="serviceRequestId">Service request ID</param>
        /// <param name="request">Service request detail creation request</param>
        /// <returns>Created service request detail response</returns>
        [HttpPost("service-request/{serviceRequestId:guid}")]
        [Authorize(Roles = "Receptionist")]
        [ApiDefaultResponse(typeof(ServiceRequestDetailResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Create(Guid serviceRequestId, [FromBody] ServiceRequestDetailCreateRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<ServiceRequestDetailResponseModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data"
                });
            }

            var result = await _serviceRequestDetailsService.CreateAsync(request, serviceRequestId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update an existing service request detail
        /// </summary>
        /// <param name="id">Service request detail ID</param>
        /// <param name="request">Service request detail update request</param>
        /// <returns>Updated service request detail response</returns>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Receptionist")]
        [ApiDefaultResponse(typeof(ServiceRequestDetailResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Update(Guid id, [FromBody] ServiceRequestDetailUpdateRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<ServiceRequestDetailResponseModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data"
                });
            }

            var result = await _serviceRequestDetailsService.UpdateAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete a service request detail
        /// </summary>
        /// <param name="id">Service request detail ID</param>
        /// <returns>Operation result</returns>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Receptionist")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _serviceRequestDetailsService.DeleteAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}
