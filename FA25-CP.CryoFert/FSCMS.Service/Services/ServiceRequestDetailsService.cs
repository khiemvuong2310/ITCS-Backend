using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FSCMS.Core.Entities;
using FSCMS.Core.Interfaces;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using FSCMS.Service.Mapping;
using FSCMS.Core.Models;
using FSCMS.Core.Enum;
using AutoMapper;

namespace FSCMS.Service.Services
{
    public class ServiceRequestDetailsService : IServiceRequestDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ServiceRequestDetailsService> _logger;
        private readonly IMapper _mapper;
        private readonly IMediaService _mediaService;
        private readonly IRedisService _redisService;

        public ServiceRequestDetailsService(
            IUnitOfWork unitOfWork,
            ILogger<ServiceRequestDetailsService> logger,
            IMapper mapper,
            IMediaService mediaService,
            IRedisService redisService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediaService = mediaService ?? throw new ArgumentNullException(nameof(mediaService));
            _redisService = redisService ?? throw new ArgumentNullException(nameof(redisService));
        }

        public async Task<BaseResponse<List<ServiceRequestDetailResponseModel>>> GetByServiceRequestAsync(Guid serviceRequestId)
        {
            const string methodName = nameof(GetByServiceRequestAsync);
            _logger.LogInformation("{MethodName} called with serviceRequestId: {ServiceRequestId}", methodName, serviceRequestId);

            try
            {
                if (serviceRequestId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid serviceRequestId provided - {ServiceRequestId}", methodName, serviceRequestId);
                    return BaseResponse<List<ServiceRequestDetailResponseModel>>.CreateError("Service request ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                // [REDIS STEP 1] Try to get from cache first
                string cacheKey = $"service_request_details:sr{serviceRequestId}";
                try
                {
                    var cachedData = await _redisService.GetAsync<List<ServiceRequestDetailResponseModel>>(cacheKey);
                    if (cachedData != null)
                    {
                        _logger.LogInformation("{MethodName}: Retrieved from Redis cache with serviceRequestId: {ServiceRequestId}", methodName, serviceRequestId);
                        return BaseResponse<List<ServiceRequestDetailResponseModel>>.CreateSuccess(cachedData, "Retrieved from cache", StatusCodes.Status200OK);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "{MethodName}: Error accessing Redis cache", methodName);
                }

                var details = await _unitOfWork.Repository<ServiceRequestDetails>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(srd => srd.Service)
                    .Where(srd => srd.ServiceRequestId == serviceRequestId && !srd.IsDeleted)
                    .OrderBy(srd => srd.Service!.Name)
                    .ToListAsync();

                var response = details.Select(srd => srd.ToResponseModel()).ToList();

                // Attach medias for each detail
                var detailIds = details.Select(d => d.Id).ToList();
                var detailMediaMap = await _mediaService.GetMediaGroupedByRelatedEntityIdsAsync(detailIds, EntityTypeMedia.ServiceRequestDetails);
                foreach (var item in response)
                {
                    if (detailMediaMap.TryGetValue(item.Id, out var medias))
                    {
                        item.MediaFiles = medias;
                    }
                }

                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} service request details", methodName, response.Count);

                return BaseResponse<List<ServiceRequestDetailResponseModel>>.CreateSuccess(response, "Service request details retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving service request details", methodName);
                return BaseResponse<List<ServiceRequestDetailResponseModel>>.CreateError($"Error retrieving service request details: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<ServiceRequestDetailResponseModel>> GetByIdAsync(Guid id)
        {
            const string methodName = nameof(GetByIdAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID provided - {Id}", methodName, id);
                    return BaseResponse<ServiceRequestDetailResponseModel>.CreateError("Service request detail ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                // [REDIS STEP 1] Try to get from cache first
                string cacheKey = $"service_request_detail:{id}";
                try
                {
                    var cachedData = await _redisService.GetAsync<ServiceRequestDetailResponseModel>(cacheKey);
                    if (cachedData != null)
                    {
                        _logger.LogInformation("{MethodName}: Retrieved from Redis cache with ID: {Id}", methodName, id);
                        return BaseResponse<ServiceRequestDetailResponseModel>.CreateSuccess(cachedData, "Retrieved from cache", StatusCodes.Status200OK);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "{MethodName}: Error accessing Redis cache", methodName);
                }

                var detail = await _unitOfWork.Repository<ServiceRequestDetails>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(srd => srd.Service)
                    .Include(srd => srd.ServiceRequest)
                    .Where(srd => srd.Id == id && !srd.IsDeleted)
                    .FirstOrDefaultAsync();

                if (detail == null)
                {
                    _logger.LogWarning("{MethodName}: Service request detail not found with ID: {Id}", methodName, id);
                    return BaseResponse<ServiceRequestDetailResponseModel>.CreateError("Service request detail not found", StatusCodes.Status404NotFound, "SERVICE_REQUEST_DETAIL_NOT_FOUND");
                }

                var response = detail.ToResponseModel();

                // Attach medias for this detail
                var detailMediaMap = await _mediaService.GetMediaGroupedByRelatedEntityIdsAsync(new[] { detail.Id }, EntityTypeMedia.ServiceRequestDetails);
                if (detailMediaMap.TryGetValue(detail.Id, out var medias))
                {
                    response.MediaFiles = medias;
                }

                // [REDIS STEP 2] Store in cache
                try
                {
                    await _redisService.SetAsync(cacheKey, response, TimeSpan.FromMinutes(10));
                    _logger.LogInformation("{MethodName}: Stored in Redis cache", methodName);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "{MethodName}: Error storing in Redis cache", methodName);
                }

                _logger.LogInformation("{MethodName}: Successfully retrieved service request detail {Id}", methodName, id);

                return BaseResponse<ServiceRequestDetailResponseModel>.CreateSuccess(response, "Service request detail retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving service request detail {Id}", methodName, id);
                return BaseResponse<ServiceRequestDetailResponseModel>.CreateError($"Error retrieving service request detail: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<ServiceRequestDetailResponseModel>> CreateAsync(ServiceRequestDetailCreateRequestModel request, Guid serviceRequestId)
        {
            const string methodName = nameof(CreateAsync);
            _logger.LogInformation("{MethodName} called with serviceRequestId: {ServiceRequestId}, request: {@Request}", methodName, serviceRequestId, request);

            try
            {
                if (serviceRequestId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid serviceRequestId provided - {ServiceRequestId}", methodName, serviceRequestId);
                    return BaseResponse<ServiceRequestDetailResponseModel>.CreateError("Service request ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<ServiceRequestDetailResponseModel>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                // Check if service request exists and is still pending
                var serviceRequest = await _unitOfWork.Repository<ServiceRequest>()
                    .GetQueryable()
                    .Where(sr => sr.Id == serviceRequestId && !sr.IsDeleted)
                    .FirstOrDefaultAsync();

                if (serviceRequest == null)
                {
                    _logger.LogWarning("{MethodName}: Service request not found with ID: {ServiceRequestId}", methodName, serviceRequestId);
                    return BaseResponse<ServiceRequestDetailResponseModel>.CreateError("Service request not found", StatusCodes.Status404NotFound, "SERVICE_REQUEST_NOT_FOUND");
                }

                if (serviceRequest.Status != ServiceRequestStatus.Pending)
                {
                    _logger.LogWarning("{MethodName}: Can only add details to pending service requests: {ServiceRequestId}", methodName, serviceRequestId);
                    return BaseResponse<ServiceRequestDetailResponseModel>.CreateError("Can only add details to pending service requests", StatusCodes.Status400BadRequest, "INVALID_STATUS");
                }

                // Check if service exists
                var service = await _unitOfWork.Repository<Core.Entities.Service>()
                    .GetQueryable()
                    .Where(s => s.Id == request.ServiceId && !s.IsDeleted)
                    .FirstOrDefaultAsync();

                if (service == null)
                {
                    _logger.LogWarning("{MethodName}: Service not found with ID: {ServiceId}", methodName, request.ServiceId);
                    return BaseResponse<ServiceRequestDetailResponseModel>.CreateError("Service not found", StatusCodes.Status404NotFound, "SERVICE_NOT_FOUND");
                }

                // Check if service is already in the request
                var existingDetail = await _unitOfWork.Repository<ServiceRequestDetails>()
                    .GetQueryable()
                    .FirstOrDefaultAsync(srd => srd.ServiceRequestId == serviceRequestId && srd.ServiceId == request.ServiceId && !srd.IsDeleted);

                if (existingDetail != null)
                {
                    _logger.LogWarning("{MethodName}: Service is already in this request: {ServiceId}", methodName, request.ServiceId);
                    return BaseResponse<ServiceRequestDetailResponseModel>.CreateError("Service is already in this request", StatusCodes.Status400BadRequest, "SERVICE_ALREADY_EXISTS");
                }

                var entity = request.ToEntity(serviceRequestId);
                await _unitOfWork.Repository<ServiceRequestDetails>().InsertAsync(entity);

                // Update service request total amount
                var allDetails = await _unitOfWork.Repository<ServiceRequestDetails>()
                    .GetQueryable()
                    .Where(srd => srd.ServiceRequestId == serviceRequestId && !srd.IsDeleted)
                    .ToListAsync();

                var newTotalAmount = allDetails.Sum(srd => srd.TotalPrice) + entity.TotalPrice;
                serviceRequest.TotalAmount = newTotalAmount;
                await _unitOfWork.Repository<ServiceRequest>().UpdateGuid(serviceRequest, serviceRequestId);

                await _unitOfWork.CommitAsync();

                // Get the created detail with service info
                var createdDetail = await _unitOfWork.Repository<ServiceRequestDetails>()
                    .GetQueryable()
                    .Include(srd => srd.Service)
                    .FirstOrDefaultAsync(srd => srd.Id == entity.Id);

                var response = createdDetail!.ToResponseModel();
                _logger.LogInformation("{MethodName}: Successfully created service request detail {Id}", methodName, entity.Id);

                return BaseResponse<ServiceRequestDetailResponseModel>.CreateSuccess(response, "Service request detail created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating service request detail", methodName);
                return BaseResponse<ServiceRequestDetailResponseModel>.CreateError($"Error creating service request detail: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<ServiceRequestDetailResponseModel>> UpdateAsync(Guid id, ServiceRequestDetailUpdateRequestModel request)
        {
            const string methodName = nameof(UpdateAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}, request: {@Request}", methodName, id, request);

            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID provided - {Id}", methodName, id);
                    return BaseResponse<ServiceRequestDetailResponseModel>.CreateError("Service request detail ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<ServiceRequestDetailResponseModel>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                var entity = await _unitOfWork.Repository<ServiceRequestDetails>()
                    .GetQueryable()
                    .Include(srd => srd.Service)
                    .Include(srd => srd.ServiceRequest)
                    .Where(srd => srd.Id == id && !srd.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Service request detail not found with ID: {Id}", methodName, id);
                    return BaseResponse<ServiceRequestDetailResponseModel>.CreateError("Service request detail not found", StatusCodes.Status404NotFound, "SERVICE_REQUEST_DETAIL_NOT_FOUND");
                }

                if (entity.ServiceRequest!.Status != ServiceRequestStatus.Pending)
                {
                    _logger.LogWarning("{MethodName}: Can only update details of pending service requests: {Id}", methodName, id);
                    return BaseResponse<ServiceRequestDetailResponseModel>.CreateError("Can only update details of pending service requests", StatusCodes.Status400BadRequest, "INVALID_STATUS");
                }

                var oldTotalPrice = entity.TotalPrice;
                entity.UpdateEntity(request);
                var newTotalPrice = entity.TotalPrice;

                await _unitOfWork.Repository<ServiceRequestDetails>().UpdateGuid(entity, id);

                // Update service request total amount
                var serviceRequest = entity.ServiceRequest;
                serviceRequest.TotalAmount = (serviceRequest.TotalAmount ?? 0) - oldTotalPrice + newTotalPrice;
                await _unitOfWork.Repository<ServiceRequest>().UpdateGuid(serviceRequest, serviceRequest.Id);

                await _unitOfWork.CommitAsync();

                var response = entity.ToResponseModel();
                _logger.LogInformation("{MethodName}: Successfully updated service request detail {Id}", methodName, id);

                return BaseResponse<ServiceRequestDetailResponseModel>.CreateSuccess(response, "Service request detail updated successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating service request detail {Id}", methodName, id);
                return BaseResponse<ServiceRequestDetailResponseModel>.CreateError($"Error updating service request detail: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
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
                    return BaseResponse<bool>.CreateError("Service request detail ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var entity = await _unitOfWork.Repository<ServiceRequestDetails>()
                    .GetQueryable()
                    .Include(srd => srd.ServiceRequest)
                    .Where(srd => srd.Id == id && !srd.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Service request detail not found with ID: {Id}", methodName, id);
                    return BaseResponse<bool>.CreateError("Service request detail not found", StatusCodes.Status404NotFound, "SERVICE_REQUEST_DETAIL_NOT_FOUND");
                }

                if (entity.ServiceRequest!.Status != ServiceRequestStatus.Pending)
                {
                    _logger.LogWarning("{MethodName}: Can only delete details from pending service requests: {Id}", methodName, id);
                    return BaseResponse<bool>.CreateError("Can only delete details from pending service requests", StatusCodes.Status400BadRequest, "INVALID_STATUS");
                }

                // Check if this is the last detail
                var detailCount = await _unitOfWork.Repository<ServiceRequestDetails>()
                    .GetQueryable()
                    .CountAsync(srd => srd.ServiceRequestId == entity.ServiceRequestId && !srd.IsDeleted);

                if (detailCount <= 1)
                {
                    _logger.LogWarning("{MethodName}: Cannot delete the last service detail: {Id}", methodName, id);
                    return BaseResponse<bool>.CreateError("Cannot delete the last service detail. Delete the entire service request instead.", StatusCodes.Status400BadRequest, "LAST_DETAIL");
                }

                var removedTotalPrice = entity.TotalPrice;
                
                // Soft delete
                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<ServiceRequestDetails>().UpdateGuid(entity, id);

                // Update service request total amount
                var serviceRequest = entity.ServiceRequest;
                serviceRequest.TotalAmount = (serviceRequest.TotalAmount ?? 0) - removedTotalPrice;
                await _unitOfWork.Repository<ServiceRequest>().UpdateGuid(serviceRequest, serviceRequest.Id);

                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully deleted service request detail {Id}", methodName, id);

                return BaseResponse<bool>.CreateSuccess(true, "Service request detail deleted successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error deleting service request detail {Id}", methodName, id);
                return BaseResponse<bool>.CreateError($"Error deleting service request detail: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<List<ServiceRequestDetailResponseModel>>> GetByServiceAsync(Guid serviceId)
        {
            const string methodName = nameof(GetByServiceAsync);
            _logger.LogInformation("{MethodName} called with serviceId: {ServiceId}", methodName, serviceId);

            try
            {
                if (serviceId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid serviceId provided - {ServiceId}", methodName, serviceId);
                    return BaseResponse<List<ServiceRequestDetailResponseModel>>.CreateError("Service ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                // [REDIS STEP 1] Try to get from cache first
                string cacheKey = $"service_request_details:service{serviceId}";
                try
                {
                    var cachedData = await _redisService.GetAsync<List<ServiceRequestDetailResponseModel>>(cacheKey);
                    if (cachedData != null)
                    {
                        _logger.LogInformation("{MethodName}: Retrieved from Redis cache with serviceId: {ServiceId}", methodName, serviceId);
                        return BaseResponse<List<ServiceRequestDetailResponseModel>>.CreateSuccess(cachedData, "Retrieved from cache", StatusCodes.Status200OK);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "{MethodName}: Error accessing Redis cache", methodName);
                }

                var details = await _unitOfWork.Repository<ServiceRequestDetails>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(srd => srd.Service)
                    .Include(srd => srd.ServiceRequest)
                    .Where(srd => srd.ServiceId == serviceId && !srd.IsDeleted)
                    .OrderByDescending(srd => srd.CreatedAt)
                    .ToListAsync();

                var response = details.Select(srd => srd.ToResponseModel()).ToList();

                // Attach medias for each detail
                var detailIds = details.Select(d => d.Id).ToList();
                var detailMediaMap = await _mediaService.GetMediaGroupedByRelatedEntityIdsAsync(detailIds, EntityTypeMedia.ServiceRequestDetails);
                foreach (var item in response)
                {
                    if (detailMediaMap.TryGetValue(item.Id, out var medias))
                    {
                        item.MediaFiles = medias;
                    }
                }

                // [REDIS STEP 2] Store in cache
                try
                {
                    await _redisService.SetAsync(cacheKey, response, TimeSpan.FromMinutes(10));
                    _logger.LogInformation("{MethodName}: Stored in Redis cache", methodName);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "{MethodName}: Error storing in Redis cache", methodName);
                }

                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} service request details", methodName, response.Count);

                return BaseResponse<List<ServiceRequestDetailResponseModel>>.CreateSuccess(response, "Service request details retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving service request details by service", methodName);
                return BaseResponse<List<ServiceRequestDetailResponseModel>>.CreateError($"Error retrieving service request details by service: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }
    }
}
