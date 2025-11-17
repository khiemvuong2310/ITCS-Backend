using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using System.Security.Claims;
using FA25_CP.CryoFert_BE.Common.Attributes;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// Medical Record Management - CRUD + Search
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        // ============================================================
        // GET BY ID
        // ============================================================
        /// <summary>
        /// Get medical record by Id
        /// </summary>
        [HttpGet("{id}")]
        [ApiDefaultResponse(typeof(MedicalRecordDetailResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new BaseResponse<MedicalRecordDetailResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid MedicalRecord ID"
                });
            }

            var result = await _medicalRecordService.GetByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        // ============================================================
        // SEARCH / GET ALL
        // ============================================================
        /// <summary>
        /// Search and get all medical records (paging + filter)
        /// </summary>
        [HttpGet]
        [ApiDefaultResponse(typeof(MedicalRecordResponse))]
        public async Task<IActionResult> GetAll([FromQuery] SearchMedicalRecordRequest request)
        {
            var result = await _medicalRecordService.GetAllAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        // ============================================================
        // CREATE
        // ============================================================
        /// <summary>
        /// Create new medical record
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Doctor,Admin")]
        [ApiDefaultResponse(typeof(MedicalRecordResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> Create([FromQuery] CreateMedicalRecordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<MedicalRecordResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _medicalRecordService.CreateAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        // ============================================================
        // UPDATE
        // ============================================================
        /// <summary>
        /// Update medical record
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Doctor,Admin")]
        [ApiDefaultResponse(typeof(MedicalRecordResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMedicalRecordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<MedicalRecordResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _medicalRecordService.UpdateAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        // ============================================================
        // DELETE (SOFT)
        // ============================================================
        /// <summary>
        /// Soft delete a medical record
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Doctor,Admin")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid MedicalRecord ID"
                });
            }

            var result = await _medicalRecordService.DeleteAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}
