using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.ReponseModel.FSCMS.Service.ReponseModel;
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

        public async Task<BaseResponse<MediaResponse>> UploadMediaAsync(UploadMediaRequest request)
        {
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

                // Check RelatedEntityType and RelatedEntityId
                if (string.IsNullOrWhiteSpace(request.RelatedEntityType) || !request.RelatedEntityId.HasValue)
                {
                    return new BaseResponse<MediaResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_ENTITY",
                        Message = "Related entity type and ID must be provided",
                        Data = null
                    };
                }

                // Optional: Check entity exists in DB
                bool entityExists = request.RelatedEntityType switch
                {
                    "Patient" => await _unitOfWork.Repository<Patient>()
                                    .AsQueryable()
                                    .AnyAsync(p => p.Id == request.RelatedEntityId.Value && !p.IsDeleted),
                    "MedicalRecord" => await _unitOfWork.Repository<MedicalRecord>()
                                    .AsQueryable()
                                    .AnyAsync(m => m.Id == request.RelatedEntityId.Value && !m.IsDeleted),
                    "Treatment" => await _unitOfWork.Repository<Treatment>()
                                    .AsQueryable()
                                    .AnyAsync(t => t.Id == request.RelatedEntityId.Value && !t.IsDeleted),
                    "TreatmentCycle" => await _unitOfWork.Repository<TreatmentCycle>()
                                    .AsQueryable()
                                    .AnyAsync(l => l.Id == request.RelatedEntityId.Value && !l.IsDeleted),
                    _ => false
                };

                if (!entityExists)
                {
                    return new BaseResponse<MediaResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "ENTITY_NOT_FOUND",
                        Message = $"Related entity {request.RelatedEntityType} with ID {request.RelatedEntityId} not found",
                        Data = null
                    };
                }

                using var stream = file.OpenReadStream();
                string filePath = await _fileStorageService.UploadFileAsync(stream, file.FileName, file.ContentType);

                var newMedia = _mapper.Map<Media>(request);
                newMedia.OriginalFileName = file.FileName;
                newMedia.FileType = file.ContentType;
                newMedia.FileSize = file.Length;
                newMedia.FileExtension = Path.GetExtension(file.FileName);
                newMedia.FilePath = filePath;

                await _unitOfWork.Repository<Media>().InsertAsync(newMedia);
                await _unitOfWork.CommitAsync();

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
                return new BaseResponse<MediaResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An error occurred while uploading media",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse> DeleteMediaAsync(Guid mediaId)
        {
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

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Media deleted successfully"
                };
            }
            catch (Exception ex)
            {
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
                    .Where(m => !m.IsDeleted); 

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

                if (!string.IsNullOrWhiteSpace(request.RelatedEntityType))
                {
                    query = query.Where(m => m.RelatedEntityType == request.RelatedEntityType);
                }

                if (request.RelatedEntityId.HasValue)
                {
                    query = query.Where(m => m.RelatedEntityId == request.RelatedEntityId.Value);
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

        public async Task<BaseResponse<MediaResponse>> UpdateMediaAsync(Guid mediaId, UpdateMediaRequest request)
        {
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
