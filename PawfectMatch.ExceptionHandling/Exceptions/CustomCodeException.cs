using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PawfectMatch.ExceptionHandling.Exceptions
{
    internal class CustomCodeException : CustomException
    {
        public CustomCodeException(string message, int exceptioncode)
            : base(message, null, exceptioncode)
        {
        }
    }
}
