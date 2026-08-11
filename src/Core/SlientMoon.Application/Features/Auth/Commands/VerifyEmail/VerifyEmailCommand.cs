using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Auth;

namespace SlientMoon.Application.Features.Auth.Commands.VerifyEmail;

public sealed record VerifyEmailCommand(
    string Email,
    string Otp
) : ICommand<VerifyEmailResponse>;