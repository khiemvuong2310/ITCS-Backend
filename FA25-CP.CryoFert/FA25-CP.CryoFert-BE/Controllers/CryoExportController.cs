using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using FA25_CP.CryoFert_BE.AppStarts;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// CryoExport Controller - Handles CRUD operations for CryoExports
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class CryoExportController : ControllerBase
    {
        private readonly ICryoExportService _cryoExportService;

        public CryoExportController(ICryoExportService cryoExportService)
        {
            _cryoExportService = cryoExportService;
        }

        /// <summary>
        /// Get CryoExport by ID
        /// </summary>
        [HttpGet("{id}")]
        [ApiDefaultResponse(typeof(CryoExportResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new BaseResponse<CryoExportResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid CryoExport ID"
                });
            }

            var result = await _cryoExportService.GetByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get all CryoExports with pagination and filtering
        /// </summary>
        [HttpGet]
        [ApiDefaultResponse(typeof(CryoExportResponse))]
        public async Task<IActionResult> GetAll([FromQuery] GetCryoExportsRequest request)
        {
            var result = await _cryoExportService.GetAllAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create a new CryoExport
        /// </summary>
        [HttpPost]
        [ApiDefaultResponse(typeof(CryoExportResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> Create([FromQuery] CreateCryoExportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<CryoExportResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _cryoExportService.CreateAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update an existing CryoExport
        /// </summary>
        [HttpPut("{id}")]
        [ApiDefaultResponse(typeof(CryoExportResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCryoExportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<CryoExportResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _cryoExportService.UpdateAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete CryoExport (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid CryoExport ID"
                });
            }

            var result = await _cryoExportService.DeleteAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}
