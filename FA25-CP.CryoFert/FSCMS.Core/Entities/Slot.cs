using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    // Bảng Slot: Khung giờ hẹn cố định (4 slot).
    // Quan hệ:
    // - 1-n với DoctorSchedule (một slot có thể được sử dụng bởi nhiều lịch làm việc)
    // - 1-1 với Appointment (một slot gắn với tối đa 1 cuộc hẹn)
    public class Slot : BaseEntity<Guid>
    {
        protected Slot() : base() { }
        public Slot(
            Guid id,
            TimeSpan startTime,
			TimeSpan endTime
        )
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
        }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? Notes { get; set; }

        //Navigation properties
        public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new List<DoctorSchedule>();
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
