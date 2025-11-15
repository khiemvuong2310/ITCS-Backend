using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using FA25_CP.CryoFert_BE.AppStarts;
using FA25_CP.CryoFert_BE.Common.Attributes;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using FSCMS.Data.UnitOfWork;
using FSCMS.Core.Entities;

namespace FA25_CP.CryoFert_BE.Controllers
{
    [ApiController]
    [Route("api/treatments/iui")] // by treatment context
    [Authorize]
    public class TreatmentIUIController : ControllerBase
    {
        private readonly ITreatmentIUIService _service;
        private readonly IUnitOfWork _unitOfWork;

        public TreatmentIUIController(ITreatmentIUIService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{treatmentId:guid}")]
        [Authorize(Roles = "Doctor,Patient")]
        [ApiDefaultResponse(typeof(TreatmentIUIResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetByTreatment(Guid treatmentId)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            
            // For Patient role, verify the treatment belongs to them
            if (userRole == "Patient")
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid accountId))
                {
                    return Unauthorized(new BaseResponse<TreatmentIUIResponseModel>
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        Message = "Invalid user token"
                    });
                }

                // Get treatment to check PatientId
                var treatment = await _unitOfWork.Repository<Treatment>()
                    .GetQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == treatmentId && !t.IsDeleted);

                if (treatment == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new BaseResponse<TreatmentIUIResponseModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Treatment not found",
                        SystemCode = "NOT_FOUND"
                    });
                }

                // Verify PatientId matches (Patient.Id == Account.Id, so accountId is patientId)
                if (treatment.PatientId != accountId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new BaseResponse<TreatmentIUIResponseModel>
                    {
                        Code = StatusCodes.Status403Forbidden,
                        Message = "You are not authorized to view this IUI information",
                        SystemCode = "FORBIDDEN"
                    });
                }
            }

            var result = await _service.GetByTreatmentIdAsync(treatmentId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]
        [ApiDefaultResponse(typeof(TreatmentIUIResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Create([FromBody] TreatmentIUICreateUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<TreatmentIUIResponseModel> { Code = StatusCodes.Status400BadRequest, Message = "Invalid request data" });
            }
            var result = await _service.CreateAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Doctor")]
        [ApiDefaultResponse(typeof(TreatmentIUIResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Update(Guid id, [FromBody] TreatmentIUICreateUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<TreatmentIUIResponseModel> { Code = StatusCodes.Status400BadRequest, Message = "Invalid request data" });
            }
            var result = await _service.UpdateAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Doctor")]
        [ApiDefaultResponse(typeof(bool), UseDynamicWrapper = false)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}


