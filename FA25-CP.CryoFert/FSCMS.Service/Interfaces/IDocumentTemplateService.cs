using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    public interface IDocumentTemplateService
    {
        /// <summary>
        /// Get document template by ID
        /// </summary>
        Task<BaseResponse<DocumentTemplateDetailResponse>> GetDocumentTemplateByIdAsync(GetDocumentTemplateByIdRequest request);

        /// <summary>
        /// Get document templates with pagination, filtering, and search
        /// </summary>
        Task<DynamicResponse<DocumentTemplateResponse>> GetDocumentTemplatesAsync(GetDocumentTemplatesRequest request);

        /// <summary>
        /// Create a new document template
        /// </summary>
        /// <param name="request">Template creation request</param>
        /// <param name="createdBy">User ID who creates the template</param>
        Task<BaseResponse<DocumentTemplateResponse>> CreateDocumentTemplateAsync(CreateDocumentTemplateRequest request, Guid createdBy);

        /// <summary>
        /// Update an existing document template
        /// </summary>
        /// <param name="request">Template update request</param>
        /// <param name="updatedBy">User ID who updates the template</param>
        Task<BaseResponse<DocumentTemplateResponse>> UpdateDocumentTemplateAsync(UpdateDocumentTemplateRequest request, Guid updatedBy);

        /// <summary>
        /// Delete a document template (soft delete)
        /// </summary>
        /// <param name="templateId">Template ID</param>
        Task<BaseResponse> DeleteDocumentTemplateAsync(Guid templateId);
        Task<BaseResponse<byte[]>> GenerateFilledPdfAsync(GenerateFilledPdfRequest request);
    }
}
