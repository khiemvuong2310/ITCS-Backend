using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FSCMS.Service.Services;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FSCMS.Core.Enum;

namespace FA25_CP.CryoFert_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize] 
    public class CryoLocationController : ControllerBase
    {
        private readonly ICryoLocationService _cryoLocationService;

        public CryoLocationController(ICryoLocationService cryoLocationService)
        {
            _cryoLocationService = cryoLocationService;
        }

        /// <summary>
        /// Create default CryoBank structure from configuration
        /// </summary>
        [HttpPost("initialize-default-bank")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(DynamicResponse<CryoLocationSummaryResponse>), 201)]
        [ProducesResponseType(typeof(DynamicResponse<CryoLocationSummaryResponse>), 400)]
        [ProducesResponseType(typeof(DynamicResponse<CryoLocationSummaryResponse>), 500)]
        public async Task<IActionResult> CreateDefaultBank()
        {
            var result = await _cryoLocationService.CreateDefaultBankAsync();
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Get initial top-level locations (Tanks)
        /// </summary>
        [HttpGet("initial-tree")]
        [ProducesResponseType(typeof(DynamicResponse<CryoLocationSummaryResponse>), 200)]
        [ProducesResponseType(typeof(DynamicResponse<CryoLocationSummaryResponse>), 500)]
        public async Task<IActionResult> GetInitialTree([FromQuery] SampleType? sampleType = null)
        {
            var result = await _cryoLocationService.GetInitialTreeAsync(sampleType);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Get CryoLocation by Id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResponse<CryoLocationResponse>), 200)]
        [ProducesResponseType(typeof(BaseResponse<CryoLocationResponse>), 404)]
        [ProducesResponseType(typeof(BaseResponse<CryoLocationResponse>), 500)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _cryoLocationService.GetByIdAsync(id);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Get children locations of a parent
        /// </summary>
        [HttpGet("{parentId}/children")]
        [ProducesResponseType(typeof(DynamicResponse<CryoLocationSummaryResponse>), 200)]
        [ProducesResponseType(typeof(DynamicResponse<CryoLocationSummaryResponse>), 500)]
        public async Task<IActionResult> GetChildren(Guid parentId, [FromQuery] bool? isActive = null)
        {
            var result = await _cryoLocationService.GetChildrenAsync(parentId, isActive);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Get full tree of a tank (Tank -> Canister -> Goblet -> Slot)
        /// </summary>
        [HttpGet("{tankId}/full-tree")]
        [ProducesResponseType(typeof(BaseResponse<CryoLocationFullTreeResponse>), 200)]
        [ProducesResponseType(typeof(BaseResponse<CryoLocationFullTreeResponse>), 404)]
        [ProducesResponseType(typeof(BaseResponse<CryoLocationFullTreeResponse>), 500)]
        public async Task<IActionResult> GetFullTreeByTankId(Guid tankId)
        {
            var result = await _cryoLocationService.GetFullTreeByTankIdAsync(tankId);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Update CryoLocation
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(BaseResponse<CryoLocationResponse>), 200)]
        [ProducesResponseType(typeof(BaseResponse<CryoLocationResponse>), 404)]
        [ProducesResponseType(typeof(BaseResponse<CryoLocationResponse>), 500)]
        public async Task<IActionResult> Update(Guid id, [FromBody] CryoLocationUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<CryoLocationResponse>
                {
                    Code = 400,
                    Message = "Invalid input data"
                });
            }

            var result = await _cryoLocationService.UpdateAsync(id, request);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Delete CryoLocation (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        [ProducesResponseType(typeof(BaseResponse), 500)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _cryoLocationService.DeleteAsync(id);
            return StatusCode(result.Code ?? 500, result);
        }
    }
}
