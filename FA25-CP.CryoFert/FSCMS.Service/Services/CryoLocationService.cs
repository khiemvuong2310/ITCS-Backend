using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
using FSCMS.Core.Models;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSCMS.Service.Services
{
    public class CryoLocationService : ICryoLocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CryoLocationService> _logger;
        private readonly IConfiguration _configuration;

        public CryoLocationService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CryoLocationService> logger,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Create default CryoBank structure based on appsettings.json configuration
        /// </summary>
        public async Task<DynamicResponse<CryoLocationSummaryResponse>> CreateDefaultBankAsync()
        {
            const string methodName = nameof(CreateDefaultBankAsync);
            _logger.LogInformation("{MethodName} called", methodName);

            try
            {
                // Check if already initialized
                var exists = await _unitOfWork.Repository<CryoLocation>()
                    .AsQueryable()
                    .AnyAsync(x => x.Type == CryoLocationType.Tank && !x.IsDeleted);

                if (exists)
                {
                    return new DynamicResponse<CryoLocationSummaryResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "BANK_ALREADY_EXISTS",
                        Message = "CryoBank already initialized",
                        Data = null
                    };
                }

                // Load CryoBankConfig from appsettings.json
                var config = _configuration.GetSection("CryoBankConfig").Get<CryoBankConfig>();
                if (config == null)
                    throw new Exception("CryoBankConfig missing in configuration.");

                var allNodes = new List<CryoLocation>();

                foreach (var tankConfig in config.Tanks)
                {
                    for (int t = 0; t < tankConfig.TankCount; t++)
                    {
                        var tank = new CryoLocation
                        {
                            Name = $"{tankConfig.SampleType} Tank{t + 1}",
                            Capacity = config.CanisterPerTank * config.GobletPerCanister * config.SlotPerGoblet,
                            Code = $"{tankConfig.SampleType.ToString().First()}-T{t + 1}",
                            SampleCount = 0,
                            Temperature = -196,
                            Notes = "Initial",
                            Type = CryoLocationType.Tank,
                            SampleType = tankConfig.SampleType,
                            IsActive = true,
                            Children = new List<CryoLocation>()
                        };
                        allNodes.Add(tank);

                        // Canisters
                        for (int c = 0; c < config.CanisterPerTank; c++)
                        {
                            var canister = new CryoLocation
                            {
                                Name = $"{tank.Name} Canister{c + 1}",
                                Capacity = config.GobletPerCanister * config.SlotPerGoblet,
                                Code = $"{tankConfig.SampleType.ToString().First()}-T{t + 1}-C{c + 1}",
                                SampleCount = 0,
                                Temperature = -196,
                                Notes = "Initial",
                                Type = CryoLocationType.Canister,
                                SampleType = tankConfig.SampleType,
                                ParentId = tank.Id,
                                Parent = tank,
                                IsActive = true,
                                Children = new List<CryoLocation>()
                            };
                            tank.Children.Add(canister);
                            allNodes.Add(canister);

                            // Goblets
                            for (int g = 0; g < config.GobletPerCanister; g++)
                            {
                                var goblet = new CryoLocation
                                {
                                    Name = $"{canister.Name} Goblet{g + 1}",
                                    Capacity = config.SlotPerGoblet,
                                    Code = $"{tankConfig.SampleType.ToString().First()}-T{t + 1}-C{c + 1}-G{g + 1}",
                                    SampleCount = 0,
                                    Temperature = -196,
                                    Notes = "Initial",
                                    Type = CryoLocationType.Goblet,
                                    SampleType = tankConfig.SampleType,
                                    ParentId = canister.Id,
                                    Parent = canister,
                                    IsActive = true,
                                    Children = new List<CryoLocation>()
                                };
                                canister.Children.Add(goblet);
                                allNodes.Add(goblet);

                                // Slots
                                for (int s = 0; s < config.SlotPerGoblet; s++)
                                {
                                    var slot = new CryoLocation
                                    {
                                        Name = $"{goblet.Name} Slot {s + 1}",
                                        Capacity = 1,
                                        Code = $"{tankConfig.SampleType.ToString().First()}-T{t + 1}-C{c + 1}-G{g + 1}-S{s + 1}",
                                        SampleCount = 0,
                                        Temperature = -196,
                                        Notes = "Initial",
                                        Type = CryoLocationType.Slot,
                                        SampleType = tankConfig.SampleType,
                                        ParentId = goblet.Id,
                                        Parent = goblet,
                                        IsActive = true
                                    };
                                    goblet.Children.Add(slot);
                                    allNodes.Add(slot);
                                }
                            }
                        }
                    }
                }

                await _unitOfWork.Repository<CryoLocation>().InsertRangeAsync(allNodes.AsQueryable());
                await _unitOfWork.CommitAsync();

                return new DynamicResponse<CryoLocationSummaryResponse>
                {
                    Code = StatusCodes.Status201Created,
                    SystemCode = "SUCCESS",
                    Message = "Default CryoBank created successfully",
                    Data = _mapper.Map<List<CryoLocationSummaryResponse>>(allNodes.Where(x => x.Type == CryoLocationType.Tank))
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating default CryoBank", methodName);
                return new DynamicResponse<CryoLocationSummaryResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<CryoLocationSummaryResponse>> GetInitialTreeAsync(SampleType? sampleType = null)
        {
            const string methodName = nameof(GetInitialTreeAsync);
            _logger.LogInformation("{MethodName} called with sampleType: {SampleType}", methodName, sampleType);

            try
            {
                var query = _unitOfWork.Repository<CryoLocation>()
                    .AsQueryable()
                    .Where(c => c.ParentId == null && !c.IsDeleted);

                if (sampleType.HasValue)
                    query = query.Where(c => c.SampleType == sampleType.Value);

                var topNodes = await query.ToListAsync();
                var response = _mapper.Map<List<CryoLocationSummaryResponse>>(topNodes);

                return new DynamicResponse<CryoLocationSummaryResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Top-level locations retrieved",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving initial tree", methodName);
                return new DynamicResponse<CryoLocationSummaryResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Internal server error",
                    Data = new List<CryoLocationSummaryResponse>()
                };
            }
        }

        public async Task<BaseResponse<CryoLocationResponse?>> GetByIdAsync(Guid id)
        {
            const string methodName = nameof(GetByIdAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

            try
            {
                var entity = await _unitOfWork.Repository<CryoLocation>()
                    .AsQueryable()
                    .Where(c => c.Id == id && !c.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    return new BaseResponse<CryoLocationResponse?>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Location not found",
                        Data = null
                    };
                }

                var response = _mapper.Map<CryoLocationResponse>(entity);
                return new BaseResponse<CryoLocationResponse?>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Location retrieved successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving location", methodName);
                return new BaseResponse<CryoLocationResponse?>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Internal server error",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<CryoLocationSummaryResponse>> GetChildrenAsync(Guid parentId, bool? isActive = null)
        {
            const string methodName = nameof(GetChildrenAsync);
            _logger.LogInformation("{MethodName} called with parentId: {ParentId}, isActive: {IsActive}", methodName, parentId, isActive);

            try
            {
                var query = _unitOfWork.Repository<CryoLocation>()
                    .AsQueryable()
                    .Where(c => c.ParentId == parentId && !c.IsDeleted);

                if (isActive.HasValue)
                    query = query.Where(c => c.IsActive == isActive.Value);

                var children = await query.ToListAsync();
                var response = _mapper.Map<List<CryoLocationSummaryResponse>>(children);

                return new DynamicResponse<CryoLocationSummaryResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Children retrieved",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving children", methodName);
                return new DynamicResponse<CryoLocationSummaryResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Internal server error",
                    Data = new List<CryoLocationSummaryResponse>()
                };
            }
        }

        public async Task<BaseResponse<CryoLocationFullTreeResponse>> GetFullTreeByTankIdAsync(Guid tankId)
        {
            const string methodName = nameof(GetFullTreeByTankIdAsync);
            _logger.LogInformation("{MethodName} called with tankId: {TankId}", methodName, tankId);

            try
            {
                var root = await _unitOfWork.Repository<CryoLocation>()
                    .AsQueryable()
                    .Include(c => c.Children) // recursive load handled in memory
                    .Where(c => c.Id == tankId && !c.IsDeleted)
                    .FirstOrDefaultAsync();

                if (root == null)
                {
                    return new BaseResponse<CryoLocationFullTreeResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Tank not found",
                        Data = null
                    };
                }

                // Recursively map tree
                CryoLocationFullTreeResponse MapTree(CryoLocation node)
                {
                    var mapped = _mapper.Map<CryoLocationFullTreeResponse>(node);
                    if (node.Children != null && node.Children.Any())
                    {
                        mapped.Children = node.Children
                            .Where(c => !c.IsDeleted)
                            .Select(MapTree)
                            .ToList();
                    }
                    return mapped;
                }

                var fullTree = MapTree(root);

                return new BaseResponse<CryoLocationFullTreeResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Full tree retrieved",
                    Data = fullTree
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving full tree", methodName);
                return new BaseResponse<CryoLocationFullTreeResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Internal server error",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<CryoLocationResponse>> UpdateAsync(Guid id, CryoLocationUpdateRequest request)
        {
            const string methodName = nameof(UpdateAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

            try
            {
                var entity = await _unitOfWork.Repository<CryoLocation>()
                    .AsQueryable()
                    .Where(c => c.Id == id && !c.IsDeleted)
                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    return new BaseResponse<CryoLocationResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Location not found"
                    };
                }

                _mapper.Map(request, entity);
                await _unitOfWork.Repository<CryoLocation>().UpdateGuid(entity, entity.Id);
                await _unitOfWork.CommitAsync();

                var response = _mapper.Map<CryoLocationResponse>(entity);
                return new BaseResponse<CryoLocationResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Location updated successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error updating location", methodName);
                return new BaseResponse<CryoLocationResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Internal server error",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            const string methodName = nameof(DeleteAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

            try
            {
                // 1. Lấy node cần xóa
                var node = await _unitOfWork.Repository<CryoLocation>()
                    .AsQueryable()
                    .Include(n => n.Children) // load children để check recursive
                    .FirstOrDefaultAsync(n => n.Id == id && !n.IsDeleted);

                if (node == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Location not found"
                    };
                }

                // 2. Kiểm tra node hiện tại và tất cả node con có sample
                if (node.SampleCount>0)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Cannot delete node because it or its children contain samples"
                    };
                }

                // 3. Soft delete node và tất cả con
                await SoftDeleteRecursive(node);

                // 4. Cập nhật lại SampleCount và Capacity cho các node cha
                await UpdateParentCountsRecursive(node.ParentId);

                await _unitOfWork.CommitAsync();

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Location deleted successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error deleting location", methodName);
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Internal server error"
                };
            }
        }

        private async Task SoftDeleteRecursive(CryoLocation node)
        {
            node.IsDeleted = true;
            node.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.Repository<CryoLocation>().UpdateGuid(node, node.Id);

            var children = await _unitOfWork.Repository<CryoLocation>()
                .AsQueryable()
                .Where(c => c.ParentId == node.Id && !c.IsDeleted)
                .ToListAsync();

            foreach (var child in children)
            {
                await SoftDeleteRecursive(child);
            }
        }

        private async Task UpdateParentCountsRecursive(Guid? parentId)
        {
            if (!parentId.HasValue)
                return;

            var parent = await _unitOfWork.Repository<CryoLocation>()
                .AsQueryable()
                .Include(p => p.Children)
                .FirstOrDefaultAsync(p => p.Id == parentId.Value && !p.IsDeleted);

            if (parent == null)
                return;

            // Tính lại SampleCount từ tất cả children chưa xóa
            parent.SampleCount = parent.Children
                .Where(c => !c.IsDeleted)
                .Sum(c => c.SampleCount);

            // Capacity không thay đổi, nếu muốn tính lại theo children thì có thể update thêm logic ở đây

            await _unitOfWork.Repository<CryoLocation>().UpdateGuid(parent, parent.Id);

            // Đi lên cha tiếp theo
            await UpdateParentCountsRecursive(parent.ParentId);
        }

    }
}
