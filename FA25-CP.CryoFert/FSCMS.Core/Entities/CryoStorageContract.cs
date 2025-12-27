using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;
using FSCMS.Core.Enum;
using FSCMS.Core.Enums;

namespace FSCMS.Core.Entities
{
    // Bảng CryoStorageContract: Hợp đồng lưu trữ cryo giữa bệnh nhân và cơ sở.
    // Quan hệ:
    // - n-1 tới Patient (PatientId)
    // - n-1 tới CryoPackage (CryoPackageId)
    // - 1-n tới CPSDetail (liên kết hợp đồng-với-các mẫu lưu trữ)
    public class CryoStorageContract : BaseEntity<Guid>
    {
        protected CryoStorageContract() : base() { }
        public CryoStorageContract(
            Guid id,
            Guid patientId,
            Guid cryoPackageId,
            string contractNumber,
            DateTime startDate,
            DateTime endDate,
            decimal totalAmount,
            bool isAutoRenew = false,
            ContractStatus status = ContractStatus.Active
        )
        {
            Id = id;
            PatientId = patientId;
            CryoPackageId = cryoPackageId;
            ContractNumber = contractNumber;
            StartDate = startDate;
            EndDate = endDate;
            TotalAmount = totalAmount;
            IsAutoRenew = isAutoRenew;
            Status = status;
        }
        public string ContractNumber { get; set; } = default!;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ContractStatus Status { get; set; } = ContractStatus.Active;
        public decimal TotalAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public bool IsAutoRenew { get; set; } = false;
        public DateTime? SignedDate { get; set; }
        public string? SignedBy { get; set; }
        public string? Notes { get; set; }
        public Guid PatientId { get; set; }
        public virtual Patient? Patient { get; set; }
        public Guid? RenewFromContractId { get; set; }
        public virtual CryoStorageContract? RenewFromContract { get; set; }
        public Guid CryoPackageId { get; set; }
        public virtual CryoPackage? CryoPackage { get; set; }
        public virtual ICollection<CPSDetail> CPSDetails { get; set; } = new List<CPSDetail>();
    }
}
