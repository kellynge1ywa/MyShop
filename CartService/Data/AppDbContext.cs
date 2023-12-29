using Microsoft.EntityFrameworkCore;

namespace CartService;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         

         modelBuilder.Entity<ProductDto>()
        .Property(p=>p.Price)
        .HasColumnType("decimal");

         modelBuilder.Entity<ProductResponseDto>()
        .Property(p=>p.Price)
        .HasColumnType("decimal");

        
        
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Cart> Carts {get;set;}
    public DbSet<ProductCart> ProductCarts {get;set;}

}
