using System;
using System.Collections.Generic;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho gói lưu trữ lạnh
    /// One-to-Many với CryoStorageContract
    /// </summary>
    public class CryoPackage : BaseEntity
    {
        public string PackageName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int DurationMonths { get; set; }
        public int MaxSamples { get; set; } // Số lượng mẫu tối đa
        public string? SampleType { get; set; } // "Embryo", "Sperm", "Oocyte", "All"
        public bool IncludesInsurance { get; set; } = false;
        public decimal? InsuranceAmount { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Benefits { get; set; } // JSON hoặc text mô tả quyền lợi
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual ICollection<CryoStorageContract>? CryoStorageContracts { get; set; } = new List<CryoStorageContract>();
    }
}

