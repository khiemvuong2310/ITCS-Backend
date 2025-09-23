using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Các loại cam kết/commit và hợp đồng
    /// </summary>
    public class Commitment : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class Contract : BaseEntity
    {
        public int PatientId { get; set; }
        public string ContractType { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; } = "Active"; // Active, Completed, Cancelled
        public string? Notes { get; set; }

        public virtual Patient? Patient { get; set; }
    }
}


