using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using FSCMS.Service.Mapping;
using FSCMS.Core.Models;
using FSCMS.Core.Entities;
using FSCMS.Core.Interfaces;

namespace FSCMS.Service.Services
{
    public class ServiceService : IServiceService
    {
        #region Dependencies

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ServiceService> _logger;

        #endregion

        #region Constructor

        public ServiceService(IUnitOfWork unitOfWork, ILogger<ServiceService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region CRUD Operations

        public async Task<DynamicResponse<ServiceResponseModel>> GetAllAsync(GetServicesRequest request)
        {
            const string methodName = nameof(GetAllAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                // 1. Validate và Normalize request trước để đảm bảo dữ liệu nhất quán
                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return new DynamicResponse<ServiceResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_REQUEST",
                        Message = "Request cannot be null",
                        MetaData = new PagingMetaData(),
                        Data = new List<ServiceResponseModel>()
                    };
                }

                request.Normalize(); // Normalize xong mới tạo Cache Key để tránh việc " A" và "A" ra 2 key khác nhau

                // =========================================================================================
                // [REDIS STEP 1]: TẠO CACHE KEY & KIỂM TRA CACHE
                // =========================================================================================

                // Tạo key đại diện cho toàn bộ tham số filter (Search, Sort, Page, Price...)
                // Key format: services_p{Page}_s{Size}_{Search}_{Category}_{Active}_{Sort}_{Order}...
                var query = _unitOfWork.Repository<Core.Entities.Service>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(s => s.ServiceCategory)
                    .Where(s => !s.IsDeleted);

                // Apply filters
                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    var searchTerm = request.SearchTerm.ToLowerInvariant();
                    query = query.Where(s =>
                        s.Name.ToLower().Contains(searchTerm) ||
                        (s.Code != null && s.Code.ToLower().Contains(searchTerm)) ||
                        (s.Description != null && s.Description.ToLower().Contains(searchTerm)));
                }

                if (request.IsActive.HasValue)
                {
                    query = query.Where(s => s.IsActive == request.IsActive.Value);
                }

                if (request.ServiceCategoryId.HasValue)
                {
                    query = query.Where(s => s.ServiceCategoryId == request.ServiceCategoryId.Value);
                }

                if (request.MinPrice.HasValue)
                {
                    query = query.Where(s => s.Price >= request.MinPrice.Value);
                }

                if (request.MaxPrice.HasValue)
                {
                    query = query.Where(s => s.Price <= request.MaxPrice.Value);
                }

                // Get total count
                var totalCount = await query.CountAsync();

                // Apply sorting
                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    var isDescending = request.Order?.ToLower() == "desc";

                    query = request.Sort.ToLower() switch
                    {
                        "name" => isDescending ? query.OrderByDescending(s => s.Name) : query.OrderBy(s => s.Name),
                        "code" => isDescending ? query.OrderByDescending(s => s.Code) : query.OrderBy(s => s.Code),
                        "price" => isDescending ? query.OrderByDescending(s => s.Price) : query.OrderBy(s => s.Price),
                        "category" => isDescending ? query.OrderByDescending(s => s.ServiceCategory!.Name) : query.OrderBy(s => s.ServiceCategory!.Name),
                        "createdat" => isDescending ? query.OrderByDescending(s => s.CreatedAt) : query.OrderBy(s => s.CreatedAt),
                        _ => isDescending ? query.OrderByDescending(s => s.ServiceCategory!.DisplayOrder).ThenByDescending(s => s.Name) : query.OrderBy(s => s.ServiceCategory!.DisplayOrder).ThenBy(s => s.Name)
                    };
                }
                else
                {
                    query = query.OrderBy(s => s.ServiceCategory!.DisplayOrder).ThenBy(s => s.Name);
                }

                // Apply pagination
                var items = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                var responseItems = items.Select(s => s.ToResponseModel()).ToList();
                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} services from Database", methodName, responseItems.Count);

                // Tạo object kết quả trả về
                var finalResponse = new DynamicResponse<ServiceResponseModel>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Services retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = totalCount,
                        CurrentPageSize = responseItems.Count
                    },
                    Data = responseItems
                };

                return finalResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving services", methodName);
                return new DynamicResponse<ServiceResponseModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred while retrieving services",
                    MetaData = new PagingMetaData(),
                    Data = new List<ServiceResponseModel>()
                };
            }
        }

        public async Task<BaseResponse<ServiceResponseModel>> GetByIdAsync(Guid id)
        {
            const string methodName = nameof(GetByIdAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID provided - {Id}", methodName, id);
                    return BaseResponse<ServiceResponseModel>.CreateError("Service ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                // Try to get entity from repository cache first (basic entity without includes)
                var cachedService = await _unitOfWork.Repository<Core.Entities.Service>().GetByIdAsync(id);
                
                // If not found in cache or entity is deleted, query with includes
                if (cachedService == null || cachedService.IsDeleted)
                {
                    var service = await _unitOfWork.Repository<Core.Entities.Service>()
                        .GetQueryable()
                        .AsNoTracking()
                        .Include(s => s.ServiceCategory)
                        .Where(s => s.Id == id && !s.IsDeleted)
                        .FirstOrDefaultAsync();

                    if (service == null)
                    {
                        _logger.LogWarning("{MethodName}: Service not found with ID: {Id}", methodName, id);
                        return BaseResponse<ServiceResponseModel>.CreateError("Service not found", StatusCodes.Status404NotFound, "SERVICE_NOT_FOUND");
                    }

                    var response = service.ToResponseModel();
                    _logger.LogInformation("{MethodName}: Successfully retrieved service {Id} from database", methodName, id);
                    return BaseResponse<ServiceResponseModel>.CreateSuccess(response, "Service retrieved successfully", StatusCodes.Status200OK);
                }

                // Entity found in cache, but we need includes for response
                // Load includes separately if entity is from cache
                var serviceWithIncludes = await _unitOfWork.Repository<Core.Entities.Service>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(s => s.ServiceCategory)
                    .Where(s => s.Id == id && !s.IsDeleted)
                    .FirstOrDefaultAsync();

                if (serviceWithIncludes == null)
                {
                    _logger.LogWarning("{MethodName}: Service not found with ID: {Id}", methodName, id);
                    return BaseResponse<ServiceResponseModel>.CreateError("Service not found", StatusCodes.Status404NotFound, "SERVICE_NOT_FOUND");
                }

                var responseFromCache = serviceWithIncludes.ToResponseModel();
                _logger.LogInformation("{MethodName}: Successfully retrieved service {Id} (entity was cached)", methodName, id);

                return BaseResponse<ServiceResponseModel>.CreateSuccess(responseFromCache, "Service retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving service {Id}", methodName, id);
                return BaseResponse<ServiceResponseModel>.CreateError($"Error retrieving service: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<ServiceResponseModel>> CreateAsync(ServiceCreateRequestModel request)
        {
            const string methodName = nameof(CreateAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<ServiceResponseModel>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                // Check if service category exists
                var categoryExists = await _unitOfWork.Repository<ServiceCategory>()
                    .GetQueryable()
                    .AnyAsync(sc => sc.Id == request.ServiceCategoryId && !sc.IsDeleted);

                if (!categoryExists)
                {
                    _logger.LogWarning("{MethodName}: Service category not found with ID: {CategoryId}", methodName, request.ServiceCategoryId);
                    return BaseResponse<ServiceResponseModel>.CreateError("Service category not found", StatusCodes.Status404NotFound, "SERVICE_CATEGORY_NOT_FOUND");
                }

                // Check if code already exists
                if (!string.IsNullOrEmpty(request.Code))
                {
                    var existingByCode = await _unitOfWork.Repository<Core.Entities.Service>()
                        .GetQueryable()
                        .FirstOrDefaultAsync(s => s.Code == request.Code && !s.IsDeleted);

                    if (existingByCode != null)
                    {
                        _logger.LogWarning("{MethodName}: Service code already exists: {Code}", methodName, request.Code);
                        return BaseResponse<ServiceResponseModel>.CreateError("Service code already exists", StatusCodes.Status400BadRequest, "CODE_ALREADY_EXISTS");
                    }
                }

                var entity = request.ToEntity();
                await _unitOfWork.Repository<Core.Entities.Service>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                // After SaveChanges, entity.Id is now available
                // Invalidate cache for this entity to ensure fresh data on next query
                // Then query with includes for response
                var createdService = await _unitOfWork.Repository<Core.Entities.Service>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(s => s.ServiceCategory)
                    .FirstOrDefaultAsync(s => s.Id == entity.Id);

                if (createdService == null)
                {
                    _logger.LogError("{MethodName}: Failed to retrieve created service {Id}", methodName, entity.Id);
                    return BaseResponse<ServiceResponseModel>.CreateError("Failed to retrieve created service", StatusCodes.Status500InternalServerError, "RETRIEVE_ERROR");
                }

                var response = createdService.ToResponseModel();
                _logger.LogInformation("{MethodName}: Successfully created service {Id}", methodName, entity.Id);

                return BaseResponse<ServiceResponseModel>.CreateSuccess(response, "Service created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating service", methodName);
                return BaseResponse<ServiceResponseModel>.CreateError($"Error creating service: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<ServiceResponseModel>> UpdateAsync(Guid id, ServiceUpdateRequestModel request)
        {
            const string methodName = nameof(UpdateAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}, request: {@Request}", methodName, id, request);

            try
            {
                // 1. Validate input parameters
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID provided - {Id}", methodName, id);
                    return BaseResponse<ServiceResponseModel>.CreateError("Service ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<ServiceResponseModel>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                // 2. Get existing entity
                var entity = await _unitOfWork.Repository<Core.Entities.Service>()
                    .GetQueryable()
                    .Include(s => s.ServiceCategory)
                    .Where(s => s.Id == id && !s.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Service not found with ID: {Id}", methodName, id);
                    return BaseResponse<ServiceResponseModel>.CreateError("Service not found", StatusCodes.Status404NotFound, "SERVICE_NOT_FOUND");
                }

                // 3. Validate ServiceCategoryId if provided (partial update - only validate if value is provided)
                if (request.ServiceCategoryId.HasValue)
                {
                    var categoryExists = await _unitOfWork.Repository<ServiceCategory>()
                        .GetQueryable()
                        .AnyAsync(sc => sc.Id == request.ServiceCategoryId.Value && !sc.IsDeleted);

                    if (!categoryExists)
                    {
                        _logger.LogWarning("{MethodName}: Service category not found with ID: {CategoryId}", methodName, request.ServiceCategoryId.Value);
                        return BaseResponse<ServiceResponseModel>.CreateError(
                            $"Service category with ID '{request.ServiceCategoryId.Value}' not found", 
                            StatusCodes.Status404NotFound, 
                            "SERVICE_CATEGORY_NOT_FOUND");
                    }
                }

                // 4. Validate Code uniqueness if provided (partial update - only validate if value is provided)
                if (!string.IsNullOrWhiteSpace(request.Code))
                {
                    var existingByCode = await _unitOfWork.Repository<Core.Entities.Service>()
                        .GetQueryable()
                        .FirstOrDefaultAsync(s => s.Code == request.Code && s.Id != id && !s.IsDeleted);

                    if (existingByCode != null)
                    {
                        _logger.LogWarning("{MethodName}: Service code already exists: {Code}", methodName, request.Code);
                        return BaseResponse<ServiceResponseModel>.CreateError(
                            $"Service code '{request.Code}' already exists", 
                            StatusCodes.Status400BadRequest, 
                            "CODE_ALREADY_EXISTS");
                    }
                }

                // 5. Apply partial update - only updates fields that are provided (not null)
                // If a property is null, the existing value will be kept
                entity.UpdateEntity(request);
                
                // 6. Save changes (UpdateGuid will automatically invalidate cache for this entity)
                await _unitOfWork.Repository<Core.Entities.Service>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();

                // 7. Retrieve updated entity with includes for response
                // Cache has been invalidated by UpdateGuid, so query fresh data
                var updatedService = await _unitOfWork.Repository<Core.Entities.Service>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(s => s.ServiceCategory)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (updatedService == null)
                {
                    _logger.LogError("{MethodName}: Failed to retrieve updated service {Id}", methodName, id);
                    return BaseResponse<ServiceResponseModel>.CreateError(
                        "Failed to retrieve updated service", 
                        StatusCodes.Status500InternalServerError, 
                        "RETRIEVE_ERROR");
                }

                var response = updatedService.ToResponseModel();
                _logger.LogInformation("{MethodName}: Successfully updated service {Id}", methodName, id);

                return BaseResponse<ServiceResponseModel>.CreateSuccess(response, "Service updated successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating service {Id}", methodName, id);
                return BaseResponse<ServiceResponseModel>.CreateError(
                    $"Error updating service: {ex.Message}", 
                    StatusCodes.Status500InternalServerError, 
                    "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Guid id)
        {
            const string methodName = nameof(DeleteAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID provided - {Id}", methodName, id);
                    return BaseResponse<bool>.CreateError("Service ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var entity = await _unitOfWork.Repository<Core.Entities.Service>()
                    .GetQueryable()
                    .Include(s => s.ServiceRequestDetails)
                    .Where(s => s.Id == id && !s.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Service not found with ID: {Id}", methodName, id);
                    return BaseResponse<bool>.CreateError("Service not found", StatusCodes.Status404NotFound, "SERVICE_NOT_FOUND");
                }

                // Check if there are service requests using this service
                if (entity.ServiceRequestDetails != null && entity.ServiceRequestDetails.Any(srd => !srd.IsDeleted))
                {
                    _logger.LogWarning("{MethodName}: Cannot delete service with existing service requests: {Id}", methodName, id);
                    return BaseResponse<bool>.CreateError("Cannot delete service with existing service requests", StatusCodes.Status400BadRequest, "SERVICE_HAS_REQUESTS");
                }

                // Soft delete
                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<Core.Entities.Service>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully deleted service {Id}", methodName, id);

                return BaseResponse<bool>.CreateSuccess(true, "Service deleted successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error deleting service {Id}", methodName, id);
                return BaseResponse<bool>.CreateError($"Error deleting service: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<List<ServiceResponseModel>>> GetActiveAsync()
        {
            const string methodName = nameof(GetActiveAsync);
            _logger.LogInformation("{MethodName} called", methodName);

            try
            {
                var services = await _unitOfWork.Repository<Core.Entities.Service>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(s => s.ServiceCategory)
                    .Where(s => s.IsActive && s.ServiceCategory!.IsActive && !s.IsDeleted && !s.ServiceCategory.IsDeleted)
                    .OrderBy(s => s.ServiceCategory!.DisplayOrder)
                    .ThenBy(s => s.Name)
                    .ToListAsync();

                var response = services.Select(s => s.ToResponseModel()).ToList();
                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} active services", methodName, response.Count);

                return BaseResponse<List<ServiceResponseModel>>.CreateSuccess(response, "Active services retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving active services", methodName);
                return BaseResponse<List<ServiceResponseModel>>.CreateError($"Error retrieving active services: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<List<ServiceResponseModel>>> GetByCategoryAsync(Guid categoryId)
        {
            const string methodName = nameof(GetByCategoryAsync);
            _logger.LogInformation("{MethodName} called with categoryId: {CategoryId}", methodName, categoryId);

            try
            {
                if (categoryId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid categoryId provided - {CategoryId}", methodName, categoryId);
                    return BaseResponse<List<ServiceResponseModel>>.CreateError("Category ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var services = await _unitOfWork.Repository<Core.Entities.Service>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(s => s.ServiceCategory)
                    .Where(s => s.ServiceCategoryId == categoryId && !s.IsDeleted)
                    .OrderBy(s => s.Name)
                    .ToListAsync();

                var response = services.Select(s => s.ToResponseModel()).ToList();
                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} services", methodName, response.Count);

                return BaseResponse<List<ServiceResponseModel>>.CreateSuccess(response, "Services retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving services by category", methodName);
                return BaseResponse<List<ServiceResponseModel>>.CreateError($"Error retrieving services by category: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<List<ServiceResponseModel>>> SearchAsync(string searchTerm)
        {
            const string methodName = nameof(SearchAsync);
            _logger.LogInformation("{MethodName} called with searchTerm: {SearchTerm}", methodName, searchTerm);

            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return await GetActiveAsync();
                }

                var services = await _unitOfWork.Repository<Core.Entities.Service>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(s => s.ServiceCategory)
                    .Where(s => s.IsActive && !s.IsDeleted &&
                               (s.Name.Contains(searchTerm) ||
                                (s.Code != null && s.Code.Contains(searchTerm)) ||
                                (s.Description != null && s.Description.Contains(searchTerm))))
                    .OrderBy(s => s.Name)
                    .ToListAsync();

                var response = services.Select(s => s.ToResponseModel()).ToList();
                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} services", methodName, response.Count);

                return BaseResponse<List<ServiceResponseModel>>.CreateSuccess(response, "Services retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error searching services", methodName);
                return BaseResponse<List<ServiceResponseModel>>.CreateError($"Error searching services: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        #endregion
    }
}
