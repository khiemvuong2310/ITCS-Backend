using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a medicine definition used in prescriptions.
    /// This entity defines basic drug information without inventory or sales tracking.
    /// </summary>
    public class Medicine : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected Medicine() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Medicine"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the medicine.</param>
        /// <param name="name">The name of the medicine (commercial or generic).</param>
        /// <param name="dosage">The dosage strength (e.g., "500mg").</param>
        /// <param name="form">The form of the medicine (e.g., "Tablet").</param>
        public Medicine(Guid id, string name, string? dosage, string? form)
        {
            Id = id;
            Name = name;
            Dosage = dosage;
            Form = form;
        }

        // ────────────────────────────────
        // Medicine Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the official or common name of the medicine.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Gets or sets the generic name or active ingredient.
        /// </summary>
        public string? GenericName { get; set; }

        /// <summary>
        /// Gets or sets the dosage strength (e.g., "500mg", "10ml").
        /// </summary>
        public string? Dosage { get; set; }

        /// <summary>
        /// Gets or sets the physical form of the medicine (e.g., "Tablet", "Capsule", "Injection").
        /// </summary>
        public string? Form { get; set; }

        /// <summary>
        /// Gets or sets the indication or therapeutic use of this medicine.
        /// </summary>
        public string? Indication { get; set; }

        /// <summary>
        /// Gets or sets the contraindications (conditions where this medicine should not be used).
        /// </summary>
        public string? Contraindication { get; set; }

        /// <summary>
        /// Gets or sets known side effects of this medicine.
        /// </summary>
        public string? SideEffects { get; set; }

        /// <summary>
        /// Indicates whether this medicine is active and selectable in prescriptions.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets any additional internal notes about the medicine.
        /// </summary>
        public string? Notes { get; set; }

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        /// <summary>
        /// The collection of prescription details that reference this medicine.
        /// </summary>
        public virtual ICollection<PrescriptionDetail> PrescriptionDetails { get; set; } = new List<PrescriptionDetail>();
    }
}
