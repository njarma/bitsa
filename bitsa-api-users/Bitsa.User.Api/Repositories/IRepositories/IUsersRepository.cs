﻿using Bitsa.User.Api.Model.Classes;
using Bitsa.User.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitsa.User.Api.Repositories.IRepositories
{
    public interface IUsersRepository
    {
        Task<UsersViewModel> GetById(int userId);
        Task<UsersViewModel> GetByAlias(string alias);
        Task AddBalance(users user, float balance);
        Task SubstractBalance(users user, float balance);
        Task<IEnumerable<UsersViewModel>> GetAll();
        Task<UsersGetViewModel> Save(UsersPostViewModel entity);
        Task<UsersGetViewModel> Update(UsersPutViewModel entity);
        Task<UsersGetViewModel> Delete(int entityId);
    }
}
