using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents an embryo sample that extends from <see cref="LabSample"/>.
    /// Contains detailed information about the embryo's development, morphology, and testing results.
    /// </summary>
    public class LabSampleEmbryo : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected LabSampleEmbryo() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabSampleEmbryo"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the embryo record.</param>
        /// <param name="labSampleId">The ID of the associated lab sample.</param>
        /// <param name="dayOfDevelopment">The day of development (e.g., Day 3, Day 5).</param>
        /// <param name="grade">The morphological grade of the embryo.</param>
        /// <param name="fertilizationMethod">The method used for fertilization (e.g., IVF, ICSI).</param>
        public LabSampleEmbryo(
            Guid id,
            Guid labSampleId,
            int dayOfDevelopment,
            string? grade,
            string? fertilizationMethod
        )
        {
            Id = id;
            LabSampleId = labSampleId;
            DayOfDevelopment = dayOfDevelopment;
            Grade = grade;
            FertilizationMethod = fertilizationMethod;
        }

        // ────────────────────────────────
        // Embryo Details
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the ID of the associated lab sample.
        /// </summary>
        public Guid LabSampleId { get; set; }

        /// <summary>
        /// Gets or sets the day of development (e.g., Day 3, Day 5).
        /// </summary>
        public int DayOfDevelopment { get; set; }

        /// <summary>
        /// Gets or sets the embryo quality grade (e.g., AA, AB, BB).
        /// </summary>
        public string? Grade { get; set; }

        /// <summary>
        /// Gets or sets the number of cells in the embryo, if applicable.
        /// </summary>
        public int? CellCount { get; set; }

        /// <summary>
        /// Gets or sets the description of the embryo's morphology.
        /// </summary>
        public string? Morphology { get; set; }

        /// <summary>
        /// Indicates whether the embryo has been biopsied.
        /// </summary>
        public bool IsBiopsied { get; set; } = false;

        /// <summary>
        /// Indicates whether the embryo has undergone PGT testing.
        /// </summary>
        public bool IsPGTTested { get; set; } = false;

        /// <summary>
        /// Gets or sets the result of the PGT test (if performed).
        /// </summary>
        public string? PGTResult { get; set; }

        /// <summary>
        /// Gets or sets the fertilization method used (e.g., IVF, ICSI).
        /// </summary>
        public string? FertilizationMethod { get; set; }

        /// <summary>
        /// Gets or sets any additional remarks or comments about the embryo.
        /// </summary>
        public string? Notes { get; set; }

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        /// <summary>
        /// The base <see cref="LabSample"/> entity associated with this embryo.
        /// </summary>
        public virtual LabSample? LabSample { get; set; }
    }
}
