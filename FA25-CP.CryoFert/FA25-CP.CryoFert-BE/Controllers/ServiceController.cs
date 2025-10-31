using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;

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
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        [ProducesResponseType(typeof(DynamicResponse<ServiceResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DynamicResponse<ServiceResponseModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] GetServicesRequest request)
        {
            if (request == null)
                request = new GetServicesRequest();

            var result = await _serviceService.GetAllAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get service by ID
        /// </summary>
        /// <param name="id">Service ID</param>
        /// <returns>Service response</returns>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        [ProducesResponseType(typeof(BaseResponse<ServiceResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<ServiceResponseModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<ServiceResponseModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<ServiceResponseModel>), StatusCodes.Status500InternalServerError)]
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
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        [ProducesResponseType(typeof(BaseResponse<List<ServiceResponseModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<List<ServiceResponseModel>>), StatusCodes.Status500InternalServerError)]
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
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        [ProducesResponseType(typeof(BaseResponse<List<ServiceResponseModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<List<ServiceResponseModel>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<List<ServiceResponseModel>>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<List<ServiceResponseModel>>), StatusCodes.Status500InternalServerError)]
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
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        [ProducesResponseType(typeof(BaseResponse<List<ServiceResponseModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<List<ServiceResponseModel>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<List<ServiceResponseModel>>), StatusCodes.Status500InternalServerError)]
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
        [Authorize(Roles = "Admin,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse<ServiceResponseModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse<ServiceResponseModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<ServiceResponseModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<ServiceResponseModel>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseResponse<ServiceResponseModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ServiceCreateUpdateRequestModel request)
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
        /// <param name="request">Service update request</param>
        /// <returns>Updated service response</returns>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse<ServiceResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<ServiceResponseModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<ServiceResponseModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<ServiceResponseModel>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseResponse<ServiceResponseModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromBody] ServiceCreateUpdateRequestModel request)
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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _serviceService.DeleteAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}
