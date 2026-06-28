using System;
using System.ComponentModel.DataAnnotations;

namespace OnionArchitecture.Application.DTOs.Email
{
    public class ConfirmEmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Code { get; set; }
    }
}