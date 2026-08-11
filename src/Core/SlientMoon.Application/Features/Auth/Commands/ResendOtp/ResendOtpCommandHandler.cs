using Application.Abstractions.Messaging;
using SlientMoon.Application.Interfaces.Repositories;
using SlientMoon.Application.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Auth.Commands.ResendOtp;

public sealed class ResendOtpCommandHandler
    : ICommandHandler<ResendOtpCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IOtpSender _otpSender;

    public ResendOtpCommandHandler(
        IUserRepository userRepository,
        IOtpSender otpSender)
    {
        _userRepository = userRepository;
        _otpSender = otpSender;
    }

    public async Task<Result> Handle(
        ResendOtpCommand command,
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

        await _otpSender.SendOtpAsync(user);

        return Result.Success();
    }
}