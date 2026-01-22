using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using FSCMS.Core.Models;

namespace FSCMS.Service.Interfaces
{
    public interface IServiceService
    {
        Task<DynamicResponse<ServiceResponseModel>> GetAllAsync(GetServicesRequest request, Guid? accountId);
        Task<BaseResponse<ServiceResponseModel>> GetByIdAsync(Guid id);
        Task<BaseResponse<ServiceResponseModel>> CreateAsync(ServiceCreateRequestModel request);
        Task<BaseResponse<ServiceResponseModel>> UpdateAsync(Guid id, ServiceUpdateRequestModel request);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);
        Task<BaseResponse<List<ServiceResponseModel>>> GetActiveAsync();
        Task<BaseResponse<List<ServiceResponseModel>>> GetByCategoryAsync(Guid categoryId);
        Task<BaseResponse<List<ServiceResponseModel>>> SearchAsync(string searchTerm);
    }
}
