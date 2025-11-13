using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using FA25_CP.CryoFert_BE.AppStarts;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// Cryo Package Management Controller - Handles CRUD operations for Cryo Packages
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize(Roles = "Admin")]
    public class CryoPackageController : ControllerBase
    {
        private readonly ICryoPackageService _cryoPackageService;

        public CryoPackageController(ICryoPackageService cryoPackageService)
        {
            _cryoPackageService = cryoPackageService;
        }

        /// <summary>
        /// Get Cryo Package by ID
        /// </summary>
        /// <param name="id">Cryo Package ID</param>
        /// <returns>Cryo Package details</returns>
        [HttpGet("{id}")]
        [ApiDefaultResponse(typeof(CryoPackageResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new BaseResponse<CryoPackageResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid package ID"
                });
            }

            var result = await _cryoPackageService.GetByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get all Cryo Packages with filters and pagination
        /// </summary>
        /// <param name="request">Filter and paging parameters</param>
        /// <returns>Paginated list of Cryo Packages</returns>
        [HttpGet]
        [AllowAnonymous] // Allow both Admin and public access (optional)
        [ApiDefaultResponse(typeof(CryoPackageResponse))]
        public async Task<IActionResult> GetAll([FromQuery] GetCryoPackagesRequest request)
        {
            var result = await _cryoPackageService.GetAllAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create a new Cryo Package
        /// </summary>
        /// <param name="request">Cryo Package creation data</param>
        /// <returns>Created Cryo Package details</returns>
        [HttpPost]
        [ApiDefaultResponse(typeof(CryoPackageResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> Create([FromQuery] CreateCryoPackageRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<CryoPackageResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _cryoPackageService.CreateAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update an existing Cryo Package
        /// </summary>
        /// <param name="id">Cryo Package ID</param>
        /// <param name="request">Cryo Package update data</param>
        /// <returns>Updated Cryo Package details</returns>
        [HttpPut("{id}")]
        [ApiDefaultResponse(typeof(CryoPackageResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCryoPackageRequest request)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new BaseResponse<CryoPackageResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid package ID"
                });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<CryoPackageResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _cryoPackageService.UpdateAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete a Cryo Package (soft delete)
        /// </summary>
        /// <param name="id">Cryo Package ID</param>
        /// <returns>Operation result</returns>
        [HttpDelete("{id}")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid package ID"
                });
            }

            var result = await _cryoPackageService.DeleteAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}
