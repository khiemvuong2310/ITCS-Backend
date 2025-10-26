using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when an internal server error occurs.
    /// </summary>
    public class InternalServerErrorException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalServerErrorException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public InternalServerErrorException(string message) : base(message, HttpStatusCode.InternalServerError)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalServerErrorException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public InternalServerErrorException(string message, Exception innerException) : base(message, HttpStatusCode.InternalServerError, innerException)
        {
        }
    }
}
