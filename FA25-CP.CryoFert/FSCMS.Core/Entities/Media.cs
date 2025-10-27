using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho tài liệu đa phương tiện (Media/File)
    /// Lưu trữ các tệp: ảnh, PDF, tài liệu xét nghiệm, v.v.
    /// Bảng độc lập, sẽ được liên kết với các bảng khác khi cần
    /// </summary>
    public class Media : BaseEntity<Guid>
    {
        protected Media() : base() { }

        public Media(
            Guid id,
            string fileName,
            string filePath,
            string fileType,
            long fileSize,
            bool isPublic = false
        )
        {
            Id = id;
            FileName = fileName;
            FilePath = filePath;
            FileType = fileType;
            FileSize = fileSize;
            IsPublic = isPublic;
            UploadDate = DateTime.UtcNow;
        }

        public string FileName { get; set; } = default!;
        public string? OriginalFileName { get; set; }
        public string FilePath { get; set; } = default!;
        public string FileType { get; set; } = default!; // "image/png", "application/pdf", etc.
        public long FileSize { get; set; } // Bytes
        public string? FileExtension { get; set; } // ".jpg", ".pdf", etc.
        public string? MimeType { get; set; }

        // Liên kết logic (không có FK vật lý)
        public Guid? RelatedEntityId { get; set; } // ID của MedicalRecord, Patient, Treatment, etc.
        public string? RelatedEntityType { get; set; } // "MedicalRecord", "Patient", "Treatment", "LabTest"

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; } // "Lab Result", "Medical Image", "Consent Form", "Report"
        public string? Tags { get; set; } // JSON array of tags
        public DateTime? UploadDate { get; set; }
        public string? UploadedBy { get; set; }
        public Guid? UploadedByUserId { get; set; }
        public bool IsPublic { get; set; } = false;
        public string? ThumbnailPath { get; set; }
        public string? StorageLocation { get; set; } // "Local", "Cloud", "S3", etc.
        public string? CloudUrl { get; set; }
        public string? Notes { get; set; }
    }
}
