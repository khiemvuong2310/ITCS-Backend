using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a medical service provided in the fertility center system.
    /// Includes services such as IVF, IUI, laboratory tests, consultations, and other procedures.
    /// </summary>
    public class Service : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected Service() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the service.</param>
        /// <param name="name">The display name of the service.</param>
        /// <param name="price">The standard price of the service.</param>
        /// <param name="serviceCategoryId">The ID of the category this service belongs to.</param>
        public Service(Guid id, string name, decimal price, Guid serviceCategoryId)
        {
            Id = id;
            Name = name;
            Price = price;
            ServiceCategoryId = serviceCategoryId;
        }

        // ────────────────────────────────
        // Service Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the name of the service (e.g., "IVF Cycle", "Semen Analysis").
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a description of the service.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the standard price for this service.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the internal or billing code for the service.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the measurement unit for the service (e.g., "session", "package", "sample").
        /// </summary>
        public string? Unit { get; set; }

        /// <summary>
        /// Gets or sets the expected duration of the service in minutes.
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// Indicates whether this service is currently active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets internal notes or additional information.
        /// </summary>
        public string? Notes { get; set; }

        // ────────────────────────────────
        // Relationships
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the ID of the category that this service belongs to.
        /// </summary>
        public Guid ServiceCategoryId { get; set; }

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        /// <summary>
        /// The category that this service is classified under.
        /// </summary>
        public virtual ServiceCategory? ServiceCategory { get; set; }

        /// <summary>
        /// The collection of service request details associated with this service.
        /// </summary>
        public virtual ICollection<ServiceRequestDetails> ServiceRequestDetails { get; set; } = new List<ServiceRequestDetails>();
    }
}
