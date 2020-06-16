using Bitsa.User.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitsa.User.Api.Repositories.IRepositories
{
    public interface IUsersRepository
    {
        Task<UsersGetViewModel> GetById(int userId);
    }
}
