using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Services
{
    public interface IHospitalDataService
    {
        Task CreateAsync(HospitalData hospitalData);
        Task DeleteAsync(Guid id);
        Task<DynamicResponse<HospitalData>> GetAllAsync(GetHospitalDataRequest request);
        Task<HospitalData> GetByIdAsync(Guid id);
        Task UpdateAsync(HospitalData hospitalData);
    }
}