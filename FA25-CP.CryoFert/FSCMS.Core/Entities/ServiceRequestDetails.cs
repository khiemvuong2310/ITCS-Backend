using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a detailed service item within a service request.
    /// This entity creates a many-to-many relationship between <see cref="ServiceRequest"/> and <see cref="Service"/>.
    /// </summary>
    public class ServiceRequestDetails : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected ServiceRequestDetails() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRequestDetails"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the service request detail.</param>
        /// <param name="serviceRequestId">The ID of the related service request.</param>
        /// <param name="serviceId">The ID of the service being requested.</param>
        /// <param name="quantity">The number of units requested.</param>
        /// <param name="unitPrice">The price per unit.</param>
        public ServiceRequestDetails(Guid id, Guid serviceRequestId, Guid serviceId, int quantity, decimal unitPrice)
        {
            Id = id;
            ServiceRequestId = serviceRequestId;
            ServiceId = serviceId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = quantity * unitPrice;
        }

        // ────────────────────────────────
        // Service Request Detail Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the ID of the related service request.
        /// </summary>
        public Guid ServiceRequestId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the service.
        /// </summary>
        public Guid ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the service requested.
        /// </summary>
        public int Quantity { get; set; } = 1;

        /// <summary>
        /// Gets or sets the price per unit for the service.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets any discount applied to this service detail.
        /// </summary>
        public decimal? Discount { get; set; }

        /// <summary>
        /// Gets or sets the total price after applying quantity and discount.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Gets or sets additional notes for this service detail.
        /// </summary>
        public string? Notes { get; set; }

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        /// <summary>
        /// The parent service request associated with this detail.
        /// </summary>
        public virtual ServiceRequest? ServiceRequest { get; set; }

        /// <summary>
        /// The service referenced by this detail.
        /// </summary>
        public virtual Service? Service { get; set; }
    }
}
