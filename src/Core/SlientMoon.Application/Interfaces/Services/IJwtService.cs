using SlientMoon.Domain.Entities;

namespace SlientMoon.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(ApplicationUser user);

        string GenerateRefreshToken();
    }
}