using System;
using FSCMS.Core.Enum;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a financial transaction in the system.
    /// Records payments, refunds, adjustments, etc.
    /// This is a standalone entity (logical relationships, no physical foreign keys).
    /// </summary>
    public class Transaction : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected Transaction() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        public Transaction(
            Guid id,
            string transactionCode,
            TransactionType transactionType,
            decimal amount,
            string currency,
            TransactionStatus status,
            DateTime transactionDate
        )
        {
            Id = id;
            TransactionCode = transactionCode;
            TransactionType = transactionType;
            Amount = amount;
            Currency = currency;
            Status = status;
            TransactionDate = transactionDate;
        }

        // ────────────────────────────────
        // Transaction Information
        // ────────────────────────────────

        public string TransactionCode { get; set; } = string.Empty;

        public TransactionType TransactionType { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; } = "VND";

        public DateTime TransactionDate { get; set; }

        public TransactionStatus Status { get; set; }

        public string? PaymentMethod { get; set; } // "Cash", "Card", "Transfer", "Insurance"

        public string? PaymentGateway { get; set; }

        public string? ReferenceNumber { get; set; }

        // ────────────────────────────────
        // Logical Relationships (no FK)
        // ────────────────────────────────

        public int? RelatedEntityId { get; set; } // e.g., Invoice, Contract, ServiceRequest
        public string? RelatedEntityType { get; set; } // "Invoice", "Contract", "ServiceRequest"

        public int? PatientId { get; set; }
        public string? PatientName { get; set; }

        public string? Description { get; set; }
        public string? Notes { get; set; }

        // ────────────────────────────────
        // Payment Details
        // ────────────────────────────────

        public string? CardNumber { get; set; } // Last 4 digits
        public string? CardType { get; set; }
        public string? BankName { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public string? ProcessedBy { get; set; }
    }
}
