using Application.Abstractions.Messaging;
using SlientMoon.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Profile.Commands.UpdateProfile;

public sealed class UpdateProfileCommandHandler
    : ICommandHandler<UpdateProfileCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateProfileCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(
        UpdateProfileCommand request,
        CancellationToken ct)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
        {
            return Result.Failure(
                Error.NotFound(
                    "User.NotFound",
                    "İstifadəçi tapılmadı."));
        }

        user.FirstName = request.Name;
        user.AvatarUrl = request.AvatarUrl;

        return Result.Success();
    }
}