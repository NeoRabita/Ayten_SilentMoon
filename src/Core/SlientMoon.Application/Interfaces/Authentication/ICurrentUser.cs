namespace SlientMoon.Application.Interfaces.Authentication
{
    public interface ICurrentUser
    {
        int UserId { get; }

        string UserName { get; }
    }
}