using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Sprache;

namespace FSCMS.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when a request is invalid or contains errors.
    /// </summary>
    public class BadRequestException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public BadRequestException(string message)
            : base(message, HttpStatusCode.BadRequest)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public BadRequestException(string message, Exception innerException)
            : base(message, HttpStatusCode.BadRequest, innerException)
        {
        }
    }
}
