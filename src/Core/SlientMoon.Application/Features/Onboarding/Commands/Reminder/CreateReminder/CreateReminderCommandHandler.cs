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

namespace SlientMoon.Application.Features.Onboarding.Commands.Reminder.CreateReminder;

public sealed class CreateReminderCommandHandler
    : ICommandHandler<CreateReminderCommand>
{
    private readonly IReminderService _reminderService;
    private readonly IReminderRepository _reminderRepository;
    private readonly IUow _uow;

    public CreateReminderCommandHandler(IReminderService reminderService, IReminderRepository reminderRepository,
        IUow uow)
    {
        _reminderService = reminderService;
        _reminderRepository = reminderRepository;
        _uow = uow;
    }

    public async Task<Result> Handle(
        CreateReminderCommand command,
        CancellationToken cancellationToken)
    {
        var reminder = await _reminderService.CreateReminderAsync(command);

        await _reminderRepository.AddAsync(reminder);

        await _uow.SaveChangesAsync(cancellationToken);


        return Result.Success();
    }
}