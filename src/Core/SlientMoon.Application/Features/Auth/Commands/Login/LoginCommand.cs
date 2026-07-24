using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Auth;

namespace SlientMoon.Application.Features.Commands.Login;

public sealed record LoginCommand(
    string Email,
    string Password
) : ICommand<LoginResponse>;