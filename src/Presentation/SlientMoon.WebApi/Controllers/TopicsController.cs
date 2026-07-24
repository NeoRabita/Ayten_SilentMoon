using MediatR;
using Microsoft.AspNetCore.Mvc;
using SlientMoon.Application.Features.Queries.Topics.GetTopics;
using System.Threading.Tasks;

namespace SlientMoon.WebApi.Controllers;

[ApiController]
[Route("api/topics")]
public class TopicsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TopicsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetTopics()
    {
        var result = await _mediator.Send(new GetTopicsQuery());

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}