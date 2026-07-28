using Application.Abstractions.Messaging;
using System.Collections.Generic;

namespace SlientMoon.Application.Features.Auth.Commands.Onboarding.UpdateTopics;

public sealed record UpdateTopicsCommand : ICommand<Result>
{
    public int UserId { get; init; }
    public List<int> TopicIds { get; init; }
}