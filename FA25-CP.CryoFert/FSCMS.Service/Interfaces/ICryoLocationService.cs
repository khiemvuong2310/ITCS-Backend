using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;

namespace FSCMS.Service.Interfaces;

public interface ICryoLocationService
{
    Task<CryoLocationResponse> CreateAsync(CryoLocationCreateRequest request);
    Task<CryoLocationResponse> UpdateAsync(Guid id, CryoLocationUpdateRequest request);
    Task<bool> DeleteAsync(Guid id);
    Task<CryoLocationResponse?> GetByIdAsync(Guid id);
    Task<List<CryoLocationResponse>> GetAllAsync();
    Task<List<CryoLocationResponse>> GetHierarchyAsync();
    Task<int> CheckAvailableCapacityAsync(Guid locationId);
}
