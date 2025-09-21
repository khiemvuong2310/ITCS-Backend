using System;
using System.Collections.Generic;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho gói dịch vụ
    /// Tập hợp nhiều dịch vụ thành một gói với giá ưu đãi và thời hạn sử dụng
    /// </summary>
    public class ServicePackage : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Currency { get; set; } = "VND";
        public int ValidityDays { get; set; } // Package validity in days
        public bool IsActive { get; set; } = true;
        public string? Terms { get; set; }
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual ICollection<ServicePackageItem>? PackageItems { get; set; } = new List<ServicePackageItem>();
        public virtual ICollection<ServiceRequest>? ServiceRequests { get; set; } = new List<ServiceRequest>();
    }
}
