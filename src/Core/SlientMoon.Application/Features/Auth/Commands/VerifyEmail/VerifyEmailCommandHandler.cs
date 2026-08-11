using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Auth;
using SlientMoon.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Auth.Commands.VerifyEmail;

public sealed class VerifyEmailCommandHandler
    : ICommandHandler<VerifyEmailCommand, VerifyEmailResponse>
{
    private readonly IUserRepository _userRepository;

    public VerifyEmailCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<VerifyEmailResponse>> Handle(
        VerifyEmailCommand command,
        CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);

        if (user == null)
        {
            return Result.Failure<VerifyEmailResponse>(
                Error.NotFound(
                    "Auth.UserNotFound",
                    "İstifadəçi tapılmadı."));
        }

        if (user.OtpCode != command.Otp)
        {
            return Result.Failure<VerifyEmailResponse>(
                Error.Validation(
                    "Auth.InvalidOtp",
                    "OTP kodu yanlışdır."));
        }

        if (user.OtpExpireDate == null ||
            user.OtpExpireDate < DateTime.UtcNow)
        {
            return Result.Failure<VerifyEmailResponse>(
                Error.Validation(
                    "Auth.OtpExpired",
                    "OTP kodunun vaxtı bitib."));
        }

        user.EmailConfirmed = true;
        user.OtpCode = null;
        user.OtpExpireDate = null;
        user.OtpAttemptCount = 0;

        return Result.Success(new VerifyEmailResponse
        {
            IsVerified = true,
            Message = "Email uğurla təsdiqləndi."
        });
    }
}