using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    // Bảng Doctor: Thông tin bác sĩ.
    // Quan hệ:
    // - 1-1 với Account (AccountId)
    // - 1-n với DoctorSchedule (lịch làm việc của bác sĩ)
    // - 1-n với Treatment (bác sĩ phụ trách nhiều đợt điều trị)
    public class Doctor : BaseEntity<Guid>
    {
        protected Doctor() : base() { }
        public Doctor(
            Guid id,
            string badgeId,
            string specialty,
            int yearsOfExperience,
            DateTime joinDate,
            bool isActive = true
        )
        {
            Id = id;
            BadgeId = badgeId;
            Specialty = specialty;
            YearsOfExperience = yearsOfExperience;
            JoinDate = joinDate;
            IsActive = isActive;
        }
        public string BadgeId { get; set; } = default!;
        public string Specialty { get; set; } = default!;
        public string? Certificates { get; set; }
        public string? LicenseNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Biography { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime? LeaveDate { get; set; }
        public Guid AccountId { get; set; }

        //Navigation properties
        public virtual Account? Account { get; set; }
        public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new List<DoctorSchedule>();
        public virtual ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();
    }
}
