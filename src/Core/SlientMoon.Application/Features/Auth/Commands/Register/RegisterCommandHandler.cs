using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Auth;
using SlientMoon.Application.Interfaces.Repositories;
using SlientMoon.Application.Interfaces.Services;
using SlientMoon.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Commands.Register;

public sealed class RegisterCommandHandler
    : ICommandHandler<RegisterCommand, RegisterResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IOtpSender _otpSender;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IOtpSender otpSender,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _otpSender = otpSender;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<RegisterResponse>> Handle(
     RegisterCommand command,
     CancellationToken ct)
    {
        var existingUser = await _userRepository.GetByEmailAsync(command.Email);

        if (existingUser != null)
        {
            return Result.Failure<RegisterResponse>(
                Error.Conflict(
                    "Auth.EmailAlreadyExists",
                    "Bu email artıq istifadə olunur."));
        }

        var user = new ApplicationUser
        {
            FirstName = command.Name,
            Email = command.Email,
            PasswordHash = _passwordHasher.Hash(command.Password),
            EmailConfirmed = false,
            AvatarUrl = null,
            CreatedAt = DateTime.UtcNow
        };

        await _otpSender.SendOtpAsync(user);

        await _userRepository.AddAsync(user);

        return Result.Success(new RegisterResponse
        {
            Message = "Qeydiyyat uğurlu oldu. E-poçtunuza göndərilən kodu daxil edin.",
            Email = user.Email,
            OtpExpiresAt = user.OtpExpireDate!.Value
        });
    }
}