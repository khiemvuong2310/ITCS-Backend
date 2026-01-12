using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using FSCMS.Service.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FSCMS.Service.Services
{
    public class NotificationService : INotificationService
    {
        #region Dependencies

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationService> _logger;
        private readonly IHubContext<NotificationHub> _hubContext;

        #endregion

        #region Constructor

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<NotificationService> logger, IHubContext<NotificationHub> hubContext)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hubContext = hubContext;
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Get notification by ID
        /// </summary>
        public async Task<BaseResponse<NotificationResponse>> GetNotificationByIdAsync(Guid id)
        {
            const string methodName = nameof(GetNotificationByIdAsync);
            _logger.LogInformation("{MethodName} called with NotificationId: {Id}", methodName, id);

            if (id == Guid.Empty)
            {
                return new BaseResponse<NotificationResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    SystemCode = "INVALID_NOTIFICATION_ID",
                    Message = "Notification ID cannot be empty",
                    Data = null
                };
            }

            var notification = await _unitOfWork.Repository<Notification>()
                .AsQueryable()
                .AsNoTracking()
                .Include(n => n.Patient)
                .Include(n => n.User)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (notification == null)
            {
                return new BaseResponse<NotificationResponse>
                {
                    Code = StatusCodes.Status404NotFound,
                    SystemCode = "NOTIFICATION_NOT_FOUND",
                    Message = "Notification not found",
                    Data = null
                };
            }

            var response = _mapper.Map<NotificationResponse>(notification);

            return new BaseResponse<NotificationResponse>
            {
                Code = StatusCodes.Status200OK,
                SystemCode = "SUCCESS",
                Message = "Notification retrieved successfully",
                Data = response
            };
        }

        /// <summary>
        /// Get list of notifications with filtering and pagination
        /// </summary>
        public async Task<DynamicResponse<NotificationResponse>> GetNotificationsAsync(GetNotificationsRequest request)
        {
            var query = _unitOfWork.Repository<Notification>().AsQueryable()
                .Include(n => n.Patient)
                .Include(n => n.User)
                .Where(u => !u.IsDeleted);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(n => n.Title.Contains(request.SearchTerm) || n.Content.Contains(request.SearchTerm));
            }

            if (request.PatientId.HasValue)
                query = query.Where(n => n.PatientId == request.PatientId.Value);

            if (request.UserId.HasValue)
                query = query.Where(n => n.UserId == request.UserId.Value);

            if (request.Type.HasValue)
                query = query.Where(n => n.Type == request.Type.Value);

            if (request.Status.HasValue)
                query = query.Where(n => n.Status == request.Status.Value);

            if (request.IsImportant.HasValue)
                query = query.Where(n => n.IsImportant == request.IsImportant.Value);

            if (request.FromDate.HasValue)
                query = query.Where(n => n.CreatedAt >= request.FromDate.Value);

            if (request.ToDate.HasValue)
                query = query.Where(n => n.CreatedAt <= request.ToDate.Value);

            var totalCount = await query.CountAsync();

            query = query.OrderByDescending(n => n.CreatedAt)
                         .Skip((request.Page - 1) * request.Size)
                         .Take(request.Size);

            var notifications = await query.ToListAsync();

            return new DynamicResponse<NotificationResponse>
            {
                Code = StatusCodes.Status200OK,
                Message = "Notifications retrieved successfully",
                MetaData = new PagingMetaData
                {
                    Page = request.Page,
                    Size = request.Size,
                    Total = totalCount
                },
                Data = _mapper.Map<List<NotificationResponse>>(notifications)
            };
        }

        /// <summary>
        /// Create a new notification
        /// </summary>
        public async Task<BaseResponse<NotificationResponse>> CreateNotificationAsync(CreateNotificationRequest request, Guid accountId)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var account = await _unitOfWork.Repository<Account>().GetByIdGuid(accountId);
                if (account == null)
                    return new BaseResponse<NotificationResponse>
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        Message = "UnAuthorize."
                    };

                object? entityExists = request.RelatedEntityType switch
                {
                    EntityTypeNotification.MedicalRecord => await _unitOfWork.Repository<MedicalRecord>()
                                    .AsQueryable()
                                    .Include(x => x.Appointment)
                                    .Where(p => p.Id == request.RelatedEntityId && !p.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeNotification.TreatmentCycle => await _unitOfWork.Repository<TreatmentCycle>()
                                    .AsQueryable()
                                    .Include(x => x.Treatment)
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeNotification.Account => await _unitOfWork.Repository<Account>()
                                    .AsQueryable()
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeNotification.Agreement => await _unitOfWork.Repository<Agreement>()
                                    .AsQueryable()
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeNotification.CryoStorageContract => await _unitOfWork.Repository<CryoStorageContract>()
                                    .AsQueryable()
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeNotification.Treatment => await _unitOfWork.Repository<Treatment>()
                                    .AsQueryable()
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeNotification.Appointment => await _unitOfWork.Repository<Appointment>()
                                    .AsQueryable()
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeNotification.CryoExport => await _unitOfWork.Repository<CryoExport>()
                                    .AsQueryable()
                                    .Include(m => m.LabSample)
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeNotification.CryoImport => await _unitOfWork.Repository<CryoImport>()
                                    .AsQueryable()
                                    .Include(m => m.LabSample)
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeNotification.ServiceRequest => await _unitOfWork.Repository<ServiceRequest>()
                                    .AsQueryable()
                                    .Include( m => m.Appointment)
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeNotification.Relationship => await _unitOfWork.Repository<Relationship>()
                                    .AsQueryable()
                                    .Include(r => r.Patient2)
                                    .Where(r => r.Id == request.RelatedEntityId && !r.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    _ => null
                };

                    if (entityExists == null)
                    {
                        return new BaseResponse<NotificationResponse>
                        {
                            Code = StatusCodes.Status404NotFound,
                            SystemCode = "ENTITY_NOT_FOUND",
                            Message = $"Related entity {request.RelatedEntityType} with ID {request.RelatedEntityId} not found",
                            Data = null
                        };
                    }

                    Guid? patientId = entityExists switch
                    {
                        MedicalRecord mr => mr.Appointment?.PatientId,
                        TreatmentCycle tc => tc.Treatment?.PatientId,
                        Account acc => acc.Id,
                        Agreement ag => ag.PatientId,
                        CryoStorageContract csc => csc.PatientId,
                        ServiceRequest sr => sr.Appointment?.PatientId,
                        CryoImport ci => ci.LabSample?.PatientId,
                        CryoExport ci => ci.LabSample?.PatientId,
                        Appointment a => a.PatientId,
                        Treatment t => t.PatientId,
                        Relationship rel => rel.Patient2Id,
                        _ => null
                    };

                    if (patientId == null)
                    {
                        return new BaseResponse<NotificationResponse>
                        {
                            Code = StatusCodes.Status400BadRequest,
                            SystemCode = "PATIENT_NOT_FOUND",
                            Message = "Cannot determine PatientId from related entity"
                        };
                    }
                        
                var notification = _mapper.Map<Notification>(request);
                notification.Status = NotificationStatus.Scheduled;
                notification.SentTime = DateTime.UtcNow;
                notification.RelatedEntityId = request.RelatedEntityId;
                notification.RelatedEntityType = request.RelatedEntityType.ToString();
                notification.PatientId = patientId;
                notification.UserId = accountId;

                await _unitOfWork.Repository<Notification>().InsertAsync(notification);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();
                // Notify AFTER commit
                await _hubContext.Clients.User(notification.PatientId.ToString())
                    .SendAsync("NotificationCreated", new
                    {
                        Id = notification.Id,
                        Title = notification.Title,
                        Content = notification.Content,
                        Type = notification.Type,
                        CreatedAt = notification.CreatedAt
                    });


                var response = _mapper.Map<NotificationResponse>(notification);

                return new BaseResponse<NotificationResponse>
                {
                    Code = StatusCodes.Status201Created,
                    Message = "Notification created successfully",
                    Data = response
                };
            } catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new BaseResponse<NotificationResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An error occurred while create notification",
                    Data = null
                };
            }
            
        }

        /// <summary>
        /// Update existing notification
        /// </summary>
        public async Task<BaseResponse<NotificationResponse>> UpdateNotificationAsync(Guid Id, UpdateNotificationRequest request)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var notification = await _unitOfWork.Repository<Notification>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(n => n.Id == Id);

                if (notification == null)
                {
                    return new BaseResponse<NotificationResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Notification not found",
                        Data = null
                    };
                }

                _mapper.Map(request, notification);
                notification.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Notification>().UpdateGuid(notification, notification.Id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                var response = _mapper.Map<NotificationResponse>(notification);

                    return new BaseResponse<NotificationResponse>
                    {
                        Code = StatusCodes.Status200OK,
                        Message = "Notification updated successfully",
                        Data = response
                    };
            } catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new BaseResponse<NotificationResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An error occurred while update notification",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Delete notification (soft delete)
        /// </summary>
        public async Task<BaseResponse> DeleteNotificationAsync(Guid id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var notification = await _unitOfWork.Repository<Notification>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(n => n.Id == id);

                if (notification == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Notification not found"
                    };
                }

                notification.IsDeleted = true;
                notification.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Notification>().UpdateGuid(notification, notification.Id);
                await _unitOfWork.CommitAsync();

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Notification deleted successfully"
                };
            } catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An error occurred while delete notification",
                };
            }
        }

        public async Task<BaseResponse<NotificationResponse>> UpdateSatusRead(Guid id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var notification = await _unitOfWork.Repository<Notification>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(n => n.Id == id);

                if (notification == null)
                {
                    return new BaseResponse<NotificationResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Notification not found"
                    };
                }

                notification.Status = NotificationStatus.Read;
                notification.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Notification>().UpdateGuid(notification, notification.Id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                var response = _mapper.Map<NotificationResponse>(notification);
                return new BaseResponse<NotificationResponse>
                    {
                        Code = StatusCodes.Status200OK,
                        Message = "Notification updated successfully",
                        Data = response
                    };
            } catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new BaseResponse<NotificationResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An error occurred while update notification",
                    Data = null
                };
            }
        }

        #endregion
    }
}
