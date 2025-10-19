using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho mẫu phôi (kế thừa từ LabSample)
    /// Chứa thông tin chi tiết về phôi
    /// </summary>
    public class LabSampleEmbryo : BaseEntity
    {
        public int LabSampleId { get; set; }
        
        public int DayOfDevelopment { get; set; } // Ngày phát triển (Day 3, Day 5, etc.)
        public string? Grade { get; set; } // Phân loại chất lượng phôi
        public int? CellCount { get; set; }
        public string? Morphology { get; set; }
        public bool IsBiopsied { get; set; } = false;
        public bool IsPGTTested { get; set; } = false;
        public string? PGTResult { get; set; }
        public string? FertilizationMethod { get; set; } // "IVF", "ICSI"
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual LabSample? LabSample { get; set; }
    }
}

