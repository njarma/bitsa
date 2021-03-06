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
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        public AdminController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;
            _mapper = mapper;
        }

        // GET: api/Admin/{id}
        [HttpGet()]
        [ProducesResponseType(typeof(UsersGetViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _adminService.GetAll();
                return Ok(_mapper.Map<IEnumerable<UsersGetViewModel>>(result) ?? throw new BitsaEntityNotExistsException());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: api/Admin
        [HttpPost]
        [ProducesResponseType(typeof(UsersGetViewModel), StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Post([FromBody] UsersPostViewModel entity)
        {
            try
            {
                return Accepted(await _adminService.Save(entity));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //PUT: api/Admin/{id}
        [HttpPut("{id:int:min(1)}")]
        [ProducesResponseType(typeof(UsersGetViewModel), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] UsersPutViewModel entity)
        {
            try
            {
                if (id != entity.Id)
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Identificador incorrecto");

                return Accepted(await _adminService.Update(entity));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //DELETE: api/Admin/{id}
        [HttpDelete("{id:int:min(1)}")]
        [ProducesResponseType(typeof(UsersGetViewModel), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                return Accepted(await _adminService.Delete(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT: api/Admin
        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddBalance([FromBody] EditUserBalanceFilterViewModel filter)
        {
            try
            {
                await _adminService.AddBalance(filter.Id, filter.Balance);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT: api/Admin
        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SubstractBalance([FromBody] EditUserBalanceFilterViewModel filter)
        {
            try
            {
                await _adminService.SubstractBalance(filter.Id, filter.Balance);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}