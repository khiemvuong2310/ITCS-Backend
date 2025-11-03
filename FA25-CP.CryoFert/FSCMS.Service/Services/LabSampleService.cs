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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSCMS.Service.Services
{
    public class LabSampleService : ILabSampleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<LabSampleService> _logger;

        public LabSampleService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<LabSampleService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region GET BY ID
        public async Task<BaseResponse<LabSampleResponse>> GetByIdAsync(Guid id)
        {
            const string methodName = nameof(GetByIdAsync);
            _logger.LogInformation("{MethodName} called with ID: {Id}", methodName, id);

            if (id == Guid.Empty)
            {
                return new BaseResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid lab sample ID.",
                    SystemCode = "INVALID_ID"
                };
            }

            try
            {
                var sample = await _unitOfWork.Repository<LabSample>()
                    .AsQueryable()
                    .Include(x => x.Patient)
                    .Include(x => x.LabSampleSperm)
                    .Include(x => x.LabSampleOocyte)
                    .Include(x => x.LabSampleEmbryo)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (sample == null)
                {
                    return new BaseResponse<LabSampleResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Lab sample not found.",
                        SystemCode = "NOT_FOUND"
                    };
                }

                var response = _mapper.Map<LabSampleDetailResponse>(sample);

                return new BaseResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Lab sample retrieved successfully.",
                    SystemCode = "SUCCESS",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving sample {Id}", methodName, id);
                return new BaseResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Internal server error.",
                    SystemCode = "INTERNAL_ERROR"
                };
            }
        }
        #endregion

        #region GET ALL
        public async Task<DynamicResponse<LabSampleResponse>> GetAllAsync(GetLabSamplesRequest request)
        {
            const string methodName = nameof(GetAllAsync);
            _logger.LogInformation("{MethodName} called", methodName);

            try
            {
                var query = _unitOfWork.Repository<LabSample>()
                    .AsQueryable()
                    .Include(x => x.Patient)
                    .Where(x => !x.IsDeleted);

                // --- Filtering ---
                if (request.SampleType.HasValue)
                    query = query.Where(x => x.SampleType == request.SampleType);

                if (request.Status.HasValue)
                    query = query.Where(x => x.Status == request.Status);

                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                    query = query.Where(x => x.SampleCode.Contains(request.SearchTerm)
                                          || (x.Notes != null && x.Notes.Contains(request.SearchTerm)));

                if (request.PatientId.HasValue)
                    query = query.Where(x => x.PatientId == request.PatientId.Value);

                var totalCount = await query.CountAsync();

                // Apply sorting
                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    var isDescending = request.Order?.ToLower() == "desc";

                    query = request.Sort.ToLower() switch
                    {
                        "collectiondate" => isDescending ? query.OrderByDescending(x => x.CollectionDate) : query.OrderBy(x => x.CollectionDate),
                        "samplecode" => isDescending ? query.OrderByDescending(x => x.SampleCode) : query.OrderBy(x => x.SampleCode),
                        _ => query.OrderByDescending(x => x.CreatedAt)
                    };
                }
                else
                {
                    query = query.OrderByDescending(u => u.CreatedAt);
                }

                // Apply pagination
                var items = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                var data = _mapper.Map<List<LabSampleResponse>>(items);

                return new DynamicResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Lab samples retrieved successfully.",
                    Data = data,
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = totalCount
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error getting all lab samples", methodName);
                return new DynamicResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Internal server error."
                };
            }
        }
        #endregion

        #region CREATE
        public async Task<BaseResponse<LabSampleResponse>> CreateAsync(CreateLabSampleRequest request)
        {
            const string methodName = nameof(CreateAsync);
            _logger.LogInformation("{MethodName} called", methodName);

            try
            {
                if (request == null)
                    return new BaseResponse<LabSampleResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid request."
                    };

                // Validate patient exists
                var patient = await _unitOfWork.Repository<Patient>().GetByIdGuid(request.PatientId);
                if (patient == null)
                    return new BaseResponse<LabSampleResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Patient not found."
                    };

                // Validate type-specific detail exists
                switch (request.SampleType)
                {
                    case SampleType.Sperm:
                        if (request.Sperm == null)
                            return new BaseResponse<LabSampleResponse>
                            {
                                Code = StatusCodes.Status400BadRequest,
                                Message = "Sperm sample details are required."
                            };
                        break;

                    case SampleType.Oocyte:
                        if (request.Oocyte == null)
                            return new BaseResponse<LabSampleResponse>
                            {
                                Code = StatusCodes.Status400BadRequest,
                                Message = "Oocyte sample details are required."
                            };
                        break;

                    case SampleType.Embryo:
                        if (request.Embryo == null)
                            return new BaseResponse<LabSampleResponse>
                            {
                                Code = StatusCodes.Status400BadRequest,
                                Message = "Embryo sample details are required."
                            };
                        break;

                    default:
                        return new BaseResponse<LabSampleResponse>
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Message = "Invalid sample type."
                        };
                }

                // Map main LabSample entity
                var entity = _mapper.Map<LabSample>(request);
                entity.SampleCode = GenerateSampleCode(request.SampleType);
                entity.PatientId = request.PatientId;

                // Map type-specific detail
                CreateSampleDetail(entity, request);

                // Insert and commit
                await _unitOfWork.Repository<LabSample>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                // Map response
                var response = _mapper.Map<LabSampleResponse>(entity);

                return new BaseResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status201Created,
                    Message = "Lab sample created successfully.",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating lab sample", methodName);
                return new BaseResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Internal server error."
                };
            }
        }

        // --- Private helpers ---

        private void CreateSampleDetail(LabSample entity, CreateLabSampleRequest request)
        {
            switch (request.SampleType)
            {
                case SampleType.Sperm:
                    if (request.Sperm != null)
                        entity.LabSampleSperm = _mapper.Map<LabSampleSperm>(request.Sperm);
                    break;

                case SampleType.Oocyte:
                    if (request.Oocyte != null)
                        entity.LabSampleOocyte = _mapper.Map<LabSampleOocyte>(request.Oocyte);
                    break;

                case SampleType.Embryo:
                    if (request.Embryo != null)
                        entity.LabSampleEmbryo = _mapper.Map<LabSampleEmbryo>(request.Embryo);
                    break;
            }
        }

        private string GenerateSampleCode(SampleType type)
        {
            string prefix = type switch
            {
                SampleType.Sperm => "SP",
                SampleType.Oocyte => "OC",
                SampleType.Embryo => "EM",
                _ => "SMP"
            };
            return $"{prefix}-{DateTime.UtcNow:yyyyMMddHHmmss}";
        }
        #endregion


        #region UPDATE
        public async Task<BaseResponse<LabSampleResponse>> UpdateAsync(Guid id, UpdateLabSampleRequest request)
        {
            const string methodName = nameof(UpdateAsync);
            _logger.LogInformation("{MethodName} called with ID: {Id}", methodName, id);

            if (id == Guid.Empty)
            {
                return new BaseResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid lab sample ID."
                };
            }

            try
            {
                var entity = await _unitOfWork.Repository<LabSample>()
                    .AsQueryable()
                    .Include(x => x.LabSampleSperm)
                    .Include(x => x.LabSampleOocyte)
                    .Include(x => x.LabSampleEmbryo)
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (entity == null)
                {
                    return new BaseResponse<LabSampleResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Lab sample not found."
                    };
                }

                _mapper.Map(request, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<LabSample>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();

                var response = _mapper.Map<LabSampleResponse>(entity);

                return new BaseResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Lab sample updated successfully.",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating sample {Id}", methodName, id);
                return new BaseResponse<LabSampleResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Internal server error."
                };
            }
        }
        #endregion

        #region DELETE
        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            const string methodName = nameof(DeleteAsync);
            _logger.LogInformation("{MethodName} called with ID: {Id}", methodName, id);

            if (id == Guid.Empty)
            {
                return new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid lab sample ID."
                };
            }

            try
            {
                var entity = await _unitOfWork.Repository<LabSample>().GetByIdGuid(id);
                if (entity == null || entity.IsDeleted)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Lab sample not found."
                    };
                }

                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<LabSample>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Lab sample deleted successfully."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error deleting lab sample {Id}", methodName, id);
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Internal server error."
                };
            }
        }
        #endregion
    }
}
