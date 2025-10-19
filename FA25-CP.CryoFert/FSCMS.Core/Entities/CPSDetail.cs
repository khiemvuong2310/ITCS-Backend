using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho chi tiết hợp đồng/đăng ký gói lưu trữ (Cryo Package Subscription Detail)
    /// Bảng trung gian tạo quan hệ Many-to-Many giữa CryoStorageContract và LabSample
    /// </summary>
    public class CPSDetail : BaseEntity
    {
        public int CryoStorageContractId { get; set; }
        public int LabSampleId { get; set; }
        
        public DateTime StorageStartDate { get; set; }
        public DateTime? StorageEndDate { get; set; }
        public string Status { get; set; } = string.Empty; // "Stored", "Released", "Disposed"
        public decimal? MonthlyFee { get; set; }
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual CryoStorageContract? CryoStorageContract { get; set; }
        public virtual LabSample? LabSample { get; set; }
    }
}

