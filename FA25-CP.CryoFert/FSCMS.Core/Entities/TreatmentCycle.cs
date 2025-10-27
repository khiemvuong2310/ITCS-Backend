using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a treatment cycle for a specific treatment plan.
    /// Many-to-One with Treatment.
    /// One-to-Many with Appointment.
    /// </summary>
    public class TreatmentCycle : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected TreatmentCycle() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreatmentCycle"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the cycle.</param>
        /// <param name="treatmentId">The ID of the associated treatment.</param>
        /// <param name="cycleName">The display name of the cycle.</param>
        /// <param name="cycleNumber">The sequence number of the cycle.</param>
        /// <param name="startDate">The start date of the cycle.</param>
        public TreatmentCycle(Guid id, Guid treatmentId, string cycleName, int cycleNumber, DateTime startDate)
        {
            Id = id;
            TreatmentId = treatmentId;
            CycleName = cycleName;
            CycleNumber = cycleNumber;
            StartDate = startDate;
            Status = TreatmentStatus.Planned;
        }

        // ────────────────────────────────
        // Cycle Information
        // ────────────────────────────────

        public Guid TreatmentId { get; set; }

        public string CycleName { get; set; } = string.Empty;

        public int CycleNumber { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public TreatmentStatus Status { get; set; }

        public string? Protocol { get; set; }
        public string? Notes { get; set; }

        public decimal? Cost { get; set; }

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        public virtual Treatment? Treatment { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
