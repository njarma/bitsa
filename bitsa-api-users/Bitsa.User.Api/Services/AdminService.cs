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
        private IUsersService _userService;
        private readonly IMapper _mapper;
        public AdminService(DomainContext context, IAdminRepository adminRepository, IUsersService userService, IMapper mapper) : base(context)
        {
            _adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper;
        }
        public async Task AddBalance(int targetId, float balance)
        {
            var entity = _mapper.Map<users>(await _userService.GetById(targetId));
            await _adminRepository.AddBalance(entity, balance);
            return;
        }
        public async Task SubstractBalance(int sourceId, float balance)
        {
            var entity = _mapper.Map<users>(await _userService.GetById(sourceId));
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
