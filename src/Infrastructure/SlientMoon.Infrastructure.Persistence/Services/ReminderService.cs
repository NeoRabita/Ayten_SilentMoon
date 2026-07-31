using Microsoft.AspNetCore.Http;
using SlientMoon.Application.Features.Auth.Commands.Onboarding.CreateReminder;
using SlientMoon.Application.Features.Auth.Commands.Onboarding.DeleteReminder;
using SlientMoon.Application.Features.Auth.Commands.Onboarding.UpdateReminder;
using SlientMoon.Application.Interfaces.Repositories;
using SlientMoon.Application.Interfaces.Services;
using SlientMoon.Domain.Entities;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SlientMoon.Infrastructure.Services;

public class ReminderService : IReminderService
{
    private readonly IReminderRepository _reminderRepository;
    private readonly IUow _uow;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ReminderService(
        IReminderRepository reminderRepository,
        IUow uow,
        IHttpContextAccessor httpContextAccessor)
    {
        _reminderRepository = reminderRepository;
        _uow = uow;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task CreateReminderAsync(CreateReminderCommand command)
    {
        var userId = int.Parse(
        _httpContextAccessor.HttpContext!
        .User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var reminder = new Reminder
        {
            UserId = userId,
            Time = command.Time,
            Days = command.Days,
            IsActive = true
        };

        await _reminderRepository.AddAsync(reminder);

        await _uow.SaveChangesAsync();
    }
    public async Task UpdateReminderAsync(UpdateReminderCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteReminderAsync(DeleteReminderCommand command)
    {
        throw new NotImplementedException();
    }
}