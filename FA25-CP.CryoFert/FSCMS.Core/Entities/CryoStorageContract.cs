using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;
using FSCMS.Core.Enum;
using FSCMS.Core.Enums;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a cryogenic storage contract between a patient and the cryobank.
    /// Each contract defines the selected package, duration, payment, and renewal information.
    /// </summary>
    public class CryoStorageContract : BaseEntity<Guid>
    {
        /// <summary>
        /// Protected constructor for EF Core.
        /// </summary>
        protected CryoStorageContract() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CryoStorageContract"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the contract.</param>
        /// <param name="patientId">The identifier of the patient who owns the contract.</param>
        /// <param name="cryoPackageId">The identifier of the selected cryo storage package.</param>
        /// <param name="contractNumber">The unique contract number.</param>
        /// <param name="startDate">The start date of the storage contract.</param>
        /// <param name="endDate">The end date of the storage contract.</param>
        /// <param name="totalAmount">The total amount of the contract.</param>
        /// <param name="isAutoRenew">Indicates whether the contract automatically renews upon expiry.</param>
        /// <param name="status">The current status of the contract (Active, Expired, Terminated, Renewed).</param>
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

        // ────────────────────────────────
        // Contract Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the unique contract number.
        /// </summary>
        public string ContractNumber { get; set; } = default!;

        /// <summary>
        /// Gets or sets the contract start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the contract end date.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the contract status (Active, Expired, Terminated, Renewed).
        /// </summary>
        public ContractStatus Status { get; set; } = ContractStatus.Active;

        /// <summary>
        /// Gets or sets the total amount of the contract.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the total amount that has been paid.
        /// </summary>
        public decimal? PaidAmount { get; set; }

        /// <summary>
        /// Indicates whether the contract automatically renews after expiry.
        /// </summary>
        public bool IsAutoRenew { get; set; } = false;

        /// <summary>
        /// Gets or sets the date the contract was signed.
        /// </summary>
        public DateTime? SignedDate { get; set; }

        /// <summary>
        /// Gets or sets the name or identifier of the person who signed the contract.
        /// </summary>
        public string? SignedBy { get; set; }

        /// <summary>
        /// Gets or sets any additional notes related to the contract.
        /// </summary>
        public string? Notes { get; set; }

        // ────────────────────────────────
        // Relationships
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the patient associated with this contract.
        /// </summary>
        public Guid PatientId { get; set; }
        public virtual Patient? Patient { get; set; }

        /// <summary>
        /// Gets or sets the cryo storage package associated with this contract.
        /// </summary>
        public Guid CryoPackageId { get; set; }
        public virtual CryoPackage? CryoPackage { get; set; }

        /// <summary>
        /// Gets or sets the collection of detailed cryo storage records linked to this contract.
        /// </summary>
        public virtual ICollection<CPSDetail> CPSDetails { get; set; } = new List<CPSDetail>();
    }
}
