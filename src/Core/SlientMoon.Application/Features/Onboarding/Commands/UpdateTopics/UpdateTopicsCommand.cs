using Application.Abstractions.Messaging;
using System.Collections.Generic;

namespace SlientMoon.Application.Features.Onboarding.Commands.UpdateTopics;

public sealed record UpdateTopicsCommand(
    List<int> TopicIds
) : ICommand<Result>;