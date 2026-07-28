using Application.Abstractions.Messaging;
using global::SlientMoon.Application.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Auth.Commands.Onboarding.UpdateTopics;

public sealed class UpdateTopicsCommandHandler
    : ICommandHandler<UpdateTopicsCommand>
{
    private readonly IUserService _userService;

    public UpdateTopicsCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result> Handle(
        UpdateTopicsCommand command,
        CancellationToken cancellationToken)
    {
        await _userService.UpdateTopicsAsync(command.TopicIds);

        return Result.Success();
    }
}