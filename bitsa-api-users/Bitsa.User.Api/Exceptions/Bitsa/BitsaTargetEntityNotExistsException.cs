using System;
using System.Net;

namespace Bitsa.User.Api.Exceptions.Bitsa
{
    public class BitsaTargetEntityNotExistsException : BitsaException
    {
        const string _message = "No se encontró la entidad destino";

        public BitsaTargetEntityNotExistsException() : base(HttpStatusCode.InternalServerError, _message)
        {

        }
        public BitsaTargetEntityNotExistsException(HttpStatusCode statusCode) : base(statusCode)
        {
        }

        public BitsaTargetEntityNotExistsException(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
        }

        public BitsaTargetEntityNotExistsException(HttpStatusCode statusCode, Exception innerException) : base(statusCode, innerException)
        {
        }
    }
}
