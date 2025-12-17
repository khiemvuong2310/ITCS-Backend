using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FA25_CP.CryoFert_BE.AppStarts;
using FA25_CP.CryoFert_BE.Common.Attributes;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// Medicine Controller - Handles medicine management operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService ?? throw new ArgumentNullException(nameof(medicineService));
        }

        /// <summary>
        /// Get all medicines with pagination
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Doctor,Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(Medicine))]
        public async Task<IActionResult> GetAll([FromQuery] PagingModel request)
        {
            try
            {
                var result = await _medicineService.GetAllAsync(request);
                return StatusCode(result.Code ?? StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new DynamicResponse<Medicine>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = $"An error occurred: {ex.Message}",
                    MetaData = new PagingMetaData(),
                    Data = new List<Medicine>()
                });
            }
        }

        /// <summary>
        /// Get medicine by ID
        /// </summary>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist,Laboratory Technician")]
        [ApiDefaultResponse(typeof(Medicine), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _medicineService.GetByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create a new medicine
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(Medicine), UseDynamicWrapper = false)]
        public async Task<IActionResult> Create([FromBody] CreateMedicineRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<Medicine>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data",
                    SystemCode = "INVALID_REQUEST"
                });
            }

            var payload = new Medicine(Guid.Empty, request.Name, request.Dosage, request.Form)
            {
                GenericName = request.GenericName,
                Indication = request.Indication,
                Contraindication = request.Contraindication,
                SideEffects = request.SideEffects,
                IsActive = request.IsActive,
                Notes = request.Notes
            };

            var result = await _medicineService.CreateAsync(payload);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update a medicine
        /// </summary>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(Medicine), UseDynamicWrapper = false)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMedicineRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<Medicine>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data",
                    SystemCode = "INVALID_REQUEST"
                });
            }

            // Map only provided fields
            var update = new Medicine(Guid.Empty, request.Name, request.Dosage, request.Form)
            {
                GenericName = request.GenericName,
                Indication = request.Indication,
                Contraindication = request.Contraindication,
                SideEffects = request.SideEffects,
                IsActive = request.IsActive ?? false, // will be compared inside service
                Notes = request.Notes
            };

            var result = await _medicineService.UpdateAsync(id, update);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete (soft delete) a medicine
        /// </summary>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _medicineService.DeleteAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }

    #region Request Models

    public class CreateMedicineRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? GenericName { get; set; }
        public string? Dosage { get; set; }
        public string? Form { get; set; }
        public string? Indication { get; set; }
        public string? Contraindication { get; set; }
        public string? SideEffects { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }
    }

    public class UpdateMedicineRequest
    {
        public string? Name { get; set; }
        public string? GenericName { get; set; }
        public string? Dosage { get; set; }
        public string? Form { get; set; }
        public string? Indication { get; set; }
        public string? Contraindication { get; set; }
        public string? SideEffects { get; set; }
        public bool? IsActive { get; set; }
        public string? Notes { get; set; }
    }

    #endregion
}


