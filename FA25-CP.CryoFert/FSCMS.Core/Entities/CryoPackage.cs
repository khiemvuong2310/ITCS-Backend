using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a cryogenic storage package offered by the cryobank.
    /// Each package defines storage duration, sample type, and included benefits.
    /// One package can be associated with multiple storage contracts.
    /// </summary>
    public class CryoPackage : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected CryoPackage() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CryoPackage"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the package.</param>
        /// <param name="packageName">The display name of the package.</param>
        /// <param name="price">The total price of the package.</param>
        /// <param name="durationMonths">The storage duration in months.</param>
        /// <param name="maxSamples">The maximum number of samples allowed in this package.</param>
        /// <param name="sampleType">The type of biological samples allowed (Embryo, Sperm, Oocyte, or All).</param>
        /// <param name="isActive">Indicates whether this package is active and available for selection.</param>
        public CryoPackage(
            Guid id,
            string packageName,
            decimal price,
            int durationMonths,
            int maxSamples,
            SampleType sampleType = SampleType.Sperm,
            bool isActive = true
        )
        {
            Id = id;
            PackageName = packageName;
            Price = price;
            DurationMonths = durationMonths;
            MaxSamples = maxSamples;
            SampleType = sampleType;
            IsActive = isActive;
        }

        // ────────────────────────────────
        // Package Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the name of the cryo storage package.
        /// </summary>
        public string PackageName { get; set; } = default!;

        /// <summary>
        /// Gets or sets a short description of the package.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the price of the package.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the duration of storage (in months).
        /// </summary>
        public int DurationMonths { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of samples allowed under this package.
        /// </summary>
        public int MaxSamples { get; set; }

        /// <summary>
        /// Gets or sets the type of biological sample allowed in this package.
        /// </summary>
        public SampleType SampleType { get; set; } = SampleType.Sperm;

        /// <summary>
        /// Indicates whether this package includes insurance coverage.
        /// </summary>
        public bool IncludesInsurance { get; set; } = false;

        /// <summary>
        /// Gets or sets the insurance coverage amount, if applicable.
        /// </summary>
        public decimal? InsuranceAmount { get; set; }

        /// <summary>
        /// Indicates whether this package is active and available for use.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets the benefits of the package (can be plain text or JSON format).
        /// </summary>
        public string? Benefits { get; set; }

        /// <summary>
        /// Gets or sets additional notes or remarks about the package.
        /// </summary>
        public string? Notes { get; set; }

        // ────────────────────────────────
        // Relationships
        // ────────────────────────────────

        /// <summary>
        /// The collection of storage contracts associated with this package.
        /// </summary>
        public virtual ICollection<CryoStorageContract> CryoStorageContracts { get; set; } = new List<CryoStorageContract>();
    }
}
