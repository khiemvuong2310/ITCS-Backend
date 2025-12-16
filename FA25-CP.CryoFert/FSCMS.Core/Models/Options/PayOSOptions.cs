using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Models.Options
{
    public class PayOSOptions
    {
        public string pos_ClientId { get; set; } = string.Empty;
        public string pos_ApiKey { get; set; } = string.Empty;
        public string pos_ChecksumKey { get; set; } = string.Empty;

        public string pos_ReturnUrl { get; set; } = string.Empty;
        public string pos_CancelUrl { get; set; } = string.Empty;
        public string pos_WebhookUrl { get; set; } = string.Empty;
    }
}
