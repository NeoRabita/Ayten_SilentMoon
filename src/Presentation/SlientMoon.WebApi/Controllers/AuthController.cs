using MediatR;
using Microsoft.AspNetCore.Mvc;
using SlientMoon.Application.DTOs.Auth;
using SlientMoon.Application.Features.Commands.ForgotPassword;
using SlientMoon.Application.Features.Commands.GoogleLogin;
using SlientMoon.Application.Features.Commands.Login;
using SlientMoon.Application.Features.Commands.RefreshToken;
using SlientMoon.Application.Features.Commands.Register;
using SlientMoon.Application.Features.Commands.ResendOtp;
using SlientMoon.Application.Features.Commands.ResetPassword;
using SlientMoon.Application.Features.Commands.VerifyEmail;
using SlientMoon.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace SlientMoon.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterCommand(
                request.Name,
                request.Email,
                request.Password);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return StatusCode(201, result.Value);
        }
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailRequest request)
        {
            var command = new VerifyEmailCommand(
                request.Email,
                request.Otp);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(new
            {
                Message = "Email uğurla təsdiqləndi."
            });
        }
        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOtp(ResendOtpRequest request)
        {
            var command = new ResendOtpCommand(request.Email);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(new
            {
                Message = "OTP kodu yenidən göndərildi."
            });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var command = new LoginCommand(
                request.Email,
                request.Password);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            var command = new RefreshTokenCommand(request.RefreshToken);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
        [HttpPost("oauth/google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            var command = new GoogleLoginCommand(request.IdToken);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            var command = new ForgotPasswordCommand(request.Email);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var command = new ResetPasswordCommand(
                request.Email,
                request.Code,
                request.Password);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}