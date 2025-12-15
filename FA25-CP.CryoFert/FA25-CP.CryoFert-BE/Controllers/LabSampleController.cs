using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using FA25_CP.CryoFert_BE.AppStarts;
using FA25_CP.CryoFert_BE.Common.Attributes;

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
        [ApiDefaultResponse(typeof(LabSampleResponse), UseDynamicWrapper = false)]
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
        [ApiDefaultResponse(typeof(LabSampleResponse))]
        public async Task<IActionResult> GetAll([FromQuery] GetLabSamplesRequest request)
        {
            var result = await _labSampleService.GetAllAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create new sperm sample
        /// </summary>
        [HttpPost("sperm")]
        //[Authorize(Roles = "LaboratoryTechnician")]
        [ApiDefaultResponse(typeof(LabSampleResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> CreateSperm([FromQuery] CreateLabSampleSpermRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new BaseResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data."
                });

            var result = await _labSampleService.CreateSpermAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create new oocyte sample
        /// </summary>
        [HttpPost("oocyte")]
        //[Authorize(Roles = "LaboratoryTechnician")]
        [ApiDefaultResponse(typeof(LabSampleResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> CreateOocyte([FromQuery] CreateLabSampleOocyteRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new BaseResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data."
                });

            var result = await _labSampleService.CreateOocyteAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create new embryo sample
        /// </summary>
        [HttpPost("embryo")]
        //[Authorize(Roles = "LaboratoryTechnician")]
        [ApiDefaultResponse(typeof(LabSampleResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> CreateEmbryo([FromQuery] CreateLabSampleEmbryoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new BaseResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data."
                });

            var result = await _labSampleService.CreateEmbryoAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPut("frozen/{id}")]
        [Authorize(Roles = "Doctor")]
        [ApiDefaultResponse(typeof(LabSampleResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> UpdateFrozen(Guid id, [FromBody] UpdateLabSampleFrozenRequest request)
        {
            if (!ModelState.IsValid || id == Guid.Empty)
            {
                return BadRequest(new BaseResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data or sample ID."
                });
            }

            var result = await _labSampleService.UpdateFrozenAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update existing sperm sample
        /// </summary>
        [HttpPut("sperm/{id}")]
        //[Authorize(Roles = "LaboratoryTechnician")]
        [ApiDefaultResponse(typeof(LabSampleResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> UpdateSperm(Guid id, [FromBody] UpdateLabSampleSpermRequest request)
        {
            if (!ModelState.IsValid || id == Guid.Empty)
            {
                return BadRequest(new BaseResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data or sample ID."
                });
            }

            var result = await _labSampleService.UpdateSpermAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update existing oocyte sample
        /// </summary>
        [HttpPut("oocyte/{id}")]
        //[Authorize(Roles = "LaboratoryTechnician")]
        [ApiDefaultResponse(typeof(LabSampleResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> UpdateOocyte(Guid id, [FromBody] UpdateLabSampleOocyteRequest request)
        {
            if (!ModelState.IsValid || id == Guid.Empty)
            {
                return BadRequest(new BaseResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data or sample ID."
                });
            }

            var result = await _labSampleService.UpdateOocyteAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update existing embryo sample
        /// </summary>
        [HttpPut("embryo/{id}")]
        //[Authorize(Roles = "LaboratoryTechnician")]
        [ApiDefaultResponse(typeof(LabSampleResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> UpdateEmbryo(Guid id, [FromBody] UpdateLabSampleEmbryoRequest request)
        {
            if (!ModelState.IsValid || id == Guid.Empty)
            {
                return BadRequest(new BaseResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data or sample ID."
                });
            }

            var result = await _labSampleService.UpdateEmbryoAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete a lab sample (soft delete)
        /// </summary>
        /// <param name="id">Lab sample ID</param>
        /// <returns>Deletion result</returns>
        [HttpDelete("{id}")]
        //[Authorize(Roles = "LaboratoryTechnician")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
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
