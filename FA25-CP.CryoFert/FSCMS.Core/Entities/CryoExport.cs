using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities;

/// <summary>
/// Represents a record of a cryogenic sample export event.
/// Logs details when a frozen sample is taken out from storage for use,
/// including the reason, destination, and thawing results.
/// </summary>
public class CryoExport : BaseEntity<Guid>
{
    /// <summary>
    /// Protected constructor for EF Core.
    /// </summary>
    protected CryoExport() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CryoExport"/> class with specified values.
    /// </summary>
    /// <param name="id">The unique identifier of the export record.</param>
    /// <param name="labSampleId">The ID of the exported lab sample.</param>
    /// <param name="cryoLocationId">The ID of the cryogenic location from which the sample is exported.</param>
    /// <param name="exportDate">The date when the export took place.</param>
    /// <param name="exportedBy">The ID of the person who performed the export.</param>
    /// <param name="witnessedBy">The ID of the witness present during the export.</param>
    /// <param name="reason">The reason for export (e.g., Transfer, Thawing, Disposal).</param>
    /// <param name="destination">The destination of the exported sample.</param>
    /// <param name="notes">Additional notes or remarks.</param>
    /// <param name="isThawed">Indicates whether the sample was thawed after export.</param>
    /// <param name="thawingDate">The date when the sample was thawed.</param>
    /// <param name="thawingResult">The result of the thawing process.</param>
    public CryoExport(
        Guid id,
        Guid labSampleId,
        Guid cryoLocationId,
        DateTime exportDate,
        Guid? exportedBy = null,
        Guid? witnessedBy = null,
        string? reason = null,
        string? destination = null,
        string? notes = null,
        bool isThawed = false,
        DateTime? thawingDate = null,
        string? thawingResult = null
    )
    {
        Id = id;
        LabSampleId = labSampleId;
        CryoLocationId = cryoLocationId;
        ExportDate = exportDate;
        ExportedBy = exportedBy;
        WitnessedBy = witnessedBy;
        Reason = reason;
        Destination = destination;
        Notes = notes;
        IsThawed = isThawed;
        ThawingDate = thawingDate;
        ThawingResult = thawingResult;
    }

    // ────────────────────────────────
    // Export Details
    // ────────────────────────────────

    /// <summary>
    /// Gets or sets the ID of the exported lab sample.
    /// </summary>
    public Guid LabSampleId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the cryogenic location from which the sample is exported.
    /// </summary>
    public Guid CryoLocationId { get; set; }

    /// <summary>
    /// Gets or sets the date when the export took place.
    /// </summary>
    public DateTime ExportDate { get; set; }

    /// <summary>
    /// Gets or sets the ID of the person who performed the export.
    /// </summary>
    public Guid? ExportedBy { get; set; }

    /// <summary>
    /// Gets or sets the ID of the person who witnessed the export.
    /// </summary>
    public Guid? WitnessedBy { get; set; }

    /// <summary>
    /// Gets or sets the reason for the export (e.g., Transfer, Thawing, Disposal).
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    /// Gets or sets the destination where the exported sample was sent.
    /// </summary>
    public string? Destination { get; set; }

    /// <summary>
    /// Gets or sets additional notes related to the export process.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Indicates whether the sample has been thawed after export.
    /// </summary>
    public bool IsThawed { get; set; } = false;

    /// <summary>
    /// Gets or sets the date when the thawing occurred.
    /// </summary>
    public DateTime? ThawingDate { get; set; }

    /// <summary>
    /// Gets or sets the result or observation of the thawing process.
    /// </summary>
    public string? ThawingResult { get; set; }

    // ────────────────────────────────
    // Navigation Properties
    // ────────────────────────────────

    /// <summary>
    /// Gets or sets the related lab sample entity.
    /// </summary>
    public virtual LabSample? LabSample { get; set; }

    /// <summary>
    /// Gets or sets the related cryogenic location entity.
    /// </summary>
    public virtual CryoLocation? CryoLocation { get; set; }
}
