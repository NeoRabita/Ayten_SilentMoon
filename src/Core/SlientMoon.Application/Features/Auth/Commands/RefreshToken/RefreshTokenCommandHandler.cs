using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Auth;
using SlientMoon.Application.Interfaces.Repositories;
using SlientMoon.Application.Interfaces.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler
    : ICommandHandler<RefreshTokenCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public RefreshTokenCommandHandler(
        IUserRepository userRepository,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<Result<LoginResponse>> Handle(
        RefreshTokenCommand command,
        CancellationToken ct)
    {
        var user = await _userRepository.GetByRefreshTokenAsync(command.RefreshToken);

        if (user == null)
        {
            return Result.Failure<LoginResponse>(
                Error.Validation(
                    "Auth.InvalidRefreshToken",
                    "Refresh token etibarsızdır."));
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