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

        public async Task<DynamicResponse<Prescription>> GetAllAsync(PagingModel request)
        {
            const string method = nameof(GetAllAsync);
            _logger.LogInformation("{Method} called with request: {@Request}", method, request);

            try
            {
                request ??= new PagingModel();
                request.Normalize();

                var query = _unitOfWork.Repository<Prescription>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(p => p.PrescriptionDetails.Where(d => !d.IsDeleted))
                    .Where(p => !p.IsDeleted);

                var total = await query.CountAsync();

                var items = await query
                    .OrderByDescending(p => p.PrescriptionDate)
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                return new DynamicResponse<Prescription>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Prescriptions retrieved successfully",
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
                _logger.LogError(ex, "{Method}: Error retrieving prescriptions", method);
                return new DynamicResponse<Prescription>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "Error retrieving prescriptions",
                    MetaData = new PagingMetaData(),
                    Data = Array.Empty<Prescription>().ToList()
                };
            }
        }

        public async Task<BaseResponse<Prescription>> GetByIdAsync(Guid prescriptionId)
        {
            const string method = nameof(GetByIdAsync);
            _logger.LogInformation("{Method} called with id: {Id}", method, prescriptionId);
            try
            {
                if (prescriptionId == Guid.Empty)
                {
                    return BaseResponse<Prescription>.CreateError("Prescription ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var item = await _unitOfWork.Repository<Prescription>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(p => p.PrescriptionDetails.Where(d => !d.IsDeleted))
                    .ThenInclude(d => d.Medicine)
                    .FirstOrDefaultAsync(p => p.Id == prescriptionId && !p.IsDeleted);

                if (item == null)
                {
                    return BaseResponse<Prescription>.CreateError("Prescription not found", StatusCodes.Status404NotFound, "NOT_FOUND");
                }

                return BaseResponse<Prescription>.CreateSuccess(item, "Prescription retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method}: Error retrieving prescription {Id}", method, prescriptionId);
                return BaseResponse<Prescription>.CreateError("Error retrieving prescription", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<Prescription>> CreateAsync(Prescription prescription)
        {
            const string method = nameof(CreateAsync);
            _logger.LogInformation("{Method} called", method);
            try
            {
                if (prescription == null)
                {
                    return BaseResponse<Prescription>.CreateError("Payload cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                var entity = new Prescription(
                    Guid.NewGuid(),
                    prescription.MedicalRecordId,
                    prescription.PrescriptionDate == default ? DateTime.UtcNow : prescription.PrescriptionDate
                )
                {
                    Diagnosis = prescription.Diagnosis,
                    Instructions = prescription.Instructions,
                    Notes = prescription.Notes,
                    IsFilled = prescription.IsFilled,
                    FilledDate = prescription.FilledDate
                };
                await _unitOfWork.Repository<Prescription>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                return BaseResponse<Prescription>.CreateSuccess(entity, "Prescription created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method}: Error creating prescription", method);
                return BaseResponse<Prescription>.CreateError("Error creating prescription", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<Prescription>> UpdateAsync(Guid prescriptionId, Prescription update)
        {
            const string method = nameof(UpdateAsync);
            _logger.LogInformation("{Method} called with id: {Id}", method, prescriptionId);
            try
            {
                var entity = await _unitOfWork.Repository<Prescription>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(p => p.Id == prescriptionId && !p.IsDeleted);
                if (entity == null)
                {
                    return BaseResponse<Prescription>.CreateError("Prescription not found", StatusCodes.Status404NotFound, "NOT_FOUND");
                }

                entity.PrescriptionDate = update.PrescriptionDate != default ? update.PrescriptionDate : entity.PrescriptionDate;
                entity.Diagnosis = update.Diagnosis ?? entity.Diagnosis;
                entity.Instructions = update.Instructions ?? entity.Instructions;
                entity.Notes = update.Notes ?? entity.Notes;
                entity.IsFilled = update.IsFilled;
                entity.FilledDate = update.FilledDate ?? entity.FilledDate;
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Prescription>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();

                return BaseResponse<Prescription>.CreateSuccess(entity, "Prescription updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method}: Error updating prescription {Id}", method, prescriptionId);
                return BaseResponse<Prescription>.CreateError("Error updating prescription", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse> DeleteAsync(Guid prescriptionId)
        {
            const string method = nameof(DeleteAsync);
            _logger.LogInformation("{Method} called with id: {Id}", method, prescriptionId);
            try
            {
                var entity = await _unitOfWork.Repository<Prescription>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(p => p.Id == prescriptionId && !p.IsDeleted);
                if (entity == null)
                {
                    return BaseResponse.CreateError("Prescription not found", StatusCodes.Status404NotFound, "NOT_FOUND");
                }

                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<Prescription>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();

                return BaseResponse.CreateSuccess("Prescription deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method}: Error deleting prescription {Id}", method, prescriptionId);
                return BaseResponse.CreateError("Error deleting prescription", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }
    }
}


