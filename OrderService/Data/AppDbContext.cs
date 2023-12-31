using Microsoft.EntityFrameworkCore;
using Models;

namespace OrderService;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
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
