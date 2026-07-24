using Application.Abstractions.Messaging;

namespace SlientMoon.Application.Features.Commands.VerifyEmail;

public sealed record VerifyEmailCommand(
    string Email,
    string Otp
) : ICommand;