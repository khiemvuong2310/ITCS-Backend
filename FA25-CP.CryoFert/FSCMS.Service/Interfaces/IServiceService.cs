using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using FSCMS.Core.Models;

namespace FSCMS.Service.Interfaces
{
    public interface IServiceService
    {
        Task<DynamicResponse<ServiceResponseModel>> GetAllAsync(GetServicesRequest request);
        Task<BaseResponse<ServiceResponseModel>> GetByIdAsync(Guid id);
        Task<BaseResponse<ServiceResponseModel>> CreateAsync(ServiceCreateUpdateRequestModel request);
        Task<BaseResponse<ServiceResponseModel>> UpdateAsync(Guid id, ServiceCreateUpdateRequestModel request);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);
        Task<BaseResponse<List<ServiceResponseModel>>> GetActiveAsync();
        Task<BaseResponse<List<ServiceResponseModel>>> GetByCategoryAsync(Guid categoryId);
        Task<BaseResponse<List<ServiceResponseModel>>> SearchAsync(string searchTerm);
    }
}
