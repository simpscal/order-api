namespace Order.Models;

public class JwtToken
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}