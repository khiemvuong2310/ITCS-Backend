using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSCMS.Service.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PrescriptionService> _logger;

        public PrescriptionService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PrescriptionService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Get All
        public async Task<DynamicResponse<PrescriptionResponse>> GetAllAsync(GetPrescriptionsRequest request)
        {
            try
            {
                var query = _unitOfWork.Repository<Prescription>()
                    .AsQueryable()
                    .Include(x => x.MedicalRecord)
                    .Where(x => !x.IsDeleted);

                // Filtering
                if (request.MedicalRecordId.HasValue)
                    query = query.Where(x => x.MedicalRecordId == request.MedicalRecordId);

                if (request.FromDate.HasValue)
                    query = query.Where(x => x.PrescriptionDate >= request.FromDate);

                if (request.ToDate.HasValue)
                    query = query.Where(x => x.PrescriptionDate <= request.ToDate);

                // Count total
                var total = await query.CountAsync();

                // Sorting
                query = request.Sort?.ToLower() switch
                {
                    "prescriptiondate" => (request.Order?.ToLower() == "desc")
                        ? query.OrderByDescending(x => x.PrescriptionDate)
                        : query.OrderBy(x => x.PrescriptionDate),
                    _ => query.OrderByDescending(x => x.CreatedAt)
                };

                // Pagination
                var items = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                var data = _mapper.Map<List<PrescriptionResponse>>(items);

                return new DynamicResponse<PrescriptionResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Prescriptions retrieved successfully",
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
                _logger.LogError(ex, "Error retrieving prescriptions");
                return new DynamicResponse<PrescriptionResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    Data = new List<PrescriptionResponse>(),
                    MetaData = new PagingMetaData()
                };
            }
        }
        #endregion

        #region Get By Id
        public async Task<BaseResponse<PrescriptionDetailResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Repository<Prescription>()
                    .AsQueryable()
                    .Include(x => x.MedicalRecord)
                    .Include(x => x.PrescriptionDetails)
                        .ThenInclude(d => d.Medicine)
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (entity == null)
                {
                    return new BaseResponse<PrescriptionDetailResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Prescription not found"
                    };
                }

                var data = _mapper.Map<PrescriptionDetailResponse>(entity);

                return new BaseResponse<PrescriptionDetailResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Prescription retrieved successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting prescription by ID: {Id}", id);
                return new BaseResponse<PrescriptionDetailResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
        #endregion

        #region Create
        public async Task<BaseResponse<PrescriptionResponse>> CreateAsync(CreatePrescriptionRequest request)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Validate MedicalRecord
                var medicalRecord = await _unitOfWork.Repository<MedicalRecord>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(x => x.Id == request.MedicalRecordId && !x.IsDeleted);

                if (medicalRecord == null)
                {
                    return new BaseResponse<PrescriptionResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid MedicalRecordId"
                    };
                }

                // Map entity
                var entity = _mapper.Map<Prescription>(request);

                // Map PrescriptionDetails if provided
                if (request.PrescriptionDetails != null && request.PrescriptionDetails.Any())
                {
                    entity.PrescriptionDetails = new List<PrescriptionDetail>();
                    foreach (var item in request.PrescriptionDetails)
                    {
                        var medicine = await _unitOfWork.Repository<Medicine>()
                            .AsQueryable()
                            .FirstOrDefaultAsync(x => x.Id == item.MedicineId && !x.IsDeleted && x.IsActive);

                        if (medicine == null)
                        {
                            return new BaseResponse<PrescriptionResponse>
                            {
                                Code = StatusCodes.Status400BadRequest,
                                Message = $"Invalid MedicineId: {item.MedicineId}"
                            };
                        }

                        var detail = _mapper.Map<PrescriptionDetail>(item);
                        detail.Medicine = medicine;
                        entity.PrescriptionDetails.Add(detail);
                    }
                }

                await _unitOfWork.Repository<Prescription>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();
                var data = _mapper.Map<PrescriptionResponse>(entity);

                return new BaseResponse<PrescriptionResponse>
                {
                    Code = StatusCodes.Status201Created,
                    Message = "Prescription created successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error creating prescription");
                return new BaseResponse<PrescriptionResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
        #endregion

        #region Update
        public async Task<BaseResponse<PrescriptionResponse>> UpdateAsync(Guid id, UpdatePrescriptionRequest request)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var entity = await _unitOfWork.Repository<Prescription>()
                    .AsQueryable()
                    .Include(x => x.PrescriptionDetails)
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (entity == null)
                {
                    return new BaseResponse<PrescriptionResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Prescription not found"
                    };
                }

                _mapper.Map(request, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                // Optional: handle update PrescriptionDetails (add/update/remove) here if needed

                await _unitOfWork.Repository<Prescription>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                var data = _mapper.Map<PrescriptionResponse>(entity);

                return new BaseResponse<PrescriptionResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Prescription updated successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error updating prescription {Id}", id);
                return new BaseResponse<PrescriptionResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
        #endregion

        #region Delete (Soft Delete)
        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var entity = await _unitOfWork.Repository<Prescription>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (entity == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Prescription not found"
                    };
                }

                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Prescription>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();
                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Prescription deleted successfully"
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error deleting prescription {Id}", id);
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
        #endregion
    }
}
