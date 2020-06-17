using AutoMapper;
using Bitsa.Identity.Api.Exceptions.Bitsa;
using Bitsa.Identity.Api.Model.Classes;
using Bitsa.Identity.Api.Repositories.IRepositories;
using Bitsa.Identity.Api.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Bitsa.Identity.Api.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private DomainContext _context;
        private IMapper _mapper;
        public UsersRepository(DomainContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task<UserPostViewModel> GetByEmail(string email)
        {
            try
            {
                var result = Task.FromResult(_mapper.Map<UserPostViewModel>(_context.users.FirstOrDefault(u => u.Email == email)));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<UserGetViewModel> Login(UserPostViewModel user, JwtSecurityToken jwt)
        {
            try
            {
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                
                var response = _mapper.Map<UserGetViewModel>(user);
                response.Access_token = encodedJwt;
                response.Expires_in = (int)TimeSpan.FromMinutes(3000).TotalSeconds;

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
