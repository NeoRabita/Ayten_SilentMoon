namespace SlientMoon.Application.DTOs.Topic;

public class TopicResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string? ImageUrl { get; set; }
}