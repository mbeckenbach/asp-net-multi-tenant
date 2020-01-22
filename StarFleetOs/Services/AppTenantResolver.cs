using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using SaasKit.Multitenancy;
using StarFleetOs.Database.Tenants;
using StarFleetOs.Database.Tenants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var tenant = await new AppTenant().GetFromCacheAsync(_cache, _db, context.Request);

            if (tenant == null)
                throw new KeyNotFoundException("No tenant for current domain");

            return new TenantContext<AppTenant>(tenant);
        }
    }
}
