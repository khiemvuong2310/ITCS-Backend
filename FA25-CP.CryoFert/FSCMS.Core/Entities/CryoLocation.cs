using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities;
public class CryoLocation : BaseEntity<Guid>
{
    // Bảng CryoLocation: Cấu trúc kho lưu trữ cryo (kho/kệ/ngăn/vị trí...).
    // Quan hệ:
    // - Tự tham chiếu: Parent/Children (cây thư mục vị trí)
    // - 1-n với LabSample (mẫu đang được lưu tại vị trí)
    // - 1-n với CryoImport/CryoExport (các giao dịch nhập/xuất liên quan)
    public CryoLocation() : base() { }

    public CryoLocation(
        string name,
        string code,
        CryoLocationType type,
        Guid? parentId = null,
        CryoLocation parent = null,
        int? capacity = null,
        int sampleCount = 0,
        SampleType sampleType = SampleType.Embryo,
        bool isActive = true,
        decimal? temperature = null,
        string? notes = null
    ) : base()
    {
        Id = Guid.NewGuid(); 
        Name = name;
        Code = code;
        Type = type;
        ParentId = parentId;
        Parent = parent;
        Capacity = capacity;
        SampleCount = sampleCount;
        SampleType = sampleType;
        IsActive = isActive;
        Temperature = temperature;
        Notes = notes;
        Children = new List<CryoLocation>();
    }
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public CryoLocationType Type { get; set; }
    public SampleType SampleType { get; set; }
    public Guid? ParentId { get; set; }
    public int? Capacity { get; set; }
    public int SampleCount { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public decimal? Temperature { get; set; }
    public string? Notes { get; set; }
    public virtual CryoLocation? Parent { get; set; }
    public virtual ICollection<CryoLocation> Children { get; set; } = new List<CryoLocation>();
    public virtual ICollection<LabSample> LabSamples { get; set; } = new List<LabSample>();
    public virtual ICollection<CryoImport> CryoImports { get; set; } = new List<CryoImport>();
    public virtual ICollection<CryoExport> CryoExports { get; set; } = new List<CryoExport>();
}
