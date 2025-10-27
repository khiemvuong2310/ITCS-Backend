using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a prescription record associated with a medical record.
    /// Each prescription contains one or more medicine details to be administered to a patient.
    /// </summary>
    public class Prescription : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected Prescription() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Prescription"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the prescription.</param>
        /// <param name="medicalRecordId">The ID of the related medical record.</param>
        /// <param name="prescriptionDate">The date when the prescription was issued.</param>
        public Prescription(Guid id, Guid medicalRecordId, DateTime prescriptionDate)
        {
            Id = id;
            MedicalRecordId = medicalRecordId;
            PrescriptionDate = prescriptionDate;
        }

        // ────────────────────────────────
        // Prescription Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the ID of the related medical record.
        /// </summary>
        public Guid MedicalRecordId { get; set; }

        /// <summary>
        /// Gets or sets the date when the prescription was issued.
        /// </summary>
        public DateTime PrescriptionDate { get; set; }

        /// <summary>
        /// Gets or sets the diagnosis related to this prescription.
        /// </summary>
        public string? Diagnosis { get; set; }

        /// <summary>
        /// Gets or sets the instructions for how the medicines should be taken.
        /// </summary>
        public string? Instructions { get; set; }

        /// <summary>
        /// Gets or sets additional notes or remarks.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Indicates whether the prescription has been filled or dispensed.
        /// </summary>
        public bool IsFilled { get; set; } = false;

        /// <summary>
        /// Gets or sets the date when the prescription was filled.
        /// </summary>
        public DateTime? FilledDate { get; set; }

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        /// <summary>
        /// The medical record associated with this prescription.
        /// </summary>
        public virtual MedicalRecord? MedicalRecord { get; set; }

        /// <summary>
        /// The collection of medicine details within this prescription.
        /// </summary>
        public virtual ICollection<PrescriptionDetail> PrescriptionDetails { get; set; } = new List<PrescriptionDetail>();
    }
}
