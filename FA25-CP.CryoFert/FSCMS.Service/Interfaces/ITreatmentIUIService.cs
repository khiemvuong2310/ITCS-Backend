using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Interfaces
{
    public interface ITreatmentIUIService
    {
        Task<BaseResponse<TreatmentIUIResponseModel>> GetByTreatmentIdAsync(Guid treatmentId);
        Task<BaseResponse<TreatmentIUIResponseModel>> CreateAsync(TreatmentIUICreateUpdateRequest request);
        Task<BaseResponse<TreatmentIUIResponseModel>> UpdateAsync(Guid id, TreatmentIUICreateUpdateRequest request);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);
    }
}


