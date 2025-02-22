using FluentValidation;

using Order.Domain.Common.Enums;
using Order.Shared.Extensions;

namespace Order.Application.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(8);
        RuleFor(x => x.Role).Must(role => role.ExistInEnum<RoleType>()).WithMessage("Role is not valid");
    }
}