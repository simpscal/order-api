using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Order.Domain.Users;

namespace Order.Application.Common.Utilities;

public class TokenUtility(IConfiguration configuration)
{
    public string GenerateToken(User user, DateTime? expires = null)
    {
        var secretKey = configuration["JwtSettings:Secret"];

        if (secretKey == null)
        {
            throw new Exception("Empty JWTSettings Secret");
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject =
                new ClaimsIdentity([
                    new Claim("userId", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                ]),
            Expires = expires ?? DateTime.UtcNow.AddHours(1),
            SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    SecurityAlgorithms.HmacSha256Signature),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}