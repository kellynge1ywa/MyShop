﻿using Microsoft.EntityFrameworkCore;

namespace CartService;

public static  class AddMigrations
{
    public static IApplicationBuilder UseMigrations( this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            return app;
        }

}
