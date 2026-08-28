using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using SlientMoon.Application.Exceptions;
using SlientMoon.Application.DTOs.Auth;
using SlientMoon.Application.DTOs.Email;
using SlientMoon.Application.Interfaces.Messaging;
using SlientMoon.Application.Messages;
using SlientMoon.Application.Interfaces.Repositories;
using SlientMoon.Application.Interfaces.Services;
using SlientMoon.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SlientMoon.Infrastructure.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUow _uow;
        private readonly IEmailService _emailService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IOtpSender _otpSender;
        private readonly IConfiguration _configuration;
        private readonly IMessagePublisher _messagePublisher;
        public AuthService(IUserRepository userRepository, IUow uow, IEmailService emailService, IPasswordHasher passwordHasher, IJwtService jwtService, IOtpSender otpSender, IConfiguration configuration, IMessagePublisher messagePublisher)
        {
            _userRepository = userRepository;
            _uow = uow;
            _emailService = emailService;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _otpSender = otpSender;
            _configuration = configuration;
            _messagePublisher = messagePublisher;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        { 
          
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);

            if (existingUser != null)
            {
                throw new ConflictException("Bu email artıq istifadə olunur.");
            }

            var user = new ApplicationUser
            {
                FirstName = request.Name,
                Email = request.Email,
                PasswordHash = _passwordHasher.Hash(request.Password),
                EmailConfirmed = false
            };

            await _otpSender.SendOtpAsync(user);

            await _userRepository.AddAsync(user);

            await _messagePublisher.PublishAsync(
                new UserRegisteredMessage(
                    user.Id,
                    user.Email
                ));

            return new RegisterResponse
            {
                Message = "Qeydiyyat uğurlu oldu. E-poçtunuza göndərilən kodu daxil edin.",
                Email = user.Email,
                OtpExpiresAt = user.OtpExpireDate.Value
            };
        }
           

        

        public async Task VerifyEmailAsync(VerifyEmailRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                throw new NotFoundException("İstifadəçi tapılmadı.");
            }
            if (user.OtpCode != request.Otp)
            {
                throw new BadRequestException("OTP kodu yanlışdır.");
            }
            if (user.OtpExpireDate == null || user.OtpExpireDate < DateTime.UtcNow)
            {
                throw new BadRequestException("OTP kodunun vaxtı bitib.");
            }
            user.EmailConfirmed = true;

            user.OtpCode = null;

            user.OtpExpireDate = null;
            user.OtpAttemptCount = 0;

        }
        public async Task ResendOtpAsync(ResendOtpRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                throw new NotFoundException("İstifadəçi tapılmadı.");
            }

            if (user.EmailConfirmed)
            {
                throw new ConflictException("Email artıq təsdiqlənib.");
            }

            await _otpSender.SendOtpAsync(user);

        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                throw new UnauthorizedException("Email və ya şifrə yanlışdır.");
            }

            if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                throw new UnauthorizedException("Email və ya şifrə yanlışdır.");
            }
            if (!user.EmailConfirmed)
            {
                throw new UnauthorizedException("Zəhmət olmasa əvvəl emailinizi təsdiqləyin.");
            }
            var accessToken = _jwtService.GenerateAccessToken(user);

            var refreshToken = _jwtService.GenerateRefreshToken();
            user.RefreshToken = new RefreshToken
            {
                Token = refreshToken,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(30),
                CreatedByIp = "127.0.0.1"
            };


            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                TokenType = "Bearer",
                ExpiresIn = (int)TimeSpan.FromMinutes(15).TotalSeconds,
                User = new UserResponse
                {
                    Id = user.Id,
                    Name = user.FirstName,
                    Email = user.Email,
                    EmailVerified = user.EmailConfirmed,
                    AvatarUrl = string.Empty,
                    CreatedAt = DateTime.UtcNow
                }
            };
        }

        public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(request.RefreshToken);

            if (user == null)
            {
                throw new NotFoundException("Refresh token tapılmadı.");
            }

            if (user.RefreshToken.IsExpired)
            {
                throw new UnauthorizedException("Refresh token-in vaxtı bitib.");
            }
            var accessToken = _jwtService.GenerateAccessToken(user);

            var refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken.Token = refreshToken;
            user.RefreshToken.Created = DateTime.UtcNow;
            user.RefreshToken.Expires = DateTime.UtcNow.AddDays(30);


            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                TokenType = "Bearer",
                ExpiresIn = (int)TimeSpan.FromMinutes(15).TotalSeconds,
                User = new UserResponse
                {
                    Id = user.Id,
                    Name = user.FirstName,
                    Email = user.Email,
                    EmailVerified = user.EmailConfirmed,
                    AvatarUrl = string.Empty,
                    CreatedAt = DateTime.UtcNow
                }
            };
        }

        public Task LogoutAsync(string refreshToken)
        {
            throw new System.NotImplementedException();
        }
        public async Task ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                throw new NotFoundException("İstifadəçi tapılmadı.");
            }

            await _otpSender.SendOtpAsync(user);

        }

        public async Task ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                throw new NotFoundException("İstifadəçi tapılmadı.");
            }

            if (user.OtpCode != request.Code)
            {
                throw new BadRequestException("OTP kodu yanlışdır.");
            }

            if (user.OtpExpireDate == null || user.OtpExpireDate < DateTime.UtcNow)
            {
                throw new BadRequestException("OTP kodunun vaxtı bitib.");
            }

            user.PasswordHash = _passwordHasher.Hash(request.Password);

            user.OtpCode = null;
            user.OtpExpireDate = null;
            user.OtpAttemptCount = 0;

        }
        public async Task<LoginResponse> GoogleLoginAsync(GoogleLoginRequest request)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(
                request.IdToken,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[]
                    {
                _configuration["GoogleAuth:ClientId"]
                    }
                });

            var user = await _userRepository.GetByEmailAsync(payload.Email);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    FirstName = payload.Name,
                    Email = payload.Email,
                    EmailConfirmed = payload.EmailVerified
                };

                await _userRepository.AddAsync(user);
            }

            var accessToken = _jwtService.GenerateAccessToken(user);

            var refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = new RefreshToken
            {
                Token = refreshToken,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(30),
                CreatedByIp = "127.0.0.1"
            };


            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                TokenType = "Bearer",
                ExpiresIn = 900,
                User = new UserResponse
                {
                    Id = user.Id,
                    Name = user.FirstName,
                    Email = user.Email,
                    EmailVerified = user.EmailConfirmed,
                    AvatarUrl = payload.Picture ?? string.Empty,
                    CreatedAt = DateTime.UtcNow
                }
            };
        }
    }
}