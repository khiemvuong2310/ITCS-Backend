using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    public class MedicalRecord : BaseEntity<Guid>
    {
        protected MedicalRecord() : base() { }
        public MedicalRecord(
            Guid id,
            Guid appointmentId,
            string? diagnosis,
            string? treatmentPlan
        )
        {
            Id = id;
            AppointmentId = appointmentId;
            Diagnosis = diagnosis;
            TreatmentPlan = treatmentPlan;
        }
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
        public virtual Appointment? Appointment { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}
