using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarFleetOs.Database.App;
using StarFleetOs.Database.Tenants;
using StarFleetOs.Database.Tenants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarFleetOs.Database
{
    public static class DatabaseInitializer
    {
        public static void UpdateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var tenantsDbContext = serviceScope.ServiceProvider.GetService<TenantsDbContext>())
                {
                    // Apply any pending migration
                    tenantsDbContext.Database.Migrate();

                    // Seed initial data
                    var defaultTenant = SeedTenant(tenantsDbContext, new AppTenant
                    {
                        Identifier = "NCC-1701",
                        Name = "USS Enterprise",
                        Domain = "localhost:5001"
                    });

                    SeedTenant(tenantsDbContext, new AppTenant
                    {
                        Identifier = "NCC-74656",
                        Name = "USS Voyager",
                        Domain = "localhost:5002"
                    });

                    SeedTenant(tenantsDbContext, new AppTenant
                    {
                        Identifier = "NX-74205",
                        Name = "USS Defiant",
                        Domain = "localhost:5003"
                    });

                    // Save initial data
                    tenantsDbContext.SaveChanges();
                }
            }
        }

        private static AppTenant SeedTenant(TenantsDbContext db, AppTenant tenant)
        {
            var defaultTenant = db.AppTenants.FirstOrDefault(t => t.Domain == tenant.Domain);
            if (defaultTenant != null) return defaultTenant;

            defaultTenant = tenant;
            db.AppTenants.Add(tenant);

            return defaultTenant;
        }
    }
}
