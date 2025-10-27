using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents the detail record of a prescribed medicine within a prescription.
    /// This entity defines dosage, quantity, frequency, and duration for a specific medicine.
    /// - Many-to-One with <see cref="Prescription"/>.
    /// - Many-to-One with <see cref="Medicine"/>.
    /// </summary>
    public class PrescriptionDetail : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected PrescriptionDetail() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrescriptionDetail"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the prescription detail.</param>
        /// <param name="prescriptionId">The ID of the parent prescription.</param>
        /// <param name="medicineId">The ID of the prescribed medicine.</param>
        /// <param name="quantity">The quantity of medicine prescribed.</param>
        public PrescriptionDetail(Guid id, Guid prescriptionId, Guid medicineId, int quantity)
        {
            Id = id;
            PrescriptionId = prescriptionId;
            MedicineId = medicineId;
            Quantity = quantity;
        }

        // ────────────────────────────────
        // Prescription Detail Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the ID of the parent prescription.
        /// </summary>
        public Guid PrescriptionId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the prescribed medicine.
        /// </summary>
        public Guid MedicineId { get; set; }

        /// <summary>
        /// Gets or sets the number of medicine units prescribed.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the dosage instruction (e.g., "1 pill", "10ml").
        /// </summary>
        public string Dosage { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the frequency of administration (e.g., "2 times/day", "Before meal").
        /// </summary>
        public string Frequency { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total duration (in days) for which the medicine should be taken.
        /// </summary>
        public int DurationDays { get; set; }

        /// <summary>
        /// Gets or sets additional usage instructions provided by the doctor.
        /// </summary>
        public string? Instructions { get; set; }

        /// <summary>
        /// Gets or sets optional remarks or internal notes.
        /// </summary>
        public string? Notes { get; set; }

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        /// <summary>
        /// The prescription that this detail belongs to.
        /// </summary>
        public virtual Prescription? Prescription { get; set; }

        /// <summary>
        /// The medicine associated with this prescription detail.
        /// </summary>
        public virtual Medicine? Medicine { get; set; }
    }
}
