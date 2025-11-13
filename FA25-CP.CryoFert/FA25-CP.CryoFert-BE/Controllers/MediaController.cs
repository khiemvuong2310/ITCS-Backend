using System.Security.Claims;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.ReponseModel.FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FA25_CP.CryoFert_BE.AppStarts;

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

            var result = await _mediaService.UploadMediaAsync(request);
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
    }
}
