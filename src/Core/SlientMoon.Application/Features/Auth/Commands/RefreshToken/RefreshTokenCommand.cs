using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Auth;

namespace SlientMoon.Application.Features.Auth.Commands.RefreshToken;

public sealed record RefreshTokenCommand(
    string RefreshToken
) : ICommand<LoginResponse>;