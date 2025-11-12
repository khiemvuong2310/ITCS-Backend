using FSCMS.Core.Entities;
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
        public string Code { get; set; } = default!;
        public CryoLocationType Type { get; set; }
        public SampleType SampleType { get; set; }
        public Guid? ParentId { get; set; }
        public int? Capacity { get; set; }
        public int SampleCount { get; set; }
        public int AvailableCapacity { get; set; }
        public bool IsActive { get; set; }
        public decimal? Temperature { get; set; }
        public string? Notes { get; set; }
        //public LabSample? LabSample { get; set; }
        // public List<CryoLocationResponse> Children { get; set; } = new();
    }

    public class CryoLocationSummaryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = default!;
        public SampleType SampleType { get; set; }
        public int SampleCount { get; set; }
        public CryoLocationType Type { get; set; }
    }

    public class CryoLocationFullTreeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;
        public CryoLocationType Type { get; set; }
        public SampleType SampleType { get; set; }
        public Guid? ParentId { get; set; }
        public int? Capacity { get; set; }
        public int SampleCount { get; set; }
        public int AvailableCapacity { get; set; }
        public bool IsActive { get; set; }
        public decimal? Temperature { get; set; }
        public string? Notes { get; set; }
        public List<CryoLocationFullTreeResponse> Children { get; set; } = new();
    }

}