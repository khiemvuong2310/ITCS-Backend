using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    public interface ICryoImportService
    {
        /// <summary>
        /// Get CryoImport by ID
        /// </summary>
        Task<BaseResponse<CryoImportResponse>> GetByIdAsync(Guid id);

        /// <summary>
        /// Get all CryoImports with pagination and filtering
        /// </summary>
        Task<DynamicResponse<CryoImportResponse>> GetAllAsync(GetCryoImportsRequest request);

        /// <summary>
        /// Create new CryoImport
        /// </summary>
        Task<BaseResponse<CryoImportResponse>> CreateAsync(CreateCryoImportRequest request);

        /// <summary>
        /// Update existing CryoImport
        /// </summary>
        Task<BaseResponse<CryoImportResponse>> UpdateAsync(Guid id, UpdateCryoImportRequest request);

        /// <summary>
        /// Delete CryoImport (soft delete)
        /// </summary>
        Task<BaseResponse> DeleteAsync(Guid id);
    }
}