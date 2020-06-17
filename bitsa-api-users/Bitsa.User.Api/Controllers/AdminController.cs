﻿using System;
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
using Bitsa.User.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Bitsa.User.Api.ViewModels.Filters;

namespace Bitsa.Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdministratorOnly")]
    public class AdminController : ClaimsController
    {
        private readonly IUsersService _service;
        private readonly IMapper _mapper;
        private readonly DomainContext _context;

        public AdminController(IUsersService service, IMapper mapper, DomainContext context)
        {
            _service = service;
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Admin/{id}
        [HttpGet()]
        [ProducesResponseType(typeof(UsersGetViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAll();
            return Ok(_mapper.Map<IEnumerable<UsersGetViewModel>>(result) ?? throw new BitsaEntityNotExistsException());
        }

        // POST: api/Admin
        [HttpPost]
        [ProducesResponseType(typeof(UsersGetViewModel), StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Post([FromBody] UsersPostViewModel entity)
        {
            return Accepted(await _service.Save(entity));
        }

        //PUT: api/Admin/{id}
        [HttpPut("{id:int:min(1)}")]
        [ProducesResponseType(typeof(UsersGetViewModel), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] UsersPutViewModel entity)
        {
            if (id != entity.Id)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Identificador incorrecto");

            return Accepted(await _service.Update(entity));
        }

        //DELETE: api/Admin/{id}
        [HttpDelete("{id:int:min(1)}")]
        [ProducesResponseType(typeof(UsersGetViewModel), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            return Accepted(await _service.Delete(id));
        }

        // PUT: api/Admin
        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddBalance([FromBody] EditUserBalanceFilterViewModel filter)
        {
            var entity = _mapper.Map<users>(await _service.GetById(filter.Id));
            await _service.AddBalance(entity, filter.Balance);
            _context.SaveChanges();
            return NoContent();
        }

        // PUT: api/Admin
        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SubstractBalance([FromBody] EditUserBalanceFilterViewModel filter)
        {
            var entity = _mapper.Map<users>(await _service.GetById(filter.Id));
            if (entity.Balance - filter.Balance < 0)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "El usuario no dispone del balance ingresado");
            await _service.SubstractBalance(entity, filter.Balance);
            _context.SaveChanges();
            return NoContent();
        }

    }
}