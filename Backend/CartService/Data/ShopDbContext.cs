using Microsoft.EntityFrameworkCore;
using Models;
// using ProductService;

namespace CartService;

public class ShopDbContext:DbContext
{
    public ShopDbContext(DbContextOptions<ShopDbContext>options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         

         modelBuilder.Entity<ProductDto>()
        .Property(p=>p.Price)
        .HasColumnType("decimal");

          modelBuilder.Entity<ProductCart>()
        .Property(p=>p.Price)
        .HasColumnType("decimal");

         modelBuilder.Entity<ProductCart>()
        .Property(p=>p.Discount)
        .HasColumnType("decimal");

           modelBuilder.Entity<ProductCart>()
        .Property(p=>p.Subtotal)
        .HasColumnType("decimal");


        //  modelBuilder.Entity<ProductResponseDto>()
        // .Property(p=>p.Price)
        // .HasColumnType("decimal");
        //  modelBuilder.Entity<Product>()
        // .Property(p=>p.Price)
        // .HasColumnType("decimal");

        
        
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Cart> Carts {get;set;}
    public DbSet<ProductCart> ProductCarts {get;set;}

}
