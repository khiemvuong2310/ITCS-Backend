using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FA25_CP.CryoFert_BE.AppStarts;

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
        [ApiDefaultResponse(typeof(NotificationResponse), UseDynamicWrapper = false)]
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
        [ApiDefaultResponse(typeof(NotificationResponse))]
        public async Task<IActionResult> GetNotifications([FromQuery] GetNotificationsRequest request)
        {
            var result = await _notificationService.GetNotificationsAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create new notification
        /// </summary>
        [HttpPost]
        [ApiDefaultResponse(typeof(NotificationResponse), UseDynamicWrapper = false)]
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
        [ApiDefaultResponse(typeof(NotificationResponse), UseDynamicWrapper = false)]
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
        [ApiDefaultResponse(typeof(object), UseDynamicWrapper = false)]
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
