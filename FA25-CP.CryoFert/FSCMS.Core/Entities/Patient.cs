using System;
using System.Collections.Generic;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho bệnh nhân trong hệ thống
    /// Chứa thông tin cá nhân, y tế và các liên kết đến các chu trình điều trị
    /// One-to-One với Account
    /// </summary>
    public class Patient : BaseEntity
    {
        public int AccountId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? EmergencyContact { get; set; }
        public string? EmergencyPhone { get; set; }
        public string NationalID { get; set; } = string.Empty;
        public string? MedicalHistory { get; set; }
        public string? Allergies { get; set; }
        public string? Insurance { get; set; }
        public string? Occupation { get; set; }
        public decimal? Height { get; set; } // cm
        public decimal? Weight { get; set; } // kg
        public string? BloodType { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual Account? Account { get; set; }
        
        // One-to-Many với Treatment
        public virtual ICollection<Treatment>? Treatments { get; set; } = new List<Treatment>();
        
        // One-to-Many với LabSample
        public virtual ICollection<LabSample>? LabSamples { get; set; } = new List<LabSample>();
        
        // One-to-Many với CryoStorageContract
        public virtual ICollection<CryoStorageContract>? CryoStorageContracts { get; set; } = new List<CryoStorageContract>();
        
        // Many-to-Many với chính nó (thông qua Relationship)
        public virtual ICollection<Relationship>? RelationshipsAsPatient1 { get; set; } = new List<Relationship>();
        public virtual ICollection<Relationship>? RelationshipsAsPatient2 { get; set; } = new List<Relationship>();
    }
}
