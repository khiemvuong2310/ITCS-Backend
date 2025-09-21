using System;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho timeline điều trị
    /// Theo dõi các bước và mốc thời gian trong quá trình điều trị
    /// </summary>
    public class TreatmentTimeline : BaseEntity
    {
        public int? TreatmentCycleId { get; set; }
        public int PatientId { get; set; }
        public string ProcessType { get; set; } = string.Empty; // IVF, IUI, Cryopreservation
        public string StepName { get; set; } = string.Empty;
        public string? StepDescription { get; set; }
        public TreatmentStatus Status { get; set; }
        public DateTime? PlannedDate { get; set; }
        public DateTime? ActualDate { get; set; }
        public int? AssignedToUserId { get; set; }
        public int UpdatedByUserId { get; set; }
        public string? Results { get; set; }
        public string? Notes { get; set; }
        public int? SortOrder { get; set; }

        // Navigation Properties
        public virtual TreatmentCycle? TreatmentCycle { get; set; }
        public virtual Patient? Patient { get; set; }
        public virtual User? AssignedToUser { get; set; }
        public virtual User? UpdatedByUser { get; set; }
    }
}
