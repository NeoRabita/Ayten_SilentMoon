using Microsoft.AspNetCore.Http;
using SlientMoon.Application.DTOs.Profile;
using SlientMoon.Application.Interfaces.Repositories;
using SlientMoon.Application.Interfaces.Services;
using SlientMoon.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SlientMoon.WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor accessor;
        private readonly IUserRepository _userRepository;

        public UserService(IHttpContextAccessor accessor, IUserRepository userRepository)
        {
            this.accessor = accessor;
            _userRepository = userRepository;
        }

        public ClaimsPrincipal GetUser()
        {
            return accessor?.HttpContext?.User;
        }
        public string GetUserId()
        {
            return accessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
        }
        public string GetUserEmail()
        {
            return accessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var userId = int.Parse(GetUserId());

            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<UserProfileResponse> GetProfileAsync()
        {
            var user = await GetCurrentUserAsync();

            return new UserProfileResponse
            {
                Id = user.Id,
                Name = $"{user.FirstName} {user.LastName}".Trim(),
                Email = user.Email,
                EmailVerified = user.EmailConfirmed,
                AvatarUrl = user.AvatarUrl,
                CreatedAt = user.CreatedAt
            };
        }

    }

}
