using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestProject.API.Controllers;


public class UserController : BaseController
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}