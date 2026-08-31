using Application.Abstractions.Messaging;
using System.Collections.Generic;

namespace SlientMoon.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQuery : IQuery<List<CategoryResponse>>
    {
        public string? Type { get; set; }

        public GetCategoriesQuery(string? type = null)
        {
            Type = type;
        }
    }
}