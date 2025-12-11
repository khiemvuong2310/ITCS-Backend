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
    public class ServiceCategoryController : ControllerBase
    {
        private readonly IServiceCategoryService _serviceCategoryService;

        public ServiceCategoryController(IServiceCategoryService serviceCategoryService)
        {
            _serviceCategoryService = serviceCategoryService;
        }

        /// <summary>
        /// Get all service categories with pagination and filtering
        /// </summary>
        /// <param name="request">Pagination and filtering request</param>
        /// <returns>Paginated service categories</returns>
        [HttpGet]
        [Authorize(Roles = "Receptionist,Doctor,Laboratory Technician")]
        [ApiDefaultResponse(typeof(ServiceCategoryResponseModel))]
        public async Task<IActionResult> GetAll([FromQuery] GetServiceCategoriesRequest request)
        {
            if (request == null)
                request = new GetServiceCategoriesRequest();

            var result = await _serviceCategoryService.GetAllAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get service category by ID
        /// </summary>
        /// <param name="id">Service category ID</param>
        /// <returns>Service category response</returns>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Receptionist,Doctor,Laboratory Technician")]
        [ApiDefaultResponse(typeof(ServiceCategoryResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _serviceCategoryService.GetByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get all active service categories
        /// </summary>
        /// <returns>List of active service categories</returns>
        [HttpGet("active")]
        [Authorize(Roles = "Receptionist,Doctor,Laboratory Technician")]
        [ApiDefaultResponse(typeof(List<ServiceCategoryResponseModel>), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetActive()
        {
            var result = await _serviceCategoryService.GetActiveAsync();
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create a new service category
        /// </summary>
        /// <param name="request">Service category creation request</param>
        /// <returns>Created service category response</returns>
        [HttpPost]
        [Authorize(Roles = "Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(ServiceCategoryResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Create([FromBody] ServiceCategoryRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<ServiceCategoryResponseModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data"
                });
            }

            var result = await _serviceCategoryService.CreateAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update an existing service category
        /// </summary>
        /// <param name="id">Service category ID</param>
        /// <param name="request">Service category update request</param>
        /// <returns>Updated service category response</returns>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(ServiceCategoryResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Update(Guid id, [FromBody] ServiceCategoryRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<ServiceCategoryResponseModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data"
                });
            }

            var result = await _serviceCategoryService.UpdateAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete a service category
        /// </summary>
        /// <param name="id">Service category ID</param>
        /// <returns>Operation result</returns>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _serviceCategoryService.DeleteAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}
