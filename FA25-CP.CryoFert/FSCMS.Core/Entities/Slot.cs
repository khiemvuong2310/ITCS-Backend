using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho khe hẹn (time slot) trong lịch làm việc của bác sĩ
    /// Many-to-One với DoctorSchedule
    /// One-to-One với Appointment
    /// </summary>
    public class Slot : BaseEntity
    {
        public int DoctorScheduleId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsBooked { get; set; } = false;
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual DoctorSchedule? DoctorSchedule { get; set; }
        public virtual Appointment? Appointment { get; set; }
    }
}

