using Application.Features.Users.Command.UserLogin;
using Application.Features.Users.Command.UserRegister;
using Core.Security.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterCommand userRegisterCommand)
        {
            AccessToken register = await Mediator.Send(userRegisterCommand);
            return Created("", register);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginCommand userLoginCommand)
        {
            AccessToken login = await Mediator.Send(userLoginCommand);
            return Ok(login);
        }
    }
}
