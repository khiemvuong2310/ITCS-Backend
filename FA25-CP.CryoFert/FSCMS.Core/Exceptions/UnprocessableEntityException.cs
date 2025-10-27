using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when the server understands the content type of the request entity, but was unable to process the contained instructions.
    /// </summary>
    public class UnprocessableEntityException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnprocessableEntityException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public UnprocessableEntityException(string message)
            : base(message, (HttpStatusCode)422)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnprocessableEntityException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public UnprocessableEntityException(string message, Exception innerException)
            : base(message, (HttpStatusCode)422, innerException)
        {
        }
    }
}
