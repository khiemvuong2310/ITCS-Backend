using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho chi tiết yêu cầu dịch vụ
    /// Bảng trung gian tạo quan hệ Many-to-Many giữa ServiceRequest và Service
    /// </summary>
    public class ServiceRequestDetails : BaseEntity
    {
        public int ServiceRequestId { get; set; }
        public int ServiceId { get; set; }
        
        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual ServiceRequest? ServiceRequest { get; set; }
        public virtual Service? Service { get; set; }
    }
}

