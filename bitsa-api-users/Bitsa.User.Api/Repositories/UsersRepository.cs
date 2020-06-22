using AutoMapper;
using Bitsa.User.Api.Exceptions.Bitsa;
using Bitsa.User.Api.Model.Classes;
using Bitsa.User.Api.Repositories.IRepositories;
using Bitsa.User.Api.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using static Util.Validators.Crypto;

namespace Bitsa.User.Api.Repositories
{
    public class UsersRepository: BaseModel, IUsersRepository
    {
        private readonly IMapper _mapper;
        public UsersRepository(DomainContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public Task<UsersViewModel> GetById(int userId)
        {

            /*
            Una alternatica utilizando expresiones lampda (método de extensión)

            var dbset = _context.users.FirstOrDefault(x => x.Id == userId);
            */

            /* Empleando Sintáxis de consulta */
            var dbset = (from u in _context.users
                         where u.Id == userId
                         //orderby u.Entry_Date descending
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
                         .AsNoTracking()
                         .FirstOrDefault();
            
            return Task.FromResult(_mapper.Map<UsersViewModel>(dbset));
        }

        public Task<UsersViewModel> GetByAlias(string alias)
        {
            /* Empleando expresiones lampda (método de extensión) */
            var dbset = _context.users.AsNoTracking()
                                      .FirstOrDefault(x => x.Alias == alias);
            return Task.FromResult(_mapper.Map<UsersViewModel>(dbset));
        }
    }
}
