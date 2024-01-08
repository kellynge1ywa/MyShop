using Microsoft.EntityFrameworkCore;
using Models;

namespace OrderService;

public class ShopDbContext:DbContext
{
    public ShopDbContext(DbContextOptions<ShopDbContext>options):base(options)
    { 
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<Order>()
        .Property(p=>p.Total)
        .HasColumnType("decimal");
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Order>Orders {get;set;}

}
