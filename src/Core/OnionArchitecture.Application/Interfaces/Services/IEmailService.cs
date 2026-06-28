using System;
using System.Threading.Tasks;
using OnionArchitecture.Application.DTOs.Email;

namespace OnionArchitecture.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}