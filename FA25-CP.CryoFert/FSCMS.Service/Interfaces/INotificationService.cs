using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    public interface INotificationService
    {
        Task<BaseResponse<NotificationResponse>> GetNotificationByIdAsync(Guid id);
        Task<DynamicResponse<NotificationResponse>> GetNotificationsAsync(GetNotificationsRequest request);
        Task<BaseResponse<NotificationResponse>> CreateNotificationAsync(CreateNotificationRequest request, Guid accountId);
        Task<BaseResponse<NotificationResponse>> UpdateNotificationAsync(Guid id, UpdateNotificationRequest request);
        Task<BaseResponse> DeleteNotificationAsync(Guid id);
        Task<BaseResponse<NotificationResponse>> UpdateSatusRead(Guid id);
    }
}
