using Order.Shared.Attributes;

namespace Order.Domain.Common.Enums;

public enum RoleType
{
    [StringValue("admin")]
    Admin,
    [StringValue("user")]
    User,
}