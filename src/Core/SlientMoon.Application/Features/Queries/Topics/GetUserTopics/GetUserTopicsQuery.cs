using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Topic;
using System.Collections.Generic;

namespace SlientMoon.Application.Features.Profile.Queries.GetUserTopics;

public sealed record GetUserTopicsQuery()
    : ICommand<List<TopicResponse>>;
