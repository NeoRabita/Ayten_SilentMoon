using SlientMoon.Application.DTOs.Auth;
using System.Threading.Tasks;

namespace SlientMoon.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);

        Task VerifyEmailAsync(VerifyEmailRequest request);

        Task ResendOtpAsync(ResendOtpRequest request);

        Task<LoginResponse> LoginAsync(LoginRequest request);

        Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request);

        Task LogoutAsync(string refreshToken);
        Task ForgotPasswordAsync(ForgotPasswordRequest request);

        Task ResetPasswordAsync(ResetPasswordRequest request);
        Task<LoginResponse> GoogleLoginAsync(GoogleLoginRequest request);
    }
}