
using Microsoft.AspNetCore.Mvc;
using SlientMoon.Application.Features.Queries.Topics.GetTopics;
using System.Threading.Tasks;

namespace SlientMoon.WebApi.Controllers;

[ApiController]
[Route("api/topics")]
public class TopicsController : BaseController
{
  

    [HttpGet]
    public async Task<IActionResult> GetTopics()
    {
        var result = await Dispatcher.Send(new GetTopicsQuery());

        return Ok(result.Value);
    }
}