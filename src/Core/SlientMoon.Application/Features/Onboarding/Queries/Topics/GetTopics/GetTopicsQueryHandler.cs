using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Topic;
using SlientMoon.Application.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Onboarding.Queries.Topics.GetTopics;

public sealed class GetTopicsQueryHandler
    : ICommandHandler<GetTopicsQuery, List<TopicResponse>>
{
    private readonly ITopicRepository _topicRepository;

    public GetTopicsQueryHandler(ITopicRepository topicRepository)
    {
        _topicRepository = topicRepository;
    }

    public async Task<Result<List<TopicResponse>>> Handle(
        GetTopicsQuery request,
        CancellationToken cancellationToken)
    {
        var topics = await _topicRepository.GetAllTopicsAsync();

        var response = topics.Select(x => new TopicResponse
        {
            Id = x.Id,
            Name = x.Name,
            ImageUrl = x.ImageUrl
        }).ToList();

        return Result.Success(response);
    }
}