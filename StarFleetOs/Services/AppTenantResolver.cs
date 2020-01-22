using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using StarFleetOs.Database.Tenants;
using StarFleetOs.Database.Tenants.Models;
using System;
using System.Threading.Tasks;

namespace StarFleetOs.Services
{
    public class AppTenantResolver : ITenantResolver<AppTenant>
    {
        private readonly IDistributedCache _cache;
        private readonly TenantsDbContext _db;

        public AppTenantResolver(
            IDistributedCache cache,
            TenantsDbContext db
        )
        {
            _cache = cache;
            _db = db;
        }

        public async Task<TenantContext<AppTenant>> ResolveAsync(HttpContext context)
        {
            var domain = context.Request.Host.Value.ToLower();
            var tenant = await new AppTenant().GetFromCacheAsync(_cache, _db, context.Request);

            if (tenant == null)
            {
                tenant = new AppTenant
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000000"),
                    Name = "New Ship",
                    Identifier = "USS Unknown",
                    Domain = domain
                };
            }

            return new TenantContext<AppTenant>(tenant);
        }
    }
}
