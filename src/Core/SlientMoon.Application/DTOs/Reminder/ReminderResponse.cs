using System;

namespace SlientMoon.Application.DTOs.Reminder;

public class ReminderResponse
{
    public int Id { get; set; }

    public TimeSpan Time { get; set; }

    public string Days { get; set; } = default!;

    public bool IsActive { get; set; }
}