using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using System;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    public interface IPrescriptionService
    {
        Task<DynamicResponse<Prescription>> GetAllAsync(PagingModel request);
        Task<BaseResponse<Prescription>> GetByIdAsync(Guid prescriptionId);
        Task<BaseResponse<Prescription>> CreateAsync(Prescription prescription);
        Task<BaseResponse<Prescription>> UpdateAsync(Guid prescriptionId, Prescription update);
        Task<BaseResponse> DeleteAsync(Guid prescriptionId);
    }
}


