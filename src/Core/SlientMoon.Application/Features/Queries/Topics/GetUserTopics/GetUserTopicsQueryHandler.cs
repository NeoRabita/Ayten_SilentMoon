using Application;
using Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;
using SlientMoon.Application.DTOs.Topic;
using SlientMoon.Application.Features.Queries.Topics.GetUserTopics;
using SlientMoon.Application.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Queries.Topics.GetUserTopics;

public sealed class GetUserTopicsQueryHandler
    : IQueryHandler<GetUserTopicsQuery, List<TopicResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetUserTopicsQueryHandler(
        IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<List<TopicResponse>>> Handle(
        GetUserTopicsQuery request,
        CancellationToken cancellationToken)
    {
        var userId = int.Parse(
            _httpContextAccessor.HttpContext!
            .User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var user = await _userRepository.GetByIdWithTopicsAsync(userId);


        if (user == null)
        {
            return Result.Failure<List<TopicResponse>>(
                Error.NotFound(
                    "User.NotFound",
                    "User not found"));
        }
        var response = user.UserTopics
            .Select(x => new TopicResponse
            {
                Id = x.Topic.Id,
                Name = x.Topic.Name,
                ImageUrl = x.Topic.ImageUrl
            })
            .ToList();

        return Result.Success(response);
    }
}
