using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Topic;
using System.Collections.Generic;

namespace SlientMoon.Application.Features.Queries.Topics.GetTopics;

public sealed record GetTopicsQuery()
    : ICommand<List<TopicResponse>>;