using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthService;

public class ShopDbContext: IdentityDbContext<ShopUser>
{
    public ShopDbContext(DbContextOptions<ShopDbContext>options):base(options)
    {
        
    }

    public DbSet<ShopUser> ShopUsers {get;set;}

}
