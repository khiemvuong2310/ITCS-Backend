using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using System;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    public interface IMedicalRecordService
    {
        Task<DynamicResponse<MedicalRecord>> GetAllAsync(PagingModel request);
        Task<BaseResponse<MedicalRecord>> GetByIdAsync(Guid medicalRecordId);
        Task<BaseResponse<MedicalRecord>> CreateAsync(MedicalRecord medicalRecord);
        Task<BaseResponse<MedicalRecord>> UpdateAsync(Guid medicalRecordId, MedicalRecord update);
        Task<BaseResponse> DeleteAsync(Guid medicalRecordId);
    }
}


