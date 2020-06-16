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

            /*
            Una alternatica utilizando expresiones lampda (método de extensión)

            var dbset = _context.users.OrderByDescending(x => x.Entry_Date)
                        .FirstOrDefault(x => x.Id == userId);
            */

            /* Empleando Sintáxis de consulta */
            var dbset = (from u in _context.users
                         where u.Id == userId
                         orderby u.Entry_Date descending
                         select new users
                         {
                             Id = u.Id,
                             First_Name = u.First_Name,
                             Last_Name = u.Last_Name,
                             Alias = u.Alias,
                             Password = u.Password,
                             Email = u.Email,
                             Entry_Date = u.Entry_Date,
                             Enabled = u.Enabled,
                             Balance = u.Balance,
                             Administrator = u.Administrator
                         })
                         .FirstOrDefault();
            
            return Task.FromResult(_mapper.Map<UsersGetViewModel>(dbset));
        }
    }
}
