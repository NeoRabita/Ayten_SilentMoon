using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Auth;

namespace SlientMoon.Application.Features.Commands.Register;

public sealed record RegisterCommand(
    string Name,
    string Email,
    string Password
) : ICommand<RegisterResponse>;