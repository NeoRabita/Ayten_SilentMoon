using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Auth;

namespace SlientMoon.Application.Features.Commands.GoogleLogin;

public sealed record GoogleLoginCommand(
    string IdToken
) : ICommand<LoginResponse>;