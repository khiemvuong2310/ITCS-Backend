using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Models.Options
{
    /// <summary>
    ///     The redis configuration options.
    /// </summary>
    public class RedisOptions
    {
        public const string KeyName = "RedisConfiguration";

        /// <summary>
        /// The connection string for Redis
        /// </summary>
        public string? ConnectionString { get; set; }

        public string? Host { get; set; }

        public int? Port { get; set; }

        public string? User { get; set; }

        public string? Password { get; set; }

        public bool UseSsl { get; set; } = true;

        public bool AbortOnConnectFail { get; set; } = false;

        public int ConnectRetry { get; set; } = 3;

        public int ConnectTimeout { get; set; } = 5000;
    }
}
