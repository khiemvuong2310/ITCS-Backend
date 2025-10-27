using System;
using FSCMS.Core.Models.Bases;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents the detailed IVF protocol for a specific treatment.
    /// One-to-One with Treatment.
    /// Extended version with stimulation, retrieval, fertilization, transfer, and embryo outcome details.
    /// Includes IVF cycle status.
    /// </summary>
    public class TreatmentIVF : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected TreatmentIVF() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreatmentIVF"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the IVF treatment detail.</param>
        /// <param name="treatmentId">The ID of the associated treatment.</param>
        /// <param name="protocol">The stimulation protocol (e.g., "Long", "Short", "Antagonist").</param>
        public TreatmentIVF(Guid id, Guid treatmentId, string protocol)
        {
            Id = id;
            TreatmentId = treatmentId;
            Protocol = protocol;
        }

        // ────────────────────────────────
        // IVF Protocol Information
        // ────────────────────────────────

        public Guid TreatmentId { get; set; }

        public string Protocol { get; set; } = string.Empty;

        public DateTime? StimulationStartDate { get; set; }
        public DateTime? OocyteRetrievalDate { get; set; }
        public DateTime? FertilizationDate { get; set; }
        public DateTime? TransferDate { get; set; }

        public int? OocytesRetrieved { get; set; }
        public int? OocytesMature { get; set; }
        public int? OocytesFertilized { get; set; }
        public int? EmbryosCultured { get; set; }
        public int? EmbryosTransferred { get; set; }
        public int? EmbryosCryopreserved { get; set; }
        public int? EmbryosFrozen { get; set; }

        public string? Notes { get; set; }
        public string? Outcome { get; set; }
        public bool? UsedICSI { get; set; }
        public string? Complications { get; set; }

        /// <summary>
        /// Current status of the IVF cycle.
        /// </summary>
        public IVFCycleStatus Status { get; set; } = IVFCycleStatus.Planned;

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        public virtual Treatment? Treatment { get; set; }
    }
}
