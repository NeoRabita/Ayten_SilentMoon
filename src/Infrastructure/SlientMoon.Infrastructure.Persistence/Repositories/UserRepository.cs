using Microsoft.EntityFrameworkCore;
using SlientMoon.Application.Interfaces.Repositories;
using SlientMoon.Domain.Entities;
using SlientMoon.Infrastructure.Persistence.Contexts;
using System.Threading.Tasks;

namespace SlientMoon.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _dbContext.ApplicationUsers
                .FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<ApplicationUser> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _dbContext.ApplicationUsers
                .Include(x => x.RefreshToken)
                .FirstOrDefaultAsync(x =>
                    x.RefreshToken != null &&
                    x.RefreshToken.Token == refreshToken);
        }
        public async Task<ApplicationUser?> GetByIdWithTopicsAsync(int userId)
        {
            return await _dbContext.ApplicationUsers
                .Include(x => x.UserTopics)
                .ThenInclude(x => x.Topic)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}