using SlientMoon.Domain.Common;

namespace SlientMoon.Domain.Entities;

public class Topic : BaseEntity
{
    public string Name { get; set; } = default!;

    public string? ImageUrl { get; set; }
}
