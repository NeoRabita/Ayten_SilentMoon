using Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlientMoon.Application.Features.Home.Queries.GetHome;
using System.Threading.Tasks;

namespace SlientMoon.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("home")]
public class HomeController : BaseController
{
    [HttpGet]
    public async Task<IResult> GetHome()
    {
        var result = await Dispatcher.Send(new GetHomeQuery());

        return HandleResult(result);
    }
}