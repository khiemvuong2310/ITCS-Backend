using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Services
{
    /// <summary>
    /// Interface for hospital data service operations
    /// </summary>
    public interface IHospitalDataService
    {
        /// <summary>
        /// Retrieves paginated hospital data based on filter criteria
        /// </summary>
        Task<DynamicResponse<HospitalDataResponse>> GetAllAsync(GetHospitalDataRequest request);

        /// <summary>
        /// Retrieves a single hospital data record by ID
        /// </summary>
        Task<BaseResponse<HospitalDataResponse>> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates a new hospital data record
        /// </summary>
        Task<BaseResponse<HospitalDataResponse>> CreateAsync(CreateHospitalDataRequest request);

        /// <summary>
        /// Updates an existing hospital data record
        /// </summary>
        Task<BaseResponse<HospitalDataResponse>> UpdateAsync(UpdateHospitalDataRequest request);

        /// <summary>
        /// Soft deletes a hospital data record
        /// </summary>
        Task<BaseResponse<object>> DeleteAsync(Guid id);
    }
}