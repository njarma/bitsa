using System;
using System.Net;

namespace Bitsa.User.Api.Exceptions.Bitsa
{
    public class BitsaEntityNotExistsException : BitsaException
    {
        const string _message = "No se encontró la entidad";

        public BitsaEntityNotExistsException() : base(HttpStatusCode.InternalServerError, _message)
        {

        }
        public BitsaEntityNotExistsException(HttpStatusCode statusCode) : base(statusCode)
        {
        }

        public BitsaEntityNotExistsException(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
        }

        public BitsaEntityNotExistsException(HttpStatusCode statusCode, Exception innerException) : base(statusCode, innerException)
        {
        }
    }
}
