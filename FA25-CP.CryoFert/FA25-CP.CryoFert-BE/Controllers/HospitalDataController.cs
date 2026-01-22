using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using FSCMS.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FA25_CP.CryoFert_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalDataController : ControllerBase
    {
        private readonly IHospitalDataService _service;

        public HospitalDataController(IHospitalDataService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Staff,Doctor,LaboratoryTechnician")]
        public async Task<ActionResult<DynamicResponse<HospitalData>>> GetAll([FromQuery] GetHospitalDataRequest request)
        {
            var response = await _service.GetAllAsync(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Receptionist,Doctor,Laboratory Technician")]
        public async Task<ActionResult<HospitalData>> GetById(Guid id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Receptionist,Doctor,Laboratory Technician")]
        public async Task<ActionResult<HospitalData>> Create([FromBody] HospitalData hospitalData)
        {
            await _service.CreateAsync(hospitalData);
            return CreatedAtAction(nameof(GetById), new { id = hospitalData.Id }, hospitalData);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Receptionist,Doctor,Laboratory Technician")]
        public async Task<IActionResult> Update(Guid id, [FromBody] HospitalData hospitalData)
        {
            if (id != hospitalData.Id)
                return BadRequest();
            await _service.UpdateAsync(hospitalData);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
