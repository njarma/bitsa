using AutoMapper;

namespace Bitsa.User.Api.Model.Classes
{
    public class BaseModel
    {
        protected readonly DomainContext _context;
        public BaseModel(DomainContext context)
        {
            _context = context;
        }
    }
}
