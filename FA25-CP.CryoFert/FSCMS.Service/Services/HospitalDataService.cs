using FSCMS.Core.Entities;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.Services
{
    public class HospitalDataService : IHospitalDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HospitalDataService> _logger;

        public HospitalDataService(IUnitOfWork unitOfWork, ILogger<HospitalDataService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Get all Hospital Data
        /// </summary>
        public async Task<DynamicResponse<HospitalData>> GetAllAsync(GetHospitalDataRequest request)
        {
            const string methodName = nameof(GetAllAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                // Validate input
                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return new DynamicResponse<HospitalData>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Request cannot be null",
                        Data = new List<HospitalData>()
                    };
                }

                // Set default values for Page and Size if not provided
                var page = request.Page > 0 ? request.Page : 1;
                var size = request.Size > 0 ? request.Size : 10;

                // Apply filtering logic if needed
                var query = _unitOfWork.Repository<HospitalData>().GetQueryable()
                    .AsNoTracking()
                    .Where(h => !h.IsDeleted);

                // Include filtering logic from the request here if needed

                // Get total count for pagination
                var totalCount = await query.CountAsync();

                // Fetch paginated results
                var items = await query.OrderBy(h => h.Value)
                    .Skip((page - 1) * size)
                    .Take(size)
                    .ToListAsync();

                return new DynamicResponse<HospitalData>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Hospital data retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = page,
                        Size = size,
                        Total = totalCount,
                        CurrentPageSize = items.Count
                    },
                    Data = items
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving hospital data", methodName);
                return new DynamicResponse<HospitalData>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Error retrieving hospital data: {ex.Message}",
                    Data = new List<HospitalData>()
                };
            }
        }

        /// <summary>
        /// Get Hospital Data by ID
        /// </summary>
        public async Task<HospitalData> GetByIdAsync(Guid id)
        {
            const string methodName = nameof(GetByIdAsync);
            _logger.LogInformation("Entering {MethodName} method with ID: {Id}", methodName, id);

            try
            {
                return await _unitOfWork.Repository<HospitalData>().GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in {MethodName}: {ErrorMessage}", methodName, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Create new Hospital Data
        /// </summary>
        public async Task CreateAsync(HospitalData hospitalData)
        {
            const string methodName = nameof(CreateAsync);
            _logger.LogInformation("Entering {MethodName} method.", methodName);

            try
            {
                await _unitOfWork.Repository<HospitalData>().InsertAsync(hospitalData);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in {MethodName}: {ErrorMessage}", methodName, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Update existing Hospital Data
        /// </summary>
        public async Task UpdateAsync(HospitalData hospitalData)
        {
            const string methodName = nameof(UpdateAsync);
            _logger.LogInformation("Entering {MethodName} method.", methodName);

            try
            {
                var existingData = await _unitOfWork.Repository<HospitalData>().GetByIdAsync(hospitalData.Id);

                if (existingData == null)
                {
                    _logger.LogWarning("{MethodName}: Hospital data not found for ID: {Id}", methodName, hospitalData.Id);
                    throw new KeyNotFoundException("Hospital data not found");
                }

                // Update only fields that are not null
                if (hospitalData.Value != null)
                {
                    existingData.Value = hospitalData.Value;
                }


                // Repeat for other properties as needed
                // if (hospitalData.SomeProperty != null)
                // {
                //     existingData.SomeProperty = hospitalData.SomeProperty;
                // }

                await _unitOfWork.Repository<HospitalData>().UpdateAsync(existingData);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating hospital data", methodName);
                throw; // Re-throw the exception after logging
            }
        }

        /// <summary>
        /// Delete Hospital Data
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            const string methodName = nameof(DeleteAsync);
            _logger.LogInformation("Entering {MethodName} method with ID: {Id}", methodName, id);

            try
            {
                var hospitalData = await _unitOfWork.Repository<HospitalData>().GetByIdAsync(id);
                if (hospitalData != null)
                {
                    _unitOfWork.Repository<HospitalData>().Delete(hospitalData);
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    _logger.LogWarning($"HospitalData with ID {id} not found for deletion.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in {MethodName}: {ErrorMessage}", methodName, ex.Message);
                throw;
            }
        }
    }
}
