using System;
using System.Collections.Generic;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho danh mục dịch vụ
    /// One-to-Many với Service
    /// </summary>
    public class ServiceCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Code { get; set; }
        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; }
        public string? Icon { get; set; }

        // Navigation Properties
        public virtual ICollection<Service>? Services { get; set; } = new List<Service>();
    }
}

