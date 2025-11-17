using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.ReponseModel
{
    public class MedicalRecordResponse
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }

        public string? ChiefComplaint { get; set; }
        public string? History { get; set; }
        public string? PhysicalExamination { get; set; }
        public string? Diagnosis { get; set; }
        public string? TreatmentPlan { get; set; }
        public string? FollowUpInstructions { get; set; }
        public string? VitalSigns { get; set; }
        public string? LabResults { get; set; }
        public string? ImagingResults { get; set; }
        public string? Notes { get; set; }

        // Optional: thông tin tối thiểu của Appointment
        public DateTime? AppointmentDate { get; set; }
        public Guid? PatientId { get; set; }
        public string? PatientName { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class MedicalRecordDetailResponse : MedicalRecordResponse
    {
        public List<PrescriptionResponse>? Prescriptions { get; set; }
        public int TotalPrescriptions => Prescriptions?.Count ?? 0;
    }
}
