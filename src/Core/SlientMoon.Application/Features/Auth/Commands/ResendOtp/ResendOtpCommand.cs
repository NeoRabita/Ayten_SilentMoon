using Application.Abstractions.Messaging;

namespace SlientMoon.Application.Features.Commands.ResendOtp;

public sealed record ResendOtpCommand(
    string Email
) : ICommand;