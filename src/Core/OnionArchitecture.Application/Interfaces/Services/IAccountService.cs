
using System.Threading.Tasks;
using OnionArchitecture.Application.DTOs.Account;
using OnionArchitecture.Application.DTOs.JWT;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<string> RegisterAsync(RegisterRequest request);
        Task<string> SendEmailVerification(ApplicationUser user);
        Task<string> ConfirmEmailAsync(string email, string code);
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<JwtTokenDto> RevokeByRefreshToken(string token);
        Task<string> ForgotPasswordAsync(ForgotPasswordRequest request);
        Task<string> ResetPasswordAsync(ResetPasswordRequest request);
    }
}