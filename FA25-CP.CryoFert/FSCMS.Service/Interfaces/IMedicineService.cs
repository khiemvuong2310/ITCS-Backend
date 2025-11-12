using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    public interface IMedicineService
    {
        Task<DynamicResponse<Medicine>> GetAllAsync(PagingModel request);
        Task<BaseResponse<Medicine>> GetByIdAsync(Guid medicineId);
        Task<BaseResponse<Medicine>> CreateAsync(Medicine medicine);
        Task<BaseResponse<Medicine>> UpdateAsync(Guid medicineId, Medicine update);
        Task<BaseResponse> DeleteAsync(Guid medicineId);
    }
}


