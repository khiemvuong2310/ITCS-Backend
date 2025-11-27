using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    public interface IDocumentTemplateService
    {
        Task<BaseResponse<byte[]>> GenerateFilledPdfAsync(GenerateFilledPdfRequest request);
    }
}
