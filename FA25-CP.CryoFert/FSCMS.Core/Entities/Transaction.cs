using System;
using FSCMS.Core.Enum;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    // Bảng Transaction: Giao dịch tài chính (thu/chi, cổng thanh toán...).
    // Bảng độc lập; liên kết logic tới đối tượng liên quan qua RelatedEntity*
    public class Transaction : BaseEntity<Guid>
    {
        public Transaction() : base() { }
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
        public string TransactionCode { get; set; } = string.Empty;

        public TransactionType TransactionType { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; } = "VND";

        public DateTime TransactionDate { get; set; }

        public TransactionStatus Status { get; set; }

        public string? PaymentMethod { get; set; } // "Cash", "Card", "Transfer", "Insurance"

        public string? PaymentGateway { get; set; }

        public string? ReferenceNumber { get; set; }
        public Guid RelatedEntityId { get; set; } // e.g., Invoice, Contract, ServiceRequest
        public string? RelatedEntityType { get; set; } // "Invoice", "Contract", "ServiceRequest"

        public Guid PatientId { get; set; }
        public string? PatientName { get; set; }

        public string? Description { get; set; }
        public string? Notes { get; set; }
        public string? CardNumber { get; set; } // Last 4 digits
        public string? CardType { get; set; }
        public string? BankName { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public string? ProcessedBy { get; set; }
    }
}
