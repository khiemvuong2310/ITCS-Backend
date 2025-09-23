using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Lịch làm việc của bác sĩ
    /// </summary>
    public class DoctorSchedule : BaseEntity
    {
        public int DoctorId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? Location { get; set; }
        public bool IsAvailable { get; set; } = true;

        public virtual Doctor? Doctor { get; set; }
    }
}


