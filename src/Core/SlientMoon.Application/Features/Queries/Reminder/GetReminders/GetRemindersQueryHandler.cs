using Application;
using Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;
using SlientMoon.Application.DTOs.Reminder;
using SlientMoon.Application.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Queries.Reminder.GetReminders;

public sealed class GetRemindersQueryHandler
    : IQueryHandler<GetRemindersQuery, List<ReminderResponse>>
{
    private readonly IReminderRepository _reminderRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetRemindersQueryHandler(
        IReminderRepository reminderRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _reminderRepository = reminderRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<List<ReminderResponse>>> Handle(
        GetRemindersQuery request,
        CancellationToken cancellationToken)
    {
        var userId = int.Parse(
            _httpContextAccessor.HttpContext!
            .User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var reminders = await _reminderRepository.GetUserRemindersAsync(userId);

        var response = reminders.Select(x => new ReminderResponse
        {
            Id = x.Id,
            Time = x.Time,
            Days = x.Days,
            IsActive = x.IsActive
        }).ToList();

        return Result.Success(response);
    }
}