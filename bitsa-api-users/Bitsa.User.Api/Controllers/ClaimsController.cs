using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Bitsa.User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        protected int GetUserId()
        {
            return int.Parse(User.Claims.First(i => i.Type == "UserId").Value);
        }

        protected int GetIsAdministrator()
        {
            return int.Parse(User.Claims.First(i => i.Type == "Administrator").Value);
        }
    }
}