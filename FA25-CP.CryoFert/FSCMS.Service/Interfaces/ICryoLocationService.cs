using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using FSCMS.Core.Enum;

namespace FSCMS.Service.Interfaces;

public interface ICryoLocationService
{
    Task<DynamicResponse<CryoLocationSummaryResponse>> CreateDefaultBankAsync();
    Task<DynamicResponse<CryoLocationSummaryResponse>> GetInitialTreeAsync(SampleType? sampleType = null);
    Task<BaseResponse<CryoLocationResponse?>> GetByIdAsync(Guid id);
    Task<DynamicResponse<CryoLocationSummaryResponse>> GetChildrenAsync(Guid parentId, bool? isActive = null);
    Task<BaseResponse<CryoLocationFullTreeResponse>> GetFullTreeByTankIdAsync(Guid tankId);
    Task<BaseResponse<CryoLocationResponse>> UpdateAsync(Guid id, CryoLocationUpdateRequest request);
    Task<BaseResponse> DeleteAsync(Guid id);
}
