using Application.Features.Profiles.Commands.UpdateUser;
using Application.Features.Profiles.Queries.GetCurrentUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestProject.API.Controllers;


public class ProfilesController : BaseController
{
    [HttpGet("CurrentUser")]
    public IActionResult CurrentUser()
    {
        var result = Mediator.Send(new GetCurrentUserQuery()).Result;
        return Ok(result);
    }  
    
    [HttpPut("CurrentUser")]
    public IActionResult CurrentUser([FromBody] UpdateUserCommand updateUserCommand)
    {
        var result = Mediator.Send(updateUserCommand).Result;
        return Ok(result);
    }
}