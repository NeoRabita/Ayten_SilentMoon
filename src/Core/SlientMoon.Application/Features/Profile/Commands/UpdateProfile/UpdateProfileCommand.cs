using Application.Abstractions.Messaging;

namespace SlientMoon.Application.Features.Profile.Commands.UpdateProfile;

public sealed record UpdateProfileCommand(
    int UserId,
    string Name,
    string? AvatarUrl
) : ICommand;