using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using static FSCMS.Service.ReponseModel.CryoStorageContractResponse;

namespace FSCMS.Service.Interfaces
{
    /// <summary>
    /// Defines operations for managing cryo storage contracts.
    /// </summary>
    public interface ICryoStorageContractService
    {
        /// <summary>
        /// Get all cryo storage contracts with pagination, sorting, and filtering.
        /// </summary>
        /// <param name="request">Filter and pagination request.</param>
        /// <returns>Dynamic response containing contract list and metadata.</returns>
        Task<DynamicResponse<CryoStorageContractResponse>> GetAllAsync(GetCryoStorageContractsRequest request);

        /// <summary>
        /// Get detailed cryo storage contract by ID.
        /// </summary>
        /// <param name="id">The contract ID.</param>
        /// <returns>Base response with detailed contract information.</returns>
        Task<BaseResponse<CryoStorageContractDetailResponse>> GetByIdAsync(Guid id);

        /// <summary>
        /// Create a new cryo storage contract.
        /// </summary>
        /// <param name="request">The contract creation request.</param>
        /// <returns>Base response with the created contract.</returns>
        Task<BaseResponse<CryoStorageContractResponse>> CreateAsync(CreateCryoStorageContractRequest request);

        /// <summary>
        /// Update an existing cryo storage contract.
        /// </summary>
        /// <param name="id">The contract ID.</param>
        /// <param name="request">The contract update request.</param>
        /// <returns>Base response with the updated contract.</returns>
        Task<BaseResponse<CryoStorageContractResponse>> UpdateAsync(Guid id, UpdateCryoStorageContractRequest request);

        /// <summary>
        /// Delete a cryo storage contract (soft delete).
        /// </summary>
        /// <param name="id">The contract ID.</param>
        /// <returns>Base response indicating operation result.</returns>
        Task<BaseResponse> DeleteAsync(Guid id);
        Task<BaseResponse> SendOtpEmailAsync(SentOtpEmailRequest request, Guid patientId);
        Task<BaseResponse<CryoStorageContractResponse>> VerifyOtpAsync(VerifyOtpRequest request, Guid patientId);
        Task<BaseResponse<RenderCryoContractResponse>> RenderCryoContractAsync(Guid id);
    }
}
