using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarFleetOs.Database.App;
using StarFleetOs.Database.App.Models;
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
                    var enterprise = SeedTenant(tenantsDbContext, new AppTenant
                    {
                        Identifier = "NCC-1701",
                        Name = "USS Enterprise",
                        Domain = "localhost:5001"
                    });

                    var voyager = SeedTenant(tenantsDbContext, new AppTenant
                    {
                        Identifier = "NCC-74656",
                        Name = "USS Voyager",
                        Domain = "localhost:5002"
                    });

                    var defiant = SeedTenant(tenantsDbContext, new AppTenant
                    {
                        Identifier = "NX-74205",
                        Name = "USS Defiant",
                        Domain = "localhost:5003"
                    });

                    // Save initial data
                    tenantsDbContext.SaveChanges();

                    using (var appDbContext = serviceScope.ServiceProvider.GetService<AppDbContext>())
                    {
                        // Apply any pending migration
                        appDbContext.Database.Migrate();

                        if (appDbContext.CrewMembers.IgnoreQueryFilters().Any() == false)
                        {
                            appDbContext.CrewMembers.Add(new CrewMember { Name = "Jean-Luc Picard", Rank = "Captain", TenantId = enterprise.Id });
                            appDbContext.CrewMembers.Add(new CrewMember { Name = "William T. Riker", Rank = "Commander", TenantId = enterprise.Id });
                            appDbContext.CrewMembers.Add(new CrewMember { Name = "Data", Rank = "Lieutenant Commander", TenantId = enterprise.Id });
                            appDbContext.CrewMembers.Add(new CrewMember { Name = "Geordi La Forge", Rank = "Lieutenant Commander", TenantId = enterprise.Id });
                            appDbContext.CrewMembers.Add(new CrewMember { Name = "Reginald Barclay", Rank = "Lieutenant", TenantId = enterprise.Id });
                            appDbContext.CrewMembers.Add(new CrewMember { Name = "Beverly Crusher", Rank = "Commander", TenantId = enterprise.Id });
                            appDbContext.CrewMembers.Add(new CrewMember { Name = "Deanna Troi", Rank = "Counselor", TenantId = enterprise.Id });

                            appDbContext.CrewMembers.Add(new CrewMember { Name = "Kathryn Janeway", Rank = "Captain", TenantId = voyager.Id });
                            appDbContext.CrewMembers.Add(new CrewMember { Name = "Seven of Nine", Rank = "Borg drone", TenantId = voyager.Id });
                            appDbContext.CrewMembers.Add(new CrewMember { Name = "Tuvok", Rank = "Lieutenant Commander", TenantId = voyager.Id });
                        }

                        // Save initial data
                        appDbContext.SaveChanges();
                    }
                }
            }
        }

        private static AppTenant SeedTenant(TenantsDbContext db, AppTenant tenant)
        {
            var dbTenant = db.AppTenants.FirstOrDefault(t => t.Domain == tenant.Domain);
            if (dbTenant != null) return dbTenant;

            dbTenant = tenant;
            db.AppTenants.Add(tenant);

            return dbTenant;
        }
    }
}
