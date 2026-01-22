using System.Security.Claims;
using FA25_CP.CryoFert_BE.AppStarts;
using FA25_CP.CryoFert_BE.Common.Attributes;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FA25_CP.CryoFert_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        /// <summary>
        /// Get all services with pagination and filtering
        /// </summary>
        /// <param name="request">Pagination and filtering request</param>
        /// <returns>Paginated services</returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Receptionist,Doctor,Laboratory Technician")]
        [ApiDefaultResponse(typeof(ServiceResponseModel))]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetAll([FromQuery] GetServicesRequest request)
        {
            if (request == null)
                request = new GetServicesRequest();
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (accountId == null)
            {
                return Unauthorized(new { message = "Cannot detect user identity" });
            }
            var result = await _serviceService.GetAllAsync(request, Guid.Parse(accountId));
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get service by ID
        /// </summary>
        /// <param name="id">Service ID</param>
        /// <returns>Service response</returns>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin,Receptionist,Doctor,Laboratory Technician")]
        [ApiDefaultResponse(typeof(ServiceResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _serviceService.GetByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get all active services
        /// </summary>
        /// <returns>List of active services</returns>
        [HttpGet("active")]
        [Authorize(Roles = "Admin,Receptionist,Doctor,Laboratory Technician")]
        [ApiDefaultResponse(typeof(List<ServiceResponseModel>), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetActive()
        {
            var result = await _serviceService.GetActiveAsync();
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get services by category
        /// </summary>
        /// <param name="categoryId">Service category ID</param>
        /// <returns>List of services in the category</returns>
        [HttpGet("category/{categoryId:guid}")]
        [Authorize(Roles = "Admin,Receptionist,Doctor,Laboratory Technician")]
        [ApiDefaultResponse(typeof(List<ServiceResponseModel>), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetByCategory(Guid categoryId)
        {
            var result = await _serviceService.GetByCategoryAsync(categoryId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Search services by name, code, or description
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        /// <returns>List of matching services</returns>
        [HttpGet("search")]
        [ApiDefaultResponse(typeof(List<ServiceResponseModel>), UseDynamicWrapper = false)]
        public async Task<IActionResult> Search([FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest(new BaseResponse<List<ServiceResponseModel>>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Search term is required"
                });
            }

            var result = await _serviceService.SearchAsync(searchTerm);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create a new service
        /// </summary>
        /// <param name="request">Service creation request</param>
        /// <returns>Created service response</returns>
        [HttpPost]
        [Authorize(Roles = "Doctor,Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(ServiceResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Create([FromBody] ServiceCreateRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<ServiceResponseModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data"
                });
            }

            var result = await _serviceService.CreateAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update an existing service
        /// </summary>
        /// <param name="id">Service ID</param>
        /// <param name="request">Service update request (all fields are optional - only provided fields will be updated)</param>
        /// <returns>Updated service response</returns>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Doctor,Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(ServiceResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Update(Guid id, [FromBody] ServiceUpdateRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<ServiceResponseModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data"
                });
            }

            var result = await _serviceService.UpdateAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete a service
        /// </summary>
        /// <param name="id">Service ID</param>
        /// <returns>Operation result</returns>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Doctor,Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _serviceService.DeleteAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}
