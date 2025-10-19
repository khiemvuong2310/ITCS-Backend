using System;
using System.Collections.Generic;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho vai trò/quyền hạn trong hệ thống
    /// Xác định các quyền truy cập và chức năng mà người dùng có thể thực hiện
    /// </summary>
    public class Role : BaseEntity
    {
        public string RoleName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public virtual ICollection<Account>? Users { get; set; } = new List<Account>();
    }
}
