using SlientMoon.Domain.Common;
using System;

namespace SlientMoon.Domain.Entities;

public class Reminder : BaseEntity
{
    public int UserId { get; set; }
    public ApplicationUser User { get; set; } = default!;

    public TimeSpan Time { get; set; }

    public string Days { get; set; } = default!;

    public bool IsActive { get; set; }
}