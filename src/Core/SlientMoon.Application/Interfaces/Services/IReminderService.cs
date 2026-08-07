using SlientMoon.Application.Features.Onboarding.Commands.Reminder.CreateReminder;
using SlientMoon.Application.Features.Onboarding.Commands.Reminder.DeleteReminder;
using SlientMoon.Application.Features.Onboarding.Commands.Reminder.UpdateReminder;
using SlientMoon.Domain.Entities;
using System.Threading.Tasks;

namespace SlientMoon.Application.Interfaces.Services;

public interface IReminderService
{
    Task<Reminder> CreateReminderAsync(CreateReminderCommand command);
    Task UpdateReminderAsync(UpdateReminderCommand command);

    Task DeleteReminderAsync(DeleteReminderCommand command);
}