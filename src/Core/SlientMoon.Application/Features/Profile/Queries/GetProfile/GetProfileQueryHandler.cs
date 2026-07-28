using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Profile;
using SlientMoon.Application.Interfaces.Repositories;
using SlientMoon.Application.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Profile.Queries.GetProfile;

public sealed class GetProfileQueryHandler
    : ICommandHandler<GetProfileQuery, UserProfileResponse>
{
    private readonly IUserService _userService;

    public GetProfileQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<UserProfileResponse>> Handle(
        GetProfileQuery request,
        CancellationToken ct)
    {
        var user = await _userService.GetCurrentUserAsync();

        if (user == null)
        {
            return Result.Failure<UserProfileResponse>(
                Error.NotFound(
                    "User.NotFound",
                    "İstifadəçi tapılmadı."));
        }

        return Result.Success(new UserProfileResponse
        {
            Id = user.Id,
            Name = $"{user.FirstName} {user.LastName}".Trim(),
            Email = user.Email,
            EmailVerified = user.EmailConfirmed,
            AvatarUrl = user.AvatarUrl,
            CreatedAt = user.CreatedAt
        });
    }
}