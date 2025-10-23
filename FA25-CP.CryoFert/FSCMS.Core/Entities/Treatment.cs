using System;
using System.Collections.Generic;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho liệu trình điều trị
    /// Many-to-One với Patient và Doctor
    /// One-to-Many với TreatmentCycle
    /// One-to-One với TreatmentIVF (nếu có)
    /// </summary>
    public class Treatment : BaseEntity
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        
        public string TreatmentName { get; set; } = string.Empty;

        //Sửa lại thành ENUM cho TreatmentType nếu cần
        public string? TreatmentType { get; set; } // "IVF", "IUI", "Consultation", etc.
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TreatmentStatus Status { get; set; }
        public string? Diagnosis { get; set; }
        public string? Goals { get; set; }
        public string? Notes { get; set; }
        public decimal? EstimatedCost { get; set; }
        public decimal? ActualCost { get; set; }

        // Navigation Properties
        public virtual Patient? Patient { get; set; }
        public virtual Doctor? Doctor { get; set; }
        
        // One-to-Many với TreatmentCycle
        public virtual ICollection<TreatmentCycle>? TreatmentCycles { get; set; } = new List<TreatmentCycle>();
        
        // One-to-One với TreatmentIVF (có thể có hoặc không)
        public virtual TreatmentIVF? TreatmentIVF { get; set; }
    }
}

