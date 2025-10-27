using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities;

/// <summary>
/// Represents an import record for a lab sample into cryogenic storage.
/// Logs details when a sample is placed into storage including temperature, actor and reason.
/// </summary>
public class CryoImport : BaseEntity<Guid>
{
    /// <summary>
    /// Protected constructor for EF Core.
    /// </summary>
    protected CryoImport() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CryoImport"/> class with specified values.
    /// </summary>
    /// <param name="id">The unique identifier of the import record.</param>
    /// <param name="labSampleId">The ID of the lab sample being imported.</param>
    /// <param name="cryoLocationId">The ID of the cryogenic location where the sample is stored.</param>
    /// <param name="importDate">The date and time when the import occurred.</param>
    /// <param name="importedBy">The ID of the user who performed the import.</param>
    /// <param name="witnessedBy">The ID of the witness present during import.</param>
    /// <param name="temperature">Recorded temperature at import (if applicable).</param>
    /// <param name="reason">The reason for import (e.g., Storage, Transfer).</param>
    /// <param name="notes">Additional notes or remarks about the import.</param>
    public CryoImport(
        Guid id,
        Guid labSampleId,
        Guid cryoLocationId,
        DateTime importDate,
        Guid? importedBy = null,
        Guid? witnessedBy = null,
        decimal? temperature = null,
        string? reason = null,
        string? notes = null
    )
    {
        Id = id;
        LabSampleId = labSampleId;
        CryoLocationId = cryoLocationId;
        ImportDate = importDate;
        ImportedBy = importedBy;
        WitnessedBy = witnessedBy;
        Temperature = temperature;
        Reason = reason;
        Notes = notes;
    }

    // ────────────────────────────────
    // Import Details
    // ────────────────────────────────

    /// <summary>
    /// Gets or sets the ID of the imported lab sample.
    /// </summary>
    public Guid LabSampleId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the cryogenic location where the sample is stored.
    /// </summary>
    public Guid CryoLocationId { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the sample was imported into storage.
    /// </summary>
    public DateTime ImportDate { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who performed the import.
    /// </summary>
    public Guid? ImportedBy { get; set; }

    /// <summary>
    /// Gets or sets the ID of the person who witnessed the import.
    /// </summary>
    public Guid? WitnessedBy { get; set; }

    /// <summary>
    /// Gets or sets the recorded temperature at the time of import.
    /// </summary>
    public decimal? Temperature { get; set; }

    /// <summary>
    /// Gets or sets the reason for importing the sample (e.g., Storage, Transfer).
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    /// Gets or sets additional notes or remarks about the import event.
    /// </summary>
    public string? Notes { get; set; }

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
