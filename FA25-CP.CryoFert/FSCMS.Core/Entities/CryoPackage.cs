using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    public class CryoPackage : BaseEntity<Guid>
    {
        protected CryoPackage() : base() { }
        public CryoPackage(
            Guid id,
            string packageName,
            decimal price,
            int durationMonths,
            int maxSamples,
            SampleType sampleType = SampleType.Sperm,
            bool isActive = true
        )
        {
            Id = id;
            PackageName = packageName;
            Price = price;
            DurationMonths = durationMonths;
            MaxSamples = maxSamples;
            SampleType = sampleType;
            IsActive = isActive;
        }
        public string PackageName { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int DurationMonths { get; set; }
        public int MaxSamples { get; set; }
        public SampleType SampleType { get; set; } = SampleType.Sperm;
        public bool IncludesInsurance { get; set; } = false;
        public decimal? InsuranceAmount { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Benefits { get; set; }
        public string? Notes { get; set; }
        public virtual ICollection<CryoStorageContract> CryoStorageContracts { get; set; } = new List<CryoStorageContract>();
    }
}
