using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho cấu hình hệ thống
    /// Lưu trữ các thông số cấu hình và quy tắc hoạt động của hệ thống
    /// </summary>
    public class SystemConfig : BaseEntity
    {
        public string CenterInfo { get; set; }
        public string NotificationRules { get; set; }
        public string Settings { get; set; }
    }
}
