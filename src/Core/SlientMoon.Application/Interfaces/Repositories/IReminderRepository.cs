using SlientMoon.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlientMoon.Application.Interfaces.Repositories;

public interface IReminderRepository : IGenericRepository<Reminder>
{
    Task<List<Reminder>> GetUserRemindersAsync(int userId);
    Task<Reminder?> GetByIdAsync(int id);
}