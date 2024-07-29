using Application.Features.Topics.Commands.CreateTopic;
using Application.Features.Topics.Queries.GetTopicList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestProject.API.Controllers;

[AllowAnonymous]
public class TopicsController : BaseController
{
    [HttpPost]
    public IActionResult Create([FromBody]CreateTopicCommand createTopicCommand)
    {
        var result = Mediator.Send(createTopicCommand).Result;
        return Created("", result);
    }
    [HttpGet]
    public IActionResult Get()
    {
        var result = Mediator.Send(new GetTopicListQuery()).Result;
        return Ok(result);
    }
    // [HttpGet("{id}")]
    // public IActionResult Get(int id)
    // {
    //     return Ok();
    // }
}