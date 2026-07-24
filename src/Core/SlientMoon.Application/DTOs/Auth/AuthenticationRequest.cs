using System;
using System.ComponentModel.DataAnnotations;

namespace SlientMoon.Application.DTOs.Auth
{
    public class AuthenticationRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}