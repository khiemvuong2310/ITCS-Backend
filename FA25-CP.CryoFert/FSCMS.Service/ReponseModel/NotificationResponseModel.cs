using System;
using FSCMS.Core.Enum;

namespace FSCMS.Service.ReponseModel
{
    public class NotificationResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public NotificationType Type { get; set; }
        public NotificationStatus Status { get; set; }
        public Guid? PatientId { get; set; }
        public string? PatientName { get; set; }
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime? ScheduledTime { get; set; }
        public DateTime? SentTime { get; set; }
        public DateTime? ReadTime { get; set; }
        public string? Channel { get; set; }
        public string? RelatedEntityType { get; set; }
        public Guid? RelatedEntityId { get; set; }
        public bool IsImportant { get; set; } = false;
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
