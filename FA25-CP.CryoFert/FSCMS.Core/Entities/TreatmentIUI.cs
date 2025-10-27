using System;
using FSCMS.Core.Models.Bases;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents the detailed IUI protocol for a specific treatment.
    /// One-to-One with Treatment.
    /// Extended version with monitoring, medication, outcome, attempt details, and cycle status.
    /// </summary>
    public class TreatmentIUI : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected TreatmentIUI() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreatmentIUI"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the IUI treatment detail.</param>
        /// <param name="treatmentId">The ID of the associated treatment.</param>
        /// <param name="protocol">The stimulation or preparation protocol for IUI.</param>
        public TreatmentIUI(Guid id, Guid treatmentId, string protocol)
        {
            Id = id;
            TreatmentId = treatmentId;
            Protocol = protocol;
        }

        // ────────────────────────────────
        // IUI Protocol Information
        // ────────────────────────────────

        public Guid TreatmentId { get; set; }
        public string Protocol { get; set; } = string.Empty;
        public string? Medications { get; set; }
        public string? Monitoring { get; set; }
        public DateTime? OvulationTriggerDate { get; set; }
        public DateTime? InseminationDate { get; set; }
        public int? MotileSpermCount { get; set; }
        public int? NumberOfAttempts { get; set; }
        public string? Outcome { get; set; }
        public string? Notes { get; set; }

        /// <summary>
        /// Current status of the IUI cycle.
        /// </summary>
        public IUICycleStatus Status { get; set; } = IUICycleStatus.Planned;

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        public virtual Treatment? Treatment { get; set; }
    }
}
