using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho đánh giá trứng
    /// Ghi lại kết quả đánh giá chất lượng trứng sau khi thu thập
    /// </summary>
    public class OocyteAssessment : BaseEntity
    {
        public int SpecimenId { get; set; }
        public int TotalOocytes { get; set; }
        public int MIIOocytes { get; set; } // Mature
        public int MIOocytes { get; set; }  // Immature
        public int GVOocytes { get; set; }  // Germinal Vesicle
        public int AbnormalOocytes { get; set; }
        public string? Morphology { get; set; }
        public string Quality { get; set; } = string.Empty; // Good/Fair/Poor
        public DateTime AssessmentDate { get; set; }
        public int AssessedByUserId { get; set; }
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual Specimen? Specimen { get; set; }
        public virtual User? AssessedByUser { get; set; }
    }
}
