using SlientMoon.Domain.Common;
using System;
using System.Collections.Generic;

namespace SlientMoon.Domain.Entities
{
    public class ApplicationUser : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool EmailConfirmed { get; set; }
        public string OtpCode { get; set; }
        public DateTime? OtpExpireDate { get; set; }
        public int OtpAttemptCount { get; set; }
        public string? AvatarUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public RefreshToken RefreshToken { get; set; }
        public ICollection<Topic> Topics { get; set; } = new List<Topic>();
        public ICollection<UserTopic> UserTopics { get; set; } = new List<UserTopic>();
        public ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();
    }

}
