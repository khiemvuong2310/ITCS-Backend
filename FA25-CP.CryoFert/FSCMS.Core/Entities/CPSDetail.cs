using FSCMS.Core.Models.Bases;
using System;

namespace FSCMS.Core.Entities;

public class CPSDetail : BaseEntity<Guid>
{
    // Bảng CPSDetail: Bảng nối giữa Hợp đồng lưu trữ và Mẫu (Contract - Sample).
    // Quan hệ:
    // - n-1 tới CryoStorageContract (CryoStorageContractId)
    // - n-1 tới LabSample (LabSampleId)
    protected CPSDetail() : base() { }

    public CPSDetail(
        Guid id,
        Guid cryoStorageContractId,
        Guid labSampleId,
        DateTime storageStartDate,
        DateTime? storageEndDate,
        string status,
        decimal? monthlyFee = null,
        string? notes = null
    )
    {
        Id = id;
        CryoStorageContractId = cryoStorageContractId;
        LabSampleId = labSampleId;
        StorageStartDate = storageStartDate;
        StorageEndDate = storageEndDate;
        Status = status;
        MonthlyFee = monthlyFee;
        Notes = notes;
    }
    public Guid CryoStorageContractId { get; set; }
    public Guid LabSampleId { get; set; }

    public DateTime StorageStartDate { get; set; }
    public DateTime? StorageEndDate { get; set; }
    public string Status { get; set; } = string.Empty; // "Stored", "Released", "Disposed"
    public decimal? MonthlyFee { get; set; }
    public string? Notes { get; set; }
    public virtual CryoStorageContract? CryoStorageContract { get; set; }
    public virtual LabSample? LabSample { get; set; }
}
