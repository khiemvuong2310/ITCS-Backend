using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using Microsoft.AspNetCore.Http;

namespace FSCMS.Service.RequestModel
{
    public class UploadMediaRequest
    {
        [Required(ErrorMessage = "File is required.")]
        public IFormFile File { get; set; } = default!;
        [Required(ErrorMessage = "FileName is required.")]
        public string FileName { get; set; } = default!;
        [Required(ErrorMessage = "RelatedEntityId is required.")]
        public Guid RelatedEntityId { get; set; } // ID của MedicalRecord, Patient, Treatment, etc.
        [Required(ErrorMessage = "RelatedEntityType is required.")]
        public EntityTypeMedia RelatedEntityType { get; set; } // "MedicalRecord", "Patient", "Treatment", "LabTest"
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; } // "Lab Result", "Medical Image", "Consent Form", "Report"
        public string? Tags { get; set; } // JSON array of tags
        public bool IsPublic { get; set; } = false;
        public string? Notes { get; set; }
    }

    public class UpdateMediaRequest
    {
        public string FileName { get; set; } = default!;
        // public Guid? RelatedEntityId { get; set; } // ID của MedicalRecord, Patient, Treatment, etc.
        // public string? RelatedEntityType { get; set; } // "MedicalRecord", "Patient", "Treatment", "LabTest"
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; } // "Lab Result", "Medical Image", "Consent Form", "Report"
        public string? Tags { get; set; } // JSON array of tags
        public bool IsPublic { get; set; } = false;
        public string? Notes { get; set; }
    }
    public class GetMediasRequest : PagingModel
    {
        public string? SearchTerm { get; set; }
        public EntityTypeMedia? RelatedEntityType { get; set; } = null;
        public Guid? RelatedEntityId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? UpLoadByUserId { get; set; }
    }

    public class UploadTemplateRequest
    {
        [Required(ErrorMessage = "TemplateType is required.")]
        public EntityTypeMedia TemplateType { get; set; }
        [Required(ErrorMessage = "File is required.")]
        public IFormFile File { get; set; } = default!;
    }

    public class GetTemplateRequest
    {
        [Required(ErrorMessage = "TemplateType is required.")]
        public EntityTypeMedia TemplateType { get; set; }
    }

    public class GetHtmlRequest
    {
        [Required(ErrorMessage = "RelatedEntityType is required.")]
        public EntityTypeMedia RelatedEntityType { get; set; } // "MedicalRecord", "Patient", "Treatment", "LabTest"
        [Required(ErrorMessage = "RelatedEntityId is required.")]
        public Guid RelatedEntityId { get; set; } // ID của MedicalRecord, Patient, Treatment, etc.
    }


    public enum EntityTypeMedia

    {
        MedicalRecord = 0,
        TreatmentCycle = 1,
        Account = 2,
        Agreement = 3,
        CryoStorageContract = 4,
        CryoImport = 5,
        CryoExport = 6,
        ServiceRequest = 7,
    }

    public class TemplateDataModel
    {
        public Patient? Patient { get; set; }
        public MedicalRecord? MedicalRecord { get; set; }
        public TreatmentCycle? TreatmentCycle { get; set; }
        public Agreement? Agreement { get; set; }
        public Appointment? Appointment { get; set; }
        public Treatment? Treatment { get; set; }
        public Doctor? Doctor { get; set; }
        public CryoStorageContract? CryoStorageContract { get; set; }
        public CryoPackage? CryoPackage { get; set; }

        public DateTime GeneratedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}