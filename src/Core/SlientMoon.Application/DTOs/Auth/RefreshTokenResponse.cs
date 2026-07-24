namespace SlientMoon.Application.DTOs.Auth;

public class RefreshTokenResponse
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public string TokenType { get; set; } = "Bearer";
    public int ExpiresIn { get; set; } = 900;
}
