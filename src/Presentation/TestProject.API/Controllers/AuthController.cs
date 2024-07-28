using Application.Features.Commands.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestProject.API.Controllers
{
    public class AuthController : BaseController
    {
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            var result = Mediator.Send(registerCommand).Result;
            return Created("", result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }
    }
}
