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
        Task<BaseResponse<NotificationResponse>> CreateNotificationAsync(CreateNotificationRequest request);
        Task<BaseResponse<NotificationResponse>> UpdateNotificationAsync(UpdateNotificationRequest request);
        Task<BaseResponse> DeleteNotificationAsync(Guid id);
    }
}
