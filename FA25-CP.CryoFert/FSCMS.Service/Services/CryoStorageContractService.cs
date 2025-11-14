using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Core.Enums;
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
    public class CryoStorageContractService : ICryoStorageContractService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CryoStorageContractService> _logger;

        public CryoStorageContractService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CryoStorageContractService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Get All
        public async Task<DynamicResponse<CryoStorageContractResponse>> GetAllAsync(GetCryoStorageContractsRequest request)
        {
            try
            {
                var query = _unitOfWork.Repository<CryoStorageContract>()
                    .AsQueryable()
                    .Include(x => x.Patient)
                    .Include(x => x.CryoPackage)
                    .Where(x => !x.IsDeleted);

                // Filtering
                if (request.CryoPackageId.HasValue)
                    query = query.Where(x => x.CryoPackageId== request.CryoPackageId);

                if (request.PatientId.HasValue)
                    query = query.Where(x => x.PatientId == request.PatientId);

                if (request.Status.HasValue)
                    query = query.Where(x => x.Status == request.Status);

                // Count total
                var total = await query.CountAsync();

                // Sorting
                query = request.Sort?.ToLower() switch
                {
                    "startdate" => (request.Order?.ToLower() == "desc")
                        ? query.OrderByDescending(x => x.StartDate)
                        : query.OrderBy(x => x.StartDate),
                    _ => query.OrderByDescending(x => x.CreatedAt)
                };

                // Pagination
                var items = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                var data = _mapper.Map<List<CryoStorageContractResponse>>(items);

                return new DynamicResponse<CryoStorageContractResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Contracts retrieved successfully",
                    Data = data,
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = total
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving contracts");
                return new DynamicResponse<CryoStorageContractResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    MetaData = new PagingMetaData(),
                    Data = new List<CryoStorageContractResponse>()
                };
            }
        }
        #endregion

        #region Get By Id
        public async Task<BaseResponse<CryoStorageContractDetailResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Repository<CryoStorageContract>()
                    .AsQueryable()
                    .Include(x => x.Patient)
                        .ThenInclude(d => d.Account)
                    .Include(x => x.CryoPackage)
                    .Include(x => x.CPSDetails)
                        .ThenInclude(d => d.LabSample)
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (entity == null)
                {
                    return new BaseResponse<CryoStorageContractDetailResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Contract not found"
                    };
                }

                var data = _mapper.Map<CryoStorageContractDetailResponse>(entity);

                data.PatientName = entity.Patient?.Account.FirstName + " " +entity.Patient?.Account.LastName;

                return new BaseResponse<CryoStorageContractDetailResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Contract retrieved successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting contract by ID: {Id}", id);
                return new BaseResponse<CryoStorageContractDetailResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
        #endregion

        #region Create
        public async Task<BaseResponse<CryoStorageContractResponse>> CreateAsync(CreateCryoStorageContractRequest request)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Validate Patient & Package
                var patient = await _unitOfWork.Repository<Patient>()
                    .AsQueryable()
                    .Include(x => x.Account)
                    .FirstOrDefaultAsync(x => x.Id == request.PatientId && !x.IsDeleted);
                var package = await _unitOfWork.Repository<CryoPackage>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(x => x.Id == request.CryoPackageId && !x.IsDeleted);

                if (patient == null || package == null)
                {
                    return new BaseResponse<CryoStorageContractResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid Patient or CryoPackage"
                    };
                }

                // Map entity
                var entity = _mapper.Map<CryoStorageContract>(request);
                var patientCode = patient?.PatientCode ?? "UNK";
                var random = new Random();
                entity.ContractNumber = $"CT-{patientCode}-{DateTime.UtcNow:yyMMdd}-{random.Next(10,99)}";
                entity.Status = ContractStatus.Active;
                entity.TotalAmount = package.Price;
                entity.StartDate = DateTime.UtcNow;
                entity.EndDate = DateTime.UtcNow.AddMonths(package.DurationMonths);
                entity.PaidAmount = 0;
                entity.SignedDate = DateTime.UtcNow;
                entity.SignedBy = patient.Account.FirstName + " " + patient.Account.LastName;                

                // Map CPSDetails if provided
                if (request.Samples != null && request.Samples.Any())
                {
                    foreach (var c in request.Samples)
                    {
                        var sample = await _unitOfWork.Repository<LabSample>()
                            .AsQueryable()
                            .FirstOrDefaultAsync(x => x.Id == c.LabSampleId && !x.IsDeleted && x.SampleType == package.SampleType);

                        if (sample == null)
                        {
                            return new BaseResponse<CryoStorageContractResponse>
                            {
                                Code = StatusCodes.Status400BadRequest,
                                Message = $"Invalid LabSampleId: {c.LabSampleId}"
                            };
                        }
                    }

                    entity.CPSDetails = request.Samples.Select(s => new CPSDetail
                    {
                        LabSampleId = s.LabSampleId,
                        StorageStartDate = entity.StartDate,
                        StorageEndDate = entity.EndDate,
                        Status = "Stored",
                        Notes = s.Notes,
                        MonthlyFee = package.Price / package.DurationMonths
                    }).ToList();
                }

                await _unitOfWork.Repository<CryoStorageContract>().InsertAsync(entity);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                var data = _mapper.Map<CryoStorageContractResponse>(entity);
                data.PatientName = entity.SignedBy;
                data.CryoPackageName = package.PackageName;

                return new BaseResponse<CryoStorageContractResponse>
                {
                    Code = StatusCodes.Status201Created,
                    Message = "Contract created successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error creating contract");
                return new BaseResponse<CryoStorageContractResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
        #endregion

        #region Update
        public async Task<BaseResponse<CryoStorageContractResponse>> UpdateAsync(Guid id, UpdateCryoStorageContractRequest request)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var entity = await _unitOfWork.Repository<CryoStorageContract>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (entity == null)
                {
                    return new BaseResponse<CryoStorageContractResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Contract not found"
                    };
                }

                _mapper.Map(request, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<CryoStorageContract>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                var data = _mapper.Map<CryoStorageContractResponse>(entity);

                return new BaseResponse<CryoStorageContractResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Contract updated successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error updating contract {Id}", id);
                return new BaseResponse<CryoStorageContractResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
        #endregion

        #region Delete (Soft Delete)
        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var entity = await _unitOfWork.Repository<CryoStorageContract>()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
                if (entity == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Contract not found"
                    };
                }

                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<CryoStorageContract>().UpdateGuid(entity, id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Contract deleted successfully"
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error deleting contract {Id}", id);
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
        #endregion
    }
}
