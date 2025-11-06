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
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<MedicalRecordService> _logger;

        public MedicalRecordService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<MedicalRecordService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<DynamicResponse<MedicalRecord>> GetAllAsync(PagingModel request)
        {
            const string method = nameof(GetAllAsync);
            _logger.LogInformation("{Method} called with request: {@Request}", method, request);

            try
            {
                request ??= new PagingModel();
                request.Normalize();

                var query = _unitOfWork.Repository<MedicalRecord>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(mr => mr.Prescriptions.Where(p => !p.IsDeleted))
                    .Where(mr => !mr.IsDeleted);

                var total = await query.CountAsync();

                var items = await query
                    .OrderByDescending(mr => mr.CreatedAt)
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                return new DynamicResponse<MedicalRecord>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Medical records retrieved successfully",
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
                _logger.LogError(ex, "{Method}: Error retrieving medical records", method);
                return new DynamicResponse<MedicalRecord>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "Error retrieving medical records",
                    MetaData = new PagingMetaData(),
                    Data = Array.Empty<MedicalRecord>().ToList()
                };
            }
        }

        public async Task<BaseResponse<MedicalRecord>> GetByIdAsync(Guid medicalRecordId)
        {
            const string method = nameof(GetByIdAsync);
            _logger.LogInformation("{Method} called with id: {Id}", method, medicalRecordId);
            try
            {
                if (medicalRecordId == Guid.Empty)
                {
                    return BaseResponse<MedicalRecord>.CreateError("MedicalRecord ID cannot be empty", StatusCodes.Status400BadRequest, "INVALID_ID");
                }

                var item = await _unitOfWork.Repository<MedicalRecord>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(mr => mr.Prescriptions.Where(p => !p.IsDeleted))
                    .FirstOrDefaultAsync(mr => mr.Id == medicalRecordId && !mr.IsDeleted);

                if (item == null)
                {
                    return BaseResponse<MedicalRecord>.CreateError("MedicalRecord not found", StatusCodes.Status404NotFound, "NOT_FOUND");
                }

                return BaseResponse<MedicalRecord>.CreateSuccess(item, "MedicalRecord retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method}: Error retrieving medical record {Id}", method, medicalRecordId);
                return BaseResponse<MedicalRecord>.CreateError("Error retrieving medical record", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<MedicalRecord>> CreateAsync(MedicalRecord medicalRecord)
        {
            const string method = nameof(CreateAsync);
            _logger.LogInformation("{Method} called", method);
            try
            {
                if (medicalRecord == null)
                {
                    return BaseResponse<MedicalRecord>.CreateError("Payload cannot be null", StatusCodes.Status400BadRequest, "INVALID_REQUEST");
                }

                var entity = new MedicalRecord(
                    Guid.NewGuid(),
                    medicalRecord.AppointmentId,
                    medicalRecord.Diagnosis,
                    medicalRecord.TreatmentPlan
                )
                {
                    ChiefComplaint = medicalRecord.ChiefComplaint,
                    History = medicalRecord.History,
                    PhysicalExamination = medicalRecord.PhysicalExamination,
                    FollowUpInstructions = medicalRecord.FollowUpInstructions,
                    VitalSigns = medicalRecord.VitalSigns,
                    LabResults = medicalRecord.LabResults,
                    ImagingResults = medicalRecord.ImagingResults,
                    Notes = medicalRecord.Notes
                };
                await _unitOfWork.Repository<MedicalRecord>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                return BaseResponse<MedicalRecord>.CreateSuccess(entity, "MedicalRecord created successfully", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method}: Error creating medical record", method);
                return BaseResponse<MedicalRecord>.CreateError("Error creating medical record", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse<MedicalRecord>> UpdateAsync(Guid medicalRecordId, MedicalRecord update)
        {
            const string method = nameof(UpdateAsync);
            _logger.LogInformation("{Method} called with id: {Id}", method, medicalRecordId);
            try
            {
                var entity = await _unitOfWork.Repository<MedicalRecord>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(mr => mr.Id == medicalRecordId && !mr.IsDeleted);
                if (entity == null)
                {
                    return BaseResponse<MedicalRecord>.CreateError("MedicalRecord not found", StatusCodes.Status404NotFound, "NOT_FOUND");
                }

                entity.ChiefComplaint = update.ChiefComplaint ?? entity.ChiefComplaint;
                entity.History = update.History ?? entity.History;
                entity.PhysicalExamination = update.PhysicalExamination ?? entity.PhysicalExamination;
                entity.Diagnosis = update.Diagnosis ?? entity.Diagnosis;
                entity.TreatmentPlan = update.TreatmentPlan ?? entity.TreatmentPlan;
                entity.FollowUpInstructions = update.FollowUpInstructions ?? entity.FollowUpInstructions;
                entity.VitalSigns = update.VitalSigns ?? entity.VitalSigns;
                entity.LabResults = update.LabResults ?? entity.LabResults;
                entity.ImagingResults = update.ImagingResults ?? entity.ImagingResults;
                entity.Notes = update.Notes ?? entity.Notes;
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<MedicalRecord>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();

                return BaseResponse<MedicalRecord>.CreateSuccess(entity, "MedicalRecord updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method}: Error updating medical record {Id}", method, medicalRecordId);
                return BaseResponse<MedicalRecord>.CreateError("Error updating medical record", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }

        public async Task<BaseResponse> DeleteAsync(Guid medicalRecordId)
        {
            const string method = nameof(DeleteAsync);
            _logger.LogInformation("{Method} called with id: {Id}", method, medicalRecordId);
            try
            {
                var entity = await _unitOfWork.Repository<MedicalRecord>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(mr => mr.Id == medicalRecordId && !mr.IsDeleted);
                if (entity == null)
                {
                    return BaseResponse.CreateError("MedicalRecord not found", StatusCodes.Status404NotFound, "NOT_FOUND");
                }

                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Repository<MedicalRecord>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();

                return BaseResponse.CreateSuccess("MedicalRecord deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method}: Error deleting medical record {Id}", method, medicalRecordId);
                return BaseResponse.CreateError("Error deleting medical record", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR");
            }
        }
    }
}


