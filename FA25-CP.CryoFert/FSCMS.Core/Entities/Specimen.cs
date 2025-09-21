using System;
using System.Collections.Generic;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho mẫu vật sinh học
    /// Quản lý tinh trùng, trứng, phôi và các mẫu vật khác trong quá trình điều trị
    /// </summary>
    public class Specimen : BaseEntity
    {
        public int PatientId { get; set; }
        public SpecimenType Type { get; set; }
        public CryopreservationMethod? Method { get; set; }
        public string? BatchNumber { get; set; }
        public int? CryobankPositionId { get; set; }
        public DateTime? CollectionDate { get; set; }
        public DateTime? StoredDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public SpecimenStatus Status { get; set; }
        public int? TreatmentCycleId { get; set; }
        public int? IVFCycleId { get; set; }
        public int? IUICycleId { get; set; }
        public string? ConsentFormId { get; set; }
        public bool IsDoubleWitnessed { get; set; } = false;
        public int? WitnessUserId { get; set; }
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual Patient? Patient { get; set; }
        public virtual CryobankPosition? CryobankPosition { get; set; }
        public virtual TreatmentCycle? TreatmentCycle { get; set; }
        public virtual IVFCycle? IVFCycle { get; set; }
        public virtual IUICycle? IUICycle { get; set; }
        public virtual User? WitnessUser { get; set; }
        
        // Quality Assessments
        public virtual ICollection<OocyteAssessment>? OocyteAssessments { get; set; } = new List<OocyteAssessment>();
        public virtual ICollection<SpermAnalysis>? SpermAnalyses { get; set; } = new List<SpermAnalysis>();
        public virtual ICollection<EmbryoAssessment>? EmbryoAssessments { get; set; } = new List<EmbryoAssessment>();
        public virtual ICollection<PGTResult>? PGTResults { get; set; } = new List<PGTResult>();
    }
}
