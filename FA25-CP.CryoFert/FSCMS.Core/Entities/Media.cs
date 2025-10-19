using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho tài liệu đa phương tiện (Media/File)
    /// Lưu trữ các tệp: ảnh, PDF, tài liệu xét nghiệm, v.v.
    /// Bảng độc lập, sẽ được liên kết với các bảng khác khi cần
    /// </summary>
    public class Media : BaseEntity
    {
        public string FileName { get; set; } = string.Empty;
        public string? OriginalFileName { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty; // "image/png", "application/pdf", etc.
        public long FileSize { get; set; } // Bytes
        public string? FileExtension { get; set; } // ".jpg", ".pdf", etc.
        public string? MimeType { get; set; }
        
        // Liên kết logic (không có FK vật lý)
        public int? RelatedEntityId { get; set; } // ID của MedicalRecord, Patient, Treatment, etc.
        public string? RelatedEntityType { get; set; } // "MedicalRecord", "Patient", "Treatment", "LabTest"
        
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; } // "Lab Result", "Medical Image", "Consent Form", "Report"
        public string? Tags { get; set; } // JSON array of tags
        public DateTime? UploadDate { get; set; }
        public string? UploadedBy { get; set; }
        public int? UploadedByUserId { get; set; }
        public bool IsPublic { get; set; } = false;
        public string? ThumbnailPath { get; set; }
        public string? StorageLocation { get; set; } // "Local", "Cloud", "S3", etc.
        public string? CloudUrl { get; set; }
        public string? Notes { get; set; }
    }
}

