using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.ReponseModel
{
    /// <summary>
    /// Response cơ bản cho DocumentTemplate
    /// </summary>
    public class DocumentTemplateResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string TemplateType { get; set; } = string.Empty;
        public int Version { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// Response chi tiết cho DocumentTemplate, có thể mở rộng thêm thông tin liên quan (audit, usage, logs...)
    /// </summary>
    public class DocumentTemplateDetailResponse : DocumentTemplateResponse
    {
        public int? TotalVersion { get; set; }
        /// <summary>
        /// Có thể thêm thông tin creator / updater name
        /// </summary>
        public string? CreatedByName { get; set; }
        public string? UpdatedByName { get; set; }
    }
}
