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
        Task TransferBalance(int sourceId, TransferBalanceFilterViewModel filter);
    }
}
