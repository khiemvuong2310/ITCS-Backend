using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using System;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    public interface IPrescriptionDetailService
    {
        Task<DynamicResponse<PrescriptionDetail>> GetAllAsync(PagingModel request);
        Task<BaseResponse<PrescriptionDetail>> GetByIdAsync(Guid prescriptionDetailId);
        Task<BaseResponse<PrescriptionDetail>> CreateAsync(PrescriptionDetail detail);
        Task<BaseResponse<PrescriptionDetail>> UpdateAsync(Guid prescriptionDetailId, PrescriptionDetail update);
        Task<BaseResponse> DeleteAsync(Guid prescriptionDetailId);
    }
}


