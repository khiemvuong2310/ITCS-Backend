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
        public string ConnectionString { get; set; } = null!;
    }
}
