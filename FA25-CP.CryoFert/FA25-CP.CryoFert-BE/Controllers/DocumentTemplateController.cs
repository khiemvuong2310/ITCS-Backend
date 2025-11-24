using System.Security.Claims;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FA25_CP.CryoFert_BE.Common.Attributes;
using System;
using System.Threading.Tasks;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// Document Template Controller - Manage templates & generate PDF
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class DocumentTemplateController : ControllerBase
    {
        private readonly IDocumentTemplateService _documentTemplateService;

        public DocumentTemplateController(IDocumentTemplateService documentTemplateService)
        {
            _documentTemplateService = documentTemplateService;
        }

        /// <summary>
        /// Get template by ID
        /// </summary>
        [HttpGet]
        [ApiDefaultResponse(typeof(DocumentTemplateDetailResponse))]
        public async Task<IActionResult> GetById(GetDocumentTemplateByIdRequest request)
        {
            var result = await _documentTemplateService.GetDocumentTemplateByIdAsync(request);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Create new template
        /// </summary>
        [HttpPost]
        [ApiDefaultResponse(typeof(DocumentTemplateResponse))]
        public async Task<IActionResult> Create([FromBody] CreateDocumentTemplateRequest request)
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (accountId == null) return Unauthorized(new { message = "Cannot detect user identity" });

            var result = await _documentTemplateService.CreateDocumentTemplateAsync(request, Guid.Parse(accountId));
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Update template
        /// </summary>
        [HttpPut]
        [ApiDefaultResponse(typeof(DocumentTemplateResponse))]
        public async Task<IActionResult> Update([FromBody] UpdateDocumentTemplateRequest request)
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (accountId == null) return Unauthorized(new { message = "Cannot detect user identity" });

            var result = await _documentTemplateService.UpdateDocumentTemplateAsync(request, Guid.Parse(accountId));
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Delete template (soft delete)
        /// </summary>
        [HttpDelete("{templateId}")]
        [ApiDefaultResponse(typeof(object))]
        public async Task<IActionResult> Delete(Guid templateId)
        {
            var result = await _documentTemplateService.DeleteDocumentTemplateAsync(templateId);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Get all templates with pagination & filtering
        /// </summary>
        [HttpGet]
        [ApiDefaultResponse(typeof(DocumentTemplateResponse))]
        public async Task<IActionResult> GetAll([FromQuery] GetDocumentTemplatesRequest request)
        {
            var result = await _documentTemplateService.GetDocumentTemplatesAsync(request);
            return StatusCode(result.Code ?? 500, result);
        }

        /// <summary>
        /// Generate PDF from template
        /// </summary>
        [HttpPost("generate-pdf")]
        [ApiDefaultResponse(typeof(byte[]))]
        public async Task<IActionResult> GeneratePdf([FromBody] GenerateFilledPdfRequest request)
        {
            var result = await _documentTemplateService.GenerateFilledPdfAsync(request);
            if (result.Code == 200 && result.Data != null)
            {
                // Return file directly
                return File(result.Data, "application/pdf", $"Document_{DateTime.UtcNow:yyyyMMddHHmmss}.pdf");
            }
            return StatusCode(result.Code ?? 500, result);
        }
    }
}
