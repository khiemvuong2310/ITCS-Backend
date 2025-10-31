using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using FSCMS.Core.Models;
using FSCMS.Core.Enum;

namespace FSCMS.Service.Interfaces
{
    public interface IServiceRequestService
    {
        Task<DynamicResponse<ServiceRequestResponseModel>> GetAllAsync(GetServiceRequestsRequest request);
        Task<BaseResponse<ServiceRequestResponseModel>> GetByIdAsync(Guid id);
        Task<BaseResponse<ServiceRequestResponseModel>> CreateAsync(ServiceRequestCreateRequestModel request);
        Task<BaseResponse<ServiceRequestResponseModel>> UpdateAsync(Guid id, ServiceRequestUpdateRequestModel request);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);
        Task<BaseResponse<List<ServiceRequestResponseModel>>> GetByStatusAsync(ServiceRequestStatus status);
        Task<BaseResponse<List<ServiceRequestResponseModel>>> GetByAppointmentAsync(Guid appointmentId);
        Task<BaseResponse<ServiceRequestResponseModel>> ApproveAsync(Guid id, string approvedBy);
        Task<BaseResponse<ServiceRequestResponseModel>> RejectAsync(Guid id, string rejectedBy);
        Task<BaseResponse<ServiceRequestResponseModel>> CompleteAsync(Guid id);
        Task<BaseResponse<ServiceRequestResponseModel>> CancelAsync(Guid id);
    }
}
