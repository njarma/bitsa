using Bitsa.Identity.Api.Model.Classes;
using Bitsa.Identity.Api.ViewModels;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Bitsa.Identity.Api.Repositories.IRepositories
{
    public interface IUsersRepository
    {
        Task<UserPostViewModel> GetByEmail(string email);
        Task<UserGetViewModel> Login(UserPostViewModel user, JwtSecurityToken jwt);
    }
}
