using Microsoft.EntityFrameworkCore;
using SlientMoon.Application.Interfaces.Repositories;
using SlientMoon.Domain.Entities;
using SlientMoon.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlientMoon.Infrastructure.Persistence.Repositories;

public class ReminderRepository
    : GenericRepository<Reminder>, IReminderRepository
{
    public ReminderRepository(AppDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<List<Reminder>> GetUserRemindersAsync(int userId)
    {
        return await _dbContext.Reminders
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<Reminder?> GetByIdAsync(int id)
    {
        return await _dbContext.Reminders
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}