using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Interfaces
{
    /// <summary>
    /// Defines a service that provides consistent date and time values across the application.
    /// This interface abstracts the system clock to enable better testability of time-dependent code.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Gets the current UTC date and time.
        /// </summary>
        DateTime UtcNow { get; }

        /// <summary>
        /// Gets the current local date and time.
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// Gets the current UTC date without time component.
        /// </summary>
        DateOnly DateOnlyUtcNow { get; }

        /// <summary>
        /// Gets the current local date without time component.
        /// </summary>
        DateOnly DateOnlyNow { get; }
    }
}
