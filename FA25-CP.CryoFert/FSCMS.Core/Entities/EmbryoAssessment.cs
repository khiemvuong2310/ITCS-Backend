using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho đánh giá phôi
    /// Ghi lại kết quả đánh giá chất lượng phôi theo từng ngày phát triển
    /// </summary>
    public class EmbryoAssessment : BaseEntity
    {
        public int SpecimenId { get; set; }
        public int DayOfDevelopment { get; set; }
        public int CellCount { get; set; }
        public decimal FragmentationRate { get; set; } // %
        public string? BlastocystGrade { get; set; } // AA, AB, BA, BB, etc.
        public string? ICMGrade { get; set; } // Inner Cell Mass Grade
        public string? TEGrade { get; set; } // Trophectoderm Grade
        public string Quality { get; set; } = string.Empty; // Good/Fair/Poor
        public bool IsSuitableForTransfer { get; set; }
        public bool IsSuitableForFreezing { get; set; }
        public DateTime AssessmentDate { get; set; }
        public int AssessedByUserId { get; set; }
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual Specimen? Specimen { get; set; }
        public virtual User? AssessedByUser { get; set; }
    }
}
