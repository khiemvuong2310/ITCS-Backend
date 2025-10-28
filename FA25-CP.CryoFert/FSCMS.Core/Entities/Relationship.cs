using System;
using FSCMS.Core.Enum;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    // Bảng Relationship: Quan hệ giữa hai bệnh nhân (vợ/chồng, người hiến, người thân...).
    // Quan hệ:
    // - Mỗi bản ghi liên kết 2 Patient (Patient1Id, Patient2Id) theo kiểu RelationshipType
    public class Relationship : BaseEntity<Guid>
    {
        protected Relationship() : base() { }
        public Relationship(Guid id, Guid patient1Id, Guid patient2Id, RelationshipType relationshipType)
        {
            Id = id;
            Patient1Id = patient1Id;
            Patient2Id = patient2Id;
            RelationshipType = relationshipType;
        }
        public Guid Patient1Id { get; set; }
        public Guid Patient2Id { get; set; }
        public RelationshipType RelationshipType { get; set; }
        public DateTime? EstablishedDate { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual Patient? Patient1 { get; set; }
        public virtual Patient? Patient2 { get; set; }
    }
}
