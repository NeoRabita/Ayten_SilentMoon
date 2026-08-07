using Application.Abstractions.Messaging;
using System;

namespace SlientMoon.Application.Features.Onboarding.Commands.Reminder.CreateReminder;

public sealed record CreateReminderCommand(
    TimeSpan Time,
    string Days
) : ICommand;