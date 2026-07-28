using System.Collections.Generic;

namespace SlientMoon.Application.DTOs.Profile;

public class UpdateProfileRequest
{
    public string Name { get; set; } = default!;

    public string? AvatarUrl { get; set; }
    public List<int> TopicIds { get; set; } = [];
}