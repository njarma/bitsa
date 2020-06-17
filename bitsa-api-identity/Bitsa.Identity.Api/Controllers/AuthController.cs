using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using API.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bitsa.Identity.Api.Exceptions.Bitsa;
using Bitsa.Identity.Api.Services.IServices;
using Bitsa.Identity.Api.ViewModels;
using Bitsa.Identity.Api.Model.Classes;

namespace Bitsa.Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ClaimsController
    {
        private readonly IUsersService _service;
        public AuthController(IUsersService service)
        {
            _service = service;
        }

        // GET: api/Auth/Login
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(UserGetViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody]UserPostViewModel User)
        {
            try
            {
                var usr = await _service.Login(User);
                return Ok(usr);
            }
            catch (AggregateException ex)
            {
                throw ex.Flatten().InnerExceptions[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}