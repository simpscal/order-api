using MediatR;

using Order.Application.Common.Repositories;

namespace Order.Application.Auth.Queries;

public class LoginQueryHandler(IUserRepository userRepository) : IRequestHandler<LoginQuery, LoginDto>
{
    public async Task<LoginDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUser(request.Email, request.Password);

        return new LoginDto { AccessToken = "asd", RefreshToken = "ads" };
    }
}

public record LoginQuery(string Email, string Password) : IRequest<LoginDto>;