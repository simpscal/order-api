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
        var secretKey = configuration.GetValue<string>("JwtSettings:Secret");

        if (secretKey == null)
        {
            throw new Exception("Empty JWTSettings Secret");
        }

        var key = Encoding.UTF8.GetBytes(secretKey);
        expires ??= DateTime.UtcNow.AddHours(1);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject =
                new ClaimsIdentity([new Claim("id", user.Id.ToString()), new Claim("email", user.Email)]),
            Expires = expires,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}