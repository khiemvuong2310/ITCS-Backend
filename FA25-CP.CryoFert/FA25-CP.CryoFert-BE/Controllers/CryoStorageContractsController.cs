using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FA25_CP.CryoFert_BE.AppStarts;
using FA25_CP.CryoFert_BE.Common.Attributes;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [ApiDefaultResponse(typeof(CryoStorageContractResponse))]
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
        [ApiDefaultResponse(typeof(CryoStorageContractDetailResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetContractById(Guid id)
        {
            var result = await _contractService.GetByIdAsync(id);
            return StatusCode(result.Code ?? 500, result);
        }

        [HttpGet("{id}/contract-html")]
        public async Task<IActionResult> GenerateContract(Guid id)
        {
            var result = await _contractService.RenderCryoContractAsync(id);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Create a new cryo storage contract
        /// </summary>
        /// <param name="request">Contract creation data</param>
        /// <returns>Created contract information</returns>
        [HttpPost]
        [Authorize(Roles = "Receptionist,Doctor,Patient,Admin")] // Only Admin or Receptionist can create
        [ApiDefaultResponse(typeof(CryoStorageContractResponse), UseDynamicWrapper = false)]
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
        [Authorize(Roles = "Receptionist")] // Only Admin or Receptionist can update
        [ApiDefaultResponse(typeof(CryoStorageContractResponse), UseDynamicWrapper = false)]
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
        [Authorize(Roles = "Receptionist")] // Only Admin can delete
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> DeleteContract(Guid id)
        {
            var result = await _contractService.DeleteAsync(id);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Send OTP to patient email to sign contract
        /// </summary>
        [HttpPost("send-otp")]
        [Authorize(Roles = "Patient")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> SendOtp([FromQuery] SentOtpEmailRequest request)
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (accountId == null)
            {
                return Unauthorized(new { message = "Cannot detect user identity" });
            }

            var result = await _contractService.SendOtpEmailAsync(request, Guid.Parse(accountId));
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Verify OTP + sign the contract
        /// </summary>
        [HttpPost("verify-otp")]
        [Authorize(Roles = "Patient")]
        [ApiDefaultResponse(typeof(CryoStorageContractResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> VerifyOtp([FromQuery] VerifyOtpRequest request)
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (accountId == null)
            {
                return Unauthorized(new { message = "Cannot detect user identity" });
            }

            var result = await _contractService.VerifyOtpAsync(request, Guid.Parse(accountId));
            return StatusCode(result.Code ?? 500, result);
        }
    }
}
