using System;
using System.Collections.Generic;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho hợp đồng lưu trữ lạnh
    /// Many-to-One với Patient và CryoPackage
    /// One-to-Many với CPSDetail
    /// </summary>
    public class CryoStorageContract : BaseEntity
    {
        public int PatientId { get; set; }
        public int CryoPackageId { get; set; }
        
        public string ContractNumber { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty; // "Active", "Expired", "Terminated", "Renewed"
        public decimal TotalAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public bool IsAutoRenew { get; set; } = false;
        public DateTime? SignedDate { get; set; }
        public string? SignedBy { get; set; }
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual Patient? Patient { get; set; }
        public virtual CryoPackage? CryoPackage { get; set; }
        
        // One-to-Many với CPSDetail
        public virtual ICollection<CPSDetail>? CPSDetails { get; set; } = new List<CPSDetail>();
    }
}

