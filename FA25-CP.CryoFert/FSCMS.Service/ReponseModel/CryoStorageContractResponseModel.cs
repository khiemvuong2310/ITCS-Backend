using System;
using System.Collections.Generic;

namespace FSCMS.Service.ReponseModel
{
    public class CryoStorageContractResponse
    {
        public Guid Id { get; set; }

        public string ContractNumber { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Status { get; set; } = default!; // Convert từ enum ContractStatus sang string
        public decimal TotalAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public DateTime? SignedDate { get; set; }
        public string? SignedBy { get; set; }
        public string? Notes { get; set; }

        public Guid PatientId { get; set; }
        public string? PatientName { get; set; }

        public Guid CryoPackageId { get; set; }
        public string? CryoPackageName { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CryoStorageContractDetailResponse : CryoStorageContractResponse
    {
        // Danh sách mẫu lưu trữ trong hợp đồng
        public List<CPSDetailResponse>? Samples { get; set; }
    }

    // Response phụ cho CPSDetail (chi tiết mẫu)
    public class CPSDetailResponse
    {
        public Guid Id { get; set; }

        public Guid LabSampleId { get; set; }
        public string? SampleCode { get; set; }
        public string? SampleType { get; set; }

        public DateTime? StorageDate { get; set; }
        public string? StorageLocation { get; set; } // Ví dụ: Tank 1 / Slot A3

        public bool IsActive { get; set; }
    }
}
