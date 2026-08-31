using System.Collections.Generic;

namespace SlientMoon.Application.DTOs.Home;

public class HomeResponse
{
    public HomeSectionDto Recommended { get; set; }
    public HomeItemDto? DailyThought { get; set; }
    public HomeSectionDto FeaturedSleep { get; set; }
    public HomeSectionDto PopularMeditations { get; set; }
}

public class HomeSectionDto
{
    public string Title { get; set; }
    public List<HomeItemDto> Items { get; set; }
}

public class HomeItemDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Type { get; set; }
    public string CategoryId { get; set; }
    public string? ImageUrl { get; set; }
    public int DurationSec { get; set; }
    public bool IsFeatured { get; set; }
    public List<string> Narrators { get; set; }
}