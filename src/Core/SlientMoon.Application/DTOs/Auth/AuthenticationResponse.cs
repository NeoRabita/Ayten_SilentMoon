using System;
using System.Collections.Generic;
using SlientMoon.Application.DTOs.JWT;

namespace SlientMoon.Application.DTOs.Auth
{
    public class AuthenticationResponse
    {
        public string Email { get; set; }
        public string Name { get; set; }
        // public List<string> Roles { get; set; }
        public JwtTokenDto Jwt { get; set; }
        public RefreshTokenDto RefreshToken { get; set; }
    }
}