using Microsoft.EntityFrameworkCore;
using Models;

namespace CouponService;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext>options): base(options)
    {
        
    }

    public DbSet<Coupon> Coupons {get;set;}

}
