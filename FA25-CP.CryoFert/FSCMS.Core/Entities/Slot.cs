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
            TimeSpan endTime,
            bool isBooked = false
        )
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            IsBooked = isBooked;
        }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsBooked { get; set; } = false;
        public string? Notes { get; set; }

        //Navigation properties
        public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new List<DoctorSchedule>();
        public virtual Appointment? Appointment { get; set; }
    }
}
