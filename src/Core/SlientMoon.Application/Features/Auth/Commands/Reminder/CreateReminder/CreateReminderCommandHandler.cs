using Application;
using Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;
using SlientMoon.Application.Interfaces.Repositories;
using SlientMoon.Domain.Entities;
using Application;
using Application.Abstractions.Messaging;
using SlientMoon.Application.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Auth.Commands.Onboarding.CreateReminder;

public sealed class CreateReminderCommandHandler
    : ICommandHandler<CreateReminderCommand>
{
    private readonly IReminderService _reminderService;

    public CreateReminderCommandHandler(IReminderService reminderService)
    {
        _reminderService = reminderService;
    }

    public async Task<Result> Handle(
        CreateReminderCommand command,
        CancellationToken cancellationToken)
    {
        await _reminderService.CreateReminderAsync(command);

        return Result.Success();
    }
}