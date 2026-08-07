using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Reminder;
using System.Collections.Generic;

namespace SlientMoon.Application.Features.Onboarding.Queries.Reminder.GetReminders;

public sealed record GetRemindersQuery()
    : IQuery<List<ReminderResponse>>;