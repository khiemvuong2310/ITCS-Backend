using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using FSCMS.Core.Models;

namespace FSCMS.Service.Interfaces
{
    public interface IServiceCategoryService
    {
        Task<DynamicResponse<ServiceCategoryResponseModel>> GetAllAsync(GetServiceCategoriesRequest request);
        Task<BaseResponse<ServiceCategoryResponseModel>> GetByIdAsync(Guid id);
        Task<BaseResponse<ServiceCategoryResponseModel>> CreateAsync(ServiceCategoryRequestModel request);
        Task<BaseResponse<ServiceCategoryResponseModel>> UpdateAsync(Guid id, ServiceCategoryRequestModel request);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);
        Task<BaseResponse<List<ServiceCategoryResponseModel>>> GetActiveAsync();
    }
}
