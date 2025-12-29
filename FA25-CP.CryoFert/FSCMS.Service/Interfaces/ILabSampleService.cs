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
        Task<BaseResponse<LabSampleResponse>> CreateEmbryoAsync(CreateLabSampleEmbryoRequest request);
        Task<BaseResponse<LabSampleResponse>> CreateOocyteAsync(CreateLabSampleOocyteRequest request);
        Task<BaseResponse<LabSampleResponse>> CreateSpermAsync(CreateLabSampleSpermRequest request);
        Task<BaseResponse<LabSampleResponse>> UpdateSpermAsync(Guid id, UpdateLabSampleSpermRequest request);
        Task<BaseResponse<LabSampleResponse>> UpdateOocyteAsync(Guid id, UpdateLabSampleOocyteRequest request);
        Task<BaseResponse<LabSampleResponse>> UpdateEmbryoAsync(Guid id, UpdateLabSampleEmbryoRequest request);
        Task<BaseResponse<LabSampleResponse>> UpdateFrozenAsync(Guid id, UpdateLabSampleFrozenRequest request);
        Task<BaseResponse<LabSampleResponse>> UpdateFertilizeAsync(Guid id, UpdateLabSampleFertilizeRequest request);
        Task<BaseResponse> DeleteAsync(Guid id);
        Task<DynamicResponse<LabSampleDetailResponse>> GetAllDetailAsync(GetLabSamplesRequestDetail request);
        Task<DynamicResponse<LabSampleDetailResponse>> GetEligibleLabSamplesAsync(GetEligibleLabSamplesRequest request);
        Task<DynamicResponse<LabSampleDetailResponse>> GetCoupleSpecimensAsync(GetEligibleLabSamplesRequest request);
    }
}
