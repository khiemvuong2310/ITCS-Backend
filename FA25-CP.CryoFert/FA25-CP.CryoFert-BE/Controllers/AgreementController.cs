using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using FSCMS.Service.Interfaces;
using FSCMS.Core.Enum;
using System;
using System.Threading.Tasks;
using FA25_CP.CryoFert_BE.Common.Attributes;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// Agreement Controller - Handles CRUD operations for treatment agreements
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class AgreementController : ControllerBase
    {
        private readonly IAgreementService _agreementService;

        public AgreementController(IAgreementService agreementService)
        {
            _agreementService = agreementService ?? throw new ArgumentNullException(nameof(agreementService));
        }

        /// <summary>
        /// Get all agreements with pagination, filtering, and sorting
        /// </summary>
        /// <param name="request">Filter, pagination, and sorting parameters</param>
        /// <returns>Paginated list of agreements</returns>
        [HttpGet]
        [ApiDefaultResponse(typeof(AgreementResponse))]
        public async Task<IActionResult> GetAll([FromQuery] GetAgreementsRequest request)
        {
            var result = await _agreementService.GetAllAsync(request);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Get an agreement by ID
        /// </summary>
        /// <param name="id">Agreement ID</param>
        /// <returns>Agreement details</returns>
        [HttpGet("{id}")]
        [ApiDefaultResponse(typeof(AgreementDetailResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _agreementService.GetByIdAsync(id);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Get an agreement by agreement code
        /// </summary>
        /// <param name="code">Agreement code</param>
        /// <returns>Agreement details</returns>
        [HttpGet("code/{code}")]
        [ApiDefaultResponse(typeof(AgreementDetailResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return BadRequest(BaseResponse<AgreementDetailResponse>.CreateError("Agreement code cannot be empty", 400, "INVALID_CODE"));
            }

            var result = await _agreementService.GetByCodeAsync(code);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Create a new agreement
        /// </summary>
        /// <param name="request">Agreement creation data</param>
        /// <returns>Created agreement information</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Doctor,Staff")]
        [ApiDefaultResponse(typeof(AgreementResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> Create([FromBody] CreateAgreementRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(BaseResponse<AgreementResponse>.CreateError("Invalid input data", 400, "INVALID_REQUEST"));
            }

            var result = await _agreementService.CreateAsync(request);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Update an existing agreement
        /// </summary>
        /// <param name="id">Agreement ID</param>
        /// <param name="request">Updated agreement data</param>
        /// <returns>Updated agreement information</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Doctor,Staff")]
        [ApiDefaultResponse(typeof(AgreementResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAgreementRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(BaseResponse<AgreementResponse>.CreateError("Invalid input data", 400, "INVALID_REQUEST"));
            }

            var result = await _agreementService.UpdateAsync(id, request);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Sign an agreement (by patient or doctor)
        /// </summary>
        /// <param name="id">Agreement ID</param>
        /// <param name="request">Signing information</param>
        /// <returns>Updated agreement information</returns>
        [HttpPost("{id}/sign")]
        [Authorize(Roles = "Admin,Doctor,Patient,Staff")]
        [ApiDefaultResponse(typeof(AgreementResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> Sign(Guid id, [FromBody] SignAgreementRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(BaseResponse<AgreementResponse>.CreateError("Invalid input data", 400, "INVALID_REQUEST"));
            }

            var result = await _agreementService.SignAsync(id, request);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Update agreement status
        /// </summary>
        /// <param name="id">Agreement ID</param>
        /// <param name="status">New status</param>
        /// <returns>Updated agreement information</returns>
        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin,Doctor,Staff")]
        [ApiDefaultResponse(typeof(AgreementResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] AgreementStatus status)
        {
            var result = await _agreementService.UpdateStatusAsync(id, status);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Delete (soft delete) an agreement
        /// </summary>
        /// <param name="id">Agreement ID</param>
        /// <returns>Deletion result</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _agreementService.DeleteAsync(id);
            return StatusCode(result.Code ?? 500, result);
        }
    }
}

