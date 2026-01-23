using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using FSCMS.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using FA25_CP.CryoFert_BE.Common.Attributes;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// Controller for managing hospital data operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalDataController : ControllerBase
    {
        private readonly IHospitalDataService _service;
        private readonly ILogger<HospitalDataController> _logger;

        public HospitalDataController(IHospitalDataService service, ILogger<HospitalDataController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        /// <summary>
        /// Retrieves paginated hospital data with optional filtering
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Staff,Doctor,LaboratoryTechnician")]
        [ApiDefaultResponse(typeof(HospitalDataResponse))]
        public async Task<ActionResult<DynamicResponse<HospitalDataResponse>>> GetAll([FromQuery] GetHospitalDataRequest request)
        {
            try
            {
                var response = await _service.GetAllAsync(request ?? new GetHospitalDataRequest());
                return StatusCode(response.Code ?? StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAll method");
                return StatusCode(StatusCodes.Status500InternalServerError, new DynamicResponse<HospitalDataResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "CONTROLLER_ERROR",
                    Message = "An unexpected error occurred",
                    Data = new List<HospitalDataResponse>()
                });
            }
        }

        /// <summary>
        /// Retrieves a specific hospital data record by ID
        /// </summary>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin,Receptionist,Doctor,LaboratoryTechnician")]
        [ApiDefaultResponse(typeof(HospitalDataResponse), UseDynamicWrapper = false)]
        public async Task<ActionResult<BaseResponse<HospitalDataResponse>>> GetById(Guid id)
        {
            try
            {
                var response = await _service.GetByIdAsync(id);
                return StatusCode(response.Code ?? StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetById method with ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, BaseResponse<HospitalDataResponse>.CreateError(
                    "An unexpected error occurred",
                    StatusCodes.Status500InternalServerError,
                    "CONTROLLER_ERROR"
                ));
            }
        }

        /// <summary>
        /// Creates a new hospital data record
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin,Receptionist,Doctor,LaboratoryTechnician")]
        [ApiDefaultResponse(typeof(HospitalDataResponse), UseDynamicWrapper = false)]
        public async Task<ActionResult<BaseResponse<HospitalDataResponse>>> Create([FromBody] CreateHospitalDataRequest request)
        {
            try
            {
                var response = await _service.CreateAsync(request);
                return StatusCode(response.Code ?? StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Create method");
                return StatusCode(StatusCodes.Status500InternalServerError, BaseResponse<HospitalDataResponse>.CreateError(
                    "An unexpected error occurred",
                    StatusCodes.Status500InternalServerError,
                    "CONTROLLER_ERROR"
                ));
            }
        }

        /// <summary>
        /// Updates an existing hospital data record (supports partial updates)
        /// </summary>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,Receptionist,Doctor,LaboratoryTechnician")]
        [ApiDefaultResponse(typeof(HospitalDataResponse), UseDynamicWrapper = false)]
        public async Task<ActionResult<BaseResponse<HospitalDataResponse>>> Update(Guid id, [FromBody] UpdateHospitalDataRequest request)
        {
            try
            {
                // Validate that route ID matches request ID
                if (request != null && id != request.Id)
                {
                    _logger.LogWarning("Route ID {RouteId} does not match request ID {RequestId}", id, request.Id);
                    return BadRequest(BaseResponse<HospitalDataResponse>.CreateError(
                        "Route ID does not match request ID",
                        StatusCodes.Status400BadRequest,
                        "ID_MISMATCH"
                    ));
                }

                // If request doesn't have ID set, use the route ID
                if (request != null && request.Id == Guid.Empty)
                {
                    request.Id = id;
                }

                var response = await _service.UpdateAsync(request);
                return StatusCode(response.Code ?? StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Update method with ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, BaseResponse<HospitalDataResponse>.CreateError(
                    "An unexpected error occurred",
                    StatusCodes.Status500InternalServerError,
                    "CONTROLLER_ERROR"
                ));
            }
        }

        /// <summary>
        /// Deletes a hospital data record
        /// </summary>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<ActionResult<BaseResponse<object>>> Delete(Guid id)
        {
            try
            {
                var response = await _service.DeleteAsync(id);
                return StatusCode(response.Code ?? StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Delete method with ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, BaseResponse<object>.CreateError(
                    "An unexpected error occurred",
                    StatusCodes.Status500InternalServerError,
                    "CONTROLLER_ERROR"
                ));
            }
        }
    }
}
