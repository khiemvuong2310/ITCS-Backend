using System;
using System.Collections.Generic;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Nhà cung cấp dịch vụ nội bộ hoặc đối tác
    /// </summary>
    public class ServiceProvider : BaseEntity
    {
        public string Name { get; set; }
        public string? ProviderType { get; set; } // Internal, External
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Service>? Services { get; set; } = new List<Service>();
    }
}


