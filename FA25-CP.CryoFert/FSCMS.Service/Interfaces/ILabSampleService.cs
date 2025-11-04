using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    public interface ILabSampleService
    {
        Task<BaseResponse<LabSampleDetailResponse>> GetByIdAsync(Guid id);
        Task<DynamicResponse<LabSampleResponse>> GetAllAsync(GetLabSamplesRequest request);
        Task<BaseResponse<LabSampleResponse>> CreateAsync(CreateLabSampleRequest request);
        Task<BaseResponse<LabSampleResponse>> UpdateAsync(Guid id, UpdateLabSampleRequest request);
        Task<BaseResponse> DeleteAsync(Guid id);
    }
}
