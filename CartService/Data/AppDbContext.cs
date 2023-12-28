using Microsoft.EntityFrameworkCore;

namespace CartService;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
    {
        
    }

}
