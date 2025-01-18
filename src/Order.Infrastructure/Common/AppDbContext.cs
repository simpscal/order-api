using Microsoft.EntityFrameworkCore;

using Order.Domain.Category;
using Order.Domain.Product;
using Order.Domain.ProductColor;
using Order.Domain.ProductSize;
using Order.Domain.SubCategory;
using Order.Domain.User;

namespace Order.Infrastructure.Common;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<ProductColor> ProductColors { get; set; }
    public DbSet<ProductSize> ProductSizes { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasMany(e => e.ProductColors)
            .WithMany(e => e.Products)
            .UsingEntity<Dictionary<string, object>>(
                "ProductProductColor",
                j => j.HasOne<ProductColor>().WithMany().HasForeignKey("ProductColorsId"),
                j => j.HasOne<Product>().WithMany().HasForeignKey("ProductsId"),
                j =>
                {
                    j.HasKey("ProductsId", "ProductColorsId");
                });

        modelBuilder.Entity<Product>()
            .HasMany(e => e.ProductSizes)
            .WithMany(e => e.Products)
            .UsingEntity<Dictionary<string, object>>(
                "ProductProductSize",
                j => j.HasOne<ProductSize>().WithMany().HasForeignKey("ProductSizesId"),
                j => j.HasOne<Product>().WithMany().HasForeignKey("ProductsId"),
                j =>
                {
                    j.HasKey("ProductsId", "ProductSizesId");
                });
    }
}