using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho vai trò/quyền hạn trong hệ thống
    /// Xác định các quyền truy cập và chức năng mà người dùng có thể thực hiện
    /// </summary>
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public virtual ICollection<UserRole>? UserRoles { get; set; } = new List<UserRole>();
    }
}
