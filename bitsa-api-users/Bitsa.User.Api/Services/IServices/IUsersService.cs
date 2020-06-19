using Bitsa.User.Api.Model.Classes;
using Bitsa.User.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitsa.User.Api.Services.IServices
{
    public interface IUsersService
    {
        Task<UsersViewModel> GetById(int userId);
        Task<UsersViewModel> GetByAlias(string alias);
        Task AddBalance(int targetId, float balance);
        Task SubstractBalance(int sourceId, float balance);
        Task<IEnumerable<UsersViewModel>> GetAll();
        Task<UsersGetViewModel> Save(UsersPostViewModel entity);
        Task<UsersGetViewModel> Update(UsersPutViewModel entity);
        Task<UsersGetViewModel> Delete(int entityId);
        Task TransferBalance(int sourceId, TransferBalanceFilterViewModel filter);
    }
}
