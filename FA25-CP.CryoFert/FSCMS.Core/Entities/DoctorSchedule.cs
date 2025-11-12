using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    // Bảng DoctorSchedule: Lịch làm việc theo ngày/giờ của bác sĩ.
    // Quan hệ:
    // - n-1 tới Doctor (DoctorId)
    // - n-1 tới Slot (SlotId) - một lịch làm việc thuộc về một slot cụ thể
    public class DoctorSchedule : BaseEntity<Guid>
    {
        protected DoctorSchedule() : base() { }
        public DoctorSchedule(
            Guid id,
            Guid doctorId,
            Guid slotId,
            DateOnly workDate,
            bool isAvailable = true
        )
        {
            Id = id;
            DoctorId = doctorId;
            SlotId = slotId;
            WorkDate = workDate;
            IsAvailable = isAvailable;
        }
        public Guid DoctorId { get; set; }
        public Guid SlotId { get; set; }
        public DateOnly WorkDate { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string? Location { get; set; }
        public string? Notes { get; set; }

        //Navigation properties
        public virtual Doctor? Doctor { get; set; }
        public virtual Slot? Slot { get; set; }
    }
}
