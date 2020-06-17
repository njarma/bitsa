using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Auth.Api.Identity.Exceptions.Auth
{
    public class AuthWrongUserException : AuthException
    {
        const string mensaje = "Usuario Inexistente";
        public AuthWrongUserException() : base(HttpStatusCode.Unauthorized, mensaje)
        {
        }

        public AuthWrongUserException(Exception ex) : base(HttpStatusCode.InternalServerError, ex.ToString())
        {
        }
    }
}
