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
    public class UsersService: BaseModel, IUsersService
    {
        private IUsersRepository _userRepository;
        public UsersService(DomainContext context, IUsersRepository usersRepository): base(context)
        {
            _userRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        }
        public Task<UsersViewModel> GetById(int userId)
        {
            return _userRepository.GetById(userId);
        }
        public Task<UsersViewModel> GetByAlias(string alias)
        {
            return _userRepository.GetByAlias(alias);
        }
        public Task AddBalance(users user, float balance)
        {
           return _userRepository.AddBalance(user, balance);
        }
        public Task SubstractBalance(users user, float balance)
        {
            return _userRepository.SubstractBalance(user, balance);
        }
        public Task<IEnumerable<UsersViewModel>> GetAll()
        {
            return _userRepository.GetAll();
        }

        public Task<UsersGetViewModel> Save(UsersPostViewModel entity)
        {
            return _userRepository.Save(entity);
        }

        public Task<UsersGetViewModel> Update(UsersPutViewModel entity)
        {
            return _userRepository.Update(entity);
        }

        public Task<UsersGetViewModel> Delete(int entityId)
        {
            return _userRepository.Delete(entityId);
        }
    }
}
