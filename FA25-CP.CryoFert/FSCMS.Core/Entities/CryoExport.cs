using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities;
public class CryoExport : BaseEntity<Guid>
{
    // Bảng CryoExport: Giao dịch xuất mẫu khỏi kho cryo (chuyển viện/sử dụng...).
    // Quan hệ:
    // - n-1 tới LabSample (LabSampleId)
    // - n-1 tới CryoLocation (CryoLocationId)
    protected CryoExport() : base()
    {
    }

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
    public Guid LabSampleId { get; set; }
    public Guid CryoLocationId { get; set; }
    public DateTime ExportDate { get; set; }
    public Guid? ExportedBy { get; set; }
    public Guid? WitnessedBy { get; set; }
    public string? Reason { get; set; }
    public string? Destination { get; set; }
    public string? Notes { get; set; }
    public bool IsThawed { get; set; } = false;
    public DateTime? ThawingDate { get; set; }
    public string? ThawingResult { get; set; }
    public virtual LabSample? LabSample { get; set; }
    public virtual CryoLocation? CryoLocation { get; set; }
}
