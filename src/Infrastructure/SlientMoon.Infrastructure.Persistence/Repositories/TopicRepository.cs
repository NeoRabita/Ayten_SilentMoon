using Microsoft.EntityFrameworkCore;
using SlientMoon.Application.Interfaces.Repositories;
using SlientMoon.Domain.Entities;
using SlientMoon.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlientMoon.Infrastructure.Persistence.Repositories
{
    public class TopicRepository : GenericRepository<Topic>, ITopicRepository
    {
        public TopicRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<List<Topic>> GetAllTopicsAsync()
        {
            return await _dbContext.Topics.ToListAsync();
        }
    }
}