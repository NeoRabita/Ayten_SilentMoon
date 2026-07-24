using System.ComponentModel.DataAnnotations;

namespace SlientMoon.Application.DTOs.Auth
{
    public class GoogleLoginRequest
    {
        [Required]
        public string IdToken { get; set; }
    }
}