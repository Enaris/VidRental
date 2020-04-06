using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VidRental.Services.Services;

namespace VidRental.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController(IUsersService usersService)
        {
            UsersService = usersService;
        }

        public IUsersService UsersService { get; }

        [HttpGet("{id}/BaseInfo", Name = UsersNames.GetUserBaseInfo)]
        public async Task<IActionResult> GetUserBaseInfo(string id)
        {
            var userBaseInfo = await UsersService.GetUserBaseInfo(id);

            if (userBaseInfo == null)
                return NotFound(this.NotFoundMessage(id));

            return Ok(userBaseInfo);
        }

    }
}