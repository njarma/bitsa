using AutoMapper;
using Bitsa.User.Api.Model.Classes;
using Bitsa.User.Api.Repositories.IRepositories;
using Bitsa.User.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitsa.User.Api.Repositories
{
    public class UsersRepository: UnitOfWork, IUsersRepository
    {
        public UsersRepository(DomainContext context, IMapper mapper): base(context, mapper) {}
        public Task<UsersGetViewModel> GetById(int userId)
        {
            var dbset = _context.users.Where(x => x.Id == userId).FirstOrDefault();
            return Task.FromResult(_mapper.Map<UsersGetViewModel>(dbset));
        }
    }
}
