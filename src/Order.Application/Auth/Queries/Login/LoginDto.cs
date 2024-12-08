namespace Order.Application.Auth.Queries.Login;

public class LoginDto
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}