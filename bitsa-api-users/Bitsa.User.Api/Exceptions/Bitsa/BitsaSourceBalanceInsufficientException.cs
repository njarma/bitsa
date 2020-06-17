using System;
using System.Net;

namespace Bitsa.User.Api.Exceptions.Bitsa
{
    public class BitsaSourceBalanceInsufficientException : BitsaException
    {
        const string message = "El balance del usuario de origen es insuficiente";

        public BitsaSourceBalanceInsufficientException() : base(HttpStatusCode.InternalServerError, message)
        {
        }

        public BitsaSourceBalanceInsufficientException(Exception ex) : base(HttpStatusCode.InternalServerError, ex.ToString())
        {
        }
    }
}
