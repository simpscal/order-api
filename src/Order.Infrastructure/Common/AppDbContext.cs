using Microsoft.EntityFrameworkCore;
using Order.Domain.Categories;
using Order.Domain.ProductColors;
using Order.Domain.ProductInventories;
using Order.Domain.Products;
using Order.Domain.ProductSizes;
using Order.Domain.SubCategories;
using Order.Domain.Users;

namespace Order.Infrastructure.Common;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; init; }
    public DbSet<Category> Categories { get; init; }
    public DbSet<SubCategory> SubCategories { get; init; }
    public DbSet<ProductColor> ProductColors { get; init; }
    public DbSet<ProductSize> ProductSizes { get; init; }
    public DbSet<ProductInventory> ProductInventories { get; init; }
    public DbSet<User> Users { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Product Inventory
        modelBuilder.Entity<ProductInventory>()
            .HasKey(productInventory =>
                new
                {
                    productInventory.Id,
                    productInventory.ProductId,
                    productInventory.ProductColorName,
                    productInventory.ProductSizeName,
                });
    }
}