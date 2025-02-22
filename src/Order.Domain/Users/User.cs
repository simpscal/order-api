using Order.Domain.Common;
using Order.Domain.Roles;

namespace Order.Domain.Users;

public class User : Entity
{
    public required string Email { get; set; }
    public required string Password { get; set; }

    public Guid RoleId { get; set; }
    public Role? Role { get; set; }
}