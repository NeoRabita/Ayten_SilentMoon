using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Auth;
using SlientMoon.Application.Features.Auth.Commands.Login;
using SlientMoon.Application.Interfaces.Repositories;
using SlientMoon.Application.Interfaces.Services;
using SlientMoon.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Auth.Commands.Login;

public sealed class LoginCommandHandler
    : ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<Result<LoginResponse>> Handle(
        LoginCommand command,
        CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);

        if (user == null)
        {
            return Result.Failure<LoginResponse>(
                Error.NotFound(
                    "Auth.UserNotFound",
                    "İstifadəçi tapılmadı."));
        }

        if (!_passwordHasher.Verify(command.Password, user.PasswordHash))
        {
            return Result.Failure<LoginResponse>(
                Error.Validation(
                    "Auth.InvalidPassword",
                    "Şifrə yanlışdır."));
        }

        if (!user.EmailConfirmed)
        {
            return Result.Failure<LoginResponse>(
                Error.Validation(
                    "Auth.EmailNotVerified",
                    "Email təsdiqlənməyib."));
        }

        var accessToken = _jwtService.GenerateAccessToken(user);

        var refreshToken = new SlientMoon.Domain.Entities.RefreshToken
        {
            Token = _jwtService.GenerateRefreshToken(),
            Expires = DateTime.UtcNow.AddDays(30),
            Created = DateTime.UtcNow
        };

        user.RefreshToken = refreshToken;

        return Result.Success(new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            TokenType = "Bearer",
            ExpiresIn = 900
        });
    }
}
