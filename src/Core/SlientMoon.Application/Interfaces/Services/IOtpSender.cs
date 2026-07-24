using System.Threading.Tasks;
using SlientMoon.Domain.Entities;

namespace SlientMoon.Application.Interfaces.Services
{
    public interface IOtpSender
    {
        Task SendOtpAsync(ApplicationUser user);
    }
}