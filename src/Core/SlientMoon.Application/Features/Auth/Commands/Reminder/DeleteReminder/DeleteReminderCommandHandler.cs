using Application;
using Application.Abstractions.Messaging;
using SlientMoon.Application.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Auth.Commands.Onboarding.DeleteReminder;

public sealed class DeleteReminderCommandHandler
    : ICommandHandler<DeleteReminderCommand>
{
    private readonly IReminderService _reminderService;

    public DeleteReminderCommandHandler(IReminderService reminderService)
    {
        _reminderService = reminderService;
    }

    public async Task<Result> Handle(
        DeleteReminderCommand command,
        CancellationToken cancellationToken)
    {
        await _reminderService.DeleteReminderAsync(command);

        return Result.Success();
    }
}