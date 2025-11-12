using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when a resource conflict occurs.
    /// </summary>
    public class ConflictException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ConflictException(string message)
            : base(message, HttpStatusCode.Conflict)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ConflictException(string message, Exception innerException)
            : base(message, HttpStatusCode.Conflict, innerException)
        {
        }
    }
}
