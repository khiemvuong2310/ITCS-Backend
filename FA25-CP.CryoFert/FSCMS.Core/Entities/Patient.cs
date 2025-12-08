using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;
using FSCMS.Core.Enums;

namespace FSCMS.Core.Entities
{
    // Bảng Patient: Thông tin hồ sơ bệnh nhân.
    // Quan hệ:
    // - 1-1 với Account (Shared PK: Id == Account.Id)
    // - 1-n với Treatment (bệnh nhân có nhiều đợt điều trị)
    // - 1-n với LabSample (nhiều mẫu xét nghiệm/lab)
    // - 1-n với CryoStorageContract (nhiều hợp đồng lưu trữ cryo)
    // - n-n tự thân thông qua Relationship (Patient1/Patient2)
    public class Patient : BaseEntity<Guid>
    {
        protected Patient() : base() { }
        public Patient(Guid id, string patientCode, string? nationalId)
        {
            Id = id;
            PatientCode = patientCode;
            NationalID = nationalId ?? string.Empty;
        }
        public string PatientCode { get; set; } = default!;
        public string NationalID { get; set; } = string.Empty;
        public string? EmergencyContact { get; set; }
        public string? EmergencyPhone { get; set; }
        public string? Insurance { get; set; }
        public string? Occupation { get; set; }
        public string? MedicalHistory { get; set; }
        public string? Allergies { get; set; }
        public string? BloodType { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        //Navigate properties
        public virtual Account? Account { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<LabSample> LabSamples { get; set; } = new List<LabSample>();
        public virtual ICollection<CryoStorageContract> CryoStorageContracts { get; set; } = new List<CryoStorageContract>();
        public virtual ICollection<Relationship> RelationshipsAsPatient1 { get; set; } = new List<Relationship>();
        public virtual ICollection<Relationship> RelationshipsAsPatient2 { get; set; } = new List<Relationship>();
    }
}
