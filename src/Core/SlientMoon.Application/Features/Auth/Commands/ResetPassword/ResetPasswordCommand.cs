using Application.Abstractions.Messaging;

namespace SlientMoon.Application.Features.Commands.ResetPassword;

public sealed record ResetPasswordCommand(
    string Email,
    string Code,
    string Password
) : ICommand;