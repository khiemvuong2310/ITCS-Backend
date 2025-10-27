using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
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
        public virtual DoctorSchedule? DoctorSchedule { get; set; }
        public virtual Appointment? Appointment { get; set; }
    }
}
