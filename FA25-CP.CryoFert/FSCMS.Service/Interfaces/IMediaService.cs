using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.ReponseModel.FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    public interface IMediaService
    {
        Task<BaseResponse<MediaResponse>> UploadMediaAsync(UploadMediaRequest request);
        Task<BaseResponse<MediaResponse>> GetMediaByIdAsync(Guid mediaId);
        Task<DynamicResponse<MediaResponse>> GetAllMediasAsync(GetMediasRequest request);
        Task<BaseResponse<MediaResponse>> UpdateMediaAsync(Guid mediaId, UpdateMediaRequest request);
        Task<BaseResponse> DeleteMediaAsync(Guid mediaId);
    }
}
