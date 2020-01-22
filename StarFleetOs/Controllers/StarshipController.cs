using Microsoft.AspNetCore.Mvc;
using StarFleetOs.Database.Tenants.Models;
using StarFleetOs.Model;

namespace StarFleetOs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarshipController : ControllerBase
    {
        private readonly AppTenant tenant;

        public StarshipController(AppTenant tenant)
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
