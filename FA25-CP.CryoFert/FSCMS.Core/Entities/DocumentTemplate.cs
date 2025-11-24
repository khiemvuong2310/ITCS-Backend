using System;
using System.Collections.Generic;
using FSCMS.Core.Enum;
using FSCMS.Core.Enums;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Bảng DocumentTemplate: lưu template cho nhiều loại văn bản (Agreement, Contract, Consent Form, ...)
    /// </summary>
    public class DocumentTemplate : BaseEntity<Guid>
    {
        protected DocumentTemplate() : base() { }

        public DocumentTemplate(
            Guid id,
            Guid createdBy,
            string name,
            string code,
            string content,
            string templateType,
            int version = 1,
            bool isActive = true
        )
        {
            Id = id;
            Name = name;
            Code = code;
            Content = content;
            TemplateType = templateType;
            Version = version;
            IsActive = isActive;
            CreatedBy = createdBy;
        }
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string TemplateType { get; set; } = default!;
        public int Version { get; set; } = 1;
        public bool IsActive { get; set; } = true;
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
