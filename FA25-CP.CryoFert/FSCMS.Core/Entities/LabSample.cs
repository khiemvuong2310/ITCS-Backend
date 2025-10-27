using System;
using System.Collections.Generic;
using FSCMS.Core.Enum;
using FSCMS.Core.Enums;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a biological sample (embryo, sperm, or oocyte) collected for laboratory use.
    /// This is the base entity for all sample types stored or processed in the cryobank.
    /// - Many-to-One with <see cref="Patient"/>.
    /// - Many-to-One with <see cref="CryoLocation"/>.
    /// - Inherits (TPT) with <see cref="LabSampleEmbryo"/>, <see cref="LabSampleSperm"/>, and <see cref="LabSampleOocyte"/>.
    /// </summary>
    public class LabSample : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected LabSample() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabSample"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the sample.</param>
        /// <param name="patientId">The ID of the patient who owns the sample.</param>
        /// <param name="sampleCode">The unique sample code assigned in the lab.</param>
        /// <param name="sampleType">The biological type of the sample (Embryo, Sperm, or Oocyte).</param>
        /// <param name="collectionDate">The date the sample was collected.</param>
        /// <param name="status">The current processing or storage status of the sample.</param>
        public LabSample(
            Guid id,
            Guid patientId,
            string sampleCode,
            SampleType sampleType,
            DateTime collectionDate,
            SpecimenStatus status
        )
        {
            Id = id;
            PatientId = patientId;
            SampleCode = sampleCode;
            SampleType = sampleType;
            CollectionDate = collectionDate;
            Status = status;
        }

        // ────────────────────────────────
        // Sample Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the ID of the patient who owns this sample.
        /// </summary>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the cryogenic location where the sample is stored, if applicable.
        /// </summary>
        public Guid? CryoLocationId { get; set; }

        /// <summary>
        /// Gets or sets the unique sample code used for internal tracking.
        /// </summary>
        public string SampleCode { get; set; } = default!;

        /// <summary>
        /// Gets or sets the biological type of this sample (Embryo, Sperm, or Oocyte).
        /// </summary>
        public SampleType SampleType { get; set; }

        /// <summary>
        /// Gets or sets the current status of the sample (e.g., Processing, Stored, Thawed, Discarded).
        /// </summary>
        public SpecimenStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the date when the sample was collected.
        /// </summary>
        public DateTime CollectionDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the sample was moved into cryo storage, if applicable.
        /// </summary>
        public DateTime? StorageDate { get; set; }

        /// <summary>
        /// Gets or sets the expiration date for the sample, based on package duration or viability.
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets a text description of the sample's quality (e.g., Grade A, Motility %, Viability).
        /// </summary>
        public string? Quality { get; set; }

        /// <summary>
        /// Gets or sets additional remarks or comments about the sample.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Indicates whether this sample is currently available for treatment or transfer.
        /// </summary>
        public bool IsAvailable { get; set; } = true;

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        /// <summary>
        /// The patient to whom this sample belongs.
        /// </summary>
        public virtual Patient? Patient { get; set; }

        /// <summary>
        /// The cryo location where this sample is stored, if any.
        /// </summary>
        public virtual CryoLocation? CryoLocation { get; set; }

        /// <summary>
        /// The embryo-specific data for this sample (TPT inheritance).
        /// </summary>
        public virtual LabSampleEmbryo? LabSampleEmbryo { get; set; }

        /// <summary>
        /// The sperm-specific data for this sample (TPT inheritance).
        /// </summary>
        public virtual LabSampleSperm? LabSampleSperm { get; set; }

        /// <summary>
        /// The oocyte-specific data for this sample (TPT inheritance).
        /// </summary>
        public virtual LabSampleOocyte? LabSampleOocyte { get; set; }

        /// <summary>
        /// The list of cryo package details or storage contract records referencing this sample.
        /// </summary>
        public virtual ICollection<CPSDetail> CPSDetails { get; set; } = new List<CPSDetail>();
    }
}
