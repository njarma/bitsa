using System.Net;

namespace Bitsa.User.Api.Exceptions.Bitsa
{
    public class BitsaInvalidOperationException : BitsaException
    {
        public BitsaInvalidOperationException() : base(HttpStatusCode.BadRequest)
        {
        }
    }
}
