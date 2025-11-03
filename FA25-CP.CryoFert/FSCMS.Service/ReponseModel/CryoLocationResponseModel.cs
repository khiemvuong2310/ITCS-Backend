using FSCMS.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FSCMS.Service.ReponseModel
{
    public class CryoLocationResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public FSCMS.Core.Enum.CryoLocationType Type { get; set; }
        public FSCMS.Core.Enum.SampleType SampleType { get; set; }
        public Guid? ParentId { get; set; }
        public int? Capacity { get; set; }
        public bool IsActive { get; set; }
        public decimal? Temperature { get; set; }
        public string? Notes { get; set; }
        public int CurrentSampleCount { get; set; }
        public List<CryoLocationResponse> Children { get; set; } = new();
    }

    public class CryoLocationSummaryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public SampleType SampleType { get; set; }
        public int TotalCapacity { get; set; }
        public int CurrentCount { get; set; }
    }

}