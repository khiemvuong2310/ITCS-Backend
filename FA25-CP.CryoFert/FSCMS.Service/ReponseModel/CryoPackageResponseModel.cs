using System;
using System.Collections.Generic;
using FSCMS.Core.Enum;

namespace FSCMS.Service.ReponseModel
{
    public class CryoPackageResponse
    {
        public Guid Id { get; set; }
        public string PackageName { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int DurationMonths { get; set; }
        public int MaxSamples { get; set; }
        public SampleType SampleType { get; set; }
        public bool IncludesInsurance { get; set; }
        public decimal? InsuranceAmount { get; set; }
        public bool IsActive { get; set; }
        public string? Benefits { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CryoPackageDetailResponse : CryoPackageResponse
    {
        // Tổng hợp số hợp đồng đã sử dụng gói này
        public int? TotalContracts { get; set; }

        // Danh sách hợp đồng liên quan (tùy chọn)
        public List<CryoStorageContractResponse>? Contracts { get; set; }
    }
}
