using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho mối quan hệ giữa các bệnh nhân
    /// Bảng trung gian tạo quan hệ Many-to-Many giữa Patient và Patient
    /// Ví dụ: vợ/chồng, hiến tặng, v.v.
    /// </summary>
    public class Relationship : BaseEntity
    {
        public int Patient1Id { get; set; }
        public int Patient2Id { get; set; }
        
        public string RelationshipType { get; set; } = string.Empty; // "Spouse", "Donor", "Parent", "Child", etc.
        public DateTime? EstablishedDate { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public virtual Patient? Patient1 { get; set; }
        public virtual Patient? Patient2 { get; set; }
    }
}

