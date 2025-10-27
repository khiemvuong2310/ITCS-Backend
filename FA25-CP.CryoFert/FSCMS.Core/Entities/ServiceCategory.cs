using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a category that groups related medical services in the fertility center system.
    /// A category may include multiple services (One-to-Many relationship with <see cref="Service"/>).
    /// </summary>
    public class ServiceCategory : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected ServiceCategory() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceCategory"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the service category.</param>
        /// <param name="name">The display name of the category.</param>
        public ServiceCategory(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        // ────────────────────────────────
        // Category Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the display name of the service category.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a detailed description of the category.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets an internal or business code for this category.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Indicates whether this category is currently active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets the display order when showing categories in the UI.
        /// </summary>
        public int DisplayOrder { get; set; }

        // /// <summary>
        // /// Gets or sets the icon or image path representing this category.
        // /// </summary>
        // public string? Icon { get; set; }

        // ────────────────────────────────
        // Relationships
        // ────────────────────────────────

        /// <summary>
        /// The collection of medical services that belong to this category.
        /// </summary>
        public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
