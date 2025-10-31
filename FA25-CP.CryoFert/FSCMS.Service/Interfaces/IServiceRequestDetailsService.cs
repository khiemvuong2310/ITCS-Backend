using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using FSCMS.Core.Models;

namespace FSCMS.Service.Interfaces
{
    public interface IServiceRequestDetailsService
    {
        Task<BaseResponse<List<ServiceRequestDetailResponseModel>>> GetByServiceRequestAsync(Guid serviceRequestId);
        Task<BaseResponse<ServiceRequestDetailResponseModel>> GetByIdAsync(Guid id);
        Task<BaseResponse<ServiceRequestDetailResponseModel>> CreateAsync(ServiceRequestDetailCreateRequestModel request, Guid serviceRequestId);
        Task<BaseResponse<ServiceRequestDetailResponseModel>> UpdateAsync(Guid id, ServiceRequestDetailUpdateRequestModel request);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);
        Task<BaseResponse<List<ServiceRequestDetailResponseModel>>> GetByServiceAsync(Guid serviceId);
    }
}
