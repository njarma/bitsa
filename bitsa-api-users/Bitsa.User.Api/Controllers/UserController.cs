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
            var userId = GetUserId();
            var result = await _service.GetById(userId);
            return Ok(_mapper.Map<UsersGetViewModel>(result) ?? throw new BitsaEntityNotExistsException());
        }

        // PUT: api/Users
        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> TransferBalance([FromBody] TransferBalanceFilterViewModel filter)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var sourceId = GetUserId();

                    var source = _mapper.Map<users>(await _service.GetById(sourceId));
                    if (source.Balance - filter.Balance < 0)
                        throw new BitsaSourceBalanceInsufficientException();
                    await _service.SubstractBalance(source, filter.Balance);

                    var target = _mapper.Map<users>(await _service.GetByAlias(filter.Alias));
                    if (target == null)
                        throw new BitsaTargetEntityNotExistsException();
                    await _service.AddBalance(target, filter.Balance);

                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                return NoContent();
            }
        }
    }
}