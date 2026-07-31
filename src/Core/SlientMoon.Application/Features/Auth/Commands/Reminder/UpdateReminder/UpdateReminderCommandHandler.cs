using Application;
using Application.Abstractions.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Auth.Commands.Onboarding.UpdateReminder;

public sealed class UpdateReminderCommandHandler
    : ICommandHandler<UpdateReminderCommand>
{
    public async Task<Result> Handle(
        UpdateReminderCommand command,
        CancellationToken cancellationToken)
    {
        return Result.Success();
    }
}