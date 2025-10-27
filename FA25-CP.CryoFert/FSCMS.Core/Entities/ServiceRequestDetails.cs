using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    public class ServiceRequestDetails : BaseEntity<Guid>
    {
        protected ServiceRequestDetails() : base() { }
        public ServiceRequestDetails(Guid id, Guid serviceRequestId, Guid serviceId, int quantity, decimal unitPrice)
        {
            Id = id;
            ServiceRequestId = serviceRequestId;
            ServiceId = serviceId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = quantity * unitPrice;
        }
        public Guid ServiceRequestId { get; set; }
        public Guid ServiceId { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Notes { get; set; }
        public virtual ServiceRequest? ServiceRequest { get; set; }
        public virtual Service? Service { get; set; }
    }
}
