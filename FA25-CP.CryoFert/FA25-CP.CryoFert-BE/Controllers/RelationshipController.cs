using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using FA25_CP.CryoFert_BE.AppStarts;
using FA25_CP.CryoFert_BE.Common.Attributes;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// Controller for relationship management operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RelationshipController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public RelationshipController(IPatientService patientService)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
        }

        #region Relationship CRUD Operations

        /// <summary>
        /// Creates a new relationship between patients (with email confirmation workflow)
        /// </summary>
        /// <param name="request">Relationship creation request</param>
        /// <returns>Created relationship response</returns>
        [HttpPost]
        [Authorize(Roles = "Receptionist,Patient")]
        [ApiDefaultResponse(typeof(RelationshipResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> CreateRelationship([FromBody] CreateRelationshipRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<RelationshipResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data",
                    SystemCode = "INVALID_REQUEST"
                });
            }

            var result = await _patientService.CreateRelationshipAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Gets a relationship by ID
        /// </summary>
        /// <param name="id">Relationship ID</param>
        /// <returns>Relationship response</returns>
        [HttpGet("{id:guid}")]
        [ApiDefaultResponse(typeof(RelationshipResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetRelationshipById(Guid id)
        {
            var result = await _patientService.GetRelationshipByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Gets all relationships with pagination and filtering
        /// </summary>
        /// <param name="request">Get relationships request</param>
        /// <returns>Paginated relationship responses</returns>
        [HttpGet]
        [Authorize(Roles = "Doctor,Receptionist.Laboratory Technician,Admin")]
        [ApiDefaultResponse(typeof(RelationshipResponse))]
        public async Task<IActionResult> GetAllRelationships([FromQuery] GetRelationshipsRequest request)
        {
            var result = await _patientService.GetAllRelationshipsAsync(request ?? new GetRelationshipsRequest());
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Gets relationships for a specific patient
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <param name="request">Pagination request</param>
        /// <returns>Paginated relationship responses</returns>
        [HttpGet("patient/{patientId:guid}")]
        [ApiDefaultResponse(typeof(RelationshipResponse))]
        public async Task<IActionResult> GetPatientRelationships(Guid patientId, [FromQuery] GetRelationshipsRequest request)
        {
            var result = await _patientService.GetPatientRelationshipsAsync(patientId, request ?? new GetRelationshipsRequest());
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Updates an existing relationship
        /// </summary>
        /// <param name="id">Relationship ID</param>
        /// <param name="request">Relationship update request</param>
        /// <returns>Updated relationship response</returns>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Receptionist")]
        [ApiDefaultResponse(typeof(RelationshipResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> UpdateRelationship(Guid id, [FromBody] UpdateRelationshipRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<RelationshipResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data"
                });
            }

            var result = await _patientService.UpdateRelationshipAsync(id, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Soft deletes a relationship
        /// </summary>
        /// <param name="id">Relationship ID</param>
        /// <returns>Operation result</returns>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Receptionist,Patient")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> DeleteRelationship(Guid id)
        {
            var result = await _patientService.DeleteRelationshipAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        #endregion

        #region Relationship Status Operations

        /// <summary>
        /// Approves a pending relationship request
        /// </summary>
        /// <param name="request">Approve relationship request</param>
        /// <returns>Updated relationship response</returns>
        [HttpPost("approve")]
        [Authorize(Roles = "Receptionist,Patient")]
        [ApiDefaultResponse(typeof(RelationshipResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> ApproveRelationship([FromBody] ApproveRelationshipRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<RelationshipResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data",
                    SystemCode = "INVALID_REQUEST"
                });
            }

            var result = await _patientService.ApproveRelationshipAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Rejects a pending relationship request
        /// </summary>
        /// <param name="request">Reject relationship request</param>
        /// <returns>Updated relationship response</returns>
        [HttpPost("reject")]
        [Authorize(Roles = "Receptionist,Patient")]
        [ApiDefaultResponse(typeof(RelationshipResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> RejectRelationship([FromBody] RejectRelationshipRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<RelationshipResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data",
                    SystemCode = "INVALID_REQUEST"
                });
            }

            var result = await _patientService.RejectRelationshipAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Cancels a pending relationship request initiated by the current patient
        /// </summary>
        /// <param name="request">Cancel relationship request</param>
        /// <returns>Updated relationship response</returns>
        [HttpPost("cancel")]
        [Authorize(Roles = "Patient")]
        [ApiDefaultResponse(typeof(RelationshipResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> CancelRelationship([FromBody] CancelRelationshipRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<RelationshipResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid request data",
                    SystemCode = "INVALID_REQUEST"
                });
            }

            var result = await _patientService.CancelRelationshipAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        #endregion

        #region Email-Based Relationship Operations (Token-based, No Auth Required)

        /// <summary>
        /// Approves a relationship request via email link with token verification.
        /// This endpoint is accessible without authentication for email-based approval.
        /// </summary>
        /// <param name="id">Relationship ID</param>
        /// <param name="token">Approval token from email</param>
        /// <returns>Redirect to success/error page or JSON response</returns>
        [HttpGet("email-approve/{id:guid}")]
        [AllowAnonymous]
        [ApiDefaultResponse(typeof(RelationshipResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> ApproveRelationshipByEmail(Guid id, [FromQuery] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new BaseResponse<RelationshipResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Token is required",
                    SystemCode = "INVALID_TOKEN"
                });
            }

            var result = await _patientService.ApproveRelationshipByTokenAsync(id, token);

            // Return HTML response for better user experience when clicking from email
            if (result.Success)
            {
                return Content(GenerateHtmlResponse(
                    "Relationship Approved",
                    "The relationship request has been approved successfully!",
                    true), "text/html");
            }
            else
            {
                return Content(GenerateHtmlResponse(
                    "Approval Failed",
                    result.Message ?? "Failed to approve the relationship request.",
                    false), "text/html");
            }
        }

        /// <summary>
        /// Rejects a relationship request via email link with token verification.
        /// This endpoint is accessible without authentication for email-based rejection.
        /// </summary>
        /// <param name="id">Relationship ID</param>
        /// <param name="token">Approval token from email</param>
        /// <param name="reason">Optional rejection reason</param>
        /// <returns>Redirect to success/error page or JSON response</returns>
        [HttpGet("email-reject/{id:guid}")]
        [AllowAnonymous]
        [ApiDefaultResponse(typeof(RelationshipResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> RejectRelationshipByEmail(Guid id, [FromQuery] string token, [FromQuery] string? reason = null)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new BaseResponse<RelationshipResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Token is required",
                    SystemCode = "INVALID_TOKEN"
                });
            }

            var result = await _patientService.RejectRelationshipByTokenAsync(id, token, reason);

            // Return HTML response for better user experience when clicking from email
            if (result.Success)
            {
                return Content(GenerateHtmlResponse(
                    "Relationship Rejected",
                    "The relationship request has been rejected.",
                    true), "text/html");
            }
            else
            {
                return Content(GenerateHtmlResponse(
                    "Rejection Failed",
                    result.Message ?? "Failed to reject the relationship request.",
                    false), "text/html");
            }
        }

        /// <summary>
        /// Generates an HTML response page for email-based actions
        /// </summary>
        private static string GenerateHtmlResponse(string title, string message, bool isSuccess)
        {
            var backgroundColor = isSuccess ? "#d4edda" : "#f8d7da";
            var textColor = isSuccess ? "#155724" : "#721c24";
            var borderColor = isSuccess ? "#c3e6cb" : "#f5c6cb";
            var icon = isSuccess ? "✓" : "✕";

            return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>{title} - CryoFert</title>
    <style>
        * {{ margin: 0; padding: 0; box-sizing: border-box; }}
        body {{
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 20px;
        }}
        .container {{
            background: white;
            border-radius: 16px;
            box-shadow: 0 20px 60px rgba(0,0,0,0.3);
            padding: 40px;
            max-width: 500px;
            width: 100%;
            text-align: center;
        }}
        .icon {{
            width: 80px;
            height: 80px;
            border-radius: 50%;
            background: {backgroundColor};
            border: 3px solid {borderColor};
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 0 auto 24px;
            font-size: 40px;
            color: {textColor};
        }}
        h1 {{
            color: #333;
            margin-bottom: 16px;
            font-size: 24px;
        }}
        p {{
            color: #666;
            line-height: 1.6;
            margin-bottom: 24px;
        }}
        .alert {{
            background: {backgroundColor};
            border: 1px solid {borderColor};
            color: {textColor};
            padding: 16px;
            border-radius: 8px;
            margin-bottom: 24px;
        }}
        .logo {{
            color: #667eea;
            font-size: 28px;
            font-weight: bold;
            margin-bottom: 24px;
        }}
        .footer {{
            color: #999;
            font-size: 14px;
            margin-top: 24px;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='logo'>🧬 CryoFert</div>
        <div class='icon'>{icon}</div>
        <h1>{title}</h1>
        <div class='alert'>
            <p style='margin: 0;'>{message}</p>
        </div>
        <p>You can now close this window.</p>
        <div class='footer'>
            Healthcare/Fertility Management System
        </div>
    </div>
</body>
</html>";
        }

        #endregion

        #region Utility Operations

        /// <summary>
        /// Gets relationship type options
        /// </summary>
        /// <returns>List of relationship types</returns>
        [HttpGet("types")]
        [Authorize(Roles = "Doctor,Receptionist")]
        [ApiDefaultResponse(typeof(System.Collections.Generic.Dictionary<int, string>), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetRelationshipTypes()
        {
            var result = await _patientService.GetRelationshipTypesAsync();
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Checks if two patients can have a relationship
        /// </summary>
        /// <param name="patient1Id">First patient ID</param>
        /// <param name="patient2Id">Second patient ID</param>
        /// <returns>Validation result</returns>
        [HttpGet("can-create")]
        [Authorize(Roles = "Receptionist")]
        [ApiDefaultResponse(typeof(bool), UseDynamicWrapper = false)]
        public async Task<IActionResult> CanCreateRelationship([FromQuery] Guid patient1Id, [FromQuery] Guid patient2Id)
        {
            if (patient1Id == Guid.Empty || patient2Id == Guid.Empty)
            {
                return BadRequest(new BaseResponse<bool>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Both patient IDs are required"
                });
            }

            var result = await _patientService.CanCreateRelationshipAsync(patient1Id, patient2Id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        #endregion
    }
}
