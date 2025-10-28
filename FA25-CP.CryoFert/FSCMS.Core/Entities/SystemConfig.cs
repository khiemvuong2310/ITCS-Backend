using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
    // Bảng SystemConfig: Cấu hình hệ thống (thông tin trung tâm, quy tắc thông báo...).
    // Không có quan hệ khoá ngoại trực tiếp.
    public class SystemConfig : BaseEntity<Guid>
    {
        protected SystemConfig() : base() { }
        public SystemConfig(Guid id, string centerInfo, string notificationRules, string settings)
        {
            Id = id;
            CenterInfo = centerInfo;
            NotificationRules = notificationRules;
            Settings = settings;
        }
        public string CenterInfo { get; set; } = string.Empty;
        public string NotificationRules { get; set; } = string.Empty;
        public string Settings { get; set; } = string.Empty;
    }
}
