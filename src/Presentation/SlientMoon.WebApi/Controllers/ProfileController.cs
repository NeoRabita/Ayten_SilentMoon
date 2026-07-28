
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
public class ProfileController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        
        var result = await Dispatcher.Send(new GetProfileQuery());

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

        var result = await Dispatcher.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }
}