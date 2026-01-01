using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FSCMS.Service.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<MedicalRecordService> _logger;
        private readonly IPrescriptionService _prescriptionService;
        private readonly IMediaService _mediaService;

        public MedicalRecordService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<MedicalRecordService> logger, IPrescriptionService prescriptionService, IMediaService mediaService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _prescriptionService = prescriptionService ?? throw new ArgumentNullException(nameof(prescriptionService));
            _mediaService = mediaService;
        }

        // =====================================================================
        // GET BY ID
        // =====================================================================
        public async Task<BaseResponse<MedicalRecordDetailResponse>> GetByIdAsync(Guid id)
        {
            const string methodName = nameof(GetByIdAsync);
            _logger.LogInformation("{MethodName} called with Id: {Id}", methodName, id);

            try
            {
                if (id == Guid.Empty)
                    return new BaseResponse<MedicalRecordDetailResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid MedicalRecord ID"
                    };

                var entity = await _unitOfWork.Repository<MedicalRecord>()
                    .AsQueryable()
                    .Include(x => x.Appointment)
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                    return new BaseResponse<MedicalRecordDetailResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "MedicalRecord not found"
                    };
                var record = _mapper.Map<MedicalRecordDetailResponse>(entity);
                GetPrescriptionsRequest rqp = new GetPrescriptionsRequest
                {
                    MedicalRecordId = id,
                };
                var prescriptionResponse = await _prescriptionService.GetAllDetailAsync(rqp);
                if (prescriptionResponse != null && prescriptionResponse.Data != null)
                {
                    record.Prescriptions = prescriptionResponse.Data;
                }
                GetMediasRequest rqm = new GetMediasRequest
                {
                    RelatedEntityType = EntityTypeMedia.MedicalRecord,
                    RelatedEntityId = record.Id,
                };
                var mediaResponse = await _mediaService.GetAllMediasAsync(rqm);
                if (mediaResponse != null && mediaResponse.Data != null)
                {
                    record.medias = mediaResponse.Data;
                }
                return new BaseResponse<MedicalRecordDetailResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "MedicalRecord retrieved successfully",
                    Data = record
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} error: {Id}", methodName, id);
                return new BaseResponse<MedicalRecordDetailResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        // =====================================================================
        // SEARCH
        // =====================================================================
        public async Task<DynamicResponse<MedicalRecordResponse>> GetAllAsync(SearchMedicalRecordRequest request)
        {
            const string methodName = nameof(GetAllAsync);
            _logger.LogInformation("{MethodName} called", methodName);

            try
            {
                var query = _unitOfWork.Repository<MedicalRecord>()
                    .AsQueryable()
                    .Include(x => x.Appointment)
                    .Where(x => !x.IsDeleted);

                // Filtering
                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    query = query.Where(x =>
                        x.Notes.Contains(request.SearchTerm) ||
                        x.LabResults.Contains(request.SearchTerm));
                }

                if (request.PatientId.HasValue)
                    query = query.Where(x => x.Appointment.PatientId == request.PatientId.Value);

                if (request.AppointmentId.HasValue)
                    query = query.Where(x => x.AppointmentId == request.AppointmentId.Value);

                if (request.FromDate.HasValue)
                    query = query.Where(x => x.CreatedAt >= request.FromDate);

                if (request.ToDate.HasValue)
                    query = query.Where(x => x.CreatedAt <= request.ToDate);

                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    var isDesc = request.Order?.ToLower() == "desc";
                    query = request.Sort.ToLower() switch
                    {
                        "history" => isDesc ? query.OrderByDescending(x => x.History) : query.OrderBy(x => x.History),
                        _ => isDesc ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt)
                    };
                }
                else
                {
                    query = query.OrderByDescending(x => x.CreatedAt);
                }

                // Paging
                var total = await query.CountAsync();

                var result = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();
                var data = _mapper.Map<List<MedicalRecordResponse>>(result);
                foreach (var record in data)
                {
                    GetPrescriptionsRequest rqp = new GetPrescriptionsRequest
                    {
                        MedicalRecordId = record.Id,
                    };
                    var prescriptionResponse = await _prescriptionService.GetAllDetailAsync(rqp);
                    if (prescriptionResponse != null && prescriptionResponse.Data != null)
                    {
                        record.Prescriptions = prescriptionResponse.Data;
                    }
                    GetMediasRequest rqm = new GetMediasRequest
                    {
                        RelatedEntityType = EntityTypeMedia.MedicalRecord,
                        RelatedEntityId = record.Id,
                    };
                    var mediaResponse = await _mediaService.GetAllMediasAsync(rqm);
                    if (mediaResponse != null && mediaResponse.Data != null)
                    {
                        record.medias = mediaResponse.Data;
                    }
                }

                return new DynamicResponse<MedicalRecordResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "MedicalRecords retrieved successfully",
                    Data = data,
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = total
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} failed", methodName);
                return new DynamicResponse<MedicalRecordResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    Data = new List<MedicalRecordResponse>(),
                    MetaData = new PagingMetaData()
                };
            }
        }

        // =====================================================================
        // CREATE
        // =====================================================================
        public async Task<BaseResponse<MedicalRecordResponse>> CreateAsync(CreateMedicalRecordRequest request)
        {
            const string methodName = nameof(CreateAsync);
            _logger.LogInformation("{MethodName} called", methodName);
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var appointment = await _unitOfWork.Repository<Appointment>()
                                .AsQueryable()
                                .Include(p => p.MedicalRecords)
                                .FirstOrDefaultAsync(p => p.Id == request.AppointmentId && !p.IsDeleted);
                if (appointment == null)
                {
                    return new BaseResponse<MedicalRecordResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Appointment Not Found",
                        Data = null
                    };
                }
                
                var entity = _mapper.Map<MedicalRecord>(request);

                await _unitOfWork.Repository<MedicalRecord>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();
                return new BaseResponse<MedicalRecordResponse>
                {
                    Code = StatusCodes.Status201Created,
                    Message = "MedicalRecord created successfully",
                    Data = _mapper.Map<MedicalRecordResponse>(entity)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "{MethodName} failed", methodName);
                return new BaseResponse<MedicalRecordResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        // =====================================================================
        // UPDATE
        // =====================================================================
        public async Task<BaseResponse<MedicalRecordResponse>> UpdateAsync(Guid id, UpdateMedicalRecordRequest request)
        {
            const string methodName = nameof(UpdateAsync);
            _logger.LogInformation("{MethodName} called with Id: {Id}", methodName, id);
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var entity = await _unitOfWork.Repository<MedicalRecord>()
                    .AsQueryable()
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    return new BaseResponse<MedicalRecordResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "MedicalRecord not found"
                    };
                }

                _mapper.Map(request, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<MedicalRecord>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();
                return new BaseResponse<MedicalRecordResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "MedicalRecord updated successfully",
                    Data = _mapper.Map<MedicalRecordResponse>(entity)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "{MethodName} failed", methodName);
                return new BaseResponse<MedicalRecordResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        // =====================================================================
        // DELETE (SOFT)
        // =====================================================================
        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            const string methodName = nameof(DeleteAsync);
            _logger.LogInformation("{MethodName} called with Id: {Id}", methodName, id);
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var entity = await _unitOfWork.Repository<MedicalRecord>()
                    .AsQueryable()
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "MedicalRecord not found"
                    };

                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<MedicalRecord>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();
                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "MedicalRecord deleted successfully"
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "{MethodName} failed", methodName);
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}
