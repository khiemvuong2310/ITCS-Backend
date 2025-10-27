using System;
using FSCMS.Core.Models.Bases;

namespace FSCMS.Core.Entities
{
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
