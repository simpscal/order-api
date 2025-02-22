using System.Text.Json;

using Microsoft.EntityFrameworkCore;

using Order.Domain.Categories;
using Order.Domain.ProductColors;
using Order.Domain.ProductInventories;
using Order.Domain.Products;
using Order.Domain.ProductSizes;
using Order.Domain.Roles;
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
    public DbSet<Role> Roles { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Product
        modelBuilder.Entity<Product>()
            .Property(product => product.ImagesByColor)
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = true }),
                v => JsonSerializer.Deserialize<Dictionary<string, string[]>>(v, new JsonSerializerOptions())!)
            .HasColumnType("jsonb");

        // Product Inventory
        modelBuilder.Entity<ProductInventory>()
            .HasKey(productInventory =>
                new
                {
                    productInventory.Id,
                    productInventory.ProductId,
                    productInventory.ProductColorId,
                    productInventory.ProductSizeId,
                });
    }
}