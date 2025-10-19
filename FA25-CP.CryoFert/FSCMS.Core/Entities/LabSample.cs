using System;
using System.Collections.Generic;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho mẫu xét nghiệm
    /// Many-to-One với Patient
    /// Many-to-One với CryoLocation
    /// Có quan hệ thừa kế với LabSampleEmbryo, LabSampleSperm, LabSampleOocyte
    /// </summary>
    public class LabSample : BaseEntity
    {
        public int PatientId { get; set; }
        public int? CryoLocationId { get; set; }
        
        public string SampleCode { get; set; } = string.Empty;
        public SpecimenType SampleType { get; set; } // Embryo, Sperm, Oocyte
        public SpecimenStatus Status { get; set; }
        public DateTime CollectionDate { get; set; }
        public DateTime? StorageDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? Quality { get; set; }
        public string? Notes { get; set; }
        public bool IsAvailable { get; set; } = true;

        // Navigation Properties
        public virtual Patient? Patient { get; set; }
        public virtual CryoLocation? CryoLocation { get; set; }
        
        // Inheritance relationships (TPT - Table Per Type)
        public virtual LabSampleEmbryo? LabSampleEmbryo { get; set; }
        public virtual LabSampleSperm? LabSampleSperm { get; set; }
        public virtual LabSampleOocyte? LabSampleOocyte { get; set; }
        
        // Relations
        public virtual ICollection<CPSDetail>? CPSDetails { get; set; } = new List<CPSDetail>();
    }
}

