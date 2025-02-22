using System.ComponentModel;

using Order.Domain.Common;
using Order.Domain.Users;

namespace Order.Domain.Roles;

public class Role : Entity
{
    [Description("RoleType")]
    public required string Name { get; set; }
    public string? Description { get; set; }

    public ICollection<User> Users = [];
}