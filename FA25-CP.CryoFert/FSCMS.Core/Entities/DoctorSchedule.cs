using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    // Bảng DoctorSchedule: Lịch làm việc theo ngày/giờ của bác sĩ.
    // Quan hệ:
    // - n-1 tới Doctor (DoctorId)
    // - 1-n tới Slot (các khung giờ hẹn thuộc lịch này)
    public class DoctorSchedule : BaseEntity<Guid>
    {
        protected DoctorSchedule() : base() { }
        public DoctorSchedule(
            Guid id,
            Guid doctorId,
            DateTime workDate,
            TimeSpan startTime,
            TimeSpan endTime,
            bool isAvailable = true
        )
        {
            Id = id;
            DoctorId = doctorId;
            WorkDate = workDate;
            StartTime = startTime;
            EndTime = endTime;
            IsAvailable = isAvailable;
        }
        public Guid DoctorId { get; set; }
        public DateTime WorkDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string? Location { get; set; }
        public string? Notes { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();
    }
}
