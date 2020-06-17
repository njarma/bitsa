using AutoMapper;
using Bitsa.User.Api.Exceptions.Bitsa;
using Bitsa.User.Api.Model.Classes;
using Bitsa.User.Api.Repositories.IRepositories;
using Bitsa.User.Api.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Util.Validators.Crypto;

namespace Bitsa.User.Api.Repositories
{
    public class AdminRepository : BaseModel, IAdminRepository
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AdminRepository(DomainContext context, IMapper mapper, IConfiguration configuration) : base(context)
        {
            _mapper = mapper;
            _configuration = configuration;
        }
        public Task<IEnumerable<UsersViewModel>> GetAll()
        {

            /*
            Una alternatica utilizando expresiones lampda (método de extensión)

            var dbset = _context.users.OrderByDescending(x => x.Id).AsEnumerable();
            */

            /* Empleando Sintáxis de consulta */
            var dbset = (from u in _context.users
                         orderby u.Id descending
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
                         .AsEnumerable();

            return Task.FromResult(_mapper.Map<IEnumerable<UsersViewModel>>(dbset));
        }

        public Task<UsersGetViewModel> Save(UsersPostViewModel entity)
        {
            var key = _configuration["Crypto:Key"];
            entity.Password = Util.Crypto.CryptText(entity.Password, key, SYM_Providers.Rijndael, KeySize.Bits_256);

            var entityDb = _context.users.Add(_mapper.Map<users>(entity)).Entity;
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<UsersGetViewModel>(entityDb));
        }

        public Task<UsersGetViewModel> Update(UsersPutViewModel entity)
        {
            var entityDb = _context.users.FirstOrDefault(x => x.Id == entity.Id);
            if (entityDb == null) throw new BitsaEntityToEditNotFoundException();
            _mapper.Map(entity, entityDb);
            //db.Entry(model).Property(x => x.Balance).State = PropertyState.Unmodified;
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<UsersGetViewModel>(entityDb));
        }

        public Task<UsersGetViewModel> Delete(int entityId)
        {
            var entityDb = _context.users.FirstOrDefault(x => x.Id == entityId);
            if (entityDb == null) throw new BitsaEntityToDeleteNotFoundException();
            _context.users.Remove(entityDb);
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<UsersGetViewModel>(entityDb));
        }

        //public Task SubstractBalance(users user, float balance)
        //{
        //    user.Balance -= balance;
        //    _context.Entry(user).State = EntityState.Modified;
        //    return Task.FromResult(_context.SaveChanges());
        //}

        //public Task AddBalance(users user, float balance)
        //{
        //    user.Balance += balance;
        //    _context.Entry(user).State = EntityState.Modified;
        //    return Task.FromResult(_context.SaveChanges());
        //}

    }
}
