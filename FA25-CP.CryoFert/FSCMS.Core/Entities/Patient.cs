using System;
using System.Collections.Generic;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho bệnh nhân trong hệ thống
    /// Chứa thông tin cá nhân, y tế và các liên kết đến các chu trình điều trị
    /// </summary>
    public class Patient : BaseEntity
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
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
        public virtual User? User { get; set; }
        public virtual ICollection<TreatmentCycle>? TreatmentCycles { get; set; } = new List<TreatmentCycle>();
        public virtual ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<Encounter>? Encounters { get; set; } = new List<Encounter>();
        public virtual ICollection<ServiceRequest>? ServiceRequests { get; set; } = new List<ServiceRequest>();
        public virtual ICollection<Feedback>? Feedbacks { get; set; } = new List<Feedback>();
        public virtual ICollection<Payment>? Payments { get; set; } = new List<Payment>();
        public virtual ICollection<Specimen>? Specimens { get; set; } = new List<Specimen>();
        public virtual ICollection<ConsentForm>? ConsentForms { get; set; } = new List<ConsentForm>();
        public virtual ICollection<PatientRecord>? PatientRecords { get; set; } = new List<PatientRecord>();
    }
}
