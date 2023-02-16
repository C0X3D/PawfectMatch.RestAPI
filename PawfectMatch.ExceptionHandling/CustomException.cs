using System.Net;

namespace PawfectMatch.ExceptionHandling
{
    public class CustomException : Exception
    {
        public List<string>? ErrorMessages { get; }

        public int StatusCode { get; }

        public CustomException(string message, List<string>? errors = default, int statusCode = (int)HttpStatusCode.InternalServerError)
            : base(message)
        {
            ErrorMessages = errors;
            StatusCode = statusCode;
        }
    }
}