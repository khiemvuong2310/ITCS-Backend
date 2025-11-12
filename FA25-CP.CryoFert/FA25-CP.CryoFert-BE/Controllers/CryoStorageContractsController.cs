using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using FSCMS.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// Cryo Storage Contract Controller - Handles CRUD operations for cryo storage contracts
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class CryoStorageContractsController : ControllerBase
    {
        private readonly ICryoStorageContractService _contractService;

        public CryoStorageContractsController(ICryoStorageContractService contractService)
        {
            _contractService = contractService;
        }

        /// <summary>
        /// Get all cryo storage contracts with pagination, filtering, and sorting
        /// </summary>
        /// <param name="request">Filter, pagination, and sorting parameters</param>
        /// <returns>Paginated list of contracts</returns>
        [HttpGet]
        [ProducesResponseType(typeof(DynamicResponse<CryoStorageContractResponse>), 200)]
        [ProducesResponseType(typeof(DynamicResponse<CryoStorageContractResponse>), 500)]
        public async Task<IActionResult> GetAllContracts([FromQuery] GetCryoStorageContractsRequest request)
        {
            var result = await _contractService.GetAllAsync(request);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Get a cryo storage contract by ID
        /// </summary>
        /// <param name="id">Contract ID</param>
        /// <returns>Contract details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResponse<CryoStorageContractDetailResponse>), 200)]
        [ProducesResponseType(typeof(BaseResponse<CryoStorageContractDetailResponse>), 404)]
        [ProducesResponseType(typeof(BaseResponse<CryoStorageContractDetailResponse>), 500)]
        public async Task<IActionResult> GetContractById(Guid id)
        {
            var result = await _contractService.GetByIdAsync(id);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Create a new cryo storage contract
        /// </summary>
        /// <param name="request">Contract creation data</param>
        /// <returns>Created contract information</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Receptionist")] // Only Admin or Receptionist can create
        [ProducesResponseType(typeof(BaseResponse<CryoStorageContractResponse>), 201)]
        [ProducesResponseType(typeof(BaseResponse<CryoStorageContractResponse>), 400)]
        [ProducesResponseType(typeof(BaseResponse<CryoStorageContractResponse>), 500)]
        public async Task<IActionResult> CreateContract([FromBody] CreateCryoStorageContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<CryoStorageContractResponse>
                {
                    Code = 400,
                    Message = "Invalid input data"
                });
            }

            var result = await _contractService.CreateAsync(request);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Update an existing cryo storage contract
        /// </summary>
        /// <param name="id">Contract ID</param>
        /// <param name="request">Updated contract data</param>
        /// <returns>Updated contract information</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Receptionist")] // Only Admin or Receptionist can update
        [ProducesResponseType(typeof(BaseResponse<CryoStorageContractResponse>), 200)]
        [ProducesResponseType(typeof(BaseResponse<CryoStorageContractResponse>), 404)]
        [ProducesResponseType(typeof(BaseResponse<CryoStorageContractResponse>), 400)]
        [ProducesResponseType(typeof(BaseResponse<CryoStorageContractResponse>), 500)]
        public async Task<IActionResult> UpdateContract(Guid id, [FromBody] UpdateCryoStorageContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<CryoStorageContractResponse>
                {
                    Code = 400,
                    Message = "Invalid input data"
                });
            }

            var result = await _contractService.UpdateAsync(id, request);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Delete (soft delete) a cryo storage contract
        /// </summary>
        /// <param name="id">Contract ID</param>
        /// <returns>Deletion result</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only Admin can delete
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        [ProducesResponseType(typeof(BaseResponse), 500)]
        public async Task<IActionResult> DeleteContract(Guid id)
        {
            var result = await _contractService.DeleteAsync(id);
            return StatusCode(result.Code ?? 500, result);
        }
    }
}
