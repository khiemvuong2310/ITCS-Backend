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
    public class CryoExportService : ICryoExportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CryoExportService> _logger;

        public CryoExportService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CryoExportService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BaseResponse<CryoExportResponse>> GetByIdAsync(Guid id)
        {
            const string methodName = nameof(GetByIdAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

            try
            {
                if (id == Guid.Empty)
                    return new BaseResponse<CryoExportResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid CryoExport ID"
                    };

                var export = await _unitOfWork.Repository<CryoExport>()
                    .AsQueryable()
                    .Include(x => x.LabSample)
                    .Include(x => x.CryoLocation)
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .FirstOrDefaultAsync();

                if (export == null)
                    return new BaseResponse<CryoExportResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "CryoExport not found"
                    };

                return new BaseResponse<CryoExportResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "CryoExport retrieved successfully",
                    Data = _mapper.Map<CryoExportResponse>(export)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving CryoExport {Id}", methodName, id);
                return new BaseResponse<CryoExportResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<DynamicResponse<CryoExportResponse>> GetAllAsync(GetCryoExportsRequest request)
        {
            try
            {
                var query = _unitOfWork.Repository<CryoExport>()
                    .AsQueryable()
                    .Include(x => x.LabSample)
                    .Include(x => x.CryoLocation)
                    .Where(x => !x.IsDeleted);

                if (request.LabSampleId.HasValue)
                    query = query.Where(x => x.LabSampleId == request.LabSampleId.Value);

                if (request.CryoLocationId.HasValue)
                    query = query.Where(x => x.CryoLocationId == request.CryoLocationId.Value);

                if (request.IsThawed.HasValue)
                    query = query.Where(x => x.IsThawed == request.IsThawed.Value);

                var total = await query.CountAsync();

                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    var isDesc = request.Order?.ToLower() == "desc";
                    query = request.Sort.ToLower() switch
                    {
                        "exportdate" => isDesc ? query.OrderByDescending(x => x.ExportDate) : query.OrderBy(x => x.ExportDate),
                        _ => isDesc ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt)
                    };
                }
                else
                {
                    query = query.OrderByDescending(x => x.CreatedAt);
                }

                var data = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                return new DynamicResponse<CryoExportResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "CryoExports retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = total
                    },
                    Data = _mapper.Map<List<CryoExportResponse>>(data)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving CryoExports");
                return new DynamicResponse<CryoExportResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    Data = new List<CryoExportResponse>(),
                    MetaData = new PagingMetaData()
                };
            }
        }

        public async Task<BaseResponse<CryoExportResponse>> CreateAsync(CreateCryoExportRequest request)
        {
            try
            {
                var labSampleExists = await _unitOfWork.Repository<LabSample>()
                    .AsQueryable()
                    .AnyAsync(r => r.Id == request.LabSampleId && !r.IsDeleted);

                if (!labSampleExists)
                {
                    return new BaseResponse<CryoExportResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid labSample ID",
                        Data = null
                    };
                }

                var locationExists = await _unitOfWork.Repository<CryoLocation>()
                    .AsQueryable()
                    .AnyAsync(r => r.Id == request.CryoLocationId && !r.IsDeleted);

                if (!locationExists)
                {
                    return new BaseResponse<CryoExportResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid Cryo Location ID",
                        Data = null
                    };
                }

                var userExists = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .AnyAsync(r => r.Id == request.WitnessedBy && !r.IsDeleted);

                if (!userExists)
                {
                    return new BaseResponse<CryoExportResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid Witnessed By user ID",
                        Data = null
                    };
                }
                var entity = _mapper.Map<CryoExport>(request);

                await _unitOfWork.Repository<CryoExport>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                return new BaseResponse<CryoExportResponse>
                {
                    Code = StatusCodes.Status201Created,
                    Message = "CryoExport created successfully",
                    Data = _mapper.Map<CryoExportResponse>(entity)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating CryoExport");
                return new BaseResponse<CryoExportResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<CryoExportResponse>> UpdateAsync(Guid id, UpdateCryoExportRequest request)
        {
            try
            {
                var entity = await _unitOfWork.Repository<CryoExport>()
                    .AsQueryable()
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                    return new BaseResponse<CryoExportResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "CryoExport not found"
                    };

                _mapper.Map(request, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<CryoExport>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();

                return new BaseResponse<CryoExportResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "CryoExport updated successfully",
                    Data = _mapper.Map<CryoExportResponse>(entity)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating CryoExport {Id}", id);
                return new BaseResponse<CryoExportResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Repository<CryoExport>()
                    .AsQueryable()
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "CryoExport not found"
                    };

                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<CryoExport>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "CryoExport deleted successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting CryoExport {Id}", id);
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}
