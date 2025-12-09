using System.Security.Claims;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.ReponseModel.FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FA25_CP.CryoFert_BE.AppStarts;
using FA25_CP.CryoFert_BE.Common.Attributes;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// Media Management Controller - Handles media CRUD operations, upload, and retrieval
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        /// <summary>
        /// Upload media file
        /// </summary>
        [HttpPost("upload")]
        [ApiDefaultResponse(typeof(MediaResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> UploadMedia([FromForm] UploadMediaRequest request)
        {
            if (request.File == null)
            {
                return BadRequest(new BaseResponse<MediaResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "File is required"
                });
            }

            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (accountId == null)
            {
                return Unauthorized(new { message = "Cannot detect user identity" });
            }

            var result = await _mediaService.UploadMediaAsync(request, Guid.Parse(accountId));
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Upload template file
        /// </summary>
        [HttpPost("upload-template")]
        [ApiDefaultResponse(typeof(MediaResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> UploadTemplate([FromForm] UploadTemplateRequest request)
        {
            if (request.File == null)
            {
                return BadRequest(new BaseResponse<MediaResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "File is required"
                });
            }

            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (accountId == null)
            {
                return Unauthorized(new { message = "Cannot detect user identity" });
            }

            var result = await _mediaService.UploadTemplateAsync(request, Guid.Parse(accountId));
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get media by ID
        /// </summary>
        [HttpGet("{mediaId}")]
        [ApiDefaultResponse(typeof(MediaResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> GetMediaById(Guid mediaId)
        {
            var result = await _mediaService.GetMediaByIdAsync(mediaId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update media metadata
        /// </summary>
        [HttpPut("{mediaId}")]
        [ApiDefaultResponse(typeof(MediaResponse), UseDynamicWrapper = false)]
        public async Task<IActionResult> UpdateMedia(Guid mediaId, [FromBody] UpdateMediaRequest request)
        {
            var result = await _mediaService.UpdateMediaAsync(mediaId, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete media (soft delete)
        /// </summary>
        [HttpDelete("{mediaId}")]
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
        public async Task<IActionResult> DeleteMedia(Guid mediaId)
        {
            var result = await _mediaService.DeleteMediaAsync(mediaId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get all media with filtering, sorting, and pagination
        /// </summary>
        [HttpGet]
        [ApiDefaultResponse(typeof(MediaResponse))]
        public async Task<IActionResult> GetAllMedias([FromQuery] GetMediasRequest request)
        {
            var result = await _mediaService.GetAllMediasAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get template
        /// </summary>
        [HttpGet("template")]
        [ApiDefaultResponse(typeof(MediaResponse))]
        public async Task<IActionResult> GetTemplate([FromQuery] GetTemplateRequest request)
        {
            var result = await _mediaService.GetTemplateAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet("html")]
        public async Task<IActionResult> GenerateHtml([FromQuery] GetHtmlRequest request)
        {
            var result = await _mediaService.RenderHtmlAsync(request);
            return StatusCode(result.Code ?? 500, result);
        }
    }
}
