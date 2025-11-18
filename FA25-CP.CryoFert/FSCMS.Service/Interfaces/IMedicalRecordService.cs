using System;
using System.Threading.Tasks;
using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Interfaces
{
    public interface IMedicalRecordService
    {
        Task<DynamicResponse<MedicalRecordResponse>> GetAllAsync(SearchMedicalRecordRequest request);
        Task<BaseResponse<MedicalRecordDetailResponse>> GetByIdAsync(Guid id);
        Task<BaseResponse<MedicalRecordResponse>> CreateAsync(CreateMedicalRecordRequest request);
        Task<BaseResponse<MedicalRecordResponse>> UpdateAsync(Guid id, UpdateMedicalRecordRequest request);
        Task<BaseResponse> DeleteAsync(Guid id);
    }
}


