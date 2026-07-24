using System;
using System.Threading.Tasks;
using SlientMoon.Application.DTOs.Email;

namespace SlientMoon.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}