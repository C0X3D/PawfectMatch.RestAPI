using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PawfectMatch.ExceptionHandling.Exceptions
{
    internal class BasicCodeException : CustomException
    {
        public BasicCodeException(string message, HttpStatusCode httpStatusCode)
            : base(message, null, (int)httpStatusCode)
        {
        }
    }
}
