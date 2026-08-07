using Application.Abstractions.Messaging;

namespace SlientMoon.Application.Features.Onboarding.Commands.Reminder.DeleteReminder;

public sealed record DeleteReminderCommand(
    int Id
) : ICommand;