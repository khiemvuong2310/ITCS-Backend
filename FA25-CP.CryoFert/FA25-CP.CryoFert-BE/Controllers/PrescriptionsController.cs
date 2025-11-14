//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;
//using FSCMS.Service.ReponseModel;
//using FSCMS.Service.RequestModel;
//using FSCMS.Service.Interfaces;
//using System;
//using System.Threading.Tasks;
//using FA25_CP.CryoFert_BE.Common.Attributes;

//namespace FA25_CP.CryoFert_BE.Controllers
//{
//    /// <summary>
//    /// Prescription Controller - Handles CRUD operations for prescriptions
//    /// </summary>
//    [ApiController]
//    [Route("api/[controller]")]
//    [Produces("application/json")]
//    [Authorize]
//    public class PrescriptionsController : ControllerBase
//    {
//        private readonly IPrescriptionService _prescriptionService;

//        public PrescriptionsController(IPrescriptionService prescriptionService)
//        {
//            _prescriptionService = prescriptionService;
//        }

//        /// <summary>
//        /// Get all prescriptions with pagination, filtering, and sorting
//        /// </summary>
//        /// <param name="request">Filter, pagination, and sorting parameters</param>
//        /// <returns>Paginated list of prescriptions</returns>
//        [HttpGet]
//        [ApiDefaultResponse(typeof(PrescriptionResponse))]
//        public async Task<IActionResult> GetAll([FromQuery] GetPrescriptionsRequest request)
//        {
//            var result = await _prescriptionService.GetAllAsync(request);
//            return StatusCode(result.Code ?? 500, result);
//        }

//        /// <summary>
//        /// Get a prescription by ID
//        /// </summary>
//        /// <param name="id">Prescription ID</param>
//        /// <returns>Prescription details</returns>
//        [HttpGet("{id}")]
//        [ApiDefaultResponse(typeof(PrescriptionDetailResponse), UseDynamicWrapper = false)]
//        public async Task<IActionResult> GetById(Guid id)
//        {
//            var result = await _prescriptionService.GetByIdAsync(id);
//            return StatusCode(result.Code ?? 500, result);
//        }

//        /// <summary>
//        /// Create a new prescription
//        /// </summary>
//        /// <param name="request">Prescription creation data</param>
//        /// <returns>Created prescription information</returns>
//        [HttpPost]
//        [Authorize(Roles = "Doctor,Admin")] // Only Doctor or Admin can create
//        [ApiDefaultResponse(typeof(PrescriptionResponse), UseDynamicWrapper = false)]
//        public async Task<IActionResult> Create([FromBody] CreatePrescriptionRequest request)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(new BaseResponse<PrescriptionResponse>
//                {
//                    Code = 400,
//                    Message = "Invalid input data"
//                });
//            }

//            var result = await _prescriptionService.CreateAsync(request);
//            return StatusCode(result.Code ?? 500, result);
//        }

//        /// <summary>
//        /// Update an existing prescription
//        /// </summary>
//        /// <param name="id">Prescription ID</param>
//        /// <param name="request">Updated prescription data</param>
//        /// <returns>Updated prescription information</returns>
//        [HttpPut("{id}")]
//        [Authorize(Roles = "Doctor,Admin")] // Only Doctor or Admin can update
//        [ApiDefaultResponse(typeof(PrescriptionResponse), UseDynamicWrapper = false)]
//        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePrescriptionRequest request)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(new BaseResponse<PrescriptionResponse>
//                {
//                    Code = 400,
//                    Message = "Invalid input data"
//                });
//            }

//            var result = await _prescriptionService.UpdateAsync(id, request);
//            return StatusCode(result.Code ?? 500, result);
//        }

//        /// <summary>
//        /// Delete (soft delete) a prescription
//        /// </summary>
//        /// <param name="id">Prescription ID</param>
//        /// <returns>Deletion result</returns>
//        [HttpDelete("{id}")]
//        [Authorize(Roles = "Admin")] // Only Admin can delete
//        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
//        public async Task<IActionResult> Delete(Guid id)
//        {
//            var result = await _prescriptionService.DeleteAsync(id);
//            return StatusCode(result.Code ?? 500, result);
//        }
//    }
//}
