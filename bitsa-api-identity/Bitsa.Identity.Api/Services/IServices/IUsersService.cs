using Bitsa.Identity.Api.Model.Classes;
using Bitsa.Identity.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitsa.Identity.Api.Services.IServices
{
    public interface IUsersService
    {
        Task<UserGetViewModel> Login(UserPostViewModel user);
    }
}
