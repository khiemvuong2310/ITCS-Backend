using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// Notification Management Controller - Handles CRUD and retrieval of notifications
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// Get notification by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResponse<NotificationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<NotificationResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<NotificationResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<NotificationResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNotificationById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new BaseResponse<NotificationResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Notification ID is required"
                });
            }

            var result = await _notificationService.GetNotificationByIdAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get list of notifications with filtering and pagination
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(DynamicResponse<NotificationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DynamicResponse<NotificationResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNotifications([FromQuery] GetNotificationsRequest request)
        {
            var result = await _notificationService.GetNotificationsAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create new notification
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<NotificationResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse<NotificationResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<NotificationResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<NotificationResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _notificationService.CreateNotificationAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update existing notification
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BaseResponse<NotificationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<NotificationResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<NotificationResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<NotificationResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateNotification(Guid id, [FromBody] UpdateNotificationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<NotificationResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            if (id == Guid.Empty || id != request.Id)
            {
                return BadRequest(new BaseResponse<NotificationResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Notification ID mismatch"
                });
            }

            var result = await _notificationService.UpdateNotificationAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete notification (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteNotification(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Notification ID is required"
                });
            }

            var result = await _notificationService.DeleteNotificationAsync(id);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}
