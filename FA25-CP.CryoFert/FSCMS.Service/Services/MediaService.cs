using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using FSCMS.Core.Entities;
using FSCMS.Core.Interfaces;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.ReponseModel.FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using FSCMS.Service.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;
using FSCMS.Core.Enum;
using FSCMS.Core.Enums;

namespace FSCMS.Service.Services
{
    public class MediaService : IMediaService
    {
        #region Dependencies

        private readonly IFileStorageService _fileStorageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MediaService> _logger;
        private readonly IMapper _mapper;
        private readonly IConverter _converter;
        private readonly IWebHostEnvironment _env;
        private readonly IRazorViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        #endregion

        #region Constructor

        public MediaService(
            IRazorViewEngine viewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider,
            IWebHostEnvironment env,
            IFileStorageService fileStorageService,
            IUnitOfWork unitOfWork,
            ILogger<MediaService> logger,
            IConverter converter,
            IMapper mapper)
        {
            // Xác định thư mục DLL theo kiến trúc
            string architecture = IntPtr.Size == 8 ? "win-x64" : "win-x86";
            string dllPath = Path.Combine(AppContext.BaseDirectory, "Libs", architecture, "libwkhtmltox.dll");

            // Tạo context và load DLL
            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(dllPath);

            // Khởi tạo converter DinkToPdf
            _converter = converter;
            //_converter = new SynchronizedConverter(new PdfTools());
            _fileStorageService = fileStorageService ?? throw new ArgumentNullException(nameof(fileStorageService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _env = env;
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }

        #endregion

        #region CRUD Operations

        public async Task<BaseResponse<MediaResponse>> UploadMediaAsync(UploadMediaRequest request, Guid? accountId)
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

                string? uploadedBy = null;
                Guid? uploadedByUserId = accountId;
                if (accountId != null)
                {
                    var account = await _unitOfWork.Repository<Account>().GetByIdGuid((Guid)accountId);
                    if (account == null)
                        return new BaseResponse<MediaResponse>
                        {
                            Code = StatusCodes.Status401Unauthorized,
                            Message = "UnAuthorize."
                        };
                    uploadedBy = $"{account.FirstName} {account.LastName}";
                }

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
                    EntityTypeMedia.CryoImport => await _unitOfWork.Repository<CryoImport>()
                                    .AsQueryable()
                                    .Include(m => m.LabSample)
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeMedia.CryoExport => await _unitOfWork.Repository<CryoExport>()
                                    .AsQueryable()
                                    .Include(m => m.LabSample)
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeMedia.ServiceRequest => await _unitOfWork.Repository<ServiceRequest>()
                                    .AsQueryable()
                                    .Include(m => m.Appointment)
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    EntityTypeMedia.ServiceRequestDetails => await _unitOfWork.Repository<ServiceRequestDetails>()
                                    .AsQueryable()
                                    .Include(m => m.ServiceRequest)
                                        .ThenInclude(m => m.Appointment)
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
                    CryoImport ci => ci.LabSample?.PatientId,
                    CryoExport ce => ce.LabSample?.PatientId,
                    ServiceRequest sr => sr.Appointment?.PatientId,
                    ServiceRequestDetails srd => srd.ServiceRequest?.Appointment?.PatientId,
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
                newMedia.UploadedBy = uploadedBy;
                newMedia.UploadedByUserId = uploadedByUserId;
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

        //public async Task<BaseResponse<MediaResponse>> UploadTemplateAsync(UploadTemplateRequest request, Guid accountId)
        //{
        //    using var transaction = await _unitOfWork.BeginTransactionAsync();
        //    var file = request.File;
        //    try
        //    {
        //        if (file == null || file.Length == 0)
        //        {
        //            return new BaseResponse<MediaResponse>
        //            {
        //                Code = StatusCodes.Status400BadRequest,
        //                SystemCode = "INVALID_FILE",
        //                Message = "File is required",
        //                Data = null
        //            };
        //        }

        //        var account = await _unitOfWork.Repository<Account>().GetByIdGuid(accountId);
        //        if (account == null)
        //            return new BaseResponse<MediaResponse>
        //            {
        //                Code = StatusCodes.Status401Unauthorized,
        //                Message = "UnAuthorize."
        //            };

        //        var oldTemplate = await _unitOfWork.Repository<Media>()
        //                            .AsQueryable()
        //                            .Where(p => p.IsTemplate && !p.IsDeleted && p.RelatedEntityType == request.TemplateType.ToString())
        //                            .FirstOrDefaultAsync();
        //        if (oldTemplate != null)
        //        {
        //            oldTemplate.IsDeleted = true;
        //            oldTemplate.UpdatedAt = DateTime.UtcNow;
        //            await _unitOfWork.Repository<Media>().UpdateGuid(oldTemplate, oldTemplate.Id);
        //            //await _fileStorageService.DeleteFileAsync(oldTemplate.FilePath);
        //            await _unitOfWork.CommitAsync();
        //        }
        //        using var stream = file.OpenReadStream();
        //        string filePath = await _fileStorageService.UploadFileAsync(stream, file.FileName, file.ContentType);

        //        var newMedia = new Media();
        //        newMedia.RelatedEntityType = request.TemplateType.ToString();
        //        newMedia.OriginalFileName = file.FileName;
        //        newMedia.FileType = file.ContentType;
        //        newMedia.FileSize = file.Length;
        //        newMedia.FileExtension = Path.GetExtension(file.FileName);
        //        newMedia.FilePath = filePath;
        //        newMedia.PatientId = null;
        //        newMedia.CloudUrl = filePath;
        //        newMedia.UploadDate = DateTime.UtcNow;
        //        newMedia.UploadedBy = $"{account.FirstName} {account.LastName}";
        //        newMedia.UploadedByUserId = accountId;
        //        newMedia.IsTemplate = true;
        //        newMedia.IsPublic = true;
        //        newMedia.Category = "Template";
        //        newMedia.Tags = "Template";
        //        newMedia.Title = $"Template {request.TemplateType.ToString()}";
        //        newMedia.Description = $"Template file for {request.TemplateType.ToString()}";
        //        newMedia.FileName = $"Template {request.TemplateType.ToString()}";
        //        newMedia.Notes = $"Template file for {request.TemplateType.ToString()}";

        //        await _unitOfWork.Repository<Media>().InsertAsync(newMedia);
        //        await _unitOfWork.CommitAsync();
        //        await transaction.CommitAsync();

        //        var createdMedia = await _unitOfWork.Repository<Media>()
        //            .AsQueryable()
        //            .FirstOrDefaultAsync(m => m.Id == newMedia.Id);

        //        return new BaseResponse<MediaResponse>
        //        {
        //            Code = StatusCodes.Status201Created,
        //            SystemCode = "SUCCESS",
        //            Message = "Template uploaded successfully",
        //            Data = _mapper.Map<MediaResponse>(createdMedia)
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        await transaction.RollbackAsync();
        //        return new BaseResponse<MediaResponse>
        //        {
        //            Code = StatusCodes.Status500InternalServerError,
        //            SystemCode = "INTERNAL_ERROR",
        //            Message = "An error occurred while uploading template",
        //            Data = null
        //        };
        //    }
        //}

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

        //public async Task<BaseResponse<MediaResponse>> GetTemplateAsync(GetTemplateRequest request)
        //{
        //    try
        //    {
        //        var media = await _unitOfWork.Repository<Media>()
        //            .AsQueryable()
        //            .AsNoTracking()
        //            .Where(u => u.IsTemplate && !u.IsDeleted && u.RelatedEntityType == request.TemplateType.ToString())
        //            .FirstOrDefaultAsync();

        //        if (media == null)
        //        {
        //            return new BaseResponse<MediaResponse>
        //            {
        //                Code = StatusCodes.Status404NotFound,
        //                SystemCode = "MEDIA_NOT_FOUND",
        //                Message = "Tenplate not found",
        //                Data = null
        //            };
        //        }

        //        var mediaResponse = _mapper.Map<MediaResponse>(media);

        //        return new BaseResponse<MediaResponse>
        //        {
        //            Code = StatusCodes.Status200OK,
        //            SystemCode = "SUCCESS",
        //            Message = "Template retrieved successfully",
        //            Data = mediaResponse
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<MediaResponse>
        //        {
        //            Code = StatusCodes.Status500InternalServerError,
        //            SystemCode = "INTERNAL_ERROR",
        //            Message = "An internal error occurred while retrieving the template",
        //            Data = null
        //        };
        //    }
        //}

        //public async Task<string> GetEtaTemplateFromCloudAsync(EntityTypeMedia type)
        //{
        //    var media = await _unitOfWork.Repository<Media>()
        //            .AsQueryable()
        //            .AsNoTracking()
        //            .Where(u => u.IsTemplate && !u.IsDeleted && u.RelatedEntityType == type.ToString())
        //            .FirstOrDefaultAsync();

        //    if (media == null)
        //    {
        //        return null;
        //    }
        //    using var client = new HttpClient();
        //    var templateContent = await client.GetStringAsync(media.FilePath);
        //    return templateContent;
        //}

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

        #endregion

        //private async Task<byte[]> GeneratePdfFromHtmlAsync(string htmlContent)
        //{
        //    var doc = new HtmlToPdfDocument()
        //    {
        //        GlobalSettings = new GlobalSettings
        //        {
        //            ColorMode = ColorMode.Color,
        //            Orientation = Orientation.Portrait,
        //            PaperSize = PaperKind.A4,
        //            DPI = 300,
        //        },
        //        Objects =
        //        {
        //            new ObjectSettings
        //            {
        //                HtmlContent = htmlContent,
        //                WebSettings = { DefaultEncoding = "utf-8" },
        //            }
        //        }
        //    };

        //    // chạy DinkToPdf
        //    return _converter.Convert(doc);
        //}

        private IFormFile ConvertPdfBytesToFormFile(byte[] pdfBytes, string fileName)
        {
            var stream = new MemoryStream(pdfBytes);
            stream.Position = 0;

            var formFile = new FormFile(stream, 0, pdfBytes.Length, "file", $"{fileName}.pdf")
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/pdf"
            };

            return formFile;
        }

        //public async Task<BaseResponse<MediaResponse>> UploadPdfFromHtmlAsync(
        //    Guid relatedEntityId,
        //    EntityTypeMedia relatedEntityType)
        //{
        //    object? entityExists = relatedEntityType switch
        //    {
        //        EntityTypeMedia.MedicalRecord => await _unitOfWork.Repository<MedicalRecord>()
        //                        .AsQueryable()
        //                        .Include(x => x.Appointment)
        //                        .Where(p => p.Id == relatedEntityId && !p.IsDeleted)
        //                        .FirstOrDefaultAsync(),
        //        EntityTypeMedia.TreatmentCycle => await _unitOfWork.Repository<TreatmentCycle>()
        //                        .AsQueryable()
        //                        .Include(x => x.Treatment)
        //                        .Where(m => m.Id == relatedEntityId && !m.IsDeleted)
        //                        .FirstOrDefaultAsync(),
        //        EntityTypeMedia.Account => await _unitOfWork.Repository<Account>()
        //                        .AsQueryable()
        //                        .Where(m => m.Id == relatedEntityId && !m.IsDeleted)
        //                        .FirstOrDefaultAsync(),
        //        EntityTypeMedia.Agreement => await _unitOfWork.Repository<Agreement>()
        //                        .AsQueryable()
        //                        .Where(m => m.Id == relatedEntityId && !m.IsDeleted)
        //                        .FirstOrDefaultAsync(),
        //        EntityTypeMedia.CryoStorageContract => await _unitOfWork.Repository<CryoStorageContract>()
        //                        .AsQueryable()
        //                        .Where(m => m.Id == relatedEntityId && !m.IsDeleted)
        //                        .FirstOrDefaultAsync(),
        //        EntityTypeMedia.CryoImport => await _unitOfWork.Repository<CryoImport>()
        //                        .AsQueryable()
        //                        .Include(m => m.LabSample)
        //                        .Where(m => m.Id == relatedEntityId && !m.IsDeleted)
        //                        .FirstOrDefaultAsync(),
        //        EntityTypeMedia.CryoExport => await _unitOfWork.Repository<CryoExport>()
        //                        .AsQueryable()
        //                        .Include(m => m.LabSample)
        //                        .Where(m => m.Id == relatedEntityId && !m.IsDeleted)
        //                        .FirstOrDefaultAsync(),
        //        _ => null
        //    };

        //    if (entityExists == null)
        //    {
        //        return new BaseResponse<MediaResponse>
        //        {
        //            Code = StatusCodes.Status404NotFound,
        //            SystemCode = "ENTITY_NOT_FOUND",
        //            Message = $"Related entity {relatedEntityType} with ID {relatedEntityId} not found",
        //            Data = null
        //        };
        //    }
        //    GetHtmlRequest getHtml = new GetHtmlRequest
        //    {
        //        RelatedEntityId = relatedEntityId,
        //        RelatedEntityType = relatedEntityType
        //    };

        //    var renderResult  = await RenderHtmlAsync(getHtml);
        //    if (renderResult.Data == null)
        //    {
        //        return new BaseResponse<MediaResponse>
        //        {
        //            Code = StatusCodes.Status500InternalServerError,
        //            SystemCode = "RENDER_HTML_ERROR",
        //            Message = "Render file HTML error",
        //            Data = null
        //        };
        //    }
        //    string htmlContent = renderResult.Data.Html;
        //    // 1. Convert HTML → PDF bytes
        //    var pdfBytes = await GeneratePdfFromHtmlAsync(htmlContent);
        //    string namePart = entityExists switch
        //    {
        //        CryoStorageContract c => c.ContractNumber ?? relatedEntityId.ToString(),
        //        Agreement a => a.AgreementCode ?? relatedEntityId.ToString(),
        //        _ => relatedEntityId.ToString()
        //    };
        //    string fileName = $"{relatedEntityType}-{namePart}";
            
        //    // 2. PDF bytes → IFormFile
        //    var formFile = ConvertPdfBytesToFormFile(pdfBytes, fileName);

        //    // 3. Tạo UploadMediaRequest để tái sử dụng MediaService hiện có
        //    var request = new UploadMediaRequest
        //    {
        //        File = formFile,
        //        FileName = fileName,
        //        RelatedEntityId = relatedEntityId,
        //        RelatedEntityType = relatedEntityType,
        //        Title = fileName,
        //        Description = "Generated PDF file",
        //        Category = "PDF Document",
        //        IsPublic = true,
        //        Tags = "PDF,Generated",
        //        Notes = "PDF generated from HTML content"
        //    };

        //    // 4. Gọi lại upload file như bình thường
        //    return await UploadMediaAsync(request, null);
        //}

        //public async Task<BaseResponse<RenderHtmlResponse>> RenderHtmlAsync(GetHtmlRequest request)
        //{
        //    try
        //    {
        //        object? entityExists = request.RelatedEntityType switch
        //        {
        //            // EntityTypeMedia.MedicalRecord => await _unitOfWork.Repository<MedicalRecord>()
        //            //                 .AsQueryable()
        //            //                 .Include(x => x.Appointment)
        //            //                 .Where(p => p.Id == request.RelatedEntityId && !p.IsDeleted)
        //            //                 .FirstOrDefaultAsync(),
        //            // EntityTypeMedia.TreatmentCycle => await _unitOfWork.Repository<TreatmentCycle>()
        //            //                 .AsQueryable()
        //            //                 .Include(x => x.Treatment)
        //            //                 .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
        //            //                 .FirstOrDefaultAsync(),
        //            // EntityTypeMedia.Account => await _unitOfWork.Repository<Account>()
        //            //                 .AsQueryable()
        //            //                 .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
        //            //                 .FirstOrDefaultAsync(),
        //            EntityTypeMedia.Agreement => await _unitOfWork.Repository<Agreement>()
        //                            .AsQueryable()
        //                            .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
        //                            .FirstOrDefaultAsync(),
        //            EntityTypeMedia.CryoStorageContract => await _unitOfWork.Repository<CryoStorageContract>()
        //                            .AsQueryable()
        //                            .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
        //                            .FirstOrDefaultAsync(),
        //            // EntityTypeMedia.CryoImport => await _unitOfWork.Repository<CryoImport>()
        //            //                 .AsQueryable()
        //            //                 .Include(m => m.LabSample)
        //            //                 .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
        //            //                 .FirstOrDefaultAsync(),
        //            // EntityTypeMedia.CryoExport => await _unitOfWork.Repository<CryoExport>()
        //            //                 .AsQueryable()
        //            //                 .Include(m => m.LabSample)
        //            //                 .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
        //            //                 .FirstOrDefaultAsync(),
        //            _ => null
        //        };

        //        if (entityExists == null)
        //        {
        //            return new BaseResponse<RenderHtmlResponse>
        //            {
        //                Code = StatusCodes.Status404NotFound,
        //                SystemCode = "ENTITY_NOT_FOUND",
        //                Message = $"Related entity {request.RelatedEntityType} with ID {request.RelatedEntityId} not found",
        //                Data = null
        //            };
        //        }

        //        string? htmlContent = entityExists switch
        //        {
        //            // MedicalRecord mr => mr.Appointment?.PatientId,
        //            // TreatmentCycle tc => tc.Treatment?.PatientId,
        //            // Account acc => acc.Id,
        //            Agreement ag => await RenderAgreementAsync(ag.Id),
        //            CryoStorageContract csc => await RenderCryoContractAsync(csc.Id),
        //            // CryoImport ci => ci.LabSample?.PatientId,
        //            // CryoExport ce => ce.LabSample?.PatientId,
        //            _ => null
        //        };

        //        if (htmlContent == null)
        //        {
        //            return new BaseResponse<RenderHtmlResponse>
        //            {
        //                Code = StatusCodes.Status404NotFound,
        //                SystemCode = "TEMPLATE_NOT_FOUND",
        //                Message = "Cannot determine Template from related entity"
        //            };
        //        }

        //        // Trả về kết quả
        //        RenderHtmlResponse data = new RenderHtmlResponse
        //        {
        //            Html = htmlContent
        //        };

        //        return new BaseResponse<RenderHtmlResponse>
        //        {
        //            Code = StatusCodes.Status200OK,
        //            Message = "Html render successfully",
        //            Data = data
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error render html from entity ID: {Id}", request.RelatedEntityId);
        //        return new BaseResponse<RenderHtmlResponse>
        //        {
        //            Code = StatusCodes.Status500InternalServerError,
        //            Message = $"An error occurred: {ex.Message}"
        //        };
        //    }
        //}
        //private async Task<string?> RenderCryoContractAsync(Guid id)
        //{
        //    try
        //    {
        //        // Lấy hợp đồng từ DB
        //        var contract = await _unitOfWork.Repository<CryoStorageContract>()
        //            .AsQueryable()
        //            .Include(x => x.Patient)
        //                .ThenInclude(d => d.Account)
        //            .Include(x => x.CryoPackage)
        //            .Include(x => x.CPSDetails)
        //                .ThenInclude(d => d.LabSample)
        //                    .ThenInclude(c => c.CryoLocation)
        //            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        //        if (contract == null)
        //        {
        //            return null;
        //        }

        //        // Lấy template HTML
        //        var templateText = await GetEtaTemplateFromCloudAsync(EntityTypeMedia.CryoStorageContract);

        //        // Replace các trường tĩnh
        //        templateText = templateText.Replace("<%= it.contract.contractNumber %>", contract.ContractNumber)
        //                                   .Replace("<%= it.contract.startDate %>", contract.StartDate?.ToString("yyyy-MM-dd") ?? "N/A")
        //                                   .Replace("<%= it.contract.endDate %>", contract.EndDate?.ToString("yyyy-MM-dd") ?? "N/A")
        //                                   .Replace("<%= it.contract.status %>", contract.Status.ToString())
        //                                   .Replace("<%= it.contract.patientName %>", $"{contract.Patient.Account.FirstName} {contract.Patient.Account.LastName}")
        //                                   .Replace("<%= it.contract.patientAddress %>", $"{contract.Patient.Account.Address}")
        //                                   .Replace("<%= it.contract.patientDob %>", $"{contract.Patient.Account.BirthDate}")
        //                                   .Replace("<%= it.contract.patientPhone %>", $"{contract.Patient.Account.Phone}")
        //                                   .Replace("<%= it.contract.signedBy %>", contract.SignedBy ?? "N/A")
        //                                   .Replace("<%= it.contract.signedDate %>", contract.SignedDate?.ToString("yyyy-MM-dd") ?? "N/A")
        //                                   .Replace("<%= it.contract.cryoPackageName %>", contract.CryoPackage.PackageName)
        //                                   .Replace("<%= it.contract.totalAmount %>", contract.TotalAmount.ToString("N0"))
        //                                   .Replace("<%= it.contract.paidAmount %>", (contract.PaidAmount?.ToString("N0") ?? "0"))
        //                                   .Replace("<%= it.contract.notes %>", string.IsNullOrEmpty(contract.Notes) ? "N/A" : contract.Notes)
        //                                   .Replace("<%= it.generatedAt %>", contract.CreatedAt.ToString("yyyy-MM-dd"))
        //                                   .Replace("<%= it.updatedAt %>", DateTime.UtcNow.ToString("yyyy-MM-dd"));

        //        // Build table samples
        //        string sampleRows = "";
        //        int index = 1;
        //        foreach (var s in contract.CPSDetails)
        //        {
        //            var locationName = s.LabSample.CryoLocation?.Name ?? "N/A";
        //            bool isActive = s.Status == "Storage";
        //            sampleRows += $"<tr>" +
        //                          $"<td>{index}</td>" +
        //                          $"<td>{s.LabSample.SampleCode}</td>" +
        //                          $"<td>{s.LabSample.SampleType}</td>" +
        //                          $"<td>{locationName}</td>" +
        //                          $"<td>{(isActive ? "Yes" : "No")}</td>" +
        //                          $"</tr>";
        //            index++;
        //        }

        //        // Replace toàn bộ vòng lặp sample table
        //        templateText = System.Text.RegularExpressions.Regex.Replace(templateText,
        //            "<% it.contract.samples.forEach\\(\\(s, index\\) => \\{ %>.*?<% }\\) %>",
        //            sampleRows,
        //            System.Text.RegularExpressions.RegexOptions.Singleline);

        //        return templateText;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error render contract ID: {Id}", id);
        //        return null;
        //    }
        //}

        //private async Task<string?> RenderAgreementAsync(Guid id)
        //{
        //    try
        //    {
        //        var agreement = await _unitOfWork.Repository<Agreement>()
        //            .AsQueryable()
        //            .Include(x => x.Patient)
        //                .ThenInclude(d => d.Account)
        //            .Include(x => x.Treatment)
        //            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        //        if (agreement == null)
        //        {
        //            return null;
        //        }

        //        // Lấy template HTML
        //        var templateText = await GetEtaTemplateFromCloudAsync(EntityTypeMedia.Agreement);

        //        // Replace các trường tĩnh
        //        templateText = templateText.Replace("<%= it.agreementType %>", agreement.Treatment.TreatmentType.ToString())
        //                                   .Replace("<%= it.status %>", agreement.Status.ToString())
        //                                   .Replace("<%= it.patient.name %>", $"{agreement.Patient.Account.FirstName} {agreement.Patient.Account.LastName}")
        //                                   .Replace("<%= it.patient.dob %>", $"{agreement.Patient.Account.BirthDate}")
        //                                   .Replace("<%= it.patient.nationalId %>", agreement.Patient.NationalID)
        //                                   .Replace("<%= it.patient.address %>", agreement.Patient.Account.Address)
        //                                   .Replace("<%= it.patient.phone %>", agreement.Patient.Account.Phone)
        //                                   .Replace("<%= it.date %>", agreement.CreatedAt.ToString("yyyy-MM-dd"));
        //        // Replace khối chữ ký Patient
        //        templateText = templateText.Replace(
        //            "<% if (it.signatures.patient) { %>SIGNED<% } %>",
        //            agreement.SignedByPatient ? "SIGNED" : ""
        //        );

        //        // Replace khối chữ ký Facility
        //        templateText = templateText.Replace(
        //            "<% if (it.signatures.facility) { %>SIGNED<% } %>",
        //            agreement.SignedByDoctor ? "SIGNED" : ""
        //        );

        //        return templateText;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error render Agreement ID: {Id}", id);
        //        return null;
        //    }
        //}

        public async Task<BaseResponse<byte[]>> GenerateFilledPdfAsync(GetHtmlRequest request)
        {
            //string tempHeaderPath = null!;
            //string tempFooterPath = null!;
            try
            {
                object? entityExists = request.RelatedEntityType switch
                {
                    //EntityTypeMedia.MedicalRecord => await _unitOfWork.Repository<MedicalRecord>()
                    //    .AsQueryable()
                    //    .Include(p => p.Appointment)
                    //    .ThenInclude(p => p.TreatmentCycle)
                    //    .ThenInclude(p => p.Treatment)
                    //    .Include(p => p.Prescriptions)
                    //    .ThenInclude(p => p.PrescriptionDetails)
                    //    .ThenInclude(p => p.Medicine)
                    //    .FirstOrDefaultAsync(p => p.Id == request.RelatedEntityId && !p.IsDeleted),
                    EntityTypeMedia.Agreement => await _unitOfWork.Repository<Agreement>()
                        .AsQueryable()
                        .Include(x => x.Treatment)
                        .ThenInclude(x => x.Doctor)
                        .ThenInclude(x => x.Account)
                        .FirstOrDefaultAsync(m => m.Id == request.RelatedEntityId && !m.IsDeleted),
                    EntityTypeMedia.CryoStorageContract => await _unitOfWork.Repository<CryoStorageContract>()
                        .AsQueryable().Include(m => m.CryoPackage)
                        .Include(m => m.CPSDetails).ThenInclude(cd => cd.LabSample)
                        .FirstOrDefaultAsync(m => m.Id == request.RelatedEntityId && !m.IsDeleted),
                    _ => null
                };

                if (entityExists == null)
                {
                    return new BaseResponse<byte[]>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "ENTITY_NOT_FOUND",
                        Message = $"Related entity {request.RelatedEntityType} with ID {request.RelatedEntityId} not found or already save file",
                        Data = null
                    };
                }
                Guid? patientId = entityExists switch
                {
                    MedicalRecord mr => mr.Appointment?.PatientId,
                    TreatmentCycle tc => tc.Treatment?.PatientId,
                    Agreement ag => ag.PatientId,
                    CryoStorageContract csc => csc.PatientId,
                    _ => null
                };
                if (patientId == null)
                {
                    return new BaseResponse<byte[]>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "PATIENT_NOT_FOUND",
                        Message = "Cannot determine PatientId from related entity"
                    };
                }
                var patient = await _unitOfWork.Repository<Patient>()
                    .AsQueryable().Include(p => p.Account)
                    .Where(p => p.Id == patientId && !p.IsDeleted)
                    .FirstOrDefaultAsync();
                if (patient == null)
                {
                    return new BaseResponse<byte[]>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "PATIENT_NOT_FOUND",
                        Message = "Cannot determine PatientId from related entity"
                    };
                }
                var dataModel = new TemplateDataModel
                {
                    Patient = patient,
                };
                bool IsLastVersion = false;
                switch (request.RelatedEntityType)
                {
                    //case EntityTypeMedia.MedicalRecord:
                    //    var medicalRecord = (MedicalRecord)entityExists;
                    //    dataModel.MedicalRecord = medicalRecord;
                    //    dataModel.Appointment = medicalRecord.Appointment;
                    //    dataModel.Treatment = medicalRecord.Appointment?.TreatmentCycle?.Treatment;
                    //    dataModel.TreatmentCycle = medicalRecord.Appointment?.TreatmentCycle;
                    //    dataModel.GeneratedAt = medicalRecord.CreatedAt;
                    //    dataModel.UpdatedAt = medicalRecord.UpdatedAt;
                    //    break;
                    case EntityTypeMedia.Agreement:
                        var agreement = (Agreement)entityExists;
                        dataModel.Agreement = agreement;
                        dataModel.Doctor = agreement.Treatment?.Doctor;
                        dataModel.Treatment = agreement.Treatment;
                        dataModel.GeneratedAt = agreement.CreatedAt;
                        dataModel.UpdatedAt = agreement.UpdatedAt;
                        IsLastVersion = (agreement.Status == AgreementStatus.Completed);
                        break;
                    case EntityTypeMedia.CryoStorageContract:
                        var cryoStorageContract = (CryoStorageContract)entityExists;
                        dataModel.CryoStorageContract = cryoStorageContract;
                        dataModel.CryoPackage = cryoStorageContract.CryoPackage;
                        dataModel.GeneratedAt = cryoStorageContract.CreatedAt;
                        dataModel.UpdatedAt = cryoStorageContract.UpdatedAt;
                        IsLastVersion = (cryoStorageContract.Status == ContractStatus.Active);
                        break;
                }
                string viewName = request.RelatedEntityType switch
                {
                    //EntityTypeMedia.MedicalRecord => "PdfTemplates/MedicalRecord",
                    EntityTypeMedia.Agreement => "PdfTemplates/Agreement",
                    EntityTypeMedia.CryoStorageContract => "PdfTemplates/CryoStorageContract",
                    _ => throw new Exception("Template not found")
                };
                string htmlContent = await RenderViewToStringAsync(viewName, dataModel);
                //string headerHtml = await RenderViewToStringAsync("PdfTemplates/header", dataModel);
                //string footerHtml = await RenderViewToStringAsync("PdfTemplates/footer", dataModel);
                //tempHeaderPath = Path.Combine(Path.GetTempPath(), $"header_{Guid.NewGuid()}.html");
                //tempFooterPath = Path.Combine(Path.GetTempPath(), $"footer_{Guid.NewGuid()}.html");
                //await File.WriteAllTextAsync(tempHeaderPath, headerHtml);
                //await File.WriteAllTextAsync(tempFooterPath, footerHtml);
                string cssPath = Path.Combine(_env.WebRootPath, "pdf", "pdf.css");
                if (!File.Exists(cssPath))
                    cssPath = null!;
                var globalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                    DocumentTitle = "Generated Document",
                    Margins = new MarginSettings
                    {
                        Top = 20,
                        Bottom = 30,
                        Left = 10,
                        Right = 10
                    }
                };
                string basePath = Path.Combine(_env.WebRootPath, "pdf");
                var objectSettings = new ObjectSettings
                {
                    HtmlContent = htmlContent,
                    WebSettings = new WebSettings
                    {
                        DefaultEncoding = "utf-8",
                        UserStyleSheet = File.Exists(cssPath) ? cssPath : null
                    },
                    HeaderSettings = new HeaderSettings
                    {
                        Left = "Global Fertility Clinic",
                        Right = IsLastVersion ? "Official Copy - Do not edit" : "Draff",
                        //Center = "123 Main Street, City, Country | Email: info@clinic.com | Phone: +1 234 567 890",
                        FontSize = 10,
                        FontName = "Arial",
                        Spacing = 8,
                        Line = true
                    },
                    FooterSettings = new FooterSettings
                    {
                        Left = "Page [page] / [topage]",
                        Right = "Generated on: "+ dataModel.GeneratedAt.ToString("dd/MM/yyyy HH:mm"),
                        FontSize = 9,
                        FontName = "Arial",
                        Spacing = 5,
                        Line = true
                    }
                };
                //var objectSettings = new ObjectSettings
                //{
                //    HtmlContent = htmlContent,
                //    WebSettings = new WebSettings
                //    {
                //        DefaultEncoding = "utf-8",
                //        UserStyleSheet = cssPath
                //    },
                //    HeaderSettings = new HeaderSettings
                //    {
                //        HtmUrl = tempHeaderPath,
                //        Spacing = 5
                //    },
                //    FooterSettings = new FooterSettings
                //    {
                //        HtmUrl = tempFooterPath,
                //        Spacing = 5
                //    }
                //};
                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };
                byte[] pdfBytes = _converter.Convert(doc);
                return new BaseResponse<byte[]>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "PDF generated successfully",
                    Data = pdfBytes
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GenerateFilledPdfAsync error");
                return new BaseResponse<byte[]>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
            }
            //finally
            //{
            //    if (tempHeaderPath != null && File.Exists(tempHeaderPath))
            //        File.Delete(tempHeaderPath);
            //    if (tempFooterPath != null && File.Exists(tempFooterPath))
            //        File.Delete(tempFooterPath);
            //}
        }
        private async Task<string> RenderViewToStringAsync(string viewName, object model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var routeData = new RouteData();
            var actionContext = new ActionContext(httpContext, routeData, new ActionDescriptor());
            using var sw = new StringWriter();
            var viewResult = _viewEngine.FindView(actionContext, viewName, false);
            if (!viewResult.Success)
            {
                throw new Exception($"View {viewName} not found.");
            }
            var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = model };
            var viewContext = new ViewContext(actionContext, viewResult.View, viewDictionary, new TempDataDictionary(actionContext.HttpContext, _tempDataProvider), sw, new HtmlHelperOptions());
            await viewResult.View.RenderAsync(viewContext);
            return sw.ToString();
        }

        public async Task<BaseResponse<MediaResponse>> UploadPdfFromHtmlAsync(
            Guid relatedEntityId,
            EntityTypeMedia relatedEntityType)
        {
            object? entityExists = null;

            switch (relatedEntityType)
            {
                case EntityTypeMedia.MedicalRecord:
                    entityExists = await _unitOfWork.Repository<MedicalRecord>()
                                .AsQueryable()
                                .Include(x => x.Appointment)
                                .Where(p => p.Id == relatedEntityId && !p.IsDeleted)
                                .FirstOrDefaultAsync();
                    break;

                case EntityTypeMedia.TreatmentCycle:
                    entityExists = await _unitOfWork.Repository<TreatmentCycle>()
                                .AsQueryable()
                                .Include(x => x.Treatment)
                                .Where(m => m.Id == relatedEntityId && !m.IsDeleted)
                                .FirstOrDefaultAsync();
                    break;

                case EntityTypeMedia.Account:
                    entityExists = await _unitOfWork.Repository<Account>()
                                .AsQueryable()
                                .Where(m => m.Id == relatedEntityId && !m.IsDeleted)
                                .FirstOrDefaultAsync();
                    break;

                case EntityTypeMedia.Agreement:
                    entityExists = await _unitOfWork.Repository<Agreement>()
                                .AsQueryable()
                                .Where(m => m.Id == relatedEntityId && !m.IsDeleted)
                                .FirstOrDefaultAsync();
                    break;

                case EntityTypeMedia.CryoStorageContract:
                    entityExists = await _unitOfWork.Repository<CryoStorageContract>()
                                .AsQueryable()
                                .Where(m => m.Id == relatedEntityId && !m.IsDeleted)
                                .FirstOrDefaultAsync();
                    break;

                case EntityTypeMedia.CryoImport:
                    entityExists = await _unitOfWork.Repository<CryoImport>()
                        .AsQueryable()
                        .Include(x => x.LabSample)
                        .Where(x => x.Id == relatedEntityId && !x.IsDeleted)
                        .FirstOrDefaultAsync();
                    break;

                case EntityTypeMedia.CryoExport:
                    entityExists = await _unitOfWork.Repository<CryoExport>()
                        .AsQueryable()
                        .Include(x => x.LabSample)
                        .Where(x => x.Id == relatedEntityId && !x.IsDeleted)
                        .FirstOrDefaultAsync();
                    break;

                default:
                    entityExists = null;
                    break;
            }
            //object? entityExists = relatedEntityType switch
            //{
            //    EntityTypeMedia.MedicalRecord => await _unitOfWork.Repository<MedicalRecord>()
            //                    .AsQueryable()
            //                    .Include(x => x.Appointment)
            //                    .Where(p => p.Id == relatedEntityId && !p.IsDeleted)
            //                    .FirstOrDefaultAsync(),
            //    EntityTypeMedia.TreatmentCycle => await _unitOfWork.Repository<TreatmentCycle>()
            //                    .AsQueryable()
            //                    .Include(x => x.Treatment)
            //                    .Where(m => m.Id == relatedEntityId && !m.IsDeleted)
            //                    .FirstOrDefaultAsync(),
            //    EntityTypeMedia.Account => await _unitOfWork.Repository<Account>()
            //                    .AsQueryable()
            //                    .Where(m => m.Id == relatedEntityId && !m.IsDeleted)
            //                    .FirstOrDefaultAsync(),
            //    EntityTypeMedia.Agreement => await _unitOfWork.Repository<Agreement>()
            //                    .AsQueryable()
            //                    .Where(m => m.Id == relatedEntityId && !m.IsDeleted)
            //                    .FirstOrDefaultAsync(),
            //    EntityTypeMedia.CryoStorageContract => await _unitOfWork.Repository<CryoStorageContract>()
            //                    .AsQueryable()
            //                    .Where(m => m.Id == relatedEntityId && !m.IsDeleted)
            //                    .FirstOrDefaultAsync(),
            //    EntityTypeMedia.CryoImport => await _unitOfWork.Repository<CryoImport>()
            //                    .AsQueryable()
            //                    .Include(m => m.LabSample)
            //                    .Where(m => m.Id == relatedEntityId && !m.IsDeleted)
            //                    .FirstOrDefaultAsync(),
            //    EntityTypeMedia.CryoExport => await _unitOfWork.Repository<CryoExport>()
            //                    .AsQueryable()
            //                    .Include(m => m.LabSample)
            //                    .Where(m => m.Id == relatedEntityId && !m.IsDeleted)
            //                    .FirstOrDefaultAsync(),
            //    _ => null
            //};

            if (entityExists == null)
            {
                return new BaseResponse<MediaResponse>
                {
                    Code = StatusCodes.Status404NotFound,
                    SystemCode = "ENTITY_NOT_FOUND",
                    Message = $"Related entity {relatedEntityType} with ID {relatedEntityId} not found",
                    Data = null
                };
            }
            GetHtmlRequest getHtml = new GetHtmlRequest
            {
                RelatedEntityId = relatedEntityId,
                RelatedEntityType = relatedEntityType
            };

            var renderResult = await GenerateFilledPdfAsync(getHtml);
            if (renderResult.Data == null)
            {
                return new BaseResponse<MediaResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "RENDER_HTML_ERROR",
                    Message = "Render file HTML error",
                    Data = null
                };
            }
            string namePart = entityExists switch
            {
                CryoStorageContract c => c.ContractNumber ?? relatedEntityId.ToString(),
                Agreement a => a.AgreementCode ?? relatedEntityId.ToString(),
                _ => relatedEntityId.ToString()
            };
            string fileName = $"{relatedEntityType}-{namePart}";

            // 2. PDF bytes → IFormFile
            var formFile = ConvertPdfBytesToFormFile(renderResult.Data, fileName);

            // 3. Tạo UploadMediaRequest để tái sử dụng MediaService hiện có
            var request = new UploadMediaRequest
            {
                File = formFile,
                FileName = fileName,
                RelatedEntityId = relatedEntityId,
                RelatedEntityType = relatedEntityType,
                Title = fileName,
                Description = "Generated PDF file",
                Category = "PDF Document",
                IsPublic = true,
                Tags = "PDF,Generated",
                Notes = "PDF generated from HTML content"
            };

            // 4. Gọi lại upload file như bình thường
            return await UploadMediaAsync(request, null);
        }
    }
}