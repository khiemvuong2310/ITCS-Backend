using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho lịch sử xuất mẫu khỏi kho lạnh
    /// Ghi lại thông tin khi mẫu được lấy ra để sử dụng
    /// </summary>
    public class CryoExport : BaseEntity
    {
        public int LabSampleId { get; set; }
        public int CryoLocationId { get; set; }
        
        public DateTime ExportDate { get; set; }
        public string? ExportedBy { get; set; } // Người thực hiện
        public string? WitnessedBy { get; set; } // Người chứng kiến
        public string? Reason { get; set; } // "Transfer", "Thawing", "Disposal", etc.
        public string? Destination { get; set; }
        public string? Notes { get; set; }
        public bool IsThawed { get; set; } = false;
        public DateTime? ThawingDate { get; set; }
        public string? ThawingResult { get; set; }

        // Navigation Properties
        public virtual LabSample? LabSample { get; set; }
        public virtual CryoLocation? CryoLocation { get; set; }
    }
}

