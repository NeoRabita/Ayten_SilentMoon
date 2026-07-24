using System;

namespace SlientMoon.Application.DTOs.Profile;

public class UserProfileResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string Email { get; set; } = default!;

    public bool EmailVerified { get; set; }

    public string? AvatarUrl { get; set; }
    

    public DateTime CreatedAt { get; set; }
}