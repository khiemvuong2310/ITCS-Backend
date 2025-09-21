using System;
using System.Collections.Generic;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho bể nitơ lỏng
    /// Quản lý các bể chứa nitơ lỏng để bảo quản mẫu vật ở nhiệt độ thấp
    /// </summary>
    public class CryobankTank : BaseEntity
    {
        public string TankCode { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Temperature { get; set; }
        public int Capacity { get; set; }
        public int CurrentOccupancy { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime LastMaintenanceDate { get; set; }
        public DateTime NextMaintenanceDate { get; set; }
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual ICollection<CryobankPosition>? Positions { get; set; } = new List<CryobankPosition>();
    }
}
