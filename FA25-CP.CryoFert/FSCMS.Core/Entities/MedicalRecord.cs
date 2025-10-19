using System;
using System.Collections.Generic;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho hồ sơ bệnh án
    /// One-to-One với Appointment
    /// One-to-Many với Prescription
    /// </summary>
    public class MedicalRecord : BaseEntity
    {
        public int AppointmentId { get; set; }
        
        public string? ChiefComplaint { get; set; } // Lý do khám
        public string? History { get; set; } // Tiền sử
        public string? PhysicalExamination { get; set; } // Khám lâm sàng
        public string? Diagnosis { get; set; } // Chẩn đoán
        public string? TreatmentPlan { get; set; } // Kế hoạch điều trị
        public string? FollowUpInstructions { get; set; } // Hướng dẫn theo dõi
        public string? VitalSigns { get; set; } // Dấu hiệu sinh tồn
        public string? LabResults { get; set; } // Kết quả xét nghiệm
        public string? ImagingResults { get; set; } // Kết quả chẩn đoán hình ảnh
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual Appointment? Appointment { get; set; }
        public virtual ICollection<Prescription>? Prescriptions { get; set; } = new List<Prescription>();
    }
}

