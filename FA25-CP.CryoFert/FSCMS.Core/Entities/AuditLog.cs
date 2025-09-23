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
    public class AuditLog : BaseEntity
    {
        public int UserId { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public string Details { get; set; }

        public virtual User? User { get; set; }
    }
}
