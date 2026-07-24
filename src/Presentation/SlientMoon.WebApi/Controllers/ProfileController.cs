using MediatR;
using SlientMoon.Application.DTOs.Profile;
using SlientMoon.Application.Features.Profile.Commands.UpdateProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlientMoon.Application.Features.Profile.Queries.GetProfile;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SlientMoon.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/me")]
public class ProfileController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _mediator.Send(new GetProfileQuery(userId));

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
    [HttpPatch]
    public async Task<IActionResult> UpdateProfile(UpdateProfileRequest request)
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var command = new UpdateProfileCommand(
            userId,
            request.Name,
            request.AvatarUrl);

        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }
}