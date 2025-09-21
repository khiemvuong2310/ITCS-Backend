using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho phân tích tinh trùng
    /// Ghi lại kết quả xét nghiệm tinh trùng bao gồm số lượng, độ di động, hình thái
    /// </summary>
    public class SpermAnalysis : BaseEntity
    {
        public int SpecimenId { get; set; }
        public decimal Volume { get; set; } // ml
        public long Concentration { get; set; } // million/ml
        public decimal ProgressiveMotility { get; set; } // %
        public decimal TotalMotility { get; set; } // %
        public decimal Vitality { get; set; } // %
        public decimal NormalMorphology { get; set; } // %
        public string? Appearance { get; set; }
        public decimal pH { get; set; }
        public string Quality { get; set; } = string.Empty; // Good/Fair/Poor
        public bool IsPostWash { get; set; } = false;
        public DateTime AnalysisDate { get; set; }
        public int AnalyzedByUserId { get; set; }
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual Specimen? Specimen { get; set; }
        public virtual User? AnalyzedByUser { get; set; }
    }
}
