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
    public class MedicineService : IMedicineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<MedicineService> _logger;

        public MedicineService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<MedicineService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<DynamicResponse<Medicine>> GetAllAsync(PagingModel request)
        {
            const string method = nameof(GetAllAsync);
            _logger.LogInformation("{Method} called with request: {@Request}", method, request);

            try
            {
                request ??= new PagingModel();
                request.Normalize();

                var query = _unitOfWork.Repository<Medicine>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(m => !m.IsDeleted);

                var total = await query.CountAsync();

                var items = await query
                    .OrderByDescending(m => m.CreatedAt)
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                return new DynamicResponse<Medicine>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Medicines retrieved successfully",
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
                _logger.LogError(ex, "{Method}: Error retrieving medicines", method);
                return new DynamicResponse<Medicine>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "Error retrieving medicines",
                    MetaData = new PagingMetaData(),
                    Data = Array.Empty<Medicine>().ToList()
                };
            }
        }

        public async Task<BaseResponse<Medicine>> GetByIdAsync(Guid medicineId)
        {
            const string method = nameof(GetByIdAsync);
            _logger.LogInformation("{Method} called with id: {Id}", method, medicineId);
            try
            {
                if (medicineId == Guid.Empty)
                {
                    return BaseResponse<Medicine>.CreateError("Medicine ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var item = await _unitOfWork.Repository<Medicine>()
                    .AsQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Id == medicineId && !m.IsDeleted);

                if (item == null)
                {
                    return BaseResponse<Medicine>.CreateError("Medicine not found", StatusCodes.Status404NotFound, "NOT_FOUND");
                }

                return BaseResponse<Medicine>.CreateSuccess(item, "Medicine retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method}: Error retrieving medicine {Id}", method, medicineId);
                return BaseResponse<Medicine>.CreateError("Error retrieving medicine", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<Medicine>> CreateAsync(Medicine medicine)
        {
            const string method = nameof(CreateAsync);
            _logger.LogInformation("{Method} called", method);
            try
            {
                if (medicine == null)
                {
                    return BaseResponse<Medicine>.CreateError("Payload cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                var entity = new Medicine(Guid.NewGuid(), medicine.Name, medicine.Dosage, medicine.Form)
                {
                    GenericName = medicine.GenericName,
                    Indication = medicine.Indication,
                    Contraindication = medicine.Contraindication,
                    SideEffects = medicine.SideEffects,
                    IsActive = medicine.IsActive,
                    Notes = medicine.Notes
                };
                await _unitOfWork.Repository<Medicine>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                return BaseResponse<Medicine>.CreateSuccess(entity, "Medicine created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method}: Error creating medicine", method);
                return BaseResponse<Medicine>.CreateError("Error creating medicine", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<Medicine>> UpdateAsync(Guid medicineId, Medicine update)
        {
            const string method = nameof(UpdateAsync);
            _logger.LogInformation("{Method} called with id: {Id}", method, medicineId);
            try
            {
                var entity = await _unitOfWork.Repository<Medicine>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(m => m.Id == medicineId && !m.IsDeleted);
                if (entity == null)
                {
                    return BaseResponse<Medicine>.CreateError("Medicine not found", StatusCodes.Status404NotFound, "NOT_FOUND");
                }

                entity.Name = update.Name ?? entity.Name;
                entity.GenericName = update.GenericName ?? entity.GenericName;
                entity.Dosage = update.Dosage ?? entity.Dosage;
                entity.Form = update.Form ?? entity.Form;
                entity.Indication = update.Indication ?? entity.Indication;
                entity.Contraindication = update.Contraindication ?? entity.Contraindication;
                entity.SideEffects = update.SideEffects ?? entity.SideEffects;
                if (update.IsActive != entity.IsActive) entity.IsActive = update.IsActive;
                entity.Notes = update.Notes ?? entity.Notes;
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Medicine>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();

                return BaseResponse<Medicine>.CreateSuccess(entity, "Medicine updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method}: Error updating medicine {Id}", method, medicineId);
                return BaseResponse<Medicine>.CreateError("Error updating medicine", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse> DeleteAsync(Guid medicineId)
        {
            const string method = nameof(DeleteAsync);
            _logger.LogInformation("{Method} called with id: {Id}", method, medicineId);
            try
            {
                var entity = await _unitOfWork.Repository<Medicine>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(m => m.Id == medicineId && !m.IsDeleted);
                if (entity == null)
                {
                    return BaseResponse.CreateError("Medicine not found", StatusCodes.Status404NotFound, "NOT_FOUND");
                }

                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<Medicine>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();

                return BaseResponse.CreateSuccess("Medicine deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method}: Error deleting medicine {Id}", method, medicineId);
                return BaseResponse.CreateError("Error deleting medicine", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }
    }
}


