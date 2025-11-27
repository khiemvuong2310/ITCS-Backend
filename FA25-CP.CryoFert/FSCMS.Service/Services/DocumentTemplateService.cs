using AutoMapper;
using FSCMS.Core.Entities;
using FSCMS.Data.UnitOfWork;
using FSCMS.Service.Interfaces;
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
using Microsoft.AspNetCore.Mvc.Razor; 
using Microsoft.AspNetCore.Mvc.ViewFeatures; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Routing; 
using Microsoft.AspNetCore.Mvc.Abstractions; 
using Microsoft.AspNetCore.Mvc.ModelBinding; 
using Microsoft.AspNetCore.Mvc.Rendering; 
using FSCMS.Service.ReponseModel; 
using FSCMS.Service.RequestModel; 
using FSCMS.Core.Enum; 
namespace FSCMS.Service.Services 
{ 
    public class DocumentTemplateService : IDocumentTemplateService 
    { 
        private readonly IUnitOfWork _unitOfWork; 
        private readonly ILogger<DocumentTemplateService> _logger; 
        private readonly IConverter _pdfConverter; 
        private readonly IWebHostEnvironment _env; 
        private readonly IRazorViewEngine _viewEngine; 
        private readonly ITempDataProvider _tempDataProvider; 
        private readonly IServiceProvider _serviceProvider; 
        public DocumentTemplateService( 
            IRazorViewEngine viewEngine, 
            ITempDataProvider tempDataProvider, 
            IServiceProvider serviceProvider, 
            IWebHostEnvironment env, 
            IUnitOfWork unitOfWork, 
            ILogger<DocumentTemplateService> logger, 
            IConverter pdfConverter) 
        { 
            _viewEngine = viewEngine; 
            _tempDataProvider = tempDataProvider; 
            _serviceProvider = serviceProvider; 
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork)); 
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
            _env = env; 
            _pdfConverter = pdfConverter ?? throw new ArgumentNullException(nameof(pdfConverter)); 
        } 
        public async Task<BaseResponse<byte[]>> GenerateFilledPdfAsync(GenerateFilledPdfRequest request) 
        { 
            string tempHeaderPath = null!; 
            string tempFooterPath = null!; 
            try 
            { 
                object? entityExists = request.TemplateType switch 
                { 
                TemplateType.MedicalRecord => await _unitOfWork.Repository<MedicalRecord>()
                    .AsQueryable()
                    .Include(p => p.Appointment)
                    .ThenInclude(p => p.TreatmentCycle)
                    .ThenInclude(p => p.Treatment)
                    .Include(p => p.Prescriptions)
                    .ThenInclude(p => p.PrescriptionDetails)
                    .ThenInclude(p => p.Medicine)
                    .FirstOrDefaultAsync(p => p.Id == request.RelatedEntityId && !p.IsDeleted),
                TemplateType.Agreement => await _unitOfWork.Repository<Agreement>()
                    .AsQueryable()
                    .Include(x => x.Treatment)
                    .ThenInclude(x => x.Doctor)
                    .ThenInclude(x => x.Account)
                    .FirstOrDefaultAsync(m => m.Id == request.RelatedEntityId && !m.IsDeleted), 
                TemplateType.CryoStorageContract => await _unitOfWork.Repository<CryoStorageContract>()
                    .AsQueryable() .Include(m => m.CryoPackage)
                    .Include(m => m.CPSDetails) .ThenInclude(cd => cd.LabSample)
                    .FirstOrDefaultAsync(m => m.Id == request.RelatedEntityId && !m.IsDeleted), _ => null };

                if (entityExists == null) 
                { 
                    return new BaseResponse<byte[]> 
                    { 
                        Code = StatusCodes.Status404NotFound, 
                        SystemCode = "ENTITY_NOT_FOUND", 
                        Message = $"Related entity {request.TemplateType} with ID {request.RelatedEntityId} not found or already save file", Data = null 
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
                    .AsQueryable() .Include(p => p.Account)
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
                switch (request.TemplateType) 
                { 
                    case TemplateType.MedicalRecord: 
                        var medicalRecord = (MedicalRecord)entityExists; 
                        dataModel.MedicalRecord = medicalRecord; 
                        dataModel.Appointment = medicalRecord.Appointment; 
                        dataModel.Treatment = medicalRecord.Appointment?.TreatmentCycle?.Treatment; 
                        dataModel.TreatmentCycle = medicalRecord.Appointment?.TreatmentCycle; 
                        dataModel.GeneratedAt = medicalRecord.CreatedAt; 
                        dataModel.UpdatedAt = medicalRecord.UpdatedAt;
                        break;  
                    case TemplateType.Agreement: 
                        var agreement = (Agreement)entityExists; 
                        dataModel.Agreement = agreement; 
                        dataModel.Doctor = agreement.Treatment?.Doctor; 
                        dataModel.Treatment = agreement.Treatment;
                        dataModel.GeneratedAt = agreement.CreatedAt; 
                        dataModel.UpdatedAt = agreement.UpdatedAt; 
                        break; 
                    case TemplateType.CryoStorageContract: 
                        var cryoStorageContract = (CryoStorageContract)entityExists; 
                        dataModel.CryoStorageContract = cryoStorageContract; 
                        dataModel.CryoPackage = cryoStorageContract.CryoPackage; 
                        dataModel.GeneratedAt = cryoStorageContract.CreatedAt; 
                        dataModel.UpdatedAt = cryoStorageContract.UpdatedAt; 
                        break; 
                } 
                string viewName = request.TemplateType switch 
                { 
                    TemplateType.MedicalRecord => "PdfTemplates/MedicalRecord",
                    TemplateType.Agreement => "PdfTemplates/Agreement", 
                    TemplateType.CryoStorageContract => "PdfTemplates/CryoStorageContract", 
                    _ => throw new Exception("Template not found") 
                }; 
                string htmlContent = await RenderViewToStringAsync(viewName, dataModel); 
                string headerHtml = await RenderViewToStringAsync("PdfTemplates/Header", dataModel); 
                string footerHtml = await RenderViewToStringAsync("PdfTemplates/Footer", dataModel); 
                tempHeaderPath = Path.Combine(Path.GetTempPath(), $"header_{Guid.NewGuid()}.html"); 
                tempFooterPath = Path.Combine(Path.GetTempPath(), $"footer_{Guid.NewGuid()}.html"); 
                await File.WriteAllTextAsync(tempHeaderPath, headerHtml); 
                await File.WriteAllTextAsync(tempFooterPath, footerHtml); 
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
                        Bottom = 20, 
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
                        UserStyleSheet = cssPath 
                    }, 
                    HeaderSettings = new HeaderSettings 
                    { 
                        HtmUrl = tempHeaderPath, 
                        Spacing = 5 
                    }, 
                    FooterSettings = new FooterSettings 
                    { 
                        HtmUrl = tempFooterPath, 
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
            } catch (Exception ex) 
            { 
                _logger.LogError(ex, "GenerateFilledPdfAsync error"); 
                return new BaseResponse<byte[]> 
                { 
                    Code = StatusCodes.Status500InternalServerError, 
                    Message = ex.Message 
                }; 
            } 
            finally 
            { 
                if (tempHeaderPath != null && File.Exists(tempHeaderPath)) 
                    File.Delete(tempHeaderPath); 
                if (tempFooterPath != null && File.Exists(tempFooterPath))
                    File.Delete(tempFooterPath); 
            } 
        } 
        private async Task<string> RenderViewToStringAsync(string viewName, object model) 
        { 
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var routeData = new RouteData(); 
            var actionContext = new ActionContext( httpContext, routeData, new ActionDescriptor() );
            using var sw = new StringWriter();
            var viewResult = _viewEngine.FindView(actionContext, viewName, false); 
            if (!viewResult.Success) 
            { 
                throw new Exception($"View {viewName} not found."); 
            } 
            var viewDictionary = new ViewDataDictionary( new EmptyModelMetadataProvider(), new ModelStateDictionary() ) { Model = model }; 
            var viewContext = new ViewContext( actionContext, viewResult.View, viewDictionary, new TempDataDictionary(actionContext.HttpContext, _tempDataProvider), sw, new HtmlHelperOptions() ); 
            await viewResult.View.RenderAsync(viewContext);
            return sw.ToString();
        }
    } 
}