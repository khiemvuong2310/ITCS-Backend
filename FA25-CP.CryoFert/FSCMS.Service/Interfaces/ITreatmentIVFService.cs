using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Interfaces
{
    public interface ITreatmentIVFService
    {
        Task<BaseResponse<TreatmentIVFResponseModel>> GetByTreatmentIdAsync(Guid treatmentId);
        Task<BaseResponse<List<TreatmentIVFResponseModel>>> GetByPatientIdAsync(Guid patientId);
        Task<BaseResponse<TreatmentIVFResponseModel>> CreateAsync(TreatmentIVFCreateUpdateRequest request);
        Task<BaseResponse<TreatmentIVFResponseModel>> UpdateAsync(Guid id, TreatmentIVFUpdateRequest request);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);
    }
}


