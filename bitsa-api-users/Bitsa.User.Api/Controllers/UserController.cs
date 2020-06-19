using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using API.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bitsa.User.Api.Exceptions.Bitsa;
using Bitsa.User.Api.Services.IServices;
using Bitsa.User.Api.ViewModels;
using Bitsa.User.Api.Model.Classes;
using AutoMapper;


namespace Bitsa.User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ClaimsController
    {
        private readonly IUsersService _service;
        private readonly IMapper _mapper;
        private readonly DomainContext _context;
        public UserController(IUsersService service, IMapper mapper, DomainContext context)
        {
            _service = service;
            _mapper = mapper;
            _context = context;
        }

        // GET: api/User/me
        [HttpGet("me")]
        [ProducesResponseType(typeof(UsersGetViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var userId = GetUserId();
                var result = await _service.GetById(userId);
                return Ok(_mapper.Map<UsersGetViewModel>(result) ?? throw new BitsaEntityNotExistsException());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT: api/Users
        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> TransferBalance([FromBody] TransferBalanceFilterViewModel filter)
        {
            try
            {
                var sourceId = GetUserId();
                await _service.TransferBalance(sourceId, filter);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}