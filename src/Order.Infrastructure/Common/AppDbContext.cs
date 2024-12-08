using Microsoft.EntityFrameworkCore;

using Order.Domain.Products;
using Order.Domain.Users;

namespace Order.Infrastructure.Common;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
}