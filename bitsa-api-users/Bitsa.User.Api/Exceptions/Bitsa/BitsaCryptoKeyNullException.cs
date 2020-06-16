using System;
using System.Net;

namespace Bitsa.User.Api.Exceptions.Bitsa
{
    public class BitsaCryptoKeyNullException : BitsaException
    {
        const string message = "Crypto Key is not defined in config file";

        public BitsaCryptoKeyNullException() : base(HttpStatusCode.InternalServerError, message)
        {
        }

        public BitsaCryptoKeyNullException(Exception ex) : base(HttpStatusCode.InternalServerError, ex.ToString())
        {
        }
    }
}
