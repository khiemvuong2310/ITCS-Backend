using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FSCMS.Service.Services
{
    public class PrescriptionDetailService : IPrescriptionDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PrescriptionDetailService> _logger;

        public PrescriptionDetailService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PrescriptionDetailService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<DynamicResponse<PrescriptionDetail>> GetAllAsync(PagingModel request)
        {
            const string method = nameof(GetAllAsync);
            _logger.LogInformation("{Method} called with request: {@Request}", method, request);
            try
            {
                request ??= new PagingModel();
                request.Normalize();

                var query = _unitOfWork.Repository<PrescriptionDetail>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(d => d.Medicine)
                    .Include(d => d.Prescription)
                    .Where(d => !d.IsDeleted);

                var total = await query.CountAsync();

                var items = await query
                    .OrderByDescending(d => d.CreatedAt)
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                return new DynamicResponse<PrescriptionDetail>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Prescription details retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = total,
                        CurrentPageSize = items.Count
                    },
                    Data = items
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method}: Error retrieving prescription details", method);
                return new DynamicResponse<PrescriptionDetail>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "Error retrieving prescription details",
                    MetaData = new PagingMetaData(),
                    Data = Array.Empty<PrescriptionDetail>().ToList()
                };
            }
        }

        public async Task<BaseResponse<PrescriptionDetail>> GetByIdAsync(Guid prescriptionDetailId)
        {
            const string method = nameof(GetByIdAsync);
            _logger.LogInformation("{Method} called with id: {Id}", method, prescriptionDetailId);
            try
            {
                if (prescriptionDetailId == Guid.Empty)
                {
                    return BaseResponse<PrescriptionDetail>.CreateError("ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var item = await _unitOfWork.Repository<PrescriptionDetail>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(d => d.Medicine)
                    .Include(d => d.Prescription)
                    .FirstOrDefaultAsync(d => d.Id == prescriptionDetailId && !d.IsDeleted);

                if (item == null)
                {
                    return BaseResponse<PrescriptionDetail>.CreateError("Prescription detail not found", StatusCodes.Status404NotFound, "NOT_FOUND");
                }

                return BaseResponse<PrescriptionDetail>.CreateSuccess(item, "Prescription detail retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method}: Error retrieving prescription detail {Id}", method, prescriptionDetailId);
                return BaseResponse<PrescriptionDetail>.CreateError("Error retrieving prescription detail", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<PrescriptionDetail>> CreateAsync(PrescriptionDetail detail)
        {
            const string method = nameof(CreateAsync);
            _logger.LogInformation("{Method} called", method);
            try
            {
                if (detail == null)
                {
                    return BaseResponse<PrescriptionDetail>.CreateError("Payload cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                var entity = new PrescriptionDetail(
                    Guid.NewGuid(),
                    detail.PrescriptionId,
                    detail.MedicineId,
                    detail.Quantity
                )
                {
                    Dosage = detail.Dosage,
                    Frequency = detail.Frequency,
                    DurationDays = detail.DurationDays,
                    Instructions = detail.Instructions,
                    Notes = detail.Notes
                };
                await _unitOfWork.Repository<PrescriptionDetail>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                return BaseResponse<PrescriptionDetail>.CreateSuccess(entity, "Prescription detail created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method}: Error creating prescription detail", method);
                return BaseResponse<PrescriptionDetail>.CreateError("Error creating prescription detail", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<PrescriptionDetail>> UpdateAsync(Guid prescriptionDetailId, PrescriptionDetail update)
        {
            const string method = nameof(UpdateAsync);
            _logger.LogInformation("{Method} called with id: {Id}", method, prescriptionDetailId);
            try
            {
                var entity = await _unitOfWork.Repository<PrescriptionDetail>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(d => d.Id == prescriptionDetailId && !d.IsDeleted);
                if (entity == null)
                {
                    return BaseResponse<PrescriptionDetail>.CreateError("Prescription detail not found", StatusCodes.Status404NotFound, "NOT_FOUND");
                }

                if (update.Quantity != 0) entity.Quantity = update.Quantity;
                if (!string.IsNullOrWhiteSpace(update.Dosage)) entity.Dosage = update.Dosage;
                if (!string.IsNullOrWhiteSpace(update.Frequency)) entity.Frequency = update.Frequency;
                if (update.DurationDays != 0) entity.DurationDays = update.DurationDays;
                entity.Instructions = update.Instructions ?? entity.Instructions;
                entity.Notes = update.Notes ?? entity.Notes;
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<PrescriptionDetail>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();

                return BaseResponse<PrescriptionDetail>.CreateSuccess(entity, "Prescription detail updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method}: Error updating prescription detail {Id}", method, prescriptionDetailId);
                return BaseResponse<PrescriptionDetail>.CreateError("Error updating prescription detail", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse> DeleteAsync(Guid prescriptionDetailId)
        {
            const string method = nameof(DeleteAsync);
            _logger.LogInformation("{Method} called with id: {Id}", method, prescriptionDetailId);
            try
            {
                var entity = await _unitOfWork.Repository<PrescriptionDetail>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(d => d.Id == prescriptionDetailId && !d.IsDeleted);
                if (entity == null)
                {
                    return BaseResponse.CreateError("Prescription detail not found", StatusCodes.Status404NotFound, "NOT_FOUND");
                }

                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<PrescriptionDetail>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();

                return BaseResponse.CreateSuccess("Prescription detail deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method}: Error deleting prescription detail {Id}", method, prescriptionDetailId);
                return BaseResponse.CreateError("Error deleting prescription detail", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }
    }
}


