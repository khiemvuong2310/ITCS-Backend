using FSCMS.Service.ReponseModel;
using FSCMS.Service.ReponseModel.FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    /// <summary>
    /// Service interface for Agreement operations
    /// </summary>
    public interface IAgreementService
    {
        /// <summary>
        /// Get all agreements with pagination, filtering, and sorting
        /// </summary>
        Task<DynamicResponse<AgreementResponse>> GetAllAsync(GetAgreementsRequest request);

        /// <summary>
        /// Get agreement by ID with full details
        /// </summary>
        Task<BaseResponse<AgreementDetailResponse>> GetByIdAsync(Guid id);

        /// <summary>
        /// Get agreement by agreement code
        /// </summary>
        Task<BaseResponse<AgreementDetailResponse>> GetByCodeAsync(string agreementCode);

        /// <summary>
        /// Create a new agreement
        /// </summary>
        Task<BaseResponse<AgreementResponse>> CreateAsync(CreateAgreementRequest request);

        /// <summary>
        /// Update an existing agreement
        /// </summary>
        Task<BaseResponse<AgreementResponse>> UpdateAsync(Guid id, UpdateAgreementRequest request);

        /// <summary>
        /// Sign an agreement (by patient or doctor)
        /// </summary>
        Task<BaseResponse<AgreementResponse>> SignAsync(Guid id, SignAgreementRequest request);

        /// <summary>
        /// Update agreement status
        /// </summary>
        Task<BaseResponse<AgreementResponse>> UpdateStatusAsync(Guid id, FSCMS.Core.Enum.AgreementStatus status);

        /// <summary>
        /// Soft delete an agreement
        /// </summary>
        Task<BaseResponse> DeleteAsync(Guid id);

        /// <summary>
        /// Request signature by sending OTP to patient
        /// </summary>
        Task<BaseResponse> RequestSignatureAsync(Guid id);

        /// <summary>
        /// Verify OTP and sign agreement
        /// </summary>
        Task<BaseResponse<AgreementResponse>> VerifySignatureAsync(Guid id, string otpCode);

        /// <summary>
        /// Get agreement file(s) from Media table
        /// </summary>
        Task<BaseResponse<List<MediaResponse>>> GetAgreementFileAsync(Guid id);
    }
}

