using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho từng dịch vụ trong gói dịch vụ
    /// Xác định dịch vụ nào thuộc gói nào và số lượng, giảm giá
    /// </summary>
    public class ServicePackageItem : BaseEntity
    {
        public int ServicePackageId { get; set; }
        public int ServiceId { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal? DiscountPercentage { get; set; }
        public bool IsRequired { get; set; } = true;
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual ServicePackage? ServicePackage { get; set; }
        public virtual Service? Service { get; set; }
    }
}
