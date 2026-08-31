
using SlientMoon.Application.Interfaces.Authentication;

namespace SlientMoon.Infrastructure.Persistence.Services
{
    public class CurrentUser : ICurrentUser
    {
        public int UserId { get; set; }

        public string UserName { get; set; }
    }
}