using Application.Abstractions.Messaging;
using System;

namespace SlientMoon.Application.Features.Onboarding.Commands.Reminder.UpdateReminder;

public sealed record UpdateReminderCommand(
    int Id,
    TimeSpan Time,
    string Days,
    bool IsActive
) : ICommand;