using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Exceptions
{
    public class TooManyRequestsException : BaseException
    {
        public TooManyRequestsException(string message)
            : base(message, HttpStatusCode.TooManyRequests)
        {
        }
    }
}
