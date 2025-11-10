using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSCMS.Service.Interfaces
{
    public interface ICryoPackageService
    {
        /// <summary>
        /// Get Cryo Package by ID
        /// </summary>
        /// <param name="id">The unique ID of the Cryo Package</param>
        /// <returns>BaseResponse containing Cryo Package information</returns>
        Task<BaseResponse<CryoPackageResponse>> GetByIdAsync(Guid id);

        /// <summary>
        /// Get all Cryo Packages with pagination and filters
        /// </summary>
        /// <param name="request">Filter and paging request model</param>
        /// <returns>DynamicResponse containing a list of Cryo Packages</returns>
        Task<DynamicResponse<CryoPackageResponse>> GetAllAsync(GetCryoPackagesRequest request);

        /// <summary>
        /// Create a new Cryo Package
        /// </summary>
        /// <param name="request">The request data for creating a Cryo Package</param>
        /// <returns>BaseResponse containing created Cryo Package information</returns>
        Task<BaseResponse<CryoPackageResponse>> CreateAsync(CreateCryoPackageRequest request);

        /// <summary>
        /// Update an existing Cryo Package
        /// </summary>
        /// <param name="id">The ID of the Cryo Package to update</param>
        /// <param name="request">The request data for updating</param>
        /// <returns>BaseResponse containing updated Cryo Package information</returns>
        Task<BaseResponse<CryoPackageResponse>> UpdateAsync(Guid id, UpdateCryoPackageRequest request);

        /// <summary>
        /// Delete a Cryo Package (soft delete)
        /// </summary>
        /// <param name="id">The ID of the Cryo Package to delete</param>
        /// <returns>BaseResponse with operation result</returns>
        Task<BaseResponse> DeleteAsync(Guid id);
    }
}
