using API.Exceptions;
using AutoMapper;
using Bitsa.User.Api.Model.Classes;
using Bitsa.User.Api.Repositories.IRepositories;
using Bitsa.User.Api.Services.IServices;
using Bitsa.User.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bitsa.User.Api.Services
{
    public class AdminService : BaseModel, IAdminService
    {
        private IAdminRepository _adminRepository;
        private IUsersRepository _userRepository;
        private readonly IMapper _mapper;
        public AdminService(DomainContext context, IAdminRepository adminRepository, IUsersRepository userRepository, IMapper mapper) : base(context)
        {
            _adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper;
        }
        public async Task AddBalance(int targetId, float balance)
        {
            var entity = _mapper.Map<users>(await _userRepository.GetById(targetId));
            await _adminRepository.AddBalance(entity, balance);
            return;
        }
        public async Task SubstractBalance(int sourceId, float balance)
        {
            var entity = _mapper.Map<users>(await _userRepository.GetById(sourceId));
            if (entity.Balance - balance < 0)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "El usuario no dispone del balance ingresado");

            await _adminRepository.SubstractBalance(entity, balance);
            return;
        }

        public Task<IEnumerable<UsersViewModel>> GetAll()
        {
            return _adminRepository.GetAll();
        }

        public Task<UsersGetViewModel> Save(UsersPostViewModel entity)
        {
            return _adminRepository.Save(entity);
        }

        public Task<UsersGetViewModel> Update(UsersPutViewModel entity)
        {
            return _adminRepository.Update(entity);
        }

        public Task<UsersGetViewModel> Delete(int entityId)
        {
            return _adminRepository.Delete(entityId);
        }
    }
}
