using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho nhật ký kiểm toán
    /// Ghi lại tất cả các hoạt động của người dùng trong hệ thống để theo dõi và bảo mật
    /// </summary>
    public class AuditLog
    {
        protected AuditLog()
        {
        }

    public AuditLog(
        Guid id,
        Guid userId,
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
        UserId = userId; // We already checked for null in AddAuditLogs method
        EntityType = entityType;
        EntityId = entityId;
        Action = action;
        OldValues = oldValues;
        NewValues = newValues;
        IpAddress = ipAddress;
        UserAgent = userAgent;
        CreatedAt = createdAt;
    }

    /// <summary>
    /// Gets or sets the unique identifier for the log entry.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who made the change.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the type of entity that was modified.
    /// </summary>
    public string EntityType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ID of the entity that was modified.
    /// </summary>
    public Guid EntityId { get; set; }

    /// <summary>
    /// Gets or sets the serialized values of the entity before changes.
    /// Nullable if this is a creation log.
    /// </summary>
    public string? OldValues { get; set; }

    /// <summary>
    /// Gets or sets the serialized values of the entity after changes.
    /// Nullable if this is a deletion log.
    /// </summary>
    public string? NewValues { get; set; }

    /// <summary>
    /// Gets or sets the type of action performed (Create, Update, Delete, etc.).
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the IP address of the user who made the change.
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// Gets or sets the user agent (browser/app) information.
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the log entry was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Navigation property for the related user.
    /// </summary>
    public virtual Account? User { get; set; }
    }
}
