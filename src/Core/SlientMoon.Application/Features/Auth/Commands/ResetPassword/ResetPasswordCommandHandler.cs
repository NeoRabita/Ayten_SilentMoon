using Application.Abstractions.Messaging;
using SlientMoon.Application.Interfaces.Repositories;
using SlientMoon.Application.Interfaces.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Auth.Commands.ResetPassword;

public sealed class ResetPasswordCommandHandler
    : ICommandHandler<ResetPasswordCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public ResetPasswordCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> Handle(
        ResetPasswordCommand command,
        CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);

        if (user == null)
        {
            return Result.Failure(
                Error.NotFound(
                    "Auth.UserNotFound",
                    "İstifadəçi tapılmadı."));
        }
        if (user.OtpCode != command.Code)
        {
            return Result.Failure(
                Error.Validation(
                    "Auth.InvalidOtp",
                    "OTP kodu yanlışdır."));
        }

        if (user.OtpExpireDate == null ||
            user.OtpExpireDate < DateTime.UtcNow)
        {
            return Result.Failure(
                Error.Validation(
                    "Auth.OtpExpired",
                    "OTP kodunun vaxtı bitib."));
        }

        user.PasswordHash = _passwordHasher.Hash(command.Password);

        user.OtpCode = null;
        user.OtpExpireDate = null;
        user.OtpAttemptCount = 0;

        return Result.Success();
    }
}