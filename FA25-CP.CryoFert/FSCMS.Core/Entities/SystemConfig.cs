using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a system configuration entity.
    /// Stores various system settings, rules, and center information for operation.
    /// </summary>
    public class SystemConfig : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected SystemConfig() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemConfig"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the system configuration.</param>
        /// <param name="centerInfo">Information about the fertility center.</param>
        /// <param name="notificationRules">Rules for sending notifications (email, SMS, push).</param>
        /// <param name="settings">Other system settings in JSON or key-value format.</param>
        public SystemConfig(Guid id, string centerInfo, string notificationRules, string settings)
        {
            Id = id;
            CenterInfo = centerInfo;
            NotificationRules = notificationRules;
            Settings = settings;
        }

        // ────────────────────────────────
        // Configuration Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets information about the center (name, address, contact, etc.).
        /// </summary>
        public string CenterInfo { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets rules for sending notifications (e.g., email, SMS, push notifications).
        /// </summary>
        public string NotificationRules { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets other system settings, typically stored in JSON or key-value format.
        /// </summary>
        public string Settings { get; set; } = string.Empty;
    }
}
