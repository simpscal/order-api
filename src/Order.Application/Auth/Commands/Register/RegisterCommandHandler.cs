using MediatR;

using Order.Application.Common.Models;
using Order.Application.Common.Utilities;
using Order.Domain.Users;
using Order.Domain.Users.Specifications;

namespace Order.Application.Auth.Commands.Register;

public class RegisterCommandHandler(
    IUserRepository userRepository,
    TokenUtility tokenUtility)
    : IRequestHandler<RegisterCommand, JwtToken>
{
    public async Task<JwtToken> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUserSpecification = new ExistingUserSpecification(request.Email);
        var existingUser = await userRepository.FindByExpressionAsync(existingUserSpecification);

        if (existingUser != null)
        {
            throw new Exception("User already exists");
        }

        var user = await userRepository.AddAsync(new User { Email = request.Email, Password = request.Password, });

        return tokenUtility.GenerateJwtToken(user);
    }
}

public record RegisterCommand(string Email, string Password) : IRequest<JwtToken>;