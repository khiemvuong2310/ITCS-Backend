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
    [Route("api/treatment-cycles")]
    [Authorize]
    public class TreatmentCycleController : ControllerBase
    {
        private readonly ITreatmentCycleService _service;
        private readonly IUnitOfWork _unitOfWork;

        public TreatmentCycleController(ITreatmentCycleService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Roles = "Receptionist,Doctor,Patient")]
        [ApiDefaultResponse(typeof(TreatmentCycleResponseModel))]
        public async Task<IActionResult> GetAll([FromQuery] GetTreatmentCyclesRequest request)
        {
            request ??= new GetTreatmentCyclesRequest();
            
            // For Patient role, automatically filter by their PatientId
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole == "Patient")
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid accountId))
                {
                    return Unauthorized(new BaseResponse<TreatmentCycleResponseModel>
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        Message = "Invalid user token"
                    });
                }

                // Patient.Id == Account.Id (shared PK), so accountId is the patientId
                request.PatientId = accountId;
            }

            var result = await _service.GetAllAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Receptionist,Doctor,Patient")]
        [ApiDefaultResponse(typeof(TreatmentCycleDetailResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            
            // For Patient role, verify the treatment cycle belongs to them
            if (userRole == "Patient")
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid accountId))
                {
                    return Unauthorized(new BaseResponse<TreatmentCycleDetailResponseModel>
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        Message = "Invalid user token"
                    });
                }

                // Get treatment cycle to check Treatment.PatientId
                var cycle = await _unitOfWork.Repository<TreatmentCycle>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(tc => tc.Treatment)
                    .FirstOrDefaultAsync(tc => tc.Id == id && !tc.IsDeleted);

                if (cycle == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new BaseResponse<TreatmentCycleDetailResponseModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Treatment cycle not found",
                        SystemCode = "NOT_FOUND"
                    });
                }

                // Verify Treatment.PatientId matches (Patient.Id == Account.Id, so accountId is patientId)
                if (cycle.Treatment == null || cycle.Treatment.PatientId != accountId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new BaseResponse<TreatmentCycleDetailResponseModel>
                    {
                        Code = StatusCodes.Status403Forbidden,
                        Message = "You are not authorized to view this treatment cycle",
                        SystemCode = "FORBIDDEN"
                    });
                }
            }

            var result = await _service.GetByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]
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
        [Authorize(Roles = "Doctor")]
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
        [Authorize(Roles = "Doctor")]
        [ApiDefaultResponse(typeof(bool), UseDynamicWrapper = false)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost("{id:guid}/start")]
        [Authorize(Roles = "Doctor")]
        [ApiDefaultResponse(typeof(TreatmentCycleResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> Start(Guid id, [FromBody] StartTreatmentCycleRequest request)
        {
            var result = await _service.StartAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost("{id:guid}/complete")]
        [Authorize(Roles = "Doctor")]
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
        [Authorize(Roles = "Doctor")]
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

        [HttpPut("status")]
        [Authorize(Roles = "Doctor")]
        [ApiDefaultResponse(typeof(TreatmentCycleResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> UpdateStatusByOrder([FromQuery] UpdateTreatmentCycleStatusByOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<TreatmentCycleResponseModel> { Code = StatusCodes.Status400BadRequest, Message = "Invalid request data" });
            }
            var result = await _service.UpdateStatusByOrderAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet("{id:guid}/samples")]
        [Authorize(Roles = "Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(List<object>), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetSamples(Guid id)
        {
            var result = await _service.GetSamplesAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost("{id:guid}/samples")]
        [Authorize(Roles = "Doctor")]
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
        [Authorize(Roles = "Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(List<AppointmentSummary>), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetAppointments(Guid id)
        {
            var result = await _service.GetAppointmentsAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost("{id:guid}/appointments")]
        [Authorize(Roles = "Doctor")]
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
        [Authorize(Roles = "Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(TreatmentCycleBillingResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetBilling(Guid id)
        {
            var result = await _service.GetBillingAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet("{id:guid}/documents")]
        [Authorize(Roles = "Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(List<DocumentSummary>), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetDocuments(Guid id)
        {
            var result = await _service.GetDocumentsAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost("{id:guid}/documents")]
        [Authorize(Roles = "Doctor")]
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


