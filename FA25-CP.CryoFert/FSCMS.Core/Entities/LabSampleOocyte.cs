using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents an oocyte (egg cell) sample extending from <see cref="LabSample"/>.
    /// Contains detailed information about oocyte maturity, quality, and vitrification status.
    /// </summary>
    public class LabSampleOocyte : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected LabSampleOocyte() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabSampleOocyte"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the oocyte record.</param>
        /// <param name="labSampleId">The ID of the associated lab sample.</param>
        /// <param name="maturityStage">The oocyte maturity stage (e.g., MII, MI, GV).</param>
        /// <param name="quality">The oocyte quality grade (e.g., A, B, C).</param>
        public LabSampleOocyte(
            Guid id,
            Guid labSampleId,
            string maturityStage,
            string? quality
        )
        {
            Id = id;
            LabSampleId = labSampleId;
            MaturityStage = maturityStage;
            Quality = quality;
        }

        // ────────────────────────────────
        // Oocyte Details
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the ID of the associated lab sample.
        /// </summary>
        public Guid LabSampleId { get; set; }

        /// <summary>
        /// Gets or sets the maturity stage of the oocyte (e.g., MII, MI, GV).
        /// </summary>
        public string MaturityStage { get; set; } = default!;

        /// <summary>
        /// Gets or sets the oocyte quality grade (e.g., A, B, C).
        /// </summary>
        public string? Quality { get; set; }

        /// <summary>
        /// Indicates whether the oocyte is considered mature and suitable for fertilization.
        /// </summary>
        public bool IsMature { get; set; }

        /// <summary>
        /// Gets or sets the date when the oocyte was retrieved.
        /// </summary>
        public DateTime? RetrievalDate { get; set; }

        /// <summary>
        /// Gets or sets a description of the cumulus cell status surrounding the oocyte.
        /// </summary>
        public string? CumulusCells { get; set; }

        /// <summary>
        /// Gets or sets the visual appearance of the oocyte cytoplasm.
        /// </summary>
        public string? CytoplasmAppearance { get; set; }

        /// <summary>
        /// Indicates whether the oocyte has been vitrified (cryopreserved).
        /// </summary>
        public bool IsVitrified { get; set; } = false;

        /// <summary>
        /// Gets or sets the date when the oocyte was vitrified, if applicable.
        /// </summary>
        public DateTime? VitrificationDate { get; set; }

        /// <summary>
        /// Gets or sets any additional remarks or comments about the oocyte.
        /// </summary>
        public string? Notes { get; set; }

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        /// <summary>
        /// The base <see cref="LabSample"/> entity associated with this oocyte.
        /// </summary>
        public virtual LabSample? LabSample { get; set; }
    }
}
