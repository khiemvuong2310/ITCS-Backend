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
    public class CryoPackageService : ICryoPackageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CryoPackageService> _logger;

        public CryoPackageService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CryoPackageService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Lấy thông tin gói Cryo theo Id
        /// </summary>
        public async Task<BaseResponse<CryoPackageResponse>> GetByIdAsync(Guid id)
        {
            const string method = nameof(GetByIdAsync);
            _logger.LogInformation("{Method} called with id: {Id}", method, id);

            try
            {
                if (id == Guid.Empty)
                    return new BaseResponse<CryoPackageResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid package ID",
                        Data = null
                    };

                var package = await _unitOfWork.Repository<CryoPackage>()
                    .AsQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (package == null)
                    return new BaseResponse<CryoPackageResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Cryo package not found",
                        Data = null
                    };

                var response = _mapper.Map<CryoPackageResponse>(package);
                return new BaseResponse<CryoPackageResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Package retrieved successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method}: Error retrieving package", method);
                return new BaseResponse<CryoPackageResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Internal server error",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả gói Cryo (có phân trang, lọc)
        /// </summary>
        public async Task<DynamicResponse<CryoPackageResponse>> GetAllAsync(GetCryoPackagesRequest request)
        {
            try
            {
                var query = _unitOfWork.Repository<CryoPackage>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(p => !p.IsDeleted);

                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                    query = query.Where(p => p.PackageName.Contains(request.SearchTerm));

                if (request.SampleType.HasValue)
                    query = query.Where(p => p.SampleType == request.SampleType.Value);

                if (request.IsActive.HasValue)
                    query = query.Where(p => p.IsActive == request.IsActive.Value);

                var total = await query.CountAsync();

                query = request.Sort?.ToLower() switch
                {
                    "price" => request.Order == "desc" ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                    "durationmonths" => request.Order == "desc" ? query.OrderByDescending(p => p.DurationMonths) : query.OrderBy(p => p.DurationMonths),
                    _ => query.OrderByDescending(p => p.CreatedAt)
                };

                var items = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                return new DynamicResponse<CryoPackageResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Packages retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = total
                    },
                    Data = _mapper.Map<List<CryoPackageResponse>>(items)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving packages");
                return new DynamicResponse<CryoPackageResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    Data = new List<CryoPackageResponse>(),
                    MetaData = new PagingMetaData()
                };
            }
        }

        /// <summary>
        /// Tạo mới gói Cryo
        /// </summary>
        public async Task<BaseResponse<CryoPackageResponse>> CreateAsync(CreateCryoPackageRequest request)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var exists = await _unitOfWork.Repository<CryoPackage>()
                    .AsQueryable()
                    .AnyAsync(p => p.PackageName == request.PackageName && !p.IsDeleted);

                if (exists)
                    return new BaseResponse<CryoPackageResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Package name already exists",
                        Data = null
                    };

                var entity = _mapper.Map<CryoPackage>(request);

                await _unitOfWork.Repository<CryoPackage>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();

                await transaction.CommitAsync();

                var result = _mapper.Map<CryoPackageResponse>(entity);

                return new BaseResponse<CryoPackageResponse>
                {
                    Code = StatusCodes.Status201Created,
                    Message = "Package created successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error creating Cryo package");
                return new BaseResponse<CryoPackageResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Error creating package: {ex.Message}",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Cập nhật gói Cryo
        /// </summary>
        public async Task<BaseResponse<CryoPackageResponse>> UpdateAsync(Guid id, UpdateCryoPackageRequest request)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var package = await _unitOfWork.Repository<CryoPackage>()
                    .AsQueryable()
                    .Where(p => p.Id == id && !p.IsDeleted)
                    .FirstOrDefaultAsync();

                if (package == null)
                    return new BaseResponse<CryoPackageResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Package not found",
                        Data = null
                    };

                _mapper.Map(request, package);
                package.UpdatedAt = DateTime.UtcNow.AddHours(7);

                await _unitOfWork.Repository<CryoPackage>().UpdateGuid(package, id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                return new BaseResponse<CryoPackageResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Package updated successfully",
                    Data = _mapper.Map<CryoPackageResponse>(package)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error updating Cryo package {Id}", id);
                return new BaseResponse<CryoPackageResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Error updating package: {ex.Message}",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Xóa mềm gói Cryo
        /// </summary>
        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var package = await _unitOfWork.Repository<CryoPackage>()
                    .AsQueryable()
                    .Where(p => p.Id == id && !p.IsDeleted)
                    .FirstOrDefaultAsync();

                if (package == null)
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Package not found"
                    };

                package.IsDeleted = true;
                package.UpdatedAt = DateTime.UtcNow.AddHours(7);

                await _unitOfWork.Repository<CryoPackage>().UpdateGuid(package, id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Package deleted successfully"
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error deleting Cryo package {Id}", id);
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Error deleting package: {ex.Message}"
                };
            }
        }
    }
}
