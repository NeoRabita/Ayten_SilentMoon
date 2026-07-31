using Application.Abstractions.Messaging;
using System;

namespace SlientMoon.Application.Features.Auth.Commands.Onboarding.UpdateReminder;

public sealed record UpdateReminderCommand(
    int Id,
    TimeSpan Time,
    string Days,
    bool IsActive
) : ICommand;