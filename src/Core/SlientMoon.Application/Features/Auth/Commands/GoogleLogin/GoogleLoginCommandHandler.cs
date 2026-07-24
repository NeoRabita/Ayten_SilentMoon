using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Auth;
using SlientMoon.Application.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Commands.GoogleLogin;

public sealed class GoogleLoginCommandHandler
    : ICommandHandler<GoogleLoginCommand, LoginResponse>
{
    private readonly IAuthService _authService;

    public GoogleLoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<LoginResponse>> Handle(
        GoogleLoginCommand command,
        CancellationToken ct)
    {
        var response = await _authService.GoogleLoginAsync(
            new GoogleLoginRequest
            {
                IdToken = command.IdToken
            });

        return Result.Success(response);
    }
}