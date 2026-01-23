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
        /// <param name="request">Request containing pagination and filter parameters</param>
        /// <returns>Paginated collection of hospital data</returns>
        Task<DynamicResponse<HospitalDataResponse>> GetAllAsync(GetHospitalDataRequest request);

        /// <summary>
        /// Retrieves a single hospital data record by ID
        /// </summary>
        /// <param name="id">The unique identifier of the hospital data</param>
        /// <returns>Hospital data response or null if not found</returns>
        Task<BaseResponse<HospitalDataResponse>> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates a new hospital data record
        /// </summary>
        /// <param name="request">Request containing hospital data details</param>
        /// <returns>Response with created hospital data details</returns>
        Task<BaseResponse<HospitalDataResponse>> CreateAsync(CreateHospitalDataRequest request);

        /// <summary>
        /// Updates an existing hospital data record
        /// </summary>
        /// <param name="request">Request containing updated hospital data details</param>
        /// <returns>Response with updated hospital data details</returns>
        Task<BaseResponse<HospitalDataResponse>> UpdateAsync(UpdateHospitalDataRequest request);

        /// <summary>
        /// Soft deletes a hospital data record
        /// </summary>
        /// <param name="id">The unique identifier of the hospital data to delete</param>
        /// <returns>Response indicating operation result</returns>
        Task<BaseResponse<object>> DeleteAsync(Guid id);
    }
}