using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    // Bảng Slot: Khung giờ hẹn thuộc một lịch làm việc cụ thể.
    // Quan hệ:
    // - n-1 tới DoctorSchedule (DoctorScheduleId)
    // - 1-1 với Appointment (một slot gắn với tối đa 1 cuộc hẹn)
    public class Slot : BaseEntity<Guid>
    {
        protected Slot() : base() { }
        public Slot(
            Guid id,
            Guid doctorScheduleId,
            TimeSpan startTime,
            TimeSpan endTime,
            bool isBooked = false
        )
        {
            Id = id;
            DoctorScheduleId = doctorScheduleId;
            StartTime = startTime;
            EndTime = endTime;
            IsBooked = isBooked;
        }
        public Guid DoctorScheduleId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsBooked { get; set; } = false;
        public string? Notes { get; set; }

        //Navigation properties
        public virtual DoctorSchedule? DoctorSchedule { get; set; }
        public virtual Appointment? Appointment { get; set; }
    }
}
