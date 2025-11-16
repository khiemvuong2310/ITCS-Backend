using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using FSCMS.Service.ReponseModel;

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

    public enum EntityTypeMedia

    {
        MedicalRecord = 0,
        TreatmentCycle = 1,
        Account = 2
    }
}