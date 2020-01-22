using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StarFleetOs.Database.Tenants;
using StarFleetOs.Database.Tenants.Models;
using StarFleetOs.Model;
using StarFleetOs.Services;

namespace StarFleetOs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarshipController : ControllerBase
    {
        private readonly IDistributedCache cache;
        private readonly TenantsDbContext db;

        public StarshipController(
            IDistributedCache cache,
            TenantsDbContext db
            )
        {
            this.cache = cache;
            this.db = db;
        }

        // GET: api/Default
        [HttpGet]
        public async Task<StarshipViewModel> Get()
        {
            var tenant = await new AppTenant().GetFromCacheAsync(cache, db, Request);

            return new StarshipViewModel
            {
                Identifier = tenant.Identifier,
                Name = tenant.Name
            };
        }
    }
}
