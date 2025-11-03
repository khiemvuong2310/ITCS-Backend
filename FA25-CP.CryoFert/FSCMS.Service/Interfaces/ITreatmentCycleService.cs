using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Interfaces
{
    public interface ITreatmentCycleService
    {
        Task<DynamicResponse<TreatmentCycleResponseModel>> GetAllAsync(GetTreatmentCyclesRequest request);
        Task<BaseResponse<TreatmentCycleDetailResponseModel>> GetByIdAsync(Guid id);
        Task<BaseResponse<TreatmentCycleResponseModel>> CreateAsync(CreateTreatmentCycleRequest request);
        Task<BaseResponse<TreatmentCycleResponseModel>> UpdateAsync(Guid id, UpdateTreatmentCycleRequest request);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);

        Task<BaseResponse<TreatmentCycleResponseModel>> StartAsync(Guid id, StartTreatmentCycleRequest request);
        Task<BaseResponse<TreatmentCycleResponseModel>> CompleteAsync(Guid id, CompleteTreatmentCycleRequest request);
        Task<BaseResponse<TreatmentCycleResponseModel>> CancelAsync(Guid id, CancelTreatmentCycleRequest request);

        Task<BaseResponse<List<object>>> GetSamplesAsync(Guid id);
        Task<BaseResponse<object>> AddSampleAsync(Guid id, AddCycleSampleRequest request);

        Task<BaseResponse<List<AppointmentSummary>>> GetAppointmentsAsync(Guid id);
        Task<BaseResponse<AppointmentSummary>> AddAppointmentAsync(Guid id, AddCycleAppointmentRequest request);

        Task<BaseResponse<TreatmentCycleBillingResponse>> GetBillingAsync(Guid id);

        Task<BaseResponse<List<DocumentSummary>>> GetDocumentsAsync(Guid id);
        Task<BaseResponse<DocumentSummary>> UploadDocumentAsync(Guid id, UploadCycleDocumentRequest request);
    }
}


