using System;
using FSCMS.Core.Enum;

namespace FSCMS.Service.ReponseModel
{
    public class TransactionResponseModel
    {
        public Guid Id { get; set; }
        public string TransactionCode { get; set; } = string.Empty;
        public string PaymentUrl { get; set; } = string.Empty;
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "VND";
        public DateTime TransactionDate { get; set; }
        public TransactionStatus Status { get; set; }

        public string? PaymentMethod { get; set; } // "Cash", "Card", "Transfer", "VNPay"
        public string? PaymentGateway { get; set; } // e.g., "VNPay"
        public string? ReferenceNumber { get; set; } // VNPay transaction id
        public string? Description { get; set; }
        public string? Notes { get; set; }

        public Guid PatientId { get; set; }
        public string? PatientName { get; set; }

        public DateTime? ProcessedDate { get; set; }
        public string? ProcessedBy { get; set; }

        // Optional: related entity info
        public string? RelatedEntityType { get; set; } // "Invoice", "Contract", etc.
        public Guid RelatedEntityId { get; set; }
        //public string Exp { get; set; }
        //public string Local { get; set; }
        //public string Now { get; set; }
    }
}
