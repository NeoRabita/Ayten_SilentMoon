using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Auth.Commands.Onboarding.UpdateTopics;

public sealed record UpdateTopicsCommand(
    List<int> TopicIds
) : ICommand<Result>;