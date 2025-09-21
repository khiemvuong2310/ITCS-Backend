using System;
using FSCMS.Core.Enum;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho thông báo
    /// Quản lý các thông báo gửi đến bệnh nhân và nhân viên qua email, SMS, push notification
    /// </summary>
    public class Notification : BaseEntity
    {
        public int? PatientId { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public NotificationType Type { get; set; }
        public NotificationStatus Status { get; set; }
        public DateTime? ScheduledTime { get; set; }
        public DateTime? SentTime { get; set; }
        public DateTime? ReadTime { get; set; }
        public string? Channel { get; set; } // Email, SMS, Push, In-App
        public string? RelatedEntityType { get; set; } // Appointment, Treatment, etc.
        public int? RelatedEntityId { get; set; }
        public bool IsImportant { get; set; } = false;
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual Patient? Patient { get; set; }
        public virtual User? User { get; set; }
    }
}
