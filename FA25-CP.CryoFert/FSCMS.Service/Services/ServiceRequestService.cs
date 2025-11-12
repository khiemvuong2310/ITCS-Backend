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
using FSCMS.Core.Enum;

namespace FSCMS.Service.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ServiceRequestService> _logger;

        public ServiceRequestService(IUnitOfWork unitOfWork, ILogger<ServiceRequestService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<DynamicResponse<ServiceRequestResponseModel>> GetAllAsync(GetServiceRequestsRequest request)
        {
            const string methodName = nameof(GetAllAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                // Validate and normalize request
                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return new DynamicResponse<ServiceRequestResponseModel>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_REQUEST",
                        Message = "Request cannot be null",
                        MetaData = new PagingMetaData(),
                        Data = new List<ServiceRequestResponseModel>()
                    };
                }

                request.Normalize();

                var query = _unitOfWork.Repository<ServiceRequest>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(sr => sr.ServiceRequestDetails)
                        .ThenInclude(srd => srd.Service)
                    .Where(sr => !sr.IsDeleted);

                // Apply filters
                if (request.Status.HasValue)
                {
                    query = query.Where(sr => sr.Status == request.Status.Value);
                }

                if (request.AppointmentId.HasValue)
                {
                    query = query.Where(sr => sr.AppointmentId == request.AppointmentId.Value);
                }

                if (request.RequestDateFrom.HasValue)
                {
                    query = query.Where(sr => sr.RequestDate >= request.RequestDateFrom.Value);
                }

                if (request.RequestDateTo.HasValue)
                {
                    query = query.Where(sr => sr.RequestDate <= request.RequestDateTo.Value);
                }

                if (request.MinAmount.HasValue)
                {
                    query = query.Where(sr => sr.TotalAmount >= request.MinAmount.Value);
                }

                if (request.MaxAmount.HasValue)
                {
                    query = query.Where(sr => sr.TotalAmount <= request.MaxAmount.Value);
                }

                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    var searchTerm = request.SearchTerm.ToLowerInvariant();
                    query = query.Where(sr => 
                        (sr.Notes != null && sr.Notes.ToLower().Contains(searchTerm)) ||
                        (sr.ApprovedBy != null && sr.ApprovedBy.ToLower().Contains(searchTerm)));
                }

                // Get total count
                var totalCount = await query.CountAsync();

                // Apply sorting
                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    var isDescending = request.Order?.ToLower() == "desc";

                    query = request.Sort.ToLower() switch
                    {
                        "requestdate" => isDescending ? query.OrderByDescending(sr => sr.RequestDate) : query.OrderBy(sr => sr.RequestDate),
                        "status" => isDescending ? query.OrderByDescending(sr => sr.Status) : query.OrderBy(sr => sr.Status),
                        "totalamount" => isDescending ? query.OrderByDescending(sr => sr.TotalAmount) : query.OrderBy(sr => sr.TotalAmount),
                        "approveddate" => isDescending ? query.OrderByDescending(sr => sr.ApprovedDate) : query.OrderBy(sr => sr.ApprovedDate),
                        "createdat" => isDescending ? query.OrderByDescending(sr => sr.CreatedAt) : query.OrderBy(sr => sr.CreatedAt),
                        _ => isDescending ? query.OrderByDescending(sr => sr.RequestDate) : query.OrderBy(sr => sr.RequestDate)
                    };
                }
                else
                {
                    query = query.OrderByDescending(sr => sr.RequestDate);
                }

                // Apply pagination
                var items = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                var responseItems = items.Select(sr => sr.ToResponseModel()).ToList();
                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} service requests", methodName, responseItems.Count);

                return new DynamicResponse<ServiceRequestResponseModel>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Service requests retrieved successfully",
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
                _logger.LogError(ex, "{MethodName}: Error retrieving service requests", methodName);
                return new DynamicResponse<ServiceRequestResponseModel>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred while retrieving service requests",
                    MetaData = new PagingMetaData(),
                    Data = new List<ServiceRequestResponseModel>()
                };
            }
        }

        public async Task<BaseResponse<ServiceRequestResponseModel>> GetByIdAsync(Guid id)
        {
            const string methodName = nameof(GetByIdAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID provided - {Id}", methodName, id);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("Service request ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var serviceRequest = await _unitOfWork.Repository<ServiceRequest>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(sr => sr.ServiceRequestDetails)
                        .ThenInclude(srd => srd.Service)
                    .Where(sr => sr.Id == id && !sr.IsDeleted)
                    .FirstOrDefaultAsync();

                if (serviceRequest == null)
                {
                    _logger.LogWarning("{MethodName}: Service request not found with ID: {Id}", methodName, id);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("Service request not found", StatusCodes.Status404NotFound, "SERVICE_REQUEST_NOT_FOUND");
                }

                var response = serviceRequest.ToResponseModel();
                _logger.LogInformation("{MethodName}: Successfully retrieved service request {Id}", methodName, id);

                return BaseResponse<ServiceRequestResponseModel>.CreateSuccess(response, "Service request retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving service request {Id}", methodName, id);
                return BaseResponse<ServiceRequestResponseModel>.CreateError($"Error retrieving service request: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<ServiceRequestResponseModel>> CreateAsync(ServiceRequestCreateRequestModel request)
        {
            const string methodName = nameof(CreateAsync);
            _logger.LogInformation("{MethodName} called with request: {@Request}", methodName, request);

            try
            {
                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                // Validate appointment exists if provided
                if (request.AppointmentId.HasValue)
                {
                    var appointmentExists = await _unitOfWork.Repository<Appointment>()
                        .GetQueryable()
                        .AnyAsync(a => a.Id == request.AppointmentId.Value && !a.IsDeleted);

                    if (!appointmentExists)
                    {
                        _logger.LogWarning("{MethodName}: Appointment not found with ID: {AppointmentId}", methodName, request.AppointmentId.Value);
                        return BaseResponse<ServiceRequestResponseModel>.CreateError("Appointment not found", StatusCodes.Status404NotFound, "APPOINTMENT_NOT_FOUND");
                    }
                }

                // Validate all services exist
                var serviceIds = request.ServiceDetails.Select(sd => sd.ServiceId).ToList();
                var existingServices = await _unitOfWork.Repository<Core.Entities.Service>()
                    .GetQueryable()
                    .Where(s => serviceIds.Contains(s.Id) && !s.IsDeleted)
                    .ToListAsync();

                if (existingServices.Count != serviceIds.Count)
                {
                    _logger.LogWarning("{MethodName}: One or more services not found", methodName);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("One or more services not found", StatusCodes.Status404NotFound, "SERVICE_NOT_FOUND");
                }

                var entity = request.ToEntity();
                
                // Create service request details
                var details = new List<ServiceRequestDetails>();
                decimal totalAmount = 0;

                foreach (var detailRequest in request.ServiceDetails)
                {
                    var detail = detailRequest.ToEntity(entity.Id);
                    details.Add(detail);
                    totalAmount += detail.TotalPrice;
                }

                entity.TotalAmount = totalAmount;

                await _unitOfWork.Repository<ServiceRequest>().InsertAsync(entity);
                
                foreach (var detail in details)
                {
                    await _unitOfWork.Repository<ServiceRequestDetails>().InsertAsync(detail);
                }

                await _unitOfWork.CommitAsync();

                // Get the created service request with all details
                var createdServiceRequest = await _unitOfWork.Repository<ServiceRequest>()
                    .GetQueryable()
                    .Include(sr => sr.ServiceRequestDetails)
                        .ThenInclude(srd => srd.Service)
                    .FirstOrDefaultAsync(sr => sr.Id == entity.Id);

                var response = createdServiceRequest!.ToResponseModel();
                _logger.LogInformation("{MethodName}: Successfully created service request {Id}", methodName, entity.Id);

                return BaseResponse<ServiceRequestResponseModel>.CreateSuccess(response, "Service request created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating service request", methodName);
                return BaseResponse<ServiceRequestResponseModel>.CreateError($"Error creating service request: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<ServiceRequestResponseModel>> UpdateAsync(Guid id, ServiceRequestUpdateRequestModel request)
        {
            const string methodName = nameof(UpdateAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}, request: {@Request}", methodName, id, request);

            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID provided - {Id}", methodName, id);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("Service request ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                if (request == null)
                {
                    _logger.LogWarning("{MethodName}: Request is null", methodName);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("Request cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                var entity = await _unitOfWork.Repository<ServiceRequest>()
                    .GetQueryable()
                    .Include(sr => sr.ServiceRequestDetails)
                        .ThenInclude(srd => srd.Service)
                    .Where(sr => sr.Id == id && !sr.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Service request not found with ID: {Id}", methodName, id);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("Service request not found", StatusCodes.Status404NotFound, "SERVICE_REQUEST_NOT_FOUND");
                }

                // Validate appointment exists if provided
                if (request.AppointmentId.HasValue)
                {
                    var appointmentExists = await _unitOfWork.Repository<Appointment>()
                        .GetQueryable()
                        .AnyAsync(a => a.Id == request.AppointmentId.Value && !a.IsDeleted);

                    if (!appointmentExists)
                    {
                        _logger.LogWarning("{MethodName}: Appointment not found with ID: {AppointmentId}", methodName, request.AppointmentId.Value);
                        return BaseResponse<ServiceRequestResponseModel>.CreateError("Appointment not found", StatusCodes.Status404NotFound, "APPOINTMENT_NOT_FOUND");
                    }
                }

                entity.UpdateEntity(request);
                await _unitOfWork.Repository<ServiceRequest>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();

                // Reload with details
                var updatedRequest = await _unitOfWork.Repository<ServiceRequest>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(sr => sr.ServiceRequestDetails)
                        .ThenInclude(srd => srd.Service)
                    .FirstOrDefaultAsync(sr => sr.Id == id);

                var response = updatedRequest!.ToResponseModel();
                _logger.LogInformation("{MethodName}: Successfully updated service request {Id}", methodName, id);

                return BaseResponse<ServiceRequestResponseModel>.CreateSuccess(response, "Service request updated successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating service request {Id}", methodName, id);
                return BaseResponse<ServiceRequestResponseModel>.CreateError($"Error updating service request: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
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
                    return BaseResponse<bool>.CreateError("Service request ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var entity = await _unitOfWork.Repository<ServiceRequest>()
                    .GetQueryable()
                    .Include(sr => sr.ServiceRequestDetails)
                    .Where(sr => sr.Id == id && !sr.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Service request not found with ID: {Id}", methodName, id);
                    return BaseResponse<bool>.CreateError("Service request not found", StatusCodes.Status404NotFound, "SERVICE_REQUEST_NOT_FOUND");
                }

                // Can only delete pending requests
                if (entity.Status != ServiceRequestStatus.Pending)
                {
                    _logger.LogWarning("{MethodName}: Only pending service requests can be deleted: {Id}", methodName, id);
                    return BaseResponse<bool>.CreateError("Only pending service requests can be deleted", StatusCodes.Status400BadRequest, "INVALID_STATUS");
                }

                // Soft delete details first
                if (entity.ServiceRequestDetails != null && entity.ServiceRequestDetails.Any(d => !d.IsDeleted))
                {
                    foreach (var detail in entity.ServiceRequestDetails.Where(d => !d.IsDeleted))
                    {
                        detail.IsDeleted = true;
                        detail.DeletedAt = DateTime.UtcNow.AddHours(7);
                        detail.UpdatedAt = DateTime.UtcNow.AddHours(7);
                        await _unitOfWork.Repository<ServiceRequestDetails>().UpdateGuid(detail, detail.Id);
                    }
                }

                // Soft delete
                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow.AddHours(7);
                entity.UpdatedAt = DateTime.UtcNow.AddHours(7);
                await _unitOfWork.Repository<ServiceRequest>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();

                _logger.LogInformation("{MethodName}: Successfully deleted service request {Id}", methodName, id);

                return BaseResponse<bool>.CreateSuccess(true, "Service request deleted successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error deleting service request {Id}", methodName, id);
                return BaseResponse<bool>.CreateError($"Error deleting service request: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<List<ServiceRequestResponseModel>>> GetByStatusAsync(ServiceRequestStatus status)
        {
            const string methodName = nameof(GetByStatusAsync);
            _logger.LogInformation("{MethodName} called with status: {Status}", methodName, status);

            try
            {
                var serviceRequests = await _unitOfWork.Repository<ServiceRequest>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(sr => sr.ServiceRequestDetails)
                        .ThenInclude(srd => srd.Service)
                    .Where(sr => sr.Status == status && !sr.IsDeleted)
                    .OrderByDescending(sr => sr.CreatedAt)
                    .ToListAsync();

                var response = serviceRequests.Select(sr => sr.ToResponseModel()).ToList();
                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} service requests", methodName, response.Count);

                return BaseResponse<List<ServiceRequestResponseModel>>.CreateSuccess(response, "Service requests retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving service requests by status", methodName);
                return BaseResponse<List<ServiceRequestResponseModel>>.CreateError($"Error retrieving service requests by status: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<List<ServiceRequestResponseModel>>> GetByAppointmentAsync(Guid appointmentId)
        {
            const string methodName = nameof(GetByAppointmentAsync);
            _logger.LogInformation("{MethodName} called with appointmentId: {AppointmentId}", methodName, appointmentId);

            try
            {
                if (appointmentId == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid appointmentId provided - {AppointmentId}", methodName, appointmentId);
                    return BaseResponse<List<ServiceRequestResponseModel>>.CreateError("Appointment ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var serviceRequests = await _unitOfWork.Repository<ServiceRequest>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Include(sr => sr.ServiceRequestDetails)
                        .ThenInclude(srd => srd.Service)
                    .Where(sr => sr.AppointmentId == appointmentId && !sr.IsDeleted)
                    .OrderByDescending(sr => sr.CreatedAt)
                    .ToListAsync();

                var response = serviceRequests.Select(sr => sr.ToResponseModel()).ToList();
                _logger.LogInformation("{MethodName}: Successfully retrieved {Count} service requests", methodName, response.Count);

                return BaseResponse<List<ServiceRequestResponseModel>>.CreateSuccess(response, "Service requests retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving service requests by appointment", methodName);
                return BaseResponse<List<ServiceRequestResponseModel>>.CreateError($"Error retrieving service requests by appointment: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<ServiceRequestResponseModel>> ApproveAsync(Guid id, string approvedBy)
        {
            const string methodName = nameof(ApproveAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}, approvedBy: {ApprovedBy}", methodName, id, approvedBy);

            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID provided - {Id}", methodName, id);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("Service request ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                if (string.IsNullOrWhiteSpace(approvedBy))
                {
                    _logger.LogWarning("{MethodName}: ApprovedBy is null or empty", methodName);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("ApprovedBy cannot be empty", StatusCodes.Status400BadRequest, "INVALID_APPROVED_BY");
                }

                var entity = await _unitOfWork.Repository<ServiceRequest>()
                    .GetQueryable()
                    .Include(sr => sr.ServiceRequestDetails)
                        .ThenInclude(srd => srd.Service)
                    .Where(sr => sr.Id == id && !sr.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Service request not found with ID: {Id}", methodName, id);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("Service request not found", StatusCodes.Status404NotFound, "SERVICE_REQUEST_NOT_FOUND");
                }

                if (entity.Status != ServiceRequestStatus.Pending)
                {
                    _logger.LogWarning("{MethodName}: Only pending service requests can be approved: {Id}", methodName, id);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("Only pending service requests can be approved", StatusCodes.Status400BadRequest, "INVALID_STATUS");
                }

                entity.Status = ServiceRequestStatus.Approved;
                entity.ApprovedDate = DateTime.UtcNow.AddHours(7);
                entity.ApprovedBy = approvedBy;

                await _unitOfWork.Repository<ServiceRequest>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();

                var response = entity.ToResponseModel();
                _logger.LogInformation("{MethodName}: Successfully approved service request {Id}", methodName, id);

                return BaseResponse<ServiceRequestResponseModel>.CreateSuccess(response, "Service request approved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error approving service request {Id}", methodName, id);
                return BaseResponse<ServiceRequestResponseModel>.CreateError($"Error approving service request: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<ServiceRequestResponseModel>> RejectAsync(Guid id, string rejectedBy)
        {
            const string methodName = nameof(RejectAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}, rejectedBy: {RejectedBy}", methodName, id, rejectedBy);

            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID provided - {Id}", methodName, id);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("Service request ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                if (string.IsNullOrWhiteSpace(rejectedBy))
                {
                    _logger.LogWarning("{MethodName}: RejectedBy is null or empty", methodName);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("RejectedBy cannot be empty", StatusCodes.Status400BadRequest, "INVALID_REJECTED_BY");
                }

                var entity = await _unitOfWork.Repository<ServiceRequest>()
                    .GetQueryable()
                    .Include(sr => sr.ServiceRequestDetails)
                        .ThenInclude(srd => srd.Service)
                    .Where(sr => sr.Id == id && !sr.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Service request not found with ID: {Id}", methodName, id);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("Service request not found", StatusCodes.Status404NotFound, "SERVICE_REQUEST_NOT_FOUND");
                }

                if (entity.Status != ServiceRequestStatus.Pending)
                {
                    _logger.LogWarning("{MethodName}: Only pending service requests can be rejected: {Id}", methodName, id);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("Only pending service requests can be rejected", StatusCodes.Status400BadRequest, "INVALID_STATUS");
                }

                entity.Status = ServiceRequestStatus.Rejected;
                entity.ApprovedBy = rejectedBy;

                await _unitOfWork.Repository<ServiceRequest>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();

                var response = entity.ToResponseModel();
                _logger.LogInformation("{MethodName}: Successfully rejected service request {Id}", methodName, id);

                return BaseResponse<ServiceRequestResponseModel>.CreateSuccess(response, "Service request rejected successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error rejecting service request {Id}", methodName, id);
                return BaseResponse<ServiceRequestResponseModel>.CreateError($"Error rejecting service request: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<ServiceRequestResponseModel>> CompleteAsync(Guid id)
        {
            const string methodName = nameof(CompleteAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID provided - {Id}", methodName, id);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("Service request ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var entity = await _unitOfWork.Repository<ServiceRequest>()
                    .GetQueryable()
                    .Include(sr => sr.ServiceRequestDetails)
                        .ThenInclude(srd => srd.Service)
                    .Where(sr => sr.Id == id && !sr.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Service request not found with ID: {Id}", methodName, id);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("Service request not found", StatusCodes.Status404NotFound, "SERVICE_REQUEST_NOT_FOUND");
                }

                if (entity.Status != ServiceRequestStatus.Approved)
                {
                    _logger.LogWarning("{MethodName}: Only approved service requests can be completed: {Id}", methodName, id);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("Only approved service requests can be completed", StatusCodes.Status400BadRequest, "INVALID_STATUS");
                }

                entity.Status = ServiceRequestStatus.Completed;

                await _unitOfWork.Repository<ServiceRequest>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();

                var response = entity.ToResponseModel();
                _logger.LogInformation("{MethodName}: Successfully completed service request {Id}", methodName, id);

                return BaseResponse<ServiceRequestResponseModel>.CreateSuccess(response, "Service request completed successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error completing service request {Id}", methodName, id);
                return BaseResponse<ServiceRequestResponseModel>.CreateError($"Error completing service request: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<ServiceRequestResponseModel>> CancelAsync(Guid id)
        {
            const string methodName = nameof(CancelAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("{MethodName}: Invalid ID provided - {Id}", methodName, id);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("Service request ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var entity = await _unitOfWork.Repository<ServiceRequest>()
                    .GetQueryable()
                    .Include(sr => sr.ServiceRequestDetails)
                        .ThenInclude(srd => srd.Service)
                    .Where(sr => sr.Id == id && !sr.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    _logger.LogWarning("{MethodName}: Service request not found with ID: {Id}", methodName, id);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("Service request not found", StatusCodes.Status404NotFound, "SERVICE_REQUEST_NOT_FOUND");
                }

                if (entity.Status == ServiceRequestStatus.Completed)
                {
                    _logger.LogWarning("{MethodName}: Completed service requests cannot be cancelled: {Id}", methodName, id);
                    return BaseResponse<ServiceRequestResponseModel>.CreateError("Completed service requests cannot be cancelled", StatusCodes.Status400BadRequest, "INVALID_STATUS");
                }

                entity.Status = ServiceRequestStatus.Cancelled;

                await _unitOfWork.Repository<ServiceRequest>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();

                var response = entity.ToResponseModel();
                _logger.LogInformation("{MethodName}: Successfully cancelled service request {Id}", methodName, id);

                return BaseResponse<ServiceRequestResponseModel>.CreateSuccess(response, "Service request cancelled successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error cancelling service request {Id}", methodName, id);
                return BaseResponse<ServiceRequestResponseModel>.CreateError($"Error cancelling service request: {ex.Message}", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }
    }
}
