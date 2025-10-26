using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when a requested resource is not found.
    /// </summary>
    public class NotFoundException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public NotFoundException(string message)
            : base(message, HttpStatusCode.NotFound)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="resourceName">The name of the resource type that was not found.</param>
        /// <param name="id">The identifier that was not found.</param>
        public NotFoundException(string resourceName, object id)
            : base($"{resourceName} with given parameter {id} was not found.", HttpStatusCode.NotFound)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public NotFoundException(string message, Exception innerException)
            : base(message, HttpStatusCode.NotFound, innerException)
        {
        }
    }
}
