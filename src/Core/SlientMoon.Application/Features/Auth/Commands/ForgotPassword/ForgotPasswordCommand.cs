using Application.Abstractions.Messaging;

namespace SlientMoon.Application.Features.Commands.ForgotPassword;

public sealed record ForgotPasswordCommand(
    string Email
) : ICommand;