using MediatR;

using Order.Application.Common.Utilities;
using Order.Domain.Common.Enums;
using Order.Domain.Roles;
using Order.Domain.Users;
using Order.Domain.Users.Specifications;
using Order.Models;
using Order.Shared.Extensions;

namespace Order.Application.Auth.Commands.Register;

public class RegisterCommandHandler(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
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

        var role = await roleRepository.GetAsync(request.Role.ToEnum<RoleType>());

        var user = await userRepository.AddAsync(
            new User
            {
                Email = request.Email,
                Password = request.Password,
                RoleId = role.Id,
            });

        if (await userRepository.SaveChangesAsync() < 1)
        {
            throw new ApplicationException("There was an error creating the user");
        }

        return tokenUtility.GenerateJwtToken(user);
    }
}

public record RegisterCommand(string Email, string Password, string Role) : IRequest<JwtToken>;