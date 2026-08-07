using Microsoft.AspNetCore.Http;
using SlientMoon.Application.Features.Onboarding.Commands.Reminder.CreateReminder;
using SlientMoon.Application.Features.Onboarding.Commands.Reminder.DeleteReminder;
using SlientMoon.Application.Features.Onboarding.Commands.Reminder.UpdateReminder;
using SlientMoon.Application.Interfaces.Repositories;
using SlientMoon.Application.Interfaces.Services;
using SlientMoon.Domain.Entities;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SlientMoon.Infrastructure.Services;

public class ReminderService : IReminderService
{
    private readonly IUserService _userService;

    public ReminderService(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<Reminder> CreateReminderAsync(CreateReminderCommand command)
    {
      var user = await _userService.GetCurrentUserAsync();

        var reminder = new Reminder
        {
            UserId = user.Id,
            Time = command.Time,
            Days = command.Days,
            IsActive = true
        };

        return reminder;
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