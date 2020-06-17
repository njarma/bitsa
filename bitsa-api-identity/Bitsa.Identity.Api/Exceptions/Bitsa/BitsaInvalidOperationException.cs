using System.Net;

namespace Bitsa.Identity.Api.Exceptions.Bitsa
{
    public class BitsaInvalidOperationException : BitsaException
    {
        public BitsaInvalidOperationException() : base(HttpStatusCode.BadRequest)
        {
        }
    }
}
