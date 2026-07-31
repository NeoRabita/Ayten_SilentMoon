using Application.Abstractions.Messaging;
using System;

namespace SlientMoon.Application.Features.Auth.Commands.Onboarding.CreateReminder;

public sealed record CreateReminderCommand(
    TimeSpan Time,
    string Days
) : ICommand;