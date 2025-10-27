using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities;

/// <summary>
/// Represents a hierarchical storage location in the cryobank.
/// Each location can represent a tank, canister, goblet, or slot,
/// and can have child locations forming a tree structure.
/// </summary>
public class CryoLocation : BaseEntity<Guid>
{
    protected CryoLocation() : base() { }

    public CryoLocation(
        Guid id,
        string name,
        CryoLocationType type,
        Guid? parentId = null,
        int? capacity = null,
        SampleType sampleType = SampleType.None,
        bool isActive = true
    )
    {
        Id = id;
        Name = name;
        Type = type;
        ParentId = parentId;
        Capacity = capacity;
        SampleType = sampleType;
        IsActive = isActive;
    }

    // ────────────────────────────────
    // Location Information
    // ────────────────────────────────

    /// <summary>
    /// Gets or sets the name of the location (e.g., Tank A, Canister 3, Slot #15).
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the type of the location (Tank, Canister, Goblet, Slot).
    /// </summary>
    public CryoLocationType Type { get; set; }

    /// <summary>
    /// Gets or sets the biological sample type stored in this location (only meaningful for Tanks).
    /// </summary>
    public SampleType SampleType { get; set; } = SampleType.None;

    /// <summary>
    /// Gets or sets the optional identifier of the parent location (for hierarchy).
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// Gets or sets the maximum capacity (number of child slots or samples).
    /// </summary>
    public int? Capacity { get; set; }

    /// <summary>
    /// Gets or sets whether this location is currently active and available for use.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the current temperature (if applicable, e.g., for tanks).
    /// </summary>
    public decimal? Temperature { get; set; }

    /// <summary>
    /// Gets or sets the optional description or note about the location.
    /// </summary>
    public string? Notes { get; set; }

    // ────────────────────────────────
    // Hierarchy Navigation
    // ────────────────────────────────

    public virtual CryoLocation? Parent { get; set; }
    public virtual ICollection<CryoLocation> Children { get; set; } = new List<CryoLocation>();

    // ────────────────────────────────
    // Relationships
    // ────────────────────────────────

    public virtual ICollection<LabSample> LabSamples { get; set; } = new List<LabSample>();
    public virtual ICollection<CryoImport> CryoImports { get; set; } = new List<CryoImport>();
    public virtual ICollection<CryoExport> CryoExports { get; set; } = new List<CryoExport>();
}
