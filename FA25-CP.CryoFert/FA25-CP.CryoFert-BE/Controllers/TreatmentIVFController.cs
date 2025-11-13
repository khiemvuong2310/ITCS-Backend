using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using FA25_CP.CryoFert_BE.AppStarts;
using FA25_CP.CryoFert_BE.Common.Attributes;

namespace FA25_CP.CryoFert_BE.Controllers
{
    [ApiController]
    [Route("api/treatments/ivf")] // by treatment context
    [Authorize]
    public class TreatmentIVFController : ControllerBase
    {
        private readonly ITreatmentIVFService _service;

        public TreatmentIVFController(ITreatmentIVFService service)
        {
            _service = service;
        }

        [HttpGet("{treatmentId:guid}")]
        [Authorize(Roles = "Admin,Doctor")]
        [ApiDefaultResponse(typeof(TreatmentIVFResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetByTreatment(Guid treatmentId)
        {
            var result = await _service.GetByTreatmentIdAsync(treatmentId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Doctor")]
        [ApiDefaultResponse(typeof(TreatmentIVFResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Create([FromBody] TreatmentIVFCreateUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<TreatmentIVFResponseModel> { Code = StatusCodes.Status400BadRequest, Message = "Invalid request data" });
            }
            var result = await _service.CreateAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,Doctor")]
        [ApiDefaultResponse(typeof(TreatmentIVFResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Update(Guid id, [FromBody] TreatmentIVFCreateUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<TreatmentIVFResponseModel> { Code = StatusCodes.Status400BadRequest, Message = "Invalid request data" });
            }
            var result = await _service.UpdateAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ApiDefaultResponse(typeof(bool), UseDynamicWrapper = false)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}


