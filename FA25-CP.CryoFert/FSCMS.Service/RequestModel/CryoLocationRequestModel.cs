using FSCMS.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FSCMS.Service.RequestModel
{
    public class CryoLocationCreateRequest
    {
        public CryoLocationType Type { get; set; }
        public SampleType SampleType { get; set; }
        public Guid? ParentId { get; set; }
        public int? Capacity { get; set; }
        public bool IsActive { get; set; } = true;
        public decimal? Temperature { get; set; }
        public string? Notes { get; set; }
    }

    public class CryoLocationUpdateRequest
    {
        public bool IsActive { get; set; }
        public decimal? Temperature { get; set; }
        public string? Notes { get; set; }
    }
}