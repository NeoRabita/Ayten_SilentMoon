using System.Security.Claims;

namespace OnionArchitecture.Application.Interfaces.Services
{
    public interface IUserService
    {
        ClaimsPrincipal GetUser();
        public string GetUserId();
        public string GetUserEmail();
    }
}