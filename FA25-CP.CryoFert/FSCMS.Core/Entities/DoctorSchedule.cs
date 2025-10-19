using System;
using System.Collections.Generic;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho lịch làm việc của bác sĩ
    /// Quản lý thông tin về ngày làm việc và các ca làm việc
    /// Many-to-One với Doctor
    /// One-to-Many với Slot
    /// </summary>
    public class DoctorSchedule : BaseEntity
    {
        public int DoctorId { get; set; }
        public DateTime WorkDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string? Location { get; set; }
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual Doctor? Doctor { get; set; }
        public virtual ICollection<Slot>? Slots { get; set; } = new List<Slot>();
    }
}


