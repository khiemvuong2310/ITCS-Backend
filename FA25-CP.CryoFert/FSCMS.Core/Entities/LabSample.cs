using System;
using System.Collections.Generic;
using FSCMS.Core.Enum;
using FSCMS.Core.Enums;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    // Bảng LabSample: Mẫu xét nghiệm/lab cốt lõi (tinh trùng/noãn/phôi...).
    // Quan hệ:
    // - n-1 tới Patient (PatientId)
    // - 0..1 - n tới CryoLocation (CryoLocationId có thể null nếu chưa lưu)
    // - 1-1 tới các bảng chi tiết chuyên biệt: LabSampleEmbryo/LabSampleSperm/LabSampleOocyte
    // - 1-n tới CPSDetail (mẫu được tham chiếu trong hợp đồng lưu trữ)
    public class LabSample : BaseEntity<Guid>
    {
        public LabSample() : base() { }
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
        public Guid PatientId { get; set; }
        public Guid? CryoLocationId { get; set; }
        public string SampleCode { get; set; } = default!;
        public SampleType SampleType { get; set; }
        public SpecimenStatus Status { get; set; }
        public DateTime CollectionDate { get; set; }

        public DateTime? StorageDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? Quality { get; set; }
        public string? Notes { get; set; }
        public bool IsAvailable { get; set; } = true;
        public bool CanFrozen { get; set; } = false;
        public bool CanFertilize { get; set; } = false;
        public virtual Patient? Patient { get; set; }
        public virtual CryoLocation? CryoLocation { get; set; }
        public virtual LabSampleEmbryo? LabSampleEmbryo { get; set; }
        public virtual LabSampleSperm? LabSampleSperm { get; set; }
        public virtual LabSampleOocyte? LabSampleOocyte { get; set; }
        public virtual ICollection<CPSDetail> CPSDetails { get; set; } = new List<CPSDetail>();
    }
}
