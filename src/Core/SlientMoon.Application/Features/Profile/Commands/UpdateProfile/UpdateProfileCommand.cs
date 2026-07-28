using Application.Abstractions.Messaging;
using System.Collections.Generic;

namespace SlientMoon.Application.Features.Profile.Commands.UpdateProfile;

public sealed record UpdateProfileCommand(
    int UserId,
    string Name,
    string AvatarUrl
) : ICommand;