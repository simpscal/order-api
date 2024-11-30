using MediatR;

using Microsoft.Extensions.Configuration;

using Order.Application.Common.Repositories;
using Order.Application.Common.Utilities;

namespace Order.Application.Auth.Queries.Login;

public class LoginQueryHandler(IUserRepository userRepository, IConfiguration configuration, TokenUtility tokenUtility)
    : IRequestHandler<LoginQuery, LoginDto>
{
    public async Task<LoginDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserAsync(request.Email, request.Password);

        var accessTokenExpire = DateTime.UtcNow.AddMinutes(
            configuration.GetValue<int>("JwtSettings:TokenExpirationInMinutes"));
        var refreshTokenExpire = DateTime.UtcNow.AddMinutes(
            configuration.GetValue<int>("JwtSettings:TokenExpirationInDays"));

        return new LoginDto
        {
            AccessToken =
                tokenUtility.GenerateToken(user, accessTokenExpire),
            RefreshToken = tokenUtility.GenerateToken(user, refreshTokenExpire),
        };
    }
}

public record LoginQuery(string Email, string Password) : IRequest<LoginDto>;