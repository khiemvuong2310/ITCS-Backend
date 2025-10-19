using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho mẫu trứng (kế thừa từ LabSample)
    /// Chứa thông tin chi tiết về tế bào trứng
    /// </summary>
    public class LabSampleOocyte : BaseEntity
    {
        public int LabSampleId { get; set; }
        
        public string MaturityStage { get; set; } = string.Empty; // "MII", "MI", "GV"
        public string? Quality { get; set; } // "A", "B", "C"
        public bool IsMature { get; set; }
        public DateTime? RetrievalDate { get; set; }
        public string? CumulusCells { get; set; } // Tình trạng tế bào cumulus
        public string? CytoplasmAppearance { get; set; }
        public bool IsVitrified { get; set; } = false;
        public DateTime? VitrificationDate { get; set; }
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual LabSample? LabSample { get; set; }
    }
}

