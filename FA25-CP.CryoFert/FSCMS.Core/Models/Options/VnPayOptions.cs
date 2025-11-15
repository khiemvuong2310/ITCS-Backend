using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Models.Options
{
    public class VnPayOptions
    {
        public string vnp_Url { get; set; } = string.Empty;
        public string vnp_Api { get; set; } = string.Empty;
        public string vnp_TmnCode { get; set; } = string.Empty;
        public string vnp_HashSecret { get; set; } = string.Empty;
        public string vnp_Returnurl { get; set; } = string.Empty;
        //public string vnp_IpnUrl { get; set; } = string.Empty;
    }
}
