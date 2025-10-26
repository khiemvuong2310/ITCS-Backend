using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSCMS.Core.Interfaces;

namespace FSCMS.Core.Services
{
    /// <summary>
    /// Implementation of IDateTimeProvider that uses the system clock.
    /// </summary>
    public class SystemDateTimeProvider : IDateTimeProvider
    {
        /// <inheritdoc/>
        public DateTime UtcNow => DateTime.UtcNow;

        /// <inheritdoc/>
        public DateTime Now => DateTime.Now;

        /// <inheritdoc/>
        public DateOnly DateOnlyUtcNow => DateOnly.FromDateTime(DateTime.UtcNow);

        /// <inheritdoc/>
        public DateOnly DateOnlyNow => DateOnly.FromDateTime(DateTime.Now);
    }
}
