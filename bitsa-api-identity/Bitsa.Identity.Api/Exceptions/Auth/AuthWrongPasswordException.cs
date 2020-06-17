using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Auth.Api.Identity.Exceptions.Auth
{
    public class AuthWrongPasswordException : AuthException
    {
        const string mensaje = "Contraseña Incorrecta";
        public AuthWrongPasswordException() : base(HttpStatusCode.Unauthorized, mensaje)
        {
        }

        public AuthWrongPasswordException(Exception ex) : base(HttpStatusCode.InternalServerError, ex.ToString())
        {
        }
    }
}
