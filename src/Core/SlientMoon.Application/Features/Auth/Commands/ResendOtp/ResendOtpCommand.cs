using Application.Abstractions.Messaging;

namespace SlientMoon.Application.Features.Auth.Commands.ResendOtp;

public sealed record ResendOtpCommand(
    string Email
) : ICommand;