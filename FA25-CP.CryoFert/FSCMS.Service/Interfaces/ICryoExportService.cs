using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    public interface ICryoExportService
    {
        /// <summary>
        /// Get CryoExport by ID
        /// </summary>
        Task<BaseResponse<CryoExportResponse>> GetByIdAsync(Guid id);

        /// <summary>
        /// Get all CryoExports with pagination and filtering
        /// </summary>
        Task<DynamicResponse<CryoExportResponse>> GetAllAsync(GetCryoExportsRequest request);

        /// <summary>
        /// Create new CryoExport
        /// </summary>
        Task<BaseResponse<CryoExportResponse>> CreateAsync(CreateCryoExportRequest request);

        /// <summary>
        /// Update existing CryoExport
        /// </summary>
        Task<BaseResponse<CryoExportResponse>> UpdateAsync(Guid id, UpdateCryoExportRequest request);

        /// <summary>
        /// Delete CryoExport (soft delete)
        /// </summary>
        Task<BaseResponse> DeleteAsync(Guid id);
    }
}