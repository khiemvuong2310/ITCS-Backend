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
    public class CryoImportService : ICryoImportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CryoImportService> _logger;

        public CryoImportService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CryoImportService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BaseResponse<CryoImportResponse>> GetByIdAsync(Guid id)
        {
            const string methodName = nameof(GetByIdAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

            try
            {
                if (id == Guid.Empty)
                    return new BaseResponse<CryoImportResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid CryoImport ID"
                    };

                var import = await _unitOfWork.Repository<CryoImport>()
                    .AsQueryable()
                    .Include(x => x.LabSample)
                    .Include(x => x.CryoLocation)
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .FirstOrDefaultAsync();

                if (import == null)
                    return new BaseResponse<CryoImportResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "CryoImport not found"
                    };

                return new BaseResponse<CryoImportResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "CryoImport retrieved successfully",
                    Data = _mapper.Map<CryoImportResponse>(import)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving CryoImport {Id}", methodName, id);
                return new BaseResponse<CryoImportResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<DynamicResponse<CryoImportResponse>> GetAllAsync(GetCryoImportsRequest request)
        {
            try
            {
                var query = _unitOfWork.Repository<CryoImport>()
                    .AsQueryable()
                    .Include(x => x.LabSample)
                    .Include(x => x.CryoLocation)
                    .Where(x => !x.IsDeleted);

                if (request.LabSampleId.HasValue)
                    query = query.Where(x => x.LabSampleId == request.LabSampleId.Value);

                if (request.CryoLocationId.HasValue)
                    query = query.Where(x => x.CryoLocationId == request.CryoLocationId.Value);

                var total = await query.CountAsync();

                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    var isDesc = request.Order?.ToLower() == "desc";
                    query = request.Sort.ToLower() switch
                    {
                        "importdate" => isDesc ? query.OrderByDescending(x => x.ImportDate) : query.OrderBy(x => x.ImportDate),
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

                return new DynamicResponse<CryoImportResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "CryoImports retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = total
                    },
                    Data = _mapper.Map<List<CryoImportResponse>>(data)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving CryoImports");
                return new DynamicResponse<CryoImportResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    Data = new List<CryoImportResponse>(),
                    MetaData = new PagingMetaData()
                };
            }
        }

        public async Task<BaseResponse<CryoImportResponse>> CreateAsync(CreateCryoImportRequest request)
        {
            try
            {
                var labSampleExists = await _unitOfWork.Repository<LabSample>()
                    .AsQueryable()
                    .AnyAsync(r => r.Id == request.LabSampleId && !r.IsDeleted);

                if (!labSampleExists) 
                { 
                    return new BaseResponse<CryoImportResponse> 
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
                    return new BaseResponse<CryoImportResponse> 
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
                    return new BaseResponse<CryoImportResponse> 
                    { 
                        Code = StatusCodes.Status400BadRequest, 
                        Message = "Invalid Witnessed By user ID", 
                        Data = null 
                    }; 
                }

                var entity = _mapper.Map<CryoImport>(request);

                await _unitOfWork.Repository<CryoImport>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                return new BaseResponse<CryoImportResponse>
                {
                    Code = StatusCodes.Status201Created,
                    Message = "CryoImport created successfully",
                    Data = _mapper.Map<CryoImportResponse>(entity)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating CryoImport");
                return new BaseResponse<CryoImportResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<CryoImportResponse>> UpdateAsync(Guid id, UpdateCryoImportRequest request)
        {
            try
            {
                var entity = await _unitOfWork.Repository<CryoImport>()
                    .AsQueryable()
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                    return new BaseResponse<CryoImportResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "CryoImport not found"
                    };

                _mapper.Map(request, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<CryoImport>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();

                return new BaseResponse<CryoImportResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "CryoImport updated successfully",
                    Data = _mapper.Map<CryoImportResponse>(entity)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating CryoImport {Id}", id);
                return new BaseResponse<CryoImportResponse>
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
                var entity = await _unitOfWork.Repository<CryoImport>()
                    .AsQueryable()
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "CryoImport not found"
                    };

                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<CryoImport>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "CryoImport deleted successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting CryoImport {Id}", id);
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}
