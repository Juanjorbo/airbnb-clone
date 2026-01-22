namespace AirbnbClone.Api.Contracts;

public sealed class AuthRegisterRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
