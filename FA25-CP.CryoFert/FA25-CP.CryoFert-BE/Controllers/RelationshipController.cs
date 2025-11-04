using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

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
        [Authorize(Roles = "Admin,Receptionist,Patient")]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status500InternalServerError)]
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
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status500InternalServerError)]
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
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ProducesResponseType(typeof(DynamicResponse<RelationshipResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DynamicResponse<RelationshipResponse>), StatusCodes.Status500InternalServerError)]
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
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ProducesResponseType(typeof(DynamicResponse<RelationshipResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DynamicResponse<RelationshipResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DynamicResponse<RelationshipResponse>), StatusCodes.Status500InternalServerError)]
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
        [Authorize(Roles = "Admin,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status500InternalServerError)]
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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
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
        [Authorize(Roles = "Admin,Receptionist,Patient")]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status500InternalServerError)]
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
        [Authorize(Roles = "Admin,Receptionist,Patient")]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<RelationshipResponse>), StatusCodes.Status500InternalServerError)]
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

        #endregion

        #region Utility Operations

        /// <summary>
        /// Gets relationship type options
        /// </summary>
        /// <returns>List of relationship types</returns>
        [HttpGet("types")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse<System.Collections.Generic.Dictionary<int, string>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<System.Collections.Generic.Dictionary<int, string>>), StatusCodes.Status500InternalServerError)]
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
        [Authorize(Roles = "Admin,Receptionist")]
        [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status500InternalServerError)]
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
