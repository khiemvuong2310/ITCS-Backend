using System.Security.Claims;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.ReponseModel.FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType(typeof(BaseResponse<MediaResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse<MediaResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<MediaResponse>), StatusCodes.Status500InternalServerError)]
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

            var result = await _mediaService.UploadMediaAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get media by ID
        /// </summary>
        [HttpGet("{mediaId}")]
        [ProducesResponseType(typeof(BaseResponse<MediaResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<MediaResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<MediaResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMediaById(Guid mediaId)
        {
            var result = await _mediaService.GetMediaByIdAsync(mediaId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update media metadata
        /// </summary>
        [HttpPut("{mediaId}")]
        [ProducesResponseType(typeof(BaseResponse<MediaResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<MediaResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<MediaResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMedia(Guid mediaId, [FromBody] UpdateMediaRequest request)
        {
            var result = await _mediaService.UpdateMediaAsync(mediaId, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete media (soft delete)
        /// </summary>
        [HttpDelete("{mediaId}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMedia(Guid mediaId)
        {
            var result = await _mediaService.DeleteMediaAsync(mediaId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get all media with filtering, sorting, and pagination
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(DynamicResponse<MediaResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DynamicResponse<MediaResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMedias([FromQuery] GetMediasRequest request)
        {
            var result = await _mediaService.GetAllMediasAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}
