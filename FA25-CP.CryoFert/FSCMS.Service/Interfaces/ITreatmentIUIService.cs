using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Interfaces
{
    public interface ITreatmentIUIService
    {
        Task<BaseResponse<TreatmentIUIResponseModel>> GetByTreatmentIdAsync(Guid treatmentId);
        Task<BaseResponse<List<TreatmentIUIResponseModel>>> GetByPatientIdAsync(Guid patientId);
        Task<BaseResponse<TreatmentIUIResponseModel>> CreateAsync(TreatmentIUICreateUpdateRequest request);
        Task<BaseResponse<TreatmentIUIResponseModel>> UpdateAsync(Guid id, TreatmentIUIUpdateRequest request);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);
    }
}


