using System;
using System.Collections.Generic;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho thuốc trong hệ thống
    /// Danh mục thuốc độc lập
    /// </summary>
    public class Medicine : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? GenericName { get; set; }
        public string? BrandName { get; set; }
        public string? Manufacturer { get; set; }
        public string? Dosage { get; set; } // "500mg", "10ml", etc.
        public string? Form { get; set; } // "Viên nén", "Viên nang", "Siro", etc.
        public string? Unit { get; set; } // "viên", "vỉ", "lọ", etc.
        public decimal? Price { get; set; }
        public string? Indication { get; set; } // Chỉ định
        public string? Contraindication { get; set; } // Chống chỉ định
        public string? SideEffects { get; set; } // Tác dụng phụ
        public string? Storage { get; set; } // Bảo quản
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual ICollection<PrescriptionDetail>? PrescriptionDetails { get; set; } = new List<PrescriptionDetail>();
    }
}

