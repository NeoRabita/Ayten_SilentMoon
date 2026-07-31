using Application.Abstractions.Messaging;

namespace SlientMoon.Application.Features.Auth.Commands.Onboarding.DeleteReminder;

public sealed record DeleteReminderCommand(
    int Id
) : ICommand;