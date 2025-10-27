using System;
using System.Collections.Generic;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a user role or permission group within the system.
    /// Defines what actions and access levels users assigned to this role can perform.
    /// </summary>
    public class Role : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected Role() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the role.</param>
        /// <param name="roleName">The display name of the role (e.g., "Administrator").</param>
        /// <param name="roleCode">The internal code name for this role (e.g., "ADMIN").</param>
        public Role(Guid id, string roleName, string roleCode)
        {
            Id = id;
            RoleName = roleName;
            RoleCode = roleCode;
        }

        // ────────────────────────────────
        // Role Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the display name of the role (e.g., "Administrator", "Doctor", "Patient").
        /// </summary>
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the internal code for this role (used for system-level checks).
        /// Example: ADMIN, DOCTOR, PATIENT, STAFF.
        /// </summary>
        public string RoleCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description or notes about this role.
        /// </summary>
        public string? Description { get; set; }

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        /// <summary>
        /// The collection of user accounts associated with this role.
        /// </summary>
        public virtual ICollection<Account> Users { get; set; } = new List<Account>();
    }
}
