
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlientMoon.Application.DTOs.Auth;
using SlientMoon.Application.Features.Auth.Commands.ForgotPassword;
using SlientMoon.Application.Features.Auth.Commands.GoogleLogin;
using SlientMoon.Application.Features.Auth.Commands.Login;
using SlientMoon.Application.Features.Auth.Commands.RefreshToken;
using SlientMoon.Application.Features.Auth.Commands.Register;
using SlientMoon.Application.Features.Auth.Commands.ResendOtp;
using SlientMoon.Application.Features.Auth.Commands.ResetPassword;
using SlientMoon.Application.Features.Auth.Commands.VerifyEmail;
using SlientMoon.Application.Interfaces.Services;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SlientMoon.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {

        [HttpPost("register")]
        public async Task<IResult> Register([FromBody] RegisterCommand command)
        {
            var result = await Dispatcher.Send(command);
            return HandleResult(result);

        }

        [HttpPost("verify-email")]
        public async Task<IResult> VerifyEmail([FromBody] VerifyEmailCommand command)
        {
            var result = await Dispatcher.Send(command);
            return HandleResult(result);
        }
        [HttpPost("resend-otp")]
        public async Task<IResult> ResendOtp([FromBody] ResendOtpCommand command)
        {
            var result = await Dispatcher.Send(command);
            return HandleResult(result);
        }
        [HttpPost("login")]
        public async Task<IResult> Login([FromBody] LoginCommand command)
        {
            var result = await Dispatcher.Send(command);
            return HandleResult(result);
        }
        [HttpPost("refresh-token")]
        public async Task<IResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var result = await Dispatcher.Send(command);
            return HandleResult(result);
        }
        [HttpPost("oauth/google")]
        public async Task<IResult> GoogleLogin([FromBody] GoogleLoginCommand command)
        {
            var result = await Dispatcher.Send(command);
            return HandleResult(result);
        }
        [HttpPost("forgot-password")]
        public async Task<IResult> ForgotPassword(ForgotPasswordCommand command)
        {
            var result = await Dispatcher.Send(command);
            return HandleResult(result);
        }

        [HttpPost("reset-password")]
        public async Task<IResult> ResetPassword(ResetPasswordCommand command)
        {
            var result = await Dispatcher.Send(command);
            return HandleResult(result);
        }
    }
}