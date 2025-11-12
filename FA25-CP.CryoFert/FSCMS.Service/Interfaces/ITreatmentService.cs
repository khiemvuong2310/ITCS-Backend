using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Interfaces
{
    public interface ITreatmentService
    {
        Task<DynamicResponse<TreatmentResponseModel>> GetAllAsync(GetTreatmentsRequest request);
        Task<BaseResponse<TreatmentDetailResponseModel>> GetByIdAsync(Guid id);
        Task<BaseResponse<TreatmentResponseModel>> CreateAsync(TreatmentCreateUpdateRequest request);
        Task<BaseResponse<TreatmentResponseModel>> UpdateAsync(Guid id, TreatmentCreateUpdateRequest request);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);
    }
}


