using Microsoft.EntityFrameworkCore;
using Models;

namespace ProductService;

public class ShopDbContext:DbContext
{
    public ShopDbContext(DbContextOptions<ShopDbContext>options):base(options)
    {
        
    }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
        .Property(p=>p.Price)
        .HasColumnType("decimal");
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Product> Products {get;set;}
    public DbSet<ProductImages> ProductImages {get;set;}

}
