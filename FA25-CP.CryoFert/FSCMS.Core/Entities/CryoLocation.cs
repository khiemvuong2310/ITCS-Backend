using System;
using System.Collections.Generic;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho vị trí lưu trữ trong kho lạnh
    /// Quản lý vị trí cụ thể của mẫu trong tank
    /// Many-to-One với CryobankTank (có thể tái sử dụng entity cũ)
    /// </summary>
    public class CryoLocation : BaseEntity
    {
        public string LocationCode { get; set; } = string.Empty; // Unique identifier
        public string? TankName { get; set; }
        public string? Canister { get; set; }
        public string? Cane { get; set; }
        public string? Position { get; set; }
        public int Capacity { get; set; }
        public int CurrentOccupancy { get; set; } = 0;
        public bool IsAvailable { get; set; } = true;
        public decimal? Temperature { get; set; }
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual ICollection<LabSample>? LabSamples { get; set; } = new List<LabSample>();
    }
}

