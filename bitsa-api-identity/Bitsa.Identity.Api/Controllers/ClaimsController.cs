﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Bitsa.Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        protected int GetUserId()
        {
            return int.Parse(User.Claims.First(i => i.Type == "UserId").Value);
        }

        protected int GetCompanyId()
        {
            return int.Parse(User.Claims.First(i => i.Type == "CompanyId").Value);
        }
    }
}