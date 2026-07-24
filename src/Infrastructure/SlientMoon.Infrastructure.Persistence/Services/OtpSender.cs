using SlientMoon.Application.DTOs.Email;
using SlientMoon.Application.Interfaces.Services;
using SlientMoon.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SlientMoon.Infrastructure.Persistence.Services
{
    public class OtpSender : IOtpSender
    {
        private readonly IEmailService _emailService;

        public OtpSender(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendOtpAsync(ApplicationUser user)
        {
            var otp = new Random().Next(100000, 999999).ToString();

            user.OtpCode = otp;
            user.OtpExpireDate = DateTime.UtcNow.AddMinutes(10);
            user.OtpAttemptCount = 0;

            await _emailService.SendAsync(new EmailRequest
            {
                To = user.Email,
                Subject = "Email Verification",
                Body = $"Your verification code is: {otp}"
            });
        }
    }
}