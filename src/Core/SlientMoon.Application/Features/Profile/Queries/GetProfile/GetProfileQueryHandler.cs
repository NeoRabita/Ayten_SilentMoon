using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Profile;
using SlientMoon.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Profile.Queries.GetProfile;

public sealed class GetProfileQueryHandler
    : ICommandHandler<GetProfileQuery, UserProfileResponse>
{
    private readonly IUserRepository _userRepository;

    public GetProfileQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserProfileResponse>> Handle(
        GetProfileQuery request,
        CancellationToken ct)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

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