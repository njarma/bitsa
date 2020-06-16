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

namespace Bitsa.User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ClaimsController
    {
        private readonly IUsersService _service;
        public UsersController(IUsersService service)
        {
            _service = service;
        }

        //// GET: api/Users
        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<UsersViewModel>), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Get()
        //{
        //    return Ok(await _service.GetAll());
        //}

        // GET: api/Users/{id}
        [HttpGet("{id:int:min(1)}")]
        [ProducesResponseType(typeof(UsersGetViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            return Ok(await _service.GetById(id) ?? throw new BitsaEntityNotExistsException());
        }

        //// POST: api/Users/GetByFilter
        //[HttpPost("[action]")]
        //[ProducesResponseType(typeof(IEnumerable<UsersViewModel>), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> GetByFilter([FromBody]UsersFilterViewModel filters)
        //{
        //    return Ok(await _service.GetByFilter(filters));
        //}

        //// POST: api/Users/
        //[HttpPost]
        //[ProducesResponseType(typeof(UsersViewModel), StatusCodes.Status202Accepted)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Post([FromBody] UsersViewModel entityToAdd)
        //{
        //    return Accepted(await _service.Save(entityToAdd));
        //}

        //// PUT: api/Users/{id}
        //[HttpPut("{id:int:min(1)}")]
        //[ProducesResponseType(typeof(UsersViewModel), StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Put([FromRoute]int id, [FromBody] UsersViewModel entityToEdit)
        //{
        //    if (id != entityToEdit.Id)
        //        throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Identificador incorrecto");
        //    return Accepted(await _service.Update(entityToEdit));
        //}

        //// DELETE: api/Users/{id}
        //[HttpDelete("{id:int:min(1)}")]
        //[ProducesResponseType(typeof(UsersViewModel), StatusCodes.Status202Accepted)]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    return Accepted(await _service.Delete(id));
        //}
    }
}