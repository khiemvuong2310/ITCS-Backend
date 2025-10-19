using System;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho cuộc hẹn
    /// Many-to-One với TreatmentCycle
    /// One-to-One với Slot
    /// One-to-One với MedicalRecord
    /// </summary>
    public class Appointment : BaseEntity
    {
        public int TreatmentCycleId { get; set; }
        public int? SlotId { get; set; }
        
        public AppointmentType Type { get; set; }
        public AppointmentStatus Status { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Reason { get; set; }
        public string? Instructions { get; set; }
        public string? Notes { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public bool IsReminderSent { get; set; } = false;

        // Navigation Properties
        public virtual TreatmentCycle? TreatmentCycle { get; set; }
        public virtual Slot? Slot { get; set; }
        
        // One-to-One với MedicalRecord
        public virtual MedicalRecord? MedicalRecord { get; set; }
    }
}
