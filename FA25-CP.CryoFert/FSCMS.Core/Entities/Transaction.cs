using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho giao dịch tài chính trong hệ thống
    /// Ghi lại tất cả các giao dịch thanh toán, hoàn tiền, v.v.
    /// Bảng độc lập (quan hệ logic, không có khóa ngoại vật lý)
    /// </summary>
    public class Transaction : BaseEntity
    {
        public string TransactionCode { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty; // "Payment", "Refund", "Adjustment"
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "VND";
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; } = string.Empty; // "Pending", "Completed", "Failed", "Cancelled"
        public string? PaymentMethod { get; set; } // "Cash", "Card", "Transfer", "Insurance"
        public string? PaymentGateway { get; set; }
        public string? ReferenceNumber { get; set; }
        
        // Liên kết logic (không có FK vật lý)
        public int? RelatedEntityId { get; set; } // ID của Invoice, Contract, ServiceRequest, etc.
        public string? RelatedEntityType { get; set; } // "Invoice", "Contract", "ServiceRequest"
        
        public int? PatientId { get; set; }
        public string? PatientName { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        
        // Payment details
        public string? CardNumber { get; set; } // Last 4 digits
        public string? CardType { get; set; }
        public string? BankName { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public string? ProcessedBy { get; set; }
    }
}

