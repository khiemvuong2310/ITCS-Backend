using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho mẫu tinh trùng (kế thừa từ LabSample)
    /// Chứa thông tin chi tiết về tinh trùng
    /// </summary>
    public class LabSampleSperm : BaseEntity
    {
        public int LabSampleId { get; set; }
        
        public decimal? Volume { get; set; } // ml
        public decimal? Concentration { get; set; } // million/ml
        public decimal? Motility { get; set; } // %
        public decimal? ProgressiveMotility { get; set; } // %
        public decimal? Morphology { get; set; } // %
        public decimal? pH { get; set; }
        public string? Viscosity { get; set; }
        public string? Liquefaction { get; set; }
        public string? Color { get; set; }
        public int? TotalSpermCount { get; set; } // million
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual LabSample? LabSample { get; set; }
    }
}

