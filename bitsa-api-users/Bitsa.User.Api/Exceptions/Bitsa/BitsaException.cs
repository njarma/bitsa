using API.Exceptions;
using System;
using System.Net;

namespace Bitsa.User.Api.Exceptions.Bitsa
{
    public class BitsaException : HttpStatusCodeException
    {

        public BitsaException(HttpStatusCode statusCode) : base(statusCode)
        {
        }
        public BitsaException(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
        }
        public BitsaException(HttpStatusCode statusCode, Exception innerException) : base(statusCode, innerException)
        {
        }
    }

}
