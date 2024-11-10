using Microsoft.EntityFrameworkCore;

using Order.Domain.Products;

namespace Order.Infrastructure.Common;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}