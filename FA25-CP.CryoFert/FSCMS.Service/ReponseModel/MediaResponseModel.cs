using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.ReponseModel
{
    using System;

    namespace FSCMS.Service.ReponseModel
    {
        public class MediaResponse
        {
            public Guid Id { get; set; }
            public string FileName { get; set; } = default!;
            public string? OriginalFileName { get; set; }
            public string FilePath { get; set; } = default!;
            public string FileType { get; set; } = default!;
            public long FileSize { get; set; }
            public string? FileExtension { get; set; }
            public Guid? RelatedEntityId { get; set; }
            public string? RelatedEntityType { get; set; }

            public string? Title { get; set; }
            public string? Description { get; set; }
            public string? Category { get; set; }
            public string? Tags { get; set; }
            public DateTime? UploadDate { get; set; }
            public string? UploadedBy { get; set; }
            public Guid? UploadedByUserId { get; set; }
            public bool IsPublic { get; set; }
            public string? ThumbnailPath { get; set; }
            public string? StorageLocation { get; set; }
            public string? Notes { get; set; }
        }
    }

}