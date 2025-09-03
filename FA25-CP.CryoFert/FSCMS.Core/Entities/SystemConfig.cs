using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    public class SystemConfig : BaseEntity
    {
        public string CenterInfo { get; set; }
        public string NotificationRules { get; set; }
        public string Settings { get; set; }
    }
}
