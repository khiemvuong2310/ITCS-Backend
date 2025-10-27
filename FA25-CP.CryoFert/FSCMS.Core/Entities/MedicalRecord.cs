using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a patient's medical record associated with a specific appointment.
    /// - One-to-One with <see cref="Appointment"/>.
    /// - One-to-Many with <see cref="Prescription"/>.
    /// </summary>
    public class MedicalRecord : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected MedicalRecord() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicalRecord"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the medical record.</param>
        /// <param name="appointmentId">The appointment associated with this medical record.</param>
        /// <param name="diagnosis">The primary diagnosis of the patient.</param>
        /// <param name="treatmentPlan">The proposed treatment plan.</param>
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

        // ────────────────────────────────
        // Medical Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the appointment ID linked to this record.
        /// </summary>
        public Guid AppointmentId { get; set; }

        /// <summary>
        /// Gets or sets the patient's main complaint or reason for visit.
        /// </summary>
        public string? ChiefComplaint { get; set; }

        /// <summary>
        /// Gets or sets the patient's medical history.
        /// </summary>
        public string? History { get; set; }

        /// <summary>
        /// Gets or sets the findings from the physical examination.
        /// </summary>
        public string? PhysicalExamination { get; set; }

        /// <summary>
        /// Gets or sets the diagnosis made by the physician.
        /// </summary>
        public string? Diagnosis { get; set; }

        /// <summary>
        /// Gets or sets the treatment plan for the patient.
        /// </summary>
        public string? TreatmentPlan { get; set; }

        /// <summary>
        /// Gets or sets the follow-up instructions given to the patient.
        /// </summary>
        public string? FollowUpInstructions { get; set; }

        /// <summary>
        /// Gets or sets the patient's vital signs (e.g., BP, HR, temperature).
        /// </summary>
        public string? VitalSigns { get; set; }

        /// <summary>
        /// Gets or sets the laboratory test results.
        /// </summary>
        public string? LabResults { get; set; }

        /// <summary>
        /// Gets or sets the results from imaging or diagnostic scans.
        /// </summary>
        public string? ImagingResults { get; set; }

        /// <summary>
        /// Gets or sets additional notes or remarks by the physician.
        /// </summary>
        public string? Notes { get; set; }

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        /// <summary>
        /// The appointment associated with this medical record.
        /// </summary>
        public virtual Appointment? Appointment { get; set; }

        /// <summary>
        /// The collection of prescriptions issued during this visit.
        /// </summary>
        public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}
