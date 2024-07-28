using Application.Features.Commands.Auth.Login;
using Application.Features.Commands.Auth.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestProject.API.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            var result = Mediator.Send(registerCommand).Result;
            return Created("", result);
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginCommand registerCommand)
        {
            var result = Mediator.Send(registerCommand).Result;
            return Ok(result);
        }
    }
}
