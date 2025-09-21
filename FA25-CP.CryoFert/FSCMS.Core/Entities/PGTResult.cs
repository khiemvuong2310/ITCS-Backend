using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho kết quả xét nghiệm di truyền tiền làm tổ (PGT)
    /// Ghi lại kết quả xét nghiệm di truyền của phôi để sàng lọc bất thường
    /// </summary>
    public class PGTResult : BaseEntity
    {
        public int SpecimenId { get; set; }
        public string TestType { get; set; } = string.Empty; // PGT-A, PGT-M, PGT-SR
        public string Result { get; set; } = string.Empty; // Normal, Abnormal, Mosaic
        public string? ChromosomalAbnormalities { get; set; }
        public string? GeneticFindings { get; set; }
        public bool IsTransferRecommended { get; set; }
        public DateTime TestDate { get; set; }
        public DateTime ResultDate { get; set; }
        public string? Laboratory { get; set; }
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual Specimen? Specimen { get; set; }
    }
}
