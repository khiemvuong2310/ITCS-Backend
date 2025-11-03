using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// Lab Sample Management Controller - Handles lab sample CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class LabSampleController : ControllerBase
    {
        private readonly ILabSampleService _labSampleService;

        public LabSampleController(ILabSampleService labSampleService)
        {
            _labSampleService = labSampleService;
        }

        /// <summary>
        /// Get lab sample by ID
        /// </summary>
        /// <param name="id">Lab sample ID</param>
        /// <returns>Lab sample information</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResponse<LabSampleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<LabSampleResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<LabSampleResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<LabSampleResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new BaseResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid lab sample ID."
                });
            }

            var result = await _labSampleService.GetByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get all lab samples with filtering and pagination
        /// </summary>
        /// <param name="request">Filter and pagination parameters</param>
        /// <returns>List of lab samples</returns>
        [HttpGet]
        [ProducesResponseType(typeof(DynamicResponse<LabSampleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DynamicResponse<LabSampleResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] GetLabSamplesRequest request)
        {
            var result = await _labSampleService.GetAllAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create new lab sample
        /// </summary>
        /// <param name="request">Lab sample creation details</param>
        /// <returns>Created lab sample</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,LaboratoryTechnician")]
        [ProducesResponseType(typeof(BaseResponse<LabSampleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse<LabSampleResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<LabSampleResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateLabSampleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data."
                });
            }

            var result = await _labSampleService.CreateAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update an existing lab sample
        /// </summary>
        /// <param name="id">Lab sample ID</param>
        /// <param name="request">Updated data</param>
        /// <returns>Updated lab sample</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,LaboratoryTechnician")]
        [ProducesResponseType(typeof(BaseResponse<LabSampleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<LabSampleResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<LabSampleResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<LabSampleResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLabSampleRequest request)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new BaseResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid lab sample ID."
                });
            }

            var result = await _labSampleService.UpdateAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete a lab sample (soft delete)
        /// </summary>
        /// <param name="id">Lab sample ID</param>
        /// <returns>Deletion result</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,LaboratoryTechnician")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid lab sample ID."
                });
            }

            var result = await _labSampleService.DeleteAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}
