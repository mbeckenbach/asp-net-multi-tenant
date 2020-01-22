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
        private readonly AppTenant tenant;

        public StarshipController(
            AppTenant tenant
            )
        {
            this.tenant = tenant;
        }

        [HttpGet]
        public StarshipViewModel Get()
        {
            return new StarshipViewModel
            {
                Identifier = tenant.Identifier,
                Name = tenant.Name
            };
        }
    }
}
