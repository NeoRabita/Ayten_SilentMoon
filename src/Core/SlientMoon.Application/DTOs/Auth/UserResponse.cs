using System;

namespace SlientMoon.Application.DTOs.Auth
{
    public class UserResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool EmailVerified { get; set; }

        public string AvatarUrl { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}