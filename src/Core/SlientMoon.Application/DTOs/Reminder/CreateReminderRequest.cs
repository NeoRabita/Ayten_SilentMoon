using System;

namespace SlientMoon.Application.DTOs.Reminder;

public class CreateReminderRequest
{
    public TimeSpan Time { get; set; }

    public string Days { get; set; } = default!;
}