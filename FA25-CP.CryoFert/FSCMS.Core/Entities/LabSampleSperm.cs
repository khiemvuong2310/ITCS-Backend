using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a sperm sample extending from <see cref="LabSample"/>.
    /// Contains detailed semen analysis parameters such as motility, morphology, and concentration.
    /// </summary>
    public class LabSampleSperm : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected LabSampleSperm() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabSampleSperm"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the sperm record.</param>
        /// <param name="labSampleId">The associated <see cref="LabSample"/> ID.</param>
        /// <param name="volume">The semen volume in milliliters (ml).</param>
        /// <param name="concentration">The sperm concentration (million/ml).</param>
        public LabSampleSperm(
            Guid id,
            Guid labSampleId,
            decimal? volume,
            decimal? concentration
        )
        {
            Id = id;
            LabSampleId = labSampleId;
            Volume = volume;
            Concentration = concentration;
        }

        // ────────────────────────────────
        // Sperm Analysis Details
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the ID of the associated lab sample.
        /// </summary>
        public Guid LabSampleId { get; set; }

        /// <summary>
        /// Gets or sets the semen volume in milliliters (ml).
        /// </summary>
        public decimal? Volume { get; set; }

        /// <summary>
        /// Gets or sets the sperm concentration (million per milliliter).
        /// </summary>
        public decimal? Concentration { get; set; }

        /// <summary>
        /// Gets or sets the total sperm motility percentage.
        /// </summary>
        public decimal? Motility { get; set; }

        /// <summary>
        /// Gets or sets the progressive motility percentage (actively moving sperm).
        /// </summary>
        public decimal? ProgressiveMotility { get; set; }

        /// <summary>
        /// Gets or sets the normal morphology percentage.
        /// </summary>
        public decimal? Morphology { get; set; }

        /// <summary>
        /// Gets or sets the pH level of the semen sample.
        /// </summary>
        public decimal? PH { get; set; }

        /// <summary>
        /// Gets or sets a description of the semen viscosity (e.g., normal, thick, watery).
        /// </summary>
        public string? Viscosity { get; set; }

        /// <summary>
        /// Gets or sets the liquefaction time or status (e.g., complete, incomplete).
        /// </summary>
        public string? Liquefaction { get; set; }

        /// <summary>
        /// Gets or sets the observed color of the semen sample.
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// Gets or sets the total sperm count (million).
        /// </summary>
        public int? TotalSpermCount { get; set; }

        /// <summary>
        /// Gets or sets any additional notes or observations about the sperm sample.
        /// </summary>
        public string? Notes { get; set; }

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        /// <summary>
        /// The base <see cref="LabSample"/> entity associated with this sperm record.
        /// </summary>
        public virtual LabSample? LabSample { get; set; }
    }
}
