using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a treatment plan for a patient.
    /// Many-to-One with Patient and Doctor.
    /// One-to-Many with TreatmentCycle.
    /// One-to-One with TreatmentIVF (if applicable).
    /// </summary>
    public class Treatment : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected Treatment() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Treatment"/> class.
        /// </summary>
        public Treatment(
            Guid id,
            Guid patientId,
            Guid doctorId,
            string treatmentName,
            TreatmentType treatmentType,
            DateTime startDate
        )
        {
            Id = id;
            PatientId = patientId;
            DoctorId = doctorId;
            TreatmentName = treatmentName;
            TreatmentType = treatmentType;
            StartDate = startDate;
            Status = TreatmentStatus.Planned;
        }

        // ────────────────────────────────
        // Treatment Information
        // ────────────────────────────────

        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }

        public string TreatmentName { get; set; } = string.Empty;

        public TreatmentType TreatmentType { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public TreatmentStatus Status { get; set; }

        public string? Diagnosis { get; set; }
        public string? Goals { get; set; }
        public string? Notes { get; set; }

        public decimal? EstimatedCost { get; set; }
        public decimal? ActualCost { get; set; }

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        public virtual Patient? Patient { get; set; }
        public virtual Doctor? Doctor { get; set; }

        public virtual ICollection<TreatmentCycle> TreatmentCycles { get; set; } = new List<TreatmentCycle>();

        public virtual TreatmentIVF? TreatmentIVF { get; set; }
    }

    
}
