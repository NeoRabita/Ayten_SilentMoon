using Application.Abstractions.Messaging;

namespace SlientMoon.Application.Features.Auth.Commands.ForgotPassword;

public sealed record ForgotPasswordCommand(
    string Email
) : ICommand;