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
                    var defaultTenant = SeedTenants(tenantsDbContext);

                    // Save initial data
                    tenantsDbContext.SaveChanges();
                }
            }
        }

        private static AppTenant SeedTenants(TenantsDbContext db)
        {
            var defaultTenant = db.AppTenants.FirstOrDefault(t => t.Domain == "localhost:5001");
            if (defaultTenant != null) return defaultTenant;

            defaultTenant = new AppTenant
            {
                Identifier = "NCC-1701",
                Name = "USS Enterprise",
                Domain = "localhost:5001"
            };
            db.AppTenants.Add(defaultTenant);

            return defaultTenant;
        }
    }
}
