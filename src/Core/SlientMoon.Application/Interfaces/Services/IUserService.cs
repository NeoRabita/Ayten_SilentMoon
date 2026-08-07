using SlientMoon.Application.DTOs.Profile;
using SlientMoon.Domain.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SlientMoon.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> GetCurrentUserAsync();

        Task<UserProfileResponse> GetProfileAsync();

    }
}