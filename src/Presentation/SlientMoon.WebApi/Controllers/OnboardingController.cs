using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlientMoon.Application.Features.Onboarding.Queries.Reminder.GetReminders;
using SlientMoon.Application.Features.Onboarding.Queries.Topics.GetTopics;
using SlientMoon.Application.Features.Onboarding.Queries.Topics.GetUserTopics;
using System.Threading.Tasks;
using SlientMoon.Application.Features.Onboarding.Commands.Reminder.CreateReminder;
using SlientMoon.Application.Features.Onboarding.Commands.Reminder.UpdateReminder;
using SlientMoon.Application.Features.Onboarding.Commands.Reminder.DeleteReminder;
using SlientMoon.Application.DTOs.Reminder;
using SlientMoon.Application.Features.Onboarding.Commands.UpdateTopics;

namespace SlientMoon.WebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class OnboardingController : BaseController
    {
        [HttpGet("topics")]
        public async Task<IResult> GetTopics()
        {
            var result = await Dispatcher.Send(new GetTopicsQuery());

            return HandleResult(result);
        }

        [HttpPut("/api/me/topics")]
        public async Task<IResult> UpdateTopics([FromBody] UpdateTopicsCommand command)
        {
            var result = await Dispatcher.Send(command);

            return HandleResult(result);
        }
        [HttpGet("me/topics")]
        public async Task<IResult> GetUserTopics()
        {
            var result = await Dispatcher.Send(new GetUserTopicsQuery());

            return HandleResult(result);
        }
        [HttpGet("me/reminders")]
        public async Task<IResult> GetReminders()
        {
            var result = await Dispatcher.Send(new GetRemindersQuery());

            return HandleResult(result);
        }
        [HttpPost("me/reminders")]
        public async Task<IResult> CreateReminder([FromBody] CreateReminderRequest request)
        {
            var command = new CreateReminderCommand(
                request.Time,
                request.Days);

            var result = await Dispatcher.Send(command);

            return HandleResult(result);
        }
        [HttpPatch("/api/me/reminders/{id}")]
        public async Task<IResult> UpdateReminder(
    int id,
    [FromBody] UpdateReminderRequest request)
        {
            var command = new UpdateReminderCommand(
                id,
                request.Time,
                request.Days,
                request.IsActive);

            var result = await Dispatcher.Send(command);

            return HandleResult(result);
        }

        [HttpDelete("/api/me/reminders/{id}")]
        public async Task<IResult> DeleteReminder(int id)
        {
            var result = await Dispatcher.Send(new DeleteReminderCommand(id));

            return HandleResult(result);
        }
    }
}
