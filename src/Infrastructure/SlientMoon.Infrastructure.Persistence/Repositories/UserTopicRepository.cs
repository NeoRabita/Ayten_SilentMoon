using Microsoft.EntityFrameworkCore;
using SlientMoon.Application.Interfaces.Repositories;
using SlientMoon.Domain.Entities;
using SlientMoon.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlientMoon.Infrastructure.Persistence.Repositories;

public class UserTopicRepository
    : GenericRepository<UserTopic>, IUserTopicRepository
{
    public UserTopicRepository(AppDbContext context)
        : base(context)
    {
    }


    public async Task<List<UserTopic>> GetUserTopicsAsync(int userId)
    {
        return await _dbContext.UserTopics
            .Include(x => x.Topic)
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

}