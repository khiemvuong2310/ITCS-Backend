using System;
using System.ComponentModel.DataAnnotations;
using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;

namespace FSCMS.Service.RequestModel
{
    /// <summary>
    /// Request tạo template mới
    /// </summary>
    public class CreateDocumentTemplateRequest
    {
        [Required(ErrorMessage = "Template name is required.")]
        [StringLength(200, ErrorMessage = "Template name cannot exceed 200 characters.")]
        public string Name { get; set; } = default!;
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; } = default!;

        [Required(ErrorMessage = "Template type is required.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "TemplateType must contain only letters.")]
        public TemplateType TemplateType { get; set; } = default!;

        public bool? IsActive { get; set; } = true;
    }

    /// <summary>
    /// Request cập nhật template
    /// </summary>
    public class UpdateDocumentTemplateRequest
    {
        [Required(ErrorMessage = "Template Id is required.")]
        public Guid TemplateId { get; set; }
        [StringLength(200, ErrorMessage = "Template name cannot exceed 200 characters.")]
        public string? Name { get; set; }
        public string? Content { get; set; }
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "TemplateType must contain only letters.")]
        public TemplateType? TemplateType { get; set; }

        public bool? IsActive { get; set; }
    }

    /// <summary>
    /// Request lấy template theo Id
    /// </summary>
    public class GetDocumentTemplateByIdRequest
    {
        [Required(ErrorMessage = "Template Id is required.")]
        public Guid TemplateId { get; set; }
    }

    /// <summary>
    /// Request lấy danh sách template, optional filter theo TemplateType
    /// </summary>
    public class GetDocumentTemplatesRequest : PagingModel
    {
        public string? SearchTerm { get; set; }
        public TemplateType? TemplateType { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Version must be greater than 0.")]
        public int? Version { get; set; }

        public bool? IsActive { get; set; }
    }

    public enum TemplateType

    {
        MedicalRecord = 0,
        TreatmentCycle = 1,
        Agreement = 2,
        CryoStorageContract = 3,
    }

    public class GenerateFilledPdfRequest
    {
        [Required(ErrorMessage = "TemplateType is required.")]
        public TemplateType TemplateType { get; set; }
        [Required(ErrorMessage = "RelatedEntityId is required.")]
        public Guid RelatedEntityId { get; set; }
    }

    public class TemplateDataModel
    {
        public Patient? Patient { get; set; }
        public MedicalRecord? MedicalRecord { get; set; }
        public TreatmentCycle? TreatmentCycle { get; set; }
        public Agreement? Agreement { get; set; }
        public CryoStorageContract? CryoStorageContract { get; set; }

        public DateTime GeneratedAt { get; set; }
    }

}
