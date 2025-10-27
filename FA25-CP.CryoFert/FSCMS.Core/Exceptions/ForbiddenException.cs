using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when a user is not authorized to perform the requested operation.
    /// </summary>
    public class ForbiddenException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ForbiddenException(string message)
            : base(message, HttpStatusCode.Forbidden)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ForbiddenException(string message, Exception innerException)
            : base(message, HttpStatusCode.Forbidden, innerException)
        {
        }
    }
}
