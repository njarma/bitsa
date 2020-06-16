using AutoMapper;
using Bitsa.User.Api.Model.Classes;
using Bitsa.User.Api.Repositories.IRepositories;
using Bitsa.User.Api.Services.IServices;
using Bitsa.User.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitsa.User.Api.Services
{
    public class UsersService: UnitOfWork, IUsersService
    {
        private IUsersRepository _userRepository;
        public UsersService(DomainContext context, IMapper mapper, IUsersRepository usersRepository): base(context, mapper)
        {
            _userRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        }
        public Task<UsersGetViewModel> GetById(int userId)
        {
            return _userRepository.GetById(userId);
        }
    }
}
