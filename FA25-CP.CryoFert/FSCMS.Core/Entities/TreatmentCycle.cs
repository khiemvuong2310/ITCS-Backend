using System;
using System.Collections.Generic;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho chu kỳ điều trị
    /// Many-to-One với Treatment
    /// One-to-Many với Appointment
    /// </summary>
    public class TreatmentCycle : BaseEntity
    {
        public int TreatmentId { get; set; }
        
        public string CycleName { get; set; } = string.Empty;
        public int CycleNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TreatmentStatus Status { get; set; }
        public string? Protocol { get; set; }
        public string? Notes { get; set; }
        public decimal? Cost { get; set; }

        // Navigation Properties
        public virtual Treatment? Treatment { get; set; }
        
        // One-to-Many với Appointment
        public virtual ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();
    }
}
