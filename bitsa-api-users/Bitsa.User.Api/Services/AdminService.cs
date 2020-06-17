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
    public class AdminService : BaseModel, IAdminService
    {
        private IAdminRepository _adminRepository;

        public AdminService(DomainContext context, IAdminRepository adminRepository) : base(context)
        {
            _adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
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
