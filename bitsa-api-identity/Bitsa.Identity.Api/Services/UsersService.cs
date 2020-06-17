using Auth.Api.Identity.Exceptions.Auth;
using AutoMapper;
using Bitsa.Identity.Api.Exceptions.Bitsa;
using Bitsa.Identity.Api.Model.Classes;
using Bitsa.Identity.Api.Repositories.IRepositories;
using Bitsa.Identity.Api.Services.IServices;
using Bitsa.Identity.Api.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Bitsa.Identity.Api.Services
{
    public class UsersService : IUsersService
    {
        IUsersRepository _repository;
        IConfiguration _configuration;
        IMapper _mapper;
        public UsersService(IUsersRepository repository,
                            IConfiguration configuration,
                            IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<UserGetViewModel> Login(UserPostViewModel user)
        {
            try
            {
                UserPostViewModel usr = await _repository.GetByEmail(user.Email);
                if (usr == null) throw new AuthWrongUserException();

                var key = _configuration["Crypto:Key"];
                var cPassword = Crypto.CryptText(user.Password, key, Util.Validators.Crypto.SYM_Providers.Rijndael, Util.Validators.Crypto.KeySize.Bits_256);
                user.Password = cPassword;

                if (cPassword != usr.Password) throw new AuthWrongPasswordException();

                var now = DateTime.UtcNow;

                var claims = new Claim[]
                    {
                    new Claim("UserId", usr.Id.ToString()),
                    new Claim("Administrator", usr.Administrator.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, usr.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
                    };

                var secretKey = _configuration["Audience:Secret"];
                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
                var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);


                var jwt = new JwtSecurityToken(
                        issuer: _configuration["Audience:Iss"],
                        audience: _configuration["Audience:Aud"],
                        claims: claims,
                        notBefore: now,
                        expires: now.Add(TimeSpan.FromMinutes(3000)),
                        signingCredentials: creds
                    );

                return await _repository.Login(usr, jwt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
