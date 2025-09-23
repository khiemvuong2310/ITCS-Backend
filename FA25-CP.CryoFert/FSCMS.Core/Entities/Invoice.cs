using System;
using System.Collections.Generic;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Hóa đơn thanh toán
    /// </summary>
    public class Invoice : BaseEntity
    {
        public int PatientId { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
        public decimal Subtotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = "Unpaid"; // Unpaid, Paid, Void
        public string? Notes { get; set; }

        public virtual Patient? Patient { get; set; }
        public virtual ICollection<InvoiceItem>? Items { get; set; } = new List<InvoiceItem>();
    }

    public class InvoiceItem : BaseEntity
    {
        public int InvoiceId { get; set; }
        public int? ServiceId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }

        public virtual Invoice? Invoice { get; set; }
        public virtual Service? Service { get; set; }
    }
}


