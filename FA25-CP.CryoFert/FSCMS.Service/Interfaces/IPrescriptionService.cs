using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    public interface IPrescriptionService
    {
        Task<DynamicResponse<PrescriptionResponse>> GetAllAsync(GetPrescriptionsRequest request);
        Task<BaseResponse<PrescriptionDetailResponse>> GetByIdAsync(Guid id);
        Task<BaseResponse<PrescriptionResponse>> CreateAsync(CreatePrescriptionRequest request);
        Task<BaseResponse<PrescriptionResponse>> UpdateAsync(Guid id, UpdatePrescriptionRequest request);
        Task<BaseResponse> DeleteAsync(Guid id);
        Task<DynamicResponse<PrescriptionDetailResponse>> GetAllDetailAsync(GetPrescriptionsRequest request);
    }
}
