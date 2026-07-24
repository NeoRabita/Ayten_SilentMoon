namespace SlientMoon.Application.DTOs.Auth
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string TokenType { get; set; }

        public int ExpiresIn { get; set; }

        public UserResponse User { get; set; }
    }
}