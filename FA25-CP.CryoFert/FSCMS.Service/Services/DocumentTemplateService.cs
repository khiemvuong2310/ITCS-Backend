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
using DinkToPdf;
using DinkToPdf.Contracts;
using System.Text.RegularExpressions;
using RazorEngine;
using RazorEngine.Templating; // For Engine.Razor
using Microsoft.AspNetCore.Hosting;



namespace FSCMS.Service.Services
{
    public class DocumentTemplateService : IDocumentTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DocumentTemplateService> _logger;
        private readonly IConverter _pdfConverter;
        private readonly IWebHostEnvironment _env;

        public DocumentTemplateService(IWebHostEnvironment env, IUnitOfWork unitOfWork, IMapper mapper, ILogger<DocumentTemplateService> logger, IConverter pdfConverter)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _env = env;
            _pdfConverter = pdfConverter ?? throw new ArgumentNullException(nameof(pdfConverter));
        }

        public async Task<BaseResponse<DocumentTemplateDetailResponse>> GetDocumentTemplateByIdAsync(GetDocumentTemplateByIdRequest request)
        {
            const string methodName = nameof(GetDocumentTemplateByIdAsync);
            try
            {
                if (request.TemplateId == Guid.Empty)
                {
                    return new BaseResponse<DocumentTemplateDetailResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        SystemCode = "INVALID_TEMPLATE_ID",
                        Message = "Template ID is required",
                        Data = null
                    };
                }

                var template = await _unitOfWork.Repository<DocumentTemplate>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(t => t.Id == request.TemplateId && !t.IsDeleted)
                    .FirstOrDefaultAsync();

                if (template == null)
                {
                    return new BaseResponse<DocumentTemplateDetailResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "TEMPLATE_NOT_FOUND",
                        Message = "Document template not found",
                        Data = null
                    };
                }

                var createdBy = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(t => t.Id == template.CreatedBy && !t.IsDeleted)
                    .FirstOrDefaultAsync();

                var updatedBy = await _unitOfWork.Repository<Account>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(t => t.Id == template.UpdatedBy && !t.IsDeleted)
                    .FirstOrDefaultAsync();

                var response = _mapper.Map<DocumentTemplateDetailResponse>(template);
                response.CreatedByName = createdBy != null ? $"{createdBy.FirstName} {createdBy.LastName}" : "";
                response.UpdatedByName = updatedBy != null ? $"{updatedBy.FirstName} {updatedBy.LastName}" : "";
                response.TotalVersion = await _unitOfWork.Repository<DocumentTemplate>()
                    .AsQueryable()
                    .CountAsync(t => t.TemplateType == template.TemplateType);
                return new BaseResponse<DocumentTemplateDetailResponse>
                {
                    Code = StatusCodes.Status200OK,
                    SystemCode = "SUCCESS",
                    Message = "Template retrieved successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}: Error retrieving template {TemplateId}", methodName, request.TemplateId);
                return new BaseResponse<DocumentTemplateDetailResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    SystemCode = "INTERNAL_ERROR",
                    Message = "An internal error occurred",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<DocumentTemplateResponse>> GetDocumentTemplatesAsync(GetDocumentTemplatesRequest request)
        {
            try
            {
                var query = _unitOfWork.Repository<DocumentTemplate>()
                    .AsQueryable()
                    .AsNoTracking();

                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                    query = query.Where(t => t.Name.Contains(request.SearchTerm) || t.Content.Contains(request.SearchTerm));

                if (request.TemplateType != null)
                    query = query.Where(t => t.TemplateType == request.TemplateType.ToString());

                if (request.Version.HasValue)
                    query = query.Where(t => t.Version == request.Version.Value);

                if (request.IsActive.HasValue)
                    query = query.Where(t => t.IsActive == request.IsActive.Value);

                var totalCount = await query.CountAsync();

                query = query.OrderByDescending(t => t.CreatedAt);

                var templates = await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    .ToListAsync();

                return new DynamicResponse<DocumentTemplateResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Templates retrieved successfully",
                    MetaData = new PagingMetaData
                    {
                        Page = request.Page,
                        Size = request.Size,
                        Total = totalCount
                    },
                    Data = _mapper.Map<List<DocumentTemplateResponse>>(templates)
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<DocumentTemplateResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    MetaData = new PagingMetaData(),
                    Data = new List<DocumentTemplateResponse>()
                };
            }
        }

        public async Task<BaseResponse<DocumentTemplateResponse>> CreateDocumentTemplateAsync(CreateDocumentTemplateRequest request, Guid createdBy)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var newTemplate = _mapper.Map<DocumentTemplate>(request);
                newTemplate.CreatedBy = createdBy;
                newTemplate.TemplateType = request.TemplateType.ToString();
                newTemplate.Code = $"DT-{request.TemplateType}-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 6)}";
                newTemplate.Version = await _unitOfWork.Repository<DocumentTemplate>()
                    .AsQueryable()
                    .CountAsync(t => t.TemplateType == request.TemplateType.ToString()) + 1;
                // Optional: check if Name+TemplateType duplicate
                var exists = await _unitOfWork.Repository<DocumentTemplate>()
                    .AsQueryable()
                    .AnyAsync(t => (t.Name == newTemplate.Name || t.IsActive) && t.TemplateType == newTemplate.TemplateType);

                if (exists)
                    return new BaseResponse<DocumentTemplateResponse>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Template with the same name and type already exists or already active",
                        Data = null
                    };

                await _unitOfWork.Repository<DocumentTemplate>().InsertAsync(newTemplate);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                var created = _mapper.Map<DocumentTemplateResponse>(newTemplate);
                return new BaseResponse<DocumentTemplateResponse>
                {
                    Code = StatusCodes.Status201Created,
                    Message = "Template created successfully",
                    Data = created
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new BaseResponse<DocumentTemplateResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<DocumentTemplateResponse>> UpdateDocumentTemplateAsync(UpdateDocumentTemplateRequest request, Guid updatedBy)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var template = await _unitOfWork.Repository<DocumentTemplate>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(t => t.Id == request.TemplateId);

                if (template == null)
                {
                    return new BaseResponse<DocumentTemplateResponse>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Template not found",
                        Data = null
                    };
                }

                _mapper.Map(request, template);
                template.UpdatedBy = updatedBy;
                template.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<DocumentTemplate>().UpdateGuid(template, template.Id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                var updated = _mapper.Map<DocumentTemplateResponse>(template);
                return new BaseResponse<DocumentTemplateResponse>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Template updated successfully",
                    Data = updated
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new BaseResponse<DocumentTemplateResponse>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse> DeleteDocumentTemplateAsync(Guid templateId)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var template = await _unitOfWork.Repository<DocumentTemplate>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(t => t.Id == templateId);

                if (template == null)
                {
                    return new BaseResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Template not found"
                    };
                }

                template.IsActive = false;
                template.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<DocumentTemplate>().UpdateGuid(template, template.Id);
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();

                return new BaseResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Template deleted successfully"
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new BaseResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Preview & download PDF - fill template data from system
        /// </summary>
        public async Task<BaseResponse<byte[]>> GenerateFilledPdfAsync(GenerateFilledPdfRequest request)
        {
            try
            {
                object? entityExists = request.TemplateType switch
                {
                    TemplateType.MedicalRecord => await _unitOfWork.Repository<MedicalRecord>()
                                    .AsQueryable()
                                    .Include(x => x.Appointment)
                                    .Where(p => p.Id == request.RelatedEntityId && !p.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    TemplateType.TreatmentCycle => await _unitOfWork.Repository<TreatmentCycle>()
                                    .AsQueryable()
                                    .Include(x => x.Treatment)
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    TemplateType.Agreement => await _unitOfWork.Repository<Agreement>()
                                    .AsQueryable()
                                    .Include(x => x.Treatment)
                                    .ThenInclude(x => x.Doctor)
                                    .ThenInclude(x => x.Account)
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    TemplateType.CryoStorageContract => await _unitOfWork.Repository<CryoStorageContract>()
                                    .AsQueryable()
                                    .Where(m => m.Id == request.RelatedEntityId && !m.IsDeleted)
                                    .FirstOrDefaultAsync(),
                    _ => null
                };

                if (entityExists == null)
                {
                    return new BaseResponse<byte[]>
                    {
                        Code = StatusCodes.Status404NotFound,
                        SystemCode = "ENTITY_NOT_FOUND",
                        Message = $"Related entity {request.TemplateType} with ID {request.RelatedEntityId} not found",
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
                    .AsQueryable()
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

                var template = await _unitOfWork.Repository<DocumentTemplate>()
                    .AsQueryable()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.TemplateType == request.TemplateType.ToString() && t.IsActive);

                if (template == null)
                    return new BaseResponse<byte[]>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Template not found",
                        Data = null
                    };

                var dataModel = new TemplateDataModel
                {
                    Patient = patient,
                };

                switch (request.TemplateType)
                {
                    case TemplateType.MedicalRecord:
                        dataModel.MedicalRecord = (MedicalRecord)entityExists;
                        dataModel.GeneratedAt = dataModel.MedicalRecord.CreatedAt;
                        break;
                    case TemplateType.TreatmentCycle:
                        dataModel.TreatmentCycle = (TreatmentCycle)entityExists;
                        dataModel.GeneratedAt = dataModel.TreatmentCycle.CreatedAt;
                        break;
                    case TemplateType.Agreement:
                        dataModel.Agreement = (Agreement)entityExists;
                        dataModel.GeneratedAt = dataModel.Agreement.CreatedAt;
                        break;
                    case TemplateType.CryoStorageContract:
                        dataModel.CryoStorageContract = (CryoStorageContract)entityExists;
                        dataModel.GeneratedAt = dataModel.CryoStorageContract.CreatedAt;
                        break;
                }

                // 5. Fill template HTML using RazorEngine
                string cacheKey = $"{template.Id}_{template.UpdatedAt?.ToString("yyyyMMddHHmmss")}" ?? "init";
                string htmlContent = Engine.Razor.RunCompile(
                    template.Content,
                    cacheKey,   // cache key  
                    null,
                    dataModel
                );

                var globalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                    DocumentTitle = "Generated Document",
                    Margins = new MarginSettings { Top = 20, Bottom = 20, Left = 10, Right = 10 }
                };

                string basePath = Path.Combine(_env.WebRootPath, "pdf");

                var objectSettings = new ObjectSettings
                {
                    HtmlContent = htmlContent,
                    WebSettings = new WebSettings
                    {
                        DefaultEncoding = "utf-8",
                        UserStyleSheet = Path.Combine(basePath, "pdf.css")
                    },
                    HeaderSettings = new HeaderSettings
                    {
                        HtmUrl = Path.Combine(basePath, "header.html"),
                        Spacing = 5
                    },
                    FooterSettings = new FooterSettings
                    {
                        HtmUrl = Path.Combine(basePath, "footer.html"),
                        Spacing = 5
                    }
                };

                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };

                byte[] pdfBytes = _pdfConverter.Convert(doc);

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
        }

        /// <summary>
        /// Edit & submit template - user fills data in WYSIWYG editor
        /// </summary>
        public async Task<BaseResponse<byte[]>> FillAndGeneratePdfAsync(string htmlTemplate, object userData)
        {
            try
            {
                // Fill user data
                string filledHtml = Engine.Razor.RunCompile(htmlTemplate, Guid.NewGuid().ToString(), null, userData);

                // Convert HTML -> PDF
                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = {
                        PaperSize = PaperKind.A4,
                        Orientation = Orientation.Portrait,
                        Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 }
                    },
                    Objects = {
                        new ObjectSettings { HtmlContent = filledHtml }
                    }
                };

                byte[] pdfBytes = _pdfConverter.Convert(doc);

                return new BaseResponse<byte[]>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "PDF generated successfully",
                    Data = pdfBytes
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FillAndGeneratePdfAsync error");
                return new BaseResponse<byte[]>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }
    }
}
