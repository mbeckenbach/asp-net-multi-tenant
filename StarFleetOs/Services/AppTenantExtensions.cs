using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StarFleetOs.Database.Tenants;
using StarFleetOs.Database.Tenants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarFleetOs.Services
{
    public static class AppTenantExtensions
    {
        private static readonly string CachePrefix = $"{nameof(StarFleetOs)}-{nameof(AppTenant)}";

        public static async Task<AppTenant> GetFromCacheAsync(
            this AppTenant appTenant,
            IDistributedCache cache,
            TenantsDbContext db,
            HttpRequest request
        )
        {
            var domain = request.Host.Value.ToLower();
            var cacheKey = $"{CachePrefix}_${domain}";

            AppTenant tenant;

            // Try to get from cache
            var fromCache = await cache.GetAsync(cacheKey);
            if (fromCache != null)
            {
                tenant = JsonConvert.DeserializeObject<AppTenant>(Encoding.UTF8.GetString(fromCache));
            }
            else
            {
                tenant = db.AppTenants
                    .FirstOrDefault(t => t.Domain == domain);

                if (tenant != null)
                {
                    await cache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(tenant)));
                }
            }

            return tenant;
        }

        public static async Task AddToCacheAsync(
            this AppTenant appTenant,
            IDistributedCache cache,
            HttpRequest request
        )
        {
            var domain = request.Host.Value.ToLower();
            var cacheKey = $"{CachePrefix}_${domain}";

            await cache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(appTenant)));
        }
    }
}
