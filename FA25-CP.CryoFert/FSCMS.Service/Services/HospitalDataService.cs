using FSCMS.Core.Entities;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.Services
{
    /// <summary>
    /// Service for managing hospital data operations following Clean Architecture principles
    /// </summary>
    public class HospitalDataService : IHospitalDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HospitalDataService> _logger;

        public HospitalDataService(IUnitOfWork unitOfWork, ILogger<HospitalDataService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Validates the request object and returns validation errors if any
        /// </summary>
        private List<System.ComponentModel.DataAnnotations.ValidationResult> ValidateRequest<T>(T request)
        {
            var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            if (request != null)
            {
                var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(request);
                System.ComponentModel.DataAnnotations.Validator.TryValidateObject(request, validationContext, validationResults, true);
            }
            return validationResults;
        }

        /// <summary>
        /// Maps HospitalData entity to HospitalDataResponse
        /// </summary>
        private HospitalDataResponse MapToResponse(HospitalData entity)
        {
            return new HospitalDataResponse
            {
                Id = entity.Id,
                Value = entity.Value,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        /// <summary>
        /// Retrieves paginated hospital data based on filter criteria
        /// </summary>
        public async Task<DynamicResponse<HospitalDataResponse>> GetAllAsync(GetHospitalDataRequest request)
        {
            const string methodName = nameof(GetAllAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                // Validate input
                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return new DynamicResponse<HospitalDataResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_REQUEST",
                        Message = "Request cannot be null",
                        Data = new List<HospitalDataResponse>()
                    };
                }

                // Normalize pagination parameters
                request.Normalize();

                // Build query with filters
                var query = _unitOfWork.Repository<HospitalData>().GetQueryable()
                    .AsNoTracking()
                    .Where(h => !h.IsDeleted);

                // Apply value filters
                if (request.MinValue.HasValue)
                {
                    query = query.Where(h => h.Value >= request.MinValue.Value);
                }

                if (request.MaxValue.HasValue)
                {
                    query = query.Where(h => h.Value <= request.MaxValue.Value);
                }

                // Apply sorting
                if (!string.IsNullOrEmpty(request.Sort))
                {
                    query = request.Sort.ToLowerInvariant() switch
                    {
                        "value" => request.Order == "desc" ? query.OrderByDescending(h => h.Value) : query.OrderBy(h => h.Value),
                        "createdat" => request.Order == "desc" ? query.OrderByDescending(h => h.CreatedAt) : query.OrderBy(h => h.CreatedAt),
                        _ => query.OrderBy(h => h.CreatedAt)
                    };
                }
                else
                {
                    query = query.OrderBy(h => h.CreatedAt);
                }

                // Get total count for pagination
                var totalCount = await query.CountAsync();

                // Fetch paginated results
                var entities = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                // Map to response models
                var responseData = entities.Select(MapToResponse).ToList();

                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} hospital data records", methodName, responseData.Count);

                return new DynamicResponse<HospitalDataResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Hospital data retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = totalCount,
                        CurrentPageSize = responseData.Count
                    },
                    Data = responseData
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving hospital data", methodName);
                return new DynamicResponse<HospitalDataResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An error occurred while retrieving hospital data",
                    Data = new List<HospitalDataResponse>()
                };
            }
        }

        /// <summary>
        /// Retrieves a single hospital data record by ID
        /// </summary>
        public async Task<BaseResponse<HospitalDataResponse>> GetByIdAsync(Guid id)
        {
            const string methodName = nameof(GetByIdAsync);
            _logger.LogInformation("{MethodName} called with ID: {Id}", methodName, id);

            try
            {
                // Validate input
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID provided", methodName);
                    return BaseResponse<HospitalDataResponse>.CreateError(
                        "Invalid ID provided", 
                        StatusCodes.Status400BadRequest, 
                        "INVALID_ID"
                    );
                }

                // Retrieve entity
                var entity = await _unitOfWork.Repository<HospitalData>()
                    .GetQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(h => h.Id == id && !h.IsDeleted);

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Hospital data not found with ID: {Id}", methodName, id);
                    return BaseResponse<HospitalDataResponse>.CreateError(
                        "Hospital data not found", 
                        StatusCodes.Status404NotFound, 
                        "NOT_FOUND"
                    );
                }

                var responseData = MapToResponse(entity);

                _logger.LogInformation("{MethodName}: Successfully retrieved hospital data with ID: {Id}", methodName, id);

                return BaseResponse<HospitalDataResponse>.CreateSuccess(
                    responseData, 
                    "Hospital data retrieved successfully"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving hospital data with ID: {Id}", methodName, id);
                return BaseResponse<HospitalDataResponse>.CreateError(
                    "An error occurred while retrieving hospital data",
                    StatusCodes.Status500InternalServerError,
                    "INTERNAL_ERROR"
                );
            }
        }

        /// <summary>
        /// Creates a new hospital data record
        /// </summary>
        public async Task<BaseResponse<HospitalDataResponse>> CreateAsync(CreateHospitalDataRequest request)
        {
            const string methodName = nameof(CreateAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                // Validate input
                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<HospitalDataResponse>.CreateError(
                        "Request cannot be null",
                        StatusCodes.Status400BadRequest,
                        "INVALID_REQUEST"
                    );
                }

                // Validate using data annotations
                var validationResults = ValidateRequest(request);
                if (validationResults.Any())
                {
                    var errorMessages = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
                    _logger.LogWarning("{MethodName}: Validation failed: {Errors}", methodName, errorMessages);
                    return BaseResponse<HospitalDataResponse>.CreateError(
                        errorMessages,
                        StatusCodes.Status400BadRequest,
                        "VALIDATION_FAILED"
                    );
                }

                // Create entity
                var entity = new HospitalData(Guid.NewGuid(), request.Value);

                // Save to database
                await _unitOfWork.Repository<HospitalData>().InsertAsync(entity);
                await _unitOfWork.SaveChangesAsync();

                var responseData = MapToResponse(entity);

                _logger.LogInformation("{MethodName}: Successfully created hospital data with ID: {Id}", methodName, entity.Id);

                return BaseResponse<HospitalDataResponse>.CreateSuccess(
                    responseData,
                    "Hospital data created successfully",
                    StatusCodes.Status201Created
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating hospital data", methodName);
                return BaseResponse<HospitalDataResponse>.CreateError(
                    "An error occurred while creating hospital data",
                    StatusCodes.Status500InternalServerError,
                    "INTERNAL_ERROR"
                );
            }
        }

        /// <summary>
        /// Updates an existing hospital data record (supports partial updates)
        /// </summary>
        public async Task<BaseResponse<HospitalDataResponse>> UpdateAsync(UpdateHospitalDataRequest request)
        {
            const string methodName = nameof(UpdateAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                // Validate input
                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<HospitalDataResponse>.CreateError(
                        "Request cannot be null",
                        StatusCodes.Status400BadRequest,
                        "INVALID_REQUEST"
                    );
                }

                // Validate using data annotations
                var validationResults = ValidateRequest(request);
                if (validationResults.Any())
                {
                    var errorMessages = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
                    _logger.LogWarning("{MethodName}: Validation failed: {Errors}", methodName, errorMessages);
                    return BaseResponse<HospitalDataResponse>.CreateError(
                        errorMessages,
                        StatusCodes.Status400BadRequest,
                        "VALIDATION_FAILED"
                    );
                }

                // Note: We allow requests with null values to support "keep existing value" scenarios

                // Retrieve existing entity
                var existingEntity = await _unitOfWork.Repository<HospitalData>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(h => h.Id == request.Id && !h.IsDeleted);

                if (existingEntity == null)
                {
                    _logger.LogWarning("{MethodName}: Hospital data not found with ID: {Id}", methodName, request.Id);
                    return BaseResponse<HospitalDataResponse>.CreateError(
                        "Hospital data not found",
                        StatusCodes.Status404NotFound,
                        "NOT_FOUND"
                    );
                }

                // Track changes for logging
                var originalValue = existingEntity.Value;
                var hasChanges = false;

                // Update fields only if they have values (partial update)
                if (request.Value.HasValue && request.Value.Value != existingEntity.Value)
                {
                    existingEntity.Value = request.Value.Value;
                    hasChanges = true;
                    _logger.LogInformation("{MethodName}: Updating Value from {OldValue} to {NewValue} for ID: {Id}", 
                        methodName, originalValue, request.Value.Value, request.Id);
                }

                // If no actual changes were made, return success without saving
                if (!hasChanges)
                {
                    _logger.LogInformation("{MethodName}: No changes detected for hospital data with ID: {Id}", methodName, request.Id);
                    var responseData = MapToResponse(existingEntity);
                    return BaseResponse<HospitalDataResponse>.CreateSuccess(
                        responseData,
                        "Hospital data is already up to date"
                    );
                }

                // Save changes
                await _unitOfWork.Repository<HospitalData>().UpdateAsync(existingEntity);
                await _unitOfWork.SaveChangesAsync();

                var updatedResponseData = MapToResponse(existingEntity);

                _logger.LogInformation("{MethodName}: Successfully updated hospital data with ID: {Id}", methodName, request.Id);

                return BaseResponse<HospitalDataResponse>.CreateSuccess(
                    updatedResponseData,
                    "Hospital data updated successfully"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating hospital data with ID: {Id}", methodName, request?.Id);
                return BaseResponse<HospitalDataResponse>.CreateError(
                    "An error occurred while updating hospital data",
                    StatusCodes.Status500InternalServerError,
                    "INTERNAL_ERROR"
                );
            }
        }

        /// <summary>
        /// Soft deletes a hospital data record
        /// </summary>
        public async Task<BaseResponse<object>> DeleteAsync(Guid id)
        {
            const string methodName = nameof(DeleteAsync);
            _logger.LogInformation("{MethodName} called with ID: {Id}", methodName, id);

            try
            {
                // Validate input
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID provided", methodName);
                    return BaseResponse<object>.CreateError(
                        "Invalid ID provided",
                        StatusCodes.Status400BadRequest,
                        "INVALID_ID"
                    );
                }

                // Retrieve existing entity
                var existingEntity = await _unitOfWork.Repository<HospitalData>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(h => h.Id == id && !h.IsDeleted);

                if (existingEntity == null)
                {
                    _logger.LogWarning("{MethodName}: Hospital data not found with ID: {Id}", methodName, id);
                    return BaseResponse<object>.CreateError(
                        "Hospital data not found",
                        StatusCodes.Status404NotFound,
                        "NOT_FOUND"
                    );
                }

                // Perform soft delete
                _unitOfWork.Repository<HospitalData>().Delete(existingEntity);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("{MethodName}: Successfully deleted hospital data with ID: {Id}", methodName, id);

                return BaseResponse<object>.CreateSuccess(
                    new { },
                    "Hospital data deleted successfully"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error deleting hospital data with ID: {Id}", methodName, id);
                return BaseResponse<object>.CreateError(
                    "An error occurred while deleting hospital data",
                    StatusCodes.Status500InternalServerError,
                    "INTERNAL_ERROR"
                );
            }
        }
    }
}
