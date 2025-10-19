using System;
using System.Collections.Generic;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho dịch vụ y tế trong hệ thống
    /// Bao gồm các dịch vụ như IVF, IUI, xét nghiệm, v.v.
    /// Many-to-One với ServiceCategory
    /// </summary>
    public class Service : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Code { get; set; }
        public string? Unit { get; set; } // "lần", "gói", "mẫu", etc.
        public int? Duration { get; set; } // Thời gian thực hiện (phút)
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        // Foreign Keys
        public int ServiceCategoryId { get; set; }

        // Navigation Properties
        public virtual ServiceCategory? ServiceCategory { get; set; }
        public virtual ICollection<ServiceRequestDetails>? ServiceRequestDetails { get; set; } = new List<ServiceRequestDetails>();
    }
}
