using SlientMoon.Application.Features.Auth.Commands.Onboarding.CreateReminder;
using SlientMoon.Application.Features.Auth.Commands.Onboarding.DeleteReminder;
using SlientMoon.Application.Features.Auth.Commands.Onboarding.UpdateReminder;
using System.Threading.Tasks;

namespace SlientMoon.Application.Interfaces.Services;

public interface IReminderService
{
    Task CreateReminderAsync(CreateReminderCommand command);
    Task UpdateReminderAsync(UpdateReminderCommand command);

    Task DeleteReminderAsync(DeleteReminderCommand command);
}