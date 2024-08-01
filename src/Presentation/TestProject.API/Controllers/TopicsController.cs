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
    
    [HttpGet("{id}")]
    public IActionResult Get([FromRoute]GetTopicByIdQuery getTopicByIdQuery)
    {
        var result = Mediator.Send(getTopicByIdQuery).Result;
        return Ok(result);
    }
    
    [HttpPost("addComment")]
    public IActionResult AddComment([FromBody]AddCommentCommand addCommentCommand)
    {
        var result = Mediator.Send(addCommentCommand).Result;
        return Ok(result);
    }
}