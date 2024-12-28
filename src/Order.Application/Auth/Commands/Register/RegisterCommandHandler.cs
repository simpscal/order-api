using MediatR;

using Order.Domain.Users;

namespace Order.Application.Auth.Commands.Register;

public class RegisterCommandHandler(IUserRepository userRepository) : IRequestHandler<RegisterCommand>
{
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await userRepository.CreateUserAsync(request.Email, request.Password);
    }
}

public record RegisterCommand(string Email, string Password) : IRequest;