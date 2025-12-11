using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using FSCMS.Core.Enum;
using FA25_CP.CryoFert_BE.AppStarts;
using FA25_CP.CryoFert_BE.Common.Attributes;
using System.Security.Claims;

namespace FA25_CP.CryoFert_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TreatmentController : ControllerBase
    {
        private readonly ITreatmentService _treatmentService;

        public TreatmentController(ITreatmentService treatmentService)
        {
            _treatmentService = treatmentService;
        }

        [HttpGet]
        [Authorize(Roles = "Receptionist,Doctor,Patient,Laboratory Technician")]
        [ApiDefaultResponse(typeof(TreatmentResponseModel))]
        public async Task<IActionResult> GetAll([FromQuery] GetTreatmentsRequest request)
        {
            request ??= new GetTreatmentsRequest();
            
            // For Patient role, automatically filter by their PatientId
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole == "Patient")
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid accountId))
                {
                    return Unauthorized(new BaseResponse<TreatmentResponseModel>
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        Message = "Invalid user token"
                    });
                }

                // Patient.Id == Account.Id (shared PK), so accountId is the patientId
                request.PatientId = accountId;
            }

            var result = await _treatmentService.GetAllAsync(request);
            
            // If no data found, return 404 Not Found
            if (result.MetaData?.Total == 0 || (result.Data != null && !result.Data.Any()))
            {
                return StatusCode(StatusCodes.Status404NotFound, new DynamicResponse<TreatmentResponseModel>
                {
                    Code = StatusCodes.Status404NotFound,
                    SystemCode = "NOT_FOUND",
                    Message = "No treatments found",
                    MetaData = result.MetaData ?? new PagingMetaData(),
                    Data = result.Data ?? new List<TreatmentResponseModel>()
                });
            }
            
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Receptionist,Doctor,Patient,Laboratory Technician")]
        [ApiDefaultResponse(typeof(TreatmentDetailResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _treatmentService.GetByIdAsync(id);
            
            // For Patient role, verify the treatment belongs to them
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole == "Patient")
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid accountId))
                {
                    return Unauthorized(new BaseResponse<TreatmentDetailResponseModel>
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        Message = "Invalid user token"
                    });
                }

                // Verify PatientId matches (Patient.Id == Account.Id, so accountId is patientId)
                if (!result.Success || result.Data == null || result.Data.PatientId != accountId)
                {
                    if (!result.Success)
                    {
                        return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
                    }

                    return StatusCode(StatusCodes.Status403Forbidden, new BaseResponse<TreatmentDetailResponseModel>
                    {
                        Code = StatusCodes.Status403Forbidden,
                        Message = "You are not authorized to view this treatment",
                        SystemCode = "FORBIDDEN"
                    });
                }
            }

            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost]
        [Authorize(Roles = "Receptionist,Doctor,Laboratory Technician")]
        [ApiDefaultResponse(typeof(TreatmentResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Create([FromBody] TreatmentCreateUpdateRequest request)
        {
            if (request == null)
            {
                return BadRequest(new BaseResponse<TreatmentResponseModel> 
                { 
                    Code = StatusCodes.Status400BadRequest, 
                    Message = "Request body is required",
                    SystemCode = "INVALID_REQUEST"
                });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .SelectMany(x => x.Value!.Errors.Select(e => $"{x.Key}: {e.ErrorMessage}"))
                    .ToList();
                
                return BadRequest(new BaseResponse<TreatmentResponseModel> 
                { 
                    Code = StatusCodes.Status400BadRequest, 
                    Message = $"Invalid request data: {string.Join("; ", errors)}",
                    SystemCode = "VALIDATION_ERROR"
                });
            }
            
            var result = await _treatmentService.CreateAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Doctor,Laboratory Technician")]
        [ApiDefaultResponse(typeof(TreatmentResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Update(Guid id, [FromBody] TreatmentUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<TreatmentResponseModel> { Code = StatusCodes.Status400BadRequest, Message = "Invalid request data" });
            }
            var result = await _treatmentService.UpdateAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update treatment status only
        /// </summary>
        /// <param name="id">Treatment ID</param>
        /// <param name="request">Request containing the new status</param>
        /// <returns>Success or error response</returns>
        [HttpPut("{id:guid}/status")]
        [Authorize(Roles = "Doctor,Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateTreatmentStatusRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data",
                    SystemCode = "INVALID_REQUEST"
                });
            }

            var result = await _treatmentService.UpdateStatusAsync(id, request.Status);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Doctor,Laboratory Technician")]
        [ApiDefaultResponse(typeof(bool), UseDynamicWrapper = false)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _treatmentService.DeleteAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPut("{treatmentId:guid}/cancel-remaining-cycles")]
        [Authorize(Roles = "Doctor,Laboratory Technician")]
        [ApiDefaultResponse(typeof(int), UseDynamicWrapper = false)]
        public async Task<IActionResult> CancelRemainingPlannedCycles(Guid treatmentId, Guid? excludeCycleId = null)
        {
            var canceledCount = await _treatmentService.CancelRemainingPlannedCyclesAsync(treatmentId, excludeCycleId);
            return StatusCode(StatusCodes.Status200OK, BaseResponse<int>.CreateSuccess(canceledCount, "Remaining planned cycles canceled successfully"));
        }
    }
}


