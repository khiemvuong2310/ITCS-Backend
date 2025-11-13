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
    [Route("api/treatment-cycles")]
    [Authorize]
    public class TreatmentCycleController : ControllerBase
    {
        private readonly ITreatmentCycleService _service;

        public TreatmentCycleController(ITreatmentCycleService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        [ApiDefaultResponse(typeof(TreatmentCycleResponseModel))]
        public async Task<IActionResult> GetAll([FromQuery] GetTreatmentCyclesRequest request)
        {
            request ??= new GetTreatmentCyclesRequest();
            var result = await _service.GetAllAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        [ApiDefaultResponse(typeof(TreatmentCycleDetailResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Doctor")]
        [ApiDefaultResponse(typeof(TreatmentCycleResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Create([FromBody] CreateTreatmentCycleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<TreatmentCycleResponseModel> { Code = StatusCodes.Status400BadRequest, Message = "Invalid request data" });
            }
            var result = await _service.CreateAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,Doctor")]
        [ApiDefaultResponse(typeof(TreatmentCycleResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTreatmentCycleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<TreatmentCycleResponseModel> { Code = StatusCodes.Status400BadRequest, Message = "Invalid request data" });
            }
            var result = await _service.UpdateAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin,Doctor")]
        [ApiDefaultResponse(typeof(bool), UseDynamicWrapper = false)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost("{id:guid}/start")]
        [Authorize(Roles = "Admin,Doctor")]
        [ApiDefaultResponse(typeof(TreatmentCycleResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Start(Guid id, [FromBody] StartTreatmentCycleRequest request)
        {
            var result = await _service.StartAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost("{id:guid}/complete")]
        [Authorize(Roles = "Admin,Doctor")]
        [ApiDefaultResponse(typeof(TreatmentCycleResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Complete(Guid id, [FromBody] CompleteTreatmentCycleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<TreatmentCycleResponseModel> { Code = StatusCodes.Status400BadRequest, Message = "Invalid request data" });
            }
            var result = await _service.CompleteAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost("{id:guid}/cancel")]
        [Authorize(Roles = "Admin,Doctor")]
        [ApiDefaultResponse(typeof(TreatmentCycleResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Cancel(Guid id, [FromBody] CancelTreatmentCycleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<TreatmentCycleResponseModel> { Code = StatusCodes.Status400BadRequest, Message = "Invalid request data" });
            }
            var result = await _service.CancelAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet("{id:guid}/samples")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(List<object>), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetSamples(Guid id)
        {
            var result = await _service.GetSamplesAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost("{id:guid}/samples")]
        [Authorize(Roles = "Admin,Doctor")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> AddSample(Guid id, [FromBody] AddCycleSampleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<object> { Code = StatusCodes.Status400BadRequest, Message = "Invalid request data" });
            }
            var result = await _service.AddSampleAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet("{id:guid}/appointments")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(List<AppointmentSummary>), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetAppointments(Guid id)
        {
            var result = await _service.GetAppointmentsAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost("{id:guid}/appointments")]
        [Authorize(Roles = "Admin,Doctor")]
        [ApiDefaultResponse(typeof(AppointmentSummary), UseDynamicWrapper = false)]
        public async Task<IActionResult> AddAppointment(Guid id, [FromBody] AddCycleAppointmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<AppointmentSummary> { Code = StatusCodes.Status400BadRequest, Message = "Invalid request data" });
            }
            var result = await _service.AddAppointmentAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet("{id:guid}/billing")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(TreatmentCycleBillingResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetBilling(Guid id)
        {
            var result = await _service.GetBillingAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet("{id:guid}/documents")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(List<DocumentSummary>), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetDocuments(Guid id)
        {
            var result = await _service.GetDocumentsAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost("{id:guid}/documents")]
        [Authorize(Roles = "Admin,Doctor")]
        [ApiDefaultResponse(typeof(DocumentSummary), UseDynamicWrapper = false)]
        public async Task<IActionResult> UploadDocument(Guid id, [FromBody] UploadCycleDocumentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<DocumentSummary> { Code = StatusCodes.Status400BadRequest, Message = "Invalid request data" });
            }
            var result = await _service.UploadDocumentAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}


