using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Core.Interfaces;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.ReponseModel.FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FSCMS.Service.Services
{
    public class MediaService : IMediaService
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MediaService> _logger;
        private readonly IMapper _mapper;

        public MediaService(
            IFileStorageService fileStorageService,
            IUnitOfWork unitOfWork,
            ILogger<MediaService> logger,
            IMapper mapper)
        {
            _fileStorageService = fileStorageService ?? throw new ArgumentNullException(nameof(fileStorageService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<MediaResponse>> UploadMediaAsync(UploadMediaRequest request, Guid accountId)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            var file = request.File;
            try
            {
                if (file == null || file.Length == 0)
                {
                    return new BaseResponse<MediaResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_FILE",
                        Message = "File is required",
                        Data = null
                    };
                }

                var account = await _unitOfWork.Repository<Account>().GetByIdGuid(accountId);
                if (account == null)
                    return new BaseResponse<MediaResponse>
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        Message = "UnAuthorize."
                    };

                // Check RelatedEntityType and RelatedEntityId
                // if (string.IsNullOrWhiteSpace(request.RelatedEntityType) || !request.RelatedEntityId.HasValue)
                // {
                //     return new BaseResponse<MediaResponse>
                //     {
                //         Code = StatusCodes.Status400BadRequest,
                //         SystemCode = "INVALID_ENTITY",
                //         Message = "Related entity type and ID must be provided",
                //         Data = null
                //     };
                // }

                // Optional: Check entity exists in DB

                object? entityExists = request.RelatedEntityType switch
                {
                    EntityTypeMedia.MedicalRecord => await _unitOfWork.Repository<MedicalRecord>()
                                    .AsQueryable()
                                    .Include(x => x.Appointment)
                                    .Where(p => p.Id == request.RelatedEntityId && !p.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeMedia.TreatmentCycle => await _unitOfWork.Repository<TreatmentCycle>()
                                    .AsQueryable()
                                    .Include(x => x.Treatment)
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeMedia.Account => await _unitOfWork.Repository<Account>()
                                    .AsQueryable()
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeMedia.Agreement => await _unitOfWork.Repository<Agreement>()
                                    .AsQueryable()
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeMedia.CryoStorageContract => await _unitOfWork.Repository<CryoStorageContract>()
                                    .AsQueryable()
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    _ => null
                };

                if (entityExists == null)
                {
                    return new BaseResponse<MediaResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "ENTITY_NOT_FOUND",
                        Message = $"Related entity {request.RelatedEntityType} with ID {request.RelatedEntityId} not found",
                        Data = null
                    };
                }

                Guid? patientId = entityExists switch
                {
                    MedicalRecord mr => mr.Appointment?.PatientId,
                    TreatmentCycle tc => tc.Treatment?.PatientId,
                    Account acc => acc.Id,
                    Agreement ag => ag.PatientId,
                    CryoStorageContract csc => csc.PatientId,
                    _ => null
                };

                if (patientId == null)
                {
                    return new BaseResponse<MediaResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "PATIENT_NOT_FOUND",
                        Message = "Cannot determine PatientId from related entity"
                    };
                }

                using var stream = file.OpenReadStream();
                string filePath = await _fileStorageService.UploadFileAsync(stream, file.FileName, file.ContentType);

                var newMedia = _mapper.Map<Media>(request);
                newMedia.RelatedEntityType = request.RelatedEntityType.ToString();
                newMedia.OriginalFileName = file.FileName;
                newMedia.FileType = file.ContentType;
                newMedia.FileSize = file.Length;
                newMedia.FileExtension = Path.GetExtension(file.FileName);
                newMedia.FilePath = filePath;
                newMedia.PatientId = patientId;
                newMedia.CloudUrl = filePath;
                newMedia.UploadDate = DateTime.UtcNow;
                newMedia.UploadedBy = $"{account.FirstName} {account.LastName}";
                newMedia.UploadedByUserId = accountId;
                newMedia.IsTemplate = false;

                await _unitOfWork.Repository<Media>().InsertAsync(newMedia);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                var createdMedia = await _unitOfWork.Repository<Media>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(m => m.Id == newMedia.Id);

                return new BaseResponse<MediaResponse>
                {
                    Code = StatusCodes.Status201Created,
                    SystemCode = "SUCCESS",
                    Message = "Media uploaded successfully",
                    Data = _mapper.Map<MediaResponse>(createdMedia)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new BaseResponse<MediaResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An error occurred while uploading media",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<MediaResponse>> UploadTemplateAsync(UploadTemplateRequest request, Guid accountId)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            var file = request.File;
            try
            {
                if (file == null || file.Length == 0)
                {
                    return new BaseResponse<MediaResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_FILE",
                        Message = "File is required",
                        Data = null
                    };
                }

                var account = await _unitOfWork.Repository<Account>().GetByIdGuid(accountId);
                if (account == null)
                    return new BaseResponse<MediaResponse>
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        Message = "UnAuthorize."
                    };

                var oldTemplate = await _unitOfWork.Repository<Media>()
                                    .AsQueryable()
                                    .Where(p => p.IsTemplate && !p.IsDeleted && p.RelatedEntityType == request.TemplateType.ToString())
                                    .FirstOrDefaultAsync();
                if(oldTemplate != null)
                {
                    oldTemplate.IsDeleted = true;
                    oldTemplate.UpdatedAt = DateTime.UtcNow;
                    await _unitOfWork.Repository<Media>().UpdateGuid(oldTemplate, oldTemplate.Id);
                    //await _fileStorageService.DeleteFileAsync(oldTemplate.FilePath);
                    await _unitOfWork.CommitAsync();
                }
                using var stream = file.OpenReadStream();
                string filePath = await _fileStorageService.UploadFileAsync(stream, file.FileName, file.ContentType);

                var newMedia = new Media();
                newMedia.RelatedEntityType = request.TemplateType.ToString();
                newMedia.OriginalFileName = file.FileName;
                newMedia.FileType = file.ContentType;
                newMedia.FileSize = file.Length;
                newMedia.FileExtension = Path.GetExtension(file.FileName);
                newMedia.FilePath = filePath;
                newMedia.PatientId = null;
                newMedia.CloudUrl = filePath;
                newMedia.UploadDate = DateTime.UtcNow;
                newMedia.UploadedBy = $"{account.FirstName} {account.LastName}";
                newMedia.UploadedByUserId = accountId;
                newMedia.IsTemplate = true;
                newMedia.IsPublic = true;
                newMedia.Category = "Template";
                newMedia.Tags = "Template";
                newMedia.Title = $"Template {request.TemplateType.ToString()}";
                newMedia.Description = $"Template file for {request.TemplateType.ToString()}";
                newMedia.FileName = $"Template {request.TemplateType.ToString()}";
                newMedia.Notes = $"Template file for {request.TemplateType.ToString()}";

                await _unitOfWork.Repository<Media>().InsertAsync(newMedia);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                var createdMedia = await _unitOfWork.Repository<Media>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(m => m.Id == newMedia.Id);

                return new BaseResponse<MediaResponse>
                {
                    Code = StatusCodes.Status201Created,
                    SystemCode = "SUCCESS",
                    Message = "Template uploaded successfully",
                    Data = _mapper.Map<MediaResponse>(createdMedia)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new BaseResponse<MediaResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An error occurred while uploading template",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse> DeleteMediaAsync(Guid mediaId)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var media = await _unitOfWork.Repository<Media>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(m => m.Id == mediaId && !m.IsDeleted);

                if (media == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "MEDIA_NOT_FOUND",
                        Message = "Media not found"
                    };
                }

                media.IsDeleted = true;
                media.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Media>().UpdateGuid(media, mediaId);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Media deleted successfully"
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An error occurred while deleting media"
                };
            }
        }

        public async Task<DynamicResponse<MediaResponse>> GetAllMediasAsync(GetMediasRequest request)
        {
            try
            {
                var query = _unitOfWork.Repository<Media>()
                    .AsQueryable()
                    .Where(m => !m.IsDeleted && !m.IsTemplate);

                // Filtering
                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    query = query.Where(m =>
                        m.FileName.Contains(request.SearchTerm) ||
                        (m.Title != null && m.Title.Contains(request.SearchTerm)) ||
                        (m.Description != null && m.Description.Contains(request.SearchTerm)) ||
                        (m.Tags != null && m.Tags.Contains(request.SearchTerm)) ||
                        (m.Notes != null && m.Notes.Contains(request.SearchTerm))
                    );
                }

                if (request.RelatedEntityType.HasValue)
                {
                    query = query.Where(m => m.RelatedEntityType == request.RelatedEntityType.ToString());
                }

                if (request.RelatedEntityId.HasValue)
                {
                    query = query.Where(m => m.RelatedEntityId == request.RelatedEntityId.Value);
                }

                if (request.PatientId.HasValue)
                {
                    bool exist = await _unitOfWork.Repository<Patient>()
                                    .AsQueryable()
                                    .AnyAsync(p => p.Id == request.PatientId && !p.IsDeleted);
                    if (!exist)
                    {
                        return new DynamicResponse<MediaResponse>
                        {
                            Code = StatusCodes.Status404NotFound,
                            Message = "Patient not found",
                        };
                    }
                    query = query.Where(m => m.PatientId == request.PatientId.Value);
                }

                if (request.UpLoadByUserId.HasValue)
                {
                    bool exist = await _unitOfWork.Repository<Account>()
                                    .AsQueryable()
                                    .AnyAsync(p => p.Id == request.UpLoadByUserId && !p.IsDeleted);
                    if (!exist)
                    {
                        return new DynamicResponse<MediaResponse>
                        {
                            Code = StatusCodes.Status404NotFound,
                            Message = "User upload file not found",
                        };
                    }
                    query = query.Where(m => m.UploadedByUserId == request.UpLoadByUserId.Value);
                }

                // Total count
                var totalCount = await query.CountAsync();

                // Sorting
                if (!string.IsNullOrWhiteSpace(request.Sort))
                {
                    var isDescending = request.Order?.ToLower() == "desc";
                    query = request.Sort.ToLower() switch
                    {
                        "filename" => isDescending ? query.OrderByDescending(m => m.FileName) : query.OrderBy(m => m.FileName),
                        "uploaddate" => isDescending ? query.OrderByDescending(m => m.CreatedAt) : query.OrderBy(m => m.CreatedAt),
                        _ => isDescending ? query.OrderByDescending(m => m.CreatedAt) : query.OrderBy(m => m.CreatedAt)
                    };
                }
                else
                {
                    query = query.OrderByDescending(m => m.CreatedAt);
                }

                // Pagination
                var mediaList = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                return new DynamicResponse<MediaResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Media retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = totalCount
                    },
                    Data = _mapper.Map<List<MediaResponse>>(mediaList)
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<MediaResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while retrieving media list",
                    MetaData = new PagingMetaData(),
                    Data = new List<MediaResponse>()
                };
            }
        }

        public async Task<BaseResponse<MediaResponse>> GetMediaByIdAsync(Guid mediaId)
        {
            try
            {
                var media = await _unitOfWork.Repository<Media>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(u => u.Id == mediaId && !u.IsDeleted)
                    .FirstOrDefaultAsync();

                if (media == null)
                {
                    return new BaseResponse<MediaResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "MEDIA_NOT_FOUND",
                        Message = "Media not found",
                        Data = null
                    };
                }

                var mediaResponse = _mapper.Map<MediaResponse>(media);

                return new BaseResponse<MediaResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Media retrieved successfully",
                    Data = mediaResponse
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<MediaResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred while retrieving the media",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<MediaResponse>> GetTemplateAsync(GetTemplateRequest request)
        {
            try
            {
                var media = await _unitOfWork.Repository<Media>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(u => u.IsTemplate && !u.IsDeleted && u.RelatedEntityType == request.TemplateType.ToString())
                    .FirstOrDefaultAsync();

                if (media == null)
                {
                    return new BaseResponse<MediaResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "MEDIA_NOT_FOUND",
                        Message = "Tenplate not found",
                        Data = null
                    };
                }

                var mediaResponse = _mapper.Map<MediaResponse>(media);

                return new BaseResponse<MediaResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Template retrieved successfully",
                    Data = mediaResponse
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<MediaResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred while retrieving the template",
                    Data = null
                };
            }
        }

        public async Task<string> GetEtaTemplateFromCloudAsync(EntityTypeMedia type)
        {
            var media = await _unitOfWork.Repository<Media>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(u => u.IsTemplate && !u.IsDeleted && u.RelatedEntityType == type.ToString())
                    .FirstOrDefaultAsync();

            if (media == null)
            {
                return null;
            }
            using var client = new HttpClient();
            var templateContent = await client.GetStringAsync(media.FilePath);
            return templateContent;
        }

        public async Task<BaseResponse<MediaResponse>> UpdateMediaAsync(Guid mediaId, UpdateMediaRequest request)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var media = await _unitOfWork.Repository<Media>()
                    .AsQueryable()
                    .Where(u => u.Id == mediaId && !u.IsDeleted)
                    .FirstOrDefaultAsync();

                if (media == null)
                {
                    return new BaseResponse<MediaResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Media not found",
                        Data = null
                    };
                }

                // Update media
                _mapper.Map(request, media);
                media.UpdatedAt = DateTime.UtcNow;
                // Save changes
                await _unitOfWork.Repository<Media>().UpdateGuid(media, mediaId);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                var updatedMedia = await _unitOfWork.Repository<Media>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(u => u.Id == mediaId);

                return new BaseResponse<MediaResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Media updated successfully",
                    Data = _mapper.Map<MediaResponse>(updatedMedia)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new BaseResponse<MediaResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }
    }
}