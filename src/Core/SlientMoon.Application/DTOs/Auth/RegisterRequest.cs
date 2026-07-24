using System.ComponentModel.DataAnnotations;

namespace SlientMoon.Application.DTOs.Auth
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [MinLength(6)]
        public string Password { get; set; }

    }
}