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
        /// Generate PDF from template
        /// </summary>
        [HttpPost("generate-pdf")]
        [ApiDefaultResponse(typeof(byte[]))]
        public async Task<IActionResult> GeneratePdf([FromQuery] GenerateFilledPdfRequest request)
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
