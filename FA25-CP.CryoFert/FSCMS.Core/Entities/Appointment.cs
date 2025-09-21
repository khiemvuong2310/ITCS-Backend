using System;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho lịch hẹn khám bệnh
    /// Quản lý thông tin lịch hẹn giữa bệnh nhân và bác sĩ
    /// </summary>
    public class Appointment : BaseEntity
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public AppointmentType Type { get; set; }
        public AppointmentStatus Status { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? TreatmentCycleId { get; set; }
        public string? Reason { get; set; }
        public string? Instructions { get; set; }
        public string? Notes { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public bool IsReminderSent { get; set; } = false;

        // Navigation Properties
        public virtual Patient? Patient { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public virtual TreatmentCycle? TreatmentCycle { get; set; }
    }
}
