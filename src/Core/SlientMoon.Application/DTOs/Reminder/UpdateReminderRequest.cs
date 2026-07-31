using System;

namespace SlientMoon.Application.DTOs.Reminder;

public class UpdateReminderRequest
{
    public TimeSpan Time { get; set; }

    public string Days { get; set; } = default!;

    public bool IsActive { get; set; }
}