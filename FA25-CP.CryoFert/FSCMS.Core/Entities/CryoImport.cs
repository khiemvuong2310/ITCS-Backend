using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho lịch sử nhập mẫu vào kho lạnh
    /// Ghi lại thông tin khi mẫu được đưa vào lưu trữ
    /// </summary>
    public class CryoImport : BaseEntity
    {
        public int LabSampleId { get; set; }
        public int CryoLocationId { get; set; }
        
        public DateTime ImportDate { get; set; }
        public string? ImportedBy { get; set; } // Người thực hiện
        public string? WitnessedBy { get; set; } // Người chứng kiến
        public string? Method { get; set; } // Slow freezing, Vitrification
        public decimal? Temperature { get; set; }
        public string? Notes { get; set; }
        public string? Reason { get; set; }

        // Navigation Properties
        public virtual LabSample? LabSample { get; set; }
        public virtual CryoLocation? CryoLocation { get; set; }
    }
}

