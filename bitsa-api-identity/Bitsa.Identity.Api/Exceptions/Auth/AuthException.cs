using API.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Auth.Api.Identity.Exceptions.Auth
{
    public class AuthException : HttpStatusCodeException
    {

        public AuthException(HttpStatusCode statusCode) : base(statusCode)
        {
        }
        public AuthException(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
        }
        public AuthException(HttpStatusCode statusCode, Exception innerException) : base(statusCode, innerException)
        {
        }
    }

}
