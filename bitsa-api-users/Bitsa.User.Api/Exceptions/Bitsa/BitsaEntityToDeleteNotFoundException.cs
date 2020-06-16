using System;
using System.Net;

namespace Bitsa.User.Api.Exceptions.Bitsa
{
    public class BitsaEntityToDeleteNotFoundException : BitsaException
    {
        const string _message = "No se encontró la entidad a eliminar";

        public BitsaEntityToDeleteNotFoundException() : base(HttpStatusCode.BadRequest, _message)
        {

        }

        public BitsaEntityToDeleteNotFoundException(HttpStatusCode statusCode) : base(statusCode, _message)
        {
        }

        public BitsaEntityToDeleteNotFoundException(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
        }

        public BitsaEntityToDeleteNotFoundException(HttpStatusCode statusCode, Exception innerException) : base(statusCode, innerException)
        {
        }
    }
}
