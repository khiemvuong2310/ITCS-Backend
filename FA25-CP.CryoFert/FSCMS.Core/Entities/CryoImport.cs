using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities;

public class CryoImport : BaseEntity<Guid>
{
    // Bảng CryoImport: Giao dịch nhập mẫu vào kho cryo.
    // Quan hệ:
    // - n-1 tới LabSample (LabSampleId)
    // - n-1 tới CryoLocation (CryoLocationId)
    protected CryoImport() : base()
    {
    }
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
    public Guid LabSampleId { get; set; }
    public Guid CryoLocationId { get; set; }
    public DateTime ImportDate { get; set; }
    public Guid? ImportedBy { get; set; }
    public Guid? WitnessedBy { get; set; }
    public decimal? Temperature { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
    public virtual LabSample? LabSample { get; set; }
    public virtual CryoLocation? CryoLocation { get; set; }
}
