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
        public IFormFile File { get; set; } = default!;
        public string FileName { get; set; } = default!;
        public Guid? RelatedEntityId { get; set; } // ID của MedicalRecord, Patient, Treatment, etc.
        public string? RelatedEntityType { get; set; } // "MedicalRecord", "Patient", "Treatment", "LabTest"
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
        public string? RelatedEntityType { get; set; }
        public Guid? RelatedEntityId { get; set; }
    }
}