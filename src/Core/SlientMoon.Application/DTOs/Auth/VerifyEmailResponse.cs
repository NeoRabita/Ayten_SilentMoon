namespace SlientMoon.Application.DTOs.Auth;

public sealed class VerifyEmailResponse
{
    public bool IsVerified { get; set; }
    public string Message { get; set; } = string.Empty;
}