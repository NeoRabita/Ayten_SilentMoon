using SlientMoon.Domain.Entities;
using System.Threading.Tasks;

namespace SlientMoon.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        Task<ApplicationUser?> GetByEmailAsync(string email);
        Task<ApplicationUser> GetByRefreshTokenAsync(string refreshToken);
        Task<ApplicationUser?> GetByIdWithTopicsAsync(int userId);

    }
}