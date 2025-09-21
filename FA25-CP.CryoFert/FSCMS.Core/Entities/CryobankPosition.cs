using System;
using System.Collections.Generic;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho vị trí trong bể nitơ
    /// Xác định vị trí cụ thể của mẫu vật trong bể nitơ (cane, canister, position)
    /// </summary>
    public class CryobankPosition : BaseEntity
    {
        public int TankId { get; set; }
        public string Cane { get; set; } = string.Empty;
        public string Canister { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public bool IsOccupied { get; set; } = false;
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual CryobankTank? Tank { get; set; }
        public virtual ICollection<Specimen>? Specimens { get; set; } = new List<Specimen>();
    }
}
