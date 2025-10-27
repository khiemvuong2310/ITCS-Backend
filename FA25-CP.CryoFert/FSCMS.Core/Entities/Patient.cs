using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;
using FSCMS.Core.Enums;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a patient within the fertility center system.
    /// Contains personal, medical, and relational information related to treatments and samples.
    /// </summary>
    public class Patient : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected Patient() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Patient"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the patient.</param>
        /// <param name="patientCode">The unique patient code (e.g., "PT-00001").</param>
        /// <param name="nationalId">The national identification number.</param>
        public Patient(Guid id, string patientCode, string nationalId)
        {
            Id = id;
            PatientCode = patientCode;
            NationalID = nationalId;
        }

        // ────────────────────────────────
        // Patient Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the unique patient code (e.g., "PT-00001").
        /// </summary>
        public string PatientCode { get; set; } = default!;

        /// <summary>
        /// Gets or sets the national identification number.
        /// </summary>
        public string NationalID { get; set; } = default!;

        /// <summary>
        /// Gets or sets the emergency contact name.
        /// </summary>
        public string? EmergencyContact { get; set; }

        /// <summary>
        /// Gets or sets the emergency contact phone number.
        /// </summary>
        public string? EmergencyPhone { get; set; }

        /// <summary>
        /// Gets or sets the insurance information.
        /// </summary>
        public string? Insurance { get; set; }

        /// <summary>
        /// Gets or sets the occupation of the patient.
        /// </summary>
        public string? Occupation { get; set; }

        // ────────────────────────────────
        // Health Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the patient's medical history.
        /// </summary>
        public string? MedicalHistory { get; set; }

        /// <summary>
        /// Gets or sets known allergies.
        /// </summary>
        public string? Allergies { get; set; }

        /// <summary>
        /// Gets or sets the blood type (e.g., "A+", "O−").
        /// </summary>
        public string? BloodType { get; set; }

        /// <summary>
        /// Gets or sets the height in centimeters.
        /// </summary>
        public decimal? Height { get; set; }

        /// <summary>
        /// Gets or sets the weight in kilograms.
        /// </summary>
        public decimal? Weight { get; set; }

        /// <summary>
        /// Indicates whether this patient record is active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets any additional notes or internal remarks.
        /// </summary>
        public string? Notes { get; set; }

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        /// <summary>
        /// The account linked to this patient (one-to-one relationship).
        /// </summary>
        public Guid AccountId { get; set; }
        public virtual Account? Account { get; set; }

        /// <summary>
        /// The collection of treatments associated with this patient.
        /// </summary>
        public virtual ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();

        /// <summary>
        /// The collection of laboratory samples belonging to this patient.
        /// </summary>
        public virtual ICollection<LabSample> LabSamples { get; set; } = new List<LabSample>();

        /// <summary>
        /// The collection of cryo-storage contracts related to this patient.
        /// </summary>
        public virtual ICollection<CryoStorageContract> CryoStorageContracts { get; set; } = new List<CryoStorageContract>();

        /// <summary>
        /// The relationships where this patient is listed as the first party.
        /// </summary>
        public virtual ICollection<Relationship> RelationshipsAsPatient1 { get; set; } = new List<Relationship>();

        /// <summary>
        /// The relationships where this patient is listed as the second party.
        /// </summary>
        public virtual ICollection<Relationship> RelationshipsAsPatient2 { get; set; } = new List<Relationship>();
    }
}
