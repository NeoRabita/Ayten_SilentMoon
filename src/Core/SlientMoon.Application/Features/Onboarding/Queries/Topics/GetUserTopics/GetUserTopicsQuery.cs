using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Topic;
using System.Collections.Generic;

namespace SlientMoon.Application.Features.Onboarding.Queries.Topics.GetUserTopics;
public sealed record GetUserTopicsQuery()
    : IQuery<List<TopicResponse>>;
