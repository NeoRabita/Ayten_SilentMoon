using System.Collections.Generic;

namespace SlientMoon.Application.Features.Home.Queries.GetHome;

public class HomeSectionResponse
{
    public string Title { get; set; }

    public List<HomeItemResponse> Items { get; set; }
}