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
    public class CryoLocationService : ICryoLocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CryoLocationService> _logger;

        public CryoLocationService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CryoLocationService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BaseResponse<List<CryoLocationResponse>>> CreateDefaultBankAsync()
        {
            const string methodName = nameof(CreateDefaultBankAsync);
            _logger.LogInformation("{MethodName} called", methodName);

            try
            {
                // Check if CryoBank already exists
                var existingTanks = await _unitOfWork.Repository<CryoLocation>()
                    .AsQueryable()
                    .AnyAsync(x => x.Type == CryoLocationType.Tank);

                if (existingTanks)
                {
                    return new BaseResponse<List<CryoLocationResponse>>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "BANK_ALREADY_EXISTS",
                        Message = "CryoBank already initialized",
                        Data = null
                    };
                }

                // Load configuration
                var config = _configuration.GetSection("CryoBankConfig").Get<CryoBankConfig>();
                var allNodes = new List<CryoLocation>();

                foreach (var tankConfig in config.Tanks)
                {
                    for (int t = 0; t < tankConfig.TankCount; t++)
                    {
                        var tank = new CryoLocation
                        {
                            Id = Guid.NewGuid(),
                            Name = $"{tankConfig.SampleType} Tank {t + 1}",
                            Type = CryoLocationType.Tank,
                            SampleType = tankConfig.SampleType,
                            IsActive = true
                        };
                        allNodes.Add(tank);

                        // Canisters
                        for (int c = 0; c < config.CanisterPerTank; c++)
                        {
                            var canister = new CryoLocation
                            {
                                Id = Guid.NewGuid(),
                                Name = $"Canister {c + 1} of {tank.Name}",
                                Type = CryoLocationType.Canister,
                                SampleType = tankConfig.SampleType,
                                ParentId = tank.Id,
                                Parent = tank,
                                IsActive = true
                            };
                            tank.Children.Add(canister);
                            allNodes.Add(canister);

                            // Goblets
                            for (int g = 0; g < config.GobletPerCanister; g++)
                            {
                                var goblet = new CryoLocation
                                {
                                    Id = Guid.NewGuid(),
                                    Name = $"Goblet {g + 1} of {canister.Name}",
                                    Type = CryoLocationType.Goblet,
                                    SampleType = tankConfig.SampleType,
                                    ParentId = canister.Id,
                                    Parent = canister,
                                    IsActive = true
                                };
                                canister.Children.Add(goblet);
                                allNodes.Add(goblet);

                                // Slots
                                for (int s = 0; s < config.SlotPerGoblet; s++)
                                {
                                    var slot = new CryoLocation
                                    {
                                        Id = Guid.NewGuid(),
                                        Name = $"Slot {s + 1} of {goblet.Name}",
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

                await _unitOfWork.Repository<CryoLocation>().InsertRangeAsync(allNodes);
                await _unitOfWork.CommitAsync();

                return new BaseResponse<List<CryoLocationResponse>>
                {
                    Code = StatusCodes.Status201Created,
                    SystemCode = "SUCCESS",
                    Message = "Default CryoBank created successfully",
                    Data = _mapper.Map<List<CryoLocationResponse>>(allNodes.Where(x => x.Type == CryoLocationType.Tank).ToList())
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error creating default CryoBank", methodName);
                return new BaseResponse<List<CryoLocationResponse>>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An error occurred while creating the CryoBank",
                    Data = null
                };
            }
        }

        // public async Task<BaseResponse<CryoLocationResponse>> UpdateAsync(Guid id, CryoLocationUpdateRequest request)
        // {
        //     const string methodName = nameof(UpdateAsync);
        //     _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

        //     try
        //     {
        //         var entity = await _unitOfWork.Repository<CryoLocation>().AsQueryable()
        //             .Include(x => x.Children)
        //             .Include(x => x.LabSamples)
        //             .FirstOrDefaultAsync(x => x.Id == id);

        //         if (entity == null)
        //         {
        //             return new BaseResponse<CryoLocationResponse>
        //             {
        //                 Code = StatusCodes.Status404NotFound,
        //                 SystemCode = "NOT_FOUND",
        //                 Message = "CryoLocation not found",
        //                 Data = null
        //             };
        //         }

        //         _mapper.Map(request, entity);
        //         await _unitOfWork.Repository<CryoLocation>().UpdateGuid(entity, entity.Id);
        //         await _unitOfWork.CommitAsync();

        //         return new BaseResponse<CryoLocationResponse>
        //         {
        //             Code = StatusCodes.Status200OK,
        //             SystemCode = "SUCCESS",
        //             Message = "CryoLocation updated successfully",
        //             Data = _mapper.Map<CryoLocationResponse>(entity)
        //         };
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "{MethodName}: Error updating CryoLocation", methodName);
        //         return new BaseResponse<CryoLocationResponse>
        //         {
        //             Code = StatusCodes.Status500InternalServerError,
        //             SystemCode = "INTERNAL_ERROR",
        //             Message = "An error occurred while updating CryoLocation",
        //             Data = null
        //         };
        //     }
        // }

        // public async Task<BaseResponse> DeleteAsync(Guid id)
        // {
        //     const string methodName = nameof(DeleteAsync);
        //     _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

        //     try
        //     {
        //         var entity = await _unitOfWork.Repository<CryoLocation>().AsQueryable()
        //             .Include(x => x.Children)
        //             .Include(x => x.LabSamples)
        //             .FirstOrDefaultAsync(x => x.Id == id);

        //         if (entity == null)
        //         {
        //             return new BaseResponse
        //             {
        //                 Code = StatusCodes.Status404NotFound,
        //                 SystemCode = "NOT_FOUND",
        //                 Message = "CryoLocation not found"
        //             };
        //         }

        //         if (entity.Children.Any() || entity.LabSamples.Any())
        //         {
        //             return new BaseResponse
        //             {
        //                 Code = StatusCodes.Status400BadRequest,
        //                 SystemCode = "HAS_DEPENDENCIES",
        //                 Message = "Cannot delete location with children or samples"
        //             };
        //         }

        //         await _unitOfWork.Repository<CryoLocation>().DeleteAsync(entity);
        //         await _unitOfWork.CommitAsync();

        //         return new BaseResponse
        //         {
        //             Code = StatusCodes.Status200OK,
        //             SystemCode = "SUCCESS",
        //             Message = "CryoLocation deleted successfully"
        //         };
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "{MethodName}: Error deleting CryoLocation", methodName);
        //         return new BaseResponse
        //         {
        //             Code = StatusCodes.Status500InternalServerError,
        //             SystemCode = "INTERNAL_ERROR",
        //             Message = "An error occurred while deleting CryoLocation"
        //         };
        //     }
        // }

        public async Task<BaseResponse<CryoLocationResponse>> GetByIdAsync(Guid id)
        {
            const string methodName = nameof(GetByIdAsync);
            _logger.LogInformation("{MethodName} called with id: {Id}", methodName, id);

            try
            {
                var entity = await _unitOfWork.Repository<CryoLocation>().AsQueryable()
                    .Include(x => x.Children)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (entity == null)
                {
                    return new BaseResponse<CryoLocationResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "NOT_FOUND",
                        Message = "CryoLocation not found",
                        Data = null
                    };
                }

                return new BaseResponse<CryoLocationResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "CryoLocation retrieved successfully",
                    Data = _mapper.Map<CryoLocationResponse>(entity)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving CryoLocation", methodName);
                return new BaseResponse<CryoLocationResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An error occurred while retrieving CryoLocation",
                    Data = null
                };
            }
        }

        // public async Task<BaseResponse<List<CryoLocationResponse>>> GetAllAsync()
        // {
        //     const string methodName = nameof(GetAllAsync);
        //     _logger.LogInformation("{MethodName} called", methodName);

        //     try
        //     {
        //         var list = await _unitOfWork.Repository<CryoLocation>().AsQueryable().ToListAsync();
        //         return new BaseResponse<List<CryoLocationResponse>>
        //         {
        //             Code = StatusCodes.Status200OK,
        //             SystemCode = "SUCCESS",
        //             Message = "CryoLocations retrieved successfully",
        //             Data = _mapper.Map<List<CryoLocationResponse>>(list)
        //         };
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "{MethodName}: Error retrieving CryoLocations", methodName);
        //         return new BaseResponse<List<CryoLocationResponse>>
        //         {
        //             Code = StatusCodes.Status500InternalServerError,
        //             SystemCode = "INTERNAL_ERROR",
        //             Message = "An error occurred while retrieving CryoLocations",
        //             Data = null
        //         };
        //     }
        // }

        public async Task<BaseResponse<List<CryoLocationResponse>>> GetHierarchyAsync()
        {
            const string methodName = nameof(GetHierarchyAsync);
            _logger.LogInformation("{MethodName} called", methodName);

            try
            {
                var allLocations = await _unitOfWork.Repository<CryoLocation>()
                    .AsQueryable()
                    .Include(x => x.LabSamples)
                    .ToListAsync();

                // Build tree recursively
                List<CryoLocationResponse> BuildTree(Guid? parentId)
                {
                    return allLocations
                        .Where(x => x.ParentId == parentId)
                        .Select(x => new CryoLocationResponse
                        {
                            Id = x.Id,
                            Name = x.Name,
                            ParentId = x.ParentId,
                            Capacity = x.Capacity,
                            CurrentSampleCount = x.LabSamples.Count,
                            Children = BuildTree(x.Id)
                        })
                        .ToList();
                }

                var tree = BuildTree(null); // root nodes
                return new BaseResponse<List<CryoLocationResponse>>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "CryoLocation full hierarchy retrieved successfully",
                    Data = tree
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving full hierarchy", methodName);
                return new BaseResponse<List<CryoLocationResponse>>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An error occurred while retrieving CryoLocation hierarchy",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<List<CryoLocationSummaryResponse>>> GetAllTanksAsync(SampleType? sampleType = null)
        {
            const string methodName = nameof(GetAllTanksAsync);
            _logger.LogInformation("{MethodName} called", methodName);

            try
            {
                var query = _unitOfWork.Repository<CryoLocation>().AsQueryable()
                    .Where(x => x.ParentId == null);

                if (sampleType.HasValue)
                    query = query.Where(x => x.SampleType == sampleType.Value);

                var tanks = await query.ToListAsync();

                var result = tanks.Select(t => new CryoLocationSummaryResponse
                {
                    Id = t.Id,
                    Name = t.Name,
                    SampleType = t.SampleType,
                    TotalCapacity = t.Capacity ?? 0,
                    CurrentCount = t.LabSamples.Count
                }).ToList();

                return new BaseResponse<List<CryoLocationSummaryResponse>>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Tanks retrieved successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving tanks", methodName);
                return new BaseResponse<List<CryoLocationSummaryResponse>>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An error occurred while retrieving tanks",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<List<CryoLocationResponse>>> GetChildrenAsync(Guid parentId)
        {
            const string methodName = nameof(GetChildrenAsync);
            _logger.LogInformation("{MethodName} called with parentId: {ParentId}", methodName, parentId);

            try
            {
                var children = await _unitOfWork.Repository<CryoLocation>().AsQueryable()
                    .Where(x => x.ParentId == parentId)
                    .Include(x => x.LabSamples) // nếu muốn hiển thị số lượng sample
                    .ToListAsync();

                var result = _mapper.Map<List<CryoLocationResponse>>(children);

                return new BaseResponse<List<CryoLocationResponse>>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Child locations retrieved successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving children", methodName);
                return new BaseResponse<List<CryoLocationResponse>>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An error occurred while retrieving children",
                    Data = null
                };
            }
        }


        public async Task<BaseResponse<int>> CheckAvailableCapacityAsync(Guid locationId)
        {
            const string methodName = nameof(CheckAvailableCapacityAsync);
            _logger.LogInformation("{MethodName} called with locationId: {Id}", methodName, locationId);

            try
            {
                var location = await _unitOfWork.Repository<CryoLocation>().AsQueryable()
                    .Include(x => x.LabSamples)
                    .FirstOrDefaultAsync(x => x.Id == locationId);

                if (location == null)
                {
                    return new BaseResponse<int>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "NOT_FOUND",
                        Message = "CryoLocation not found",
                        Data = 0
                    };
                }

                int available = location.Capacity.HasValue ? location.Capacity.Value - location.LabSamples.Count : int.MaxValue;
                return new BaseResponse<int>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Available capacity retrieved successfully",
                    Data = available
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error checking capacity", methodName);
                return new BaseResponse<int>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An error occurred while checking capacity",
                    Data = 0
                };
            }
        }
    }
}
