using FSCMS.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FSCMS.Service.RequestModel
{
    public class CryoLocationCreateRequest
    {
        public string Name { get; set; } = default!;
        public FSCMS.Core.Enum.CryoLocationType Type { get; set; }
        public FSCMS.Core.Enum.SampleType SampleType { get; set; } = FSCMS.Core.Enum.SampleType.None;
        public Guid? ParentId { get; set; }
        public int? Capacity { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class CryoLocationUpdateRequest
    {
        public string Name { get; set; } = default!;
        public FSCMS.Core.Enum.CryoLocationType Type { get; set; }
        public FSCMS.Core.Enum.SampleType SampleType { get; set; } = FSCMS.Core.Enum.SampleType.None;
        public Guid? ParentId { get; set; }
        public int? Capacity { get; set; }
        public bool IsActive { get; set; }
    }
}