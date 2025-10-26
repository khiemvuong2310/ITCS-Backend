using System;
using System.Collections.Generic;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho bác sĩ trong hệ thống
    /// Chứa thông tin chuyên môn, chứng chỉ và lịch làm việc của bác sĩ
    /// One-to-One với Account
    /// </summary>
    public class Doctor : BaseEntity
    {
        public int AccountId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string? Certificates { get; set; }
        public string? LicenseNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Biography { get; set; }

        // Navigation Properties
        public virtual Account? Account { get; set; }
        
        // One-to-Many với DoctorSchedule
        public virtual ICollection<DoctorSchedule>? DoctorSchedules { get; set; } = new List<DoctorSchedule>();
        
        // One-to-Many với Treatment
        public virtual ICollection<Treatment>? Treatments { get; set; } = new List<Treatment>();
    }
}
