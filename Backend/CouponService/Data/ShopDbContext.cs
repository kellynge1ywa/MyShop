using Microsoft.EntityFrameworkCore;
using Models;

namespace CouponService;

public class ShopDbContext:DbContext
{
    public ShopDbContext(DbContextOptions<ShopDbContext>options): base(options)
    {
        
    }

    public DbSet<Coupon> Coupons {get;set;}

}
