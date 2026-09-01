using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Auth;

namespace SlientMoon.Application.Features.Auth.Commands.ResendOtp;

public sealed record ResendOtpCommand(
    string Email
) : IBaseCommand;