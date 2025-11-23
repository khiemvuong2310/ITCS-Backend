using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    // Bảng AuditLog: Nhật ký thao tác người dùng trên dữ liệu (ai/lúc nào/làm gì).
    // Quan hệ:
    // - n-1 tới Account (UserId)
    public class AuditLog
    {
        protected AuditLog()
        {
        }

        public AuditLog(
            Guid id,
            Guid? userId,
            string entityType,
            Guid entityId,
            string action,
            string? oldValues,
            string? newValues,
            string? ipAddress,
            string? userAgent,
            DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            EntityType = entityType;
            EntityId = entityId;
            Action = action;
            OldValues = oldValues;
            NewValues = newValues;
            IpAddress = ipAddress;
            UserAgent = userAgent;
            CreatedAt = createdAt;
        }
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public Guid EntityId { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string Action { get; set; } = string.Empty;
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Account? User { get; set; }
    }
}
