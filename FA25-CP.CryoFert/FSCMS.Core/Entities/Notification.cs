using System;
using FSCMS.Core.Enum;
using FSCMS.Core.Enums;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Represents a notification within the system.
    /// Handles alerts sent to patients and staff via email, SMS, push notification, or in-app messages.
    /// </summary>
    public class Notification : BaseEntity<Guid>
    {
        /// <summary>
        /// Default constructor for EF Core.
        /// </summary>
        protected Notification() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the notification.</param>
        /// <param name="title">The title of the notification.</param>
        /// <param name="content">The message content of the notification.</param>
        /// <param name="type">The notification type (e.g., Email, SMS, In-App).</param>
        public Notification(Guid id, string title, string content, NotificationType type)
        {
            Id = id;
            Title = title;
            Content = content;
            Type = type;
        }

        // ────────────────────────────────
        // Notification Information
        // ────────────────────────────────

        /// <summary>
        /// Gets or sets the related patient ID (if applicable).
        /// </summary>
        public Guid? PatientId { get; set; }

        /// <summary>
        /// Gets or sets the related user account ID (if applicable).
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Gets or sets the title of the notification.
        /// </summary>
        public string Title { get; set; } = default!;

        /// <summary>
        /// Gets or sets the message content of the notification.
        /// </summary>
        public string Content { get; set; } = default!;

        /// <summary>
        /// Gets or sets the type of the notification.
        /// </summary>
        public NotificationType Type { get; set; }

        /// <summary>
        /// Gets or sets the current status of the notification.
        /// </summary>
        public NotificationStatus Status { get; set; } = NotificationStatus.Delivered;

        /// <summary>
        /// Gets or sets the scheduled time for sending the notification.
        /// </summary>
        public DateTime? ScheduledTime { get; set; }

        /// <summary>
        /// Gets or sets the actual time when the notification was sent.
        /// </summary>
        public DateTime? SentTime { get; set; }

        /// <summary>
        /// Gets or sets the time when the notification was read by the recipient.
        /// </summary>
        public DateTime? ReadTime { get; set; }

        /// <summary>
        /// Gets or sets the communication channel used (Email, SMS, Push, In-App).
        /// </summary>
        public string? Channel { get; set; }

        /// <summary>
        /// Gets or sets the related entity type (e.g., Appointment, Treatment).
        /// </summary>
        public string? RelatedEntityType { get; set; }

        /// <summary>
        /// Gets or sets the related entity ID.
        /// </summary>
        public Guid? RelatedEntityId { get; set; }

        /// <summary>
        /// Indicates whether the notification is marked as important.
        /// </summary>
        public bool IsImportant { get; set; } = false;

        /// <summary>
        /// Gets or sets any internal notes or metadata.
        /// </summary>
        public string? Notes { get; set; }

        // ────────────────────────────────
        // Navigation Properties
        // ────────────────────────────────

        /// <summary>
        /// The patient associated with this notification.
        /// </summary>
        public virtual Patient? Patient { get; set; }

        /// <summary>
        /// The user account associated with this notification.
        /// </summary>
        public virtual Account? User { get; set; }
    }
}
