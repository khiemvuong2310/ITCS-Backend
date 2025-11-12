using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// CryoImport Controller - Handles CRUD operations for CryoImports
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class CryoImportController : ControllerBase
    {
        private readonly ICryoImportService _cryoImportService;

        public CryoImportController(ICryoImportService cryoImportService)
        {
            _cryoImportService = cryoImportService;
        }

        /// <summary>
        /// Get CryoImport by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResponse<CryoImportResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<CryoImportResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<CryoImportResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<CryoImportResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new BaseResponse<CryoImportResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid CryoImport ID"
                });
            }

            var result = await _cryoImportService.GetByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get all CryoImports with pagination and filtering
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(DynamicResponse<CryoImportResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DynamicResponse<CryoImportResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] GetCryoImportsRequest request)
        {
            var result = await _cryoImportService.GetAllAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create a new CryoImport
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<CryoImportResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse<CryoImportResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<CryoImportResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromQuery] CreateCryoImportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<CryoImportResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _cryoImportService.CreateAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update an existing CryoImport
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BaseResponse<CryoImportResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<CryoImportResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<CryoImportResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<CryoImportResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCryoImportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<CryoImportResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _cryoImportService.UpdateAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete CryoImport (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid CryoImport ID"
                });
            }

            var result = await _cryoImportService.DeleteAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}
