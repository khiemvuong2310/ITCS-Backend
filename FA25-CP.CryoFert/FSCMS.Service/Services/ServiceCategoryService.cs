using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FSCMS.Core.Entities;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using FSCMS.Service.Mapping;
using FSCMS.Core.Models;

namespace FSCMS.Service.Services
{
    public class ServiceCategoryService : IServiceCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ServiceCategoryService> _logger;

        public ServiceCategoryService(IUnitOfWork unitOfWork, ILogger<ServiceCategoryService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<DynamicResponse<ServiceCategoryResponseModel>> GetAllAsync(GetServiceCategoriesRequest request)
        {
            const string methodName = nameof(GetAllAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                // Validate and normalize request
                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return new DynamicResponse<ServiceCategoryResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_REQUEST",
                        Message = "Request cannot be null",
                        MetaData = new PagingMetaData(),
                        Data = new List<ServiceCategoryResponseModel>()
                    };
                }

                request.Normalize();

                var query = _unitOfWork.Repository<ServiceCategory>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(sc => sc.Services)
                    .Where(sc => !sc.IsDeleted);

                // Apply filters
                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    var searchTerm = request.SearchTerm.ToLowerInvariant();
                    query = query.Where(sc => 
                        sc.Name.ToLower().Contains(searchTerm) ||
                        (sc.Code != null && sc.Code.ToLower().Contains(searchTerm)) ||
                        (sc.Description != null && sc.Description.ToLower().Contains(searchTerm)));
                }

                if (request.IsActive.HasValue)
                {
                    query = query.Where(sc => sc.IsActive == request.IsActive.Value);
                }

                // Get total count
                var totalCount = await query.CountAsync();

                // Apply sorting
                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    var isDescending = request.Order?.ToLower() == "desc";

                    query = request.Sort.ToLower() switch
                    {
                        "name" => isDescending ? query.OrderByDescending(sc => sc.Name) : query.OrderBy(sc => sc.Name),
                        "code" => isDescending ? query.OrderByDescending(sc => sc.Code) : query.OrderBy(sc => sc.Code),
                        "displayorder" => isDescending ? query.OrderByDescending(sc => sc.DisplayOrder) : query.OrderBy(sc => sc.DisplayOrder),
                        "createdat" => isDescending ? query.OrderByDescending(sc => sc.CreatedAt) : query.OrderBy(sc => sc.CreatedAt),
                        _ => isDescending ? query.OrderByDescending(sc => sc.DisplayOrder).ThenByDescending(sc => sc.Name) : query.OrderBy(sc => sc.DisplayOrder).ThenBy(sc => sc.Name)
                    };
                }
                else
                {
                    query = query.OrderBy(sc => sc.DisplayOrder).ThenBy(sc => sc.Name);
                }

                // Apply pagination
                var items = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                var responseItems = items.Select(sc => sc.ToResponseModel()).ToList();
                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} service categories", methodName, responseItems.Count);

                return new DynamicResponse<ServiceCategoryResponseModel>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Service categories retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = totalCount,
                        CurrentPageSize = responseItems.Count
                    },
                    Data = responseItems
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving service categories", methodName);
                return new DynamicResponse<ServiceCategoryResponseModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred while retrieving service categories",
                    MetaData = new PagingMetaData(),
                    Data = new List<ServiceCategoryResponseModel>()
                };
            }
        }

        public async Task<BaseResponse<ServiceCategoryResponseModel>> GetByIdAsync(Guid id)
        {
            const string methodName = nameof(GetByIdAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

            try
            {
                // Input validation
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID provided - {Id}", methodName, id);
                    return new BaseResponse<ServiceCategoryResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_ID",
                        Message = "Service category ID cannot be empty or invalid",
                        Data = null
                    };
                }

                var serviceCategory = await _unitOfWork.Repository<ServiceCategory>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(sc => sc.Services)
                    .Where(sc => sc.Id == id && !sc.IsDeleted)
                    .FirstOrDefaultAsync();

                if (serviceCategory == null)
                {
                    _logger.LogWarning("{MethodName}: Service category not found with ID: {Id}", methodName, id);
                    return new BaseResponse<ServiceCategoryResponseModel>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "SERVICE_CATEGORY_NOT_FOUND",
                        Message = "Service category not found",
                        Data = null
                    };
                }

                var response = serviceCategory.ToResponseModel();
                _logger.LogInformation("{MethodName}: Successfully retrieved service category {Id}", methodName, id);

                return new BaseResponse<ServiceCategoryResponseModel>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Service category retrieved successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving service category {Id}", methodName, id);
                return new BaseResponse<ServiceCategoryResponseModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred while retrieving the service category",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ServiceCategoryResponseModel>> CreateAsync(ServiceCategoryRequestModel request)
        {
            const string methodName = nameof(CreateAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                // Input validation
                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<ServiceCategoryResponseModel>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                // Check if code already exists
                if (!string.IsNullOrEmpty(request.Code))
                {
                    var existingByCode = await _unitOfWork.Repository<ServiceCategory>()
                        .GetQueryable()
                        .FirstOrDefaultAsync(sc => sc.Code == request.Code && !sc.IsDeleted);

                    if (existingByCode != null)
                    {
                        _logger.LogWarning("{MethodName}: Service category code already exists: {Code}", methodName, request.Code);
                        return BaseResponse<ServiceCategoryResponseModel>.CreateError("Service category code already exists", StatusCodes.Status400BadRequest, "CODE_ALREADY_EXISTS");
                    }
                }

                var entity = request.ToEntity();
                await _unitOfWork.Repository<ServiceCategory>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                // Reload with services to get accurate count
                var createdCategory = await _unitOfWork.Repository<ServiceCategory>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(sc => sc.Services)
                    .FirstOrDefaultAsync(sc => sc.Id == entity.Id);

                var response = createdCategory!.ToResponseModel();
                _logger.LogInformation("{MethodName}: Successfully created service category {Id}", methodName, entity.Id);

                return BaseResponse<ServiceCategoryResponseModel>.CreateSuccess(response, "Service category created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating service category", methodName);
                return BaseResponse<ServiceCategoryResponseModel>.CreateError($"Error creating service category: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<ServiceCategoryResponseModel>> UpdateAsync(Guid id, ServiceCategoryRequestModel request)
        {
            const string methodName = nameof(UpdateAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}, request: {@Request}", methodName, id, request);

            try
            {
                // Input validation
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID provided - {Id}", methodName, id);
                    return BaseResponse<ServiceCategoryResponseModel>.CreateError("Service category ID cannot be empty or invalid", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<ServiceCategoryResponseModel>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                var entity = await _unitOfWork.Repository<ServiceCategory>()
                    .GetQueryable()
                    .Include(sc => sc.Services)
                    .FirstOrDefaultAsync(sc => sc.Id == id && !sc.IsDeleted);

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Service category not found with ID: {Id}", methodName, id);
                    return BaseResponse<ServiceCategoryResponseModel>.CreateError("Service category not found", StatusCodes.Status404NotFound, "SERVICE_CATEGORY_NOT_FOUND");
                }

                // Check if code already exists (excluding current entity)
                if (!string.IsNullOrEmpty(request.Code))
                {
                    var existingByCode = await _unitOfWork.Repository<ServiceCategory>()
                        .GetQueryable()
                        .FirstOrDefaultAsync(sc => sc.Code == request.Code && sc.Id != id && !sc.IsDeleted);

                    if (existingByCode != null)
                    {
                        _logger.LogWarning("{MethodName}: Service category code already exists: {Code}", methodName, request.Code);
                        return BaseResponse<ServiceCategoryResponseModel>.CreateError("Service category code already exists", StatusCodes.Status400BadRequest, "CODE_ALREADY_EXISTS");
                    }
                }

                entity.UpdateEntity(request);
                await _unitOfWork.Repository<ServiceCategory>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();

                // Reload with services
                var updatedCategory = await _unitOfWork.Repository<ServiceCategory>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(sc => sc.Services)
                    .FirstOrDefaultAsync(sc => sc.Id == id);

                var response = updatedCategory!.ToResponseModel();
                _logger.LogInformation("{MethodName}: Successfully updated service category {Id}", methodName, id);

                return BaseResponse<ServiceCategoryResponseModel>.CreateSuccess(response, "Service category updated successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating service category {Id}", methodName, id);
                return BaseResponse<ServiceCategoryResponseModel>.CreateError($"Error updating service category: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Guid id)
        {
            const string methodName = nameof(DeleteAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

            try
            {
                // Input validation
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID provided - {Id}", methodName, id);
                    return BaseResponse<bool>.CreateError("Service category ID cannot be empty or invalid", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var entity = await _unitOfWork.Repository<ServiceCategory>()
                    .GetQueryable()
                    .Include(sc => sc.Services)
                    .FirstOrDefaultAsync(sc => sc.Id == id && !sc.IsDeleted);

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Service category not found with ID: {Id}", methodName, id);
                    return BaseResponse<bool>.CreateError("Service category not found", StatusCodes.Status404NotFound, "SERVICE_CATEGORY_NOT_FOUND");
                }

                // Check if there are services using this category
                if (entity.Services != null && entity.Services.Any(s => !s.IsDeleted))
                {
                    _logger.LogWarning("{MethodName}: Cannot delete service category with existing services: {Id}", methodName, id);
                    return BaseResponse<bool>.CreateError("Cannot delete service category with existing services", StatusCodes.Status400BadRequest, "CATEGORY_HAS_SERVICES");
                }

                // Soft delete
                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<ServiceCategory>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully deleted service category {Id}", methodName, id);

                return BaseResponse<bool>.CreateSuccess(true, "Service category deleted successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error deleting service category {Id}", methodName, id);
                return BaseResponse<bool>.CreateError($"Error deleting service category: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<List<ServiceCategoryResponseModel>>> GetActiveAsync()
        {
            const string methodName = nameof(GetActiveAsync);
            _logger.LogInformation("{MethodName} called", methodName);

            try
            {
                var categories = await _unitOfWork.Repository<ServiceCategory>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(sc => sc.Services)
                    .Where(sc => sc.IsActive && !sc.IsDeleted)
                    .OrderBy(sc => sc.DisplayOrder)
                    .ThenBy(sc => sc.Name)
                    .ToListAsync();

                var response = categories.Select(sc => sc.ToResponseModel()).ToList();
                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} active service categories", methodName, response.Count);

                return BaseResponse<List<ServiceCategoryResponseModel>>.CreateSuccess(response, "Active service categories retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving active service categories", methodName);
                return BaseResponse<List<ServiceCategoryResponseModel>>.CreateError($"Error retrieving active service categories: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }
    }
}
