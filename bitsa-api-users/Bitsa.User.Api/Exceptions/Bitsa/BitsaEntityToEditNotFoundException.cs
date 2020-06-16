using System;
using System.Net;

namespace Bitsa.User.Api.Exceptions.Bitsa
{
    public class BitsaEntityToEditNotFoundException : BitsaException
    {
        const string _message = "No se encontró la entidad a editar";

        public BitsaEntityToEditNotFoundException() : base(HttpStatusCode.InternalServerError, _message)
        {

        }
        public BitsaEntityToEditNotFoundException(HttpStatusCode statusCode) : base(statusCode, _message)
        {
        }

        public BitsaEntityToEditNotFoundException(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
        }

        public BitsaEntityToEditNotFoundException(HttpStatusCode statusCode, Exception innerException) : base(statusCode, innerException)
        {
        }
    }
}
