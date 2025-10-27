using System;
using FSCMS.Core.Enum;
using FSCMS.Core.Enums;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    public class Notification : BaseEntity<Guid>
    {
        protected Notification() : base() { }
        public Notification(Guid id, string title, string content, NotificationType type)
        {
            Id = id;
            Title = title;
            Content = content;
            Type = type;
        }
        public Guid? PatientId { get; set; }
        public Guid? UserId { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public NotificationType Type { get; set; }
        public NotificationStatus Status { get; set; } = NotificationStatus.Delivered;
        public DateTime? ScheduledTime { get; set; }
        public DateTime? SentTime { get; set; }
        public DateTime? ReadTime { get; set; }
        public string? Channel { get; set; }
        public string? RelatedEntityType { get; set; }
        public Guid? RelatedEntityId { get; set; }
        public bool IsImportant { get; set; } = false;
        public string? Notes { get; set; }
        public virtual Patient? Patient { get; set; }
        public virtual Account? User { get; set; }
    }
}
