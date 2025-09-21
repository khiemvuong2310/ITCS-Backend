using System;
using System.Collections.Generic;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho chu trình điều trị
    /// Quản lý toàn bộ quá trình điều trị của bệnh nhân (IVF, IUI, FET)
    /// </summary>
    public class TreatmentCycle : BaseEntity
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string TreatmentType { get; set; } = string.Empty; // IVF, IUI, FET
        public string Protocol { get; set; } = string.Empty;
        public TreatmentStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual Patient? Patient { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public virtual ICollection<CycleMonitoring>? Monitorings { get; set; } = new List<CycleMonitoring>();
        public virtual ICollection<TreatmentTimeline>? Timelines { get; set; } = new List<TreatmentTimeline>();
        public virtual IVFCycle? IVFCycle { get; set; }
        public virtual IUICycle? IUICycle { get; set; }
    }
}
