using Application.Features.Profiles.Queries.GetCurrentUser;
using Microsoft.AspNetCore.Mvc;

namespace TestProject.API.Controllers;


public class ProfilesController : BaseController
{
    [HttpGet("GetCurrentUser")]
    public IActionResult GetCurrentUser()
    {
        var result = Mediator.Send(new GetCurrentUserQuery()).Result;
        return Ok(result);
    }
}