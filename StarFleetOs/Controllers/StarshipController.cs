using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarFleetOs.Model;

namespace StarFleetOs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarshipController : ControllerBase
    {
        // GET: api/Default
        [HttpGet]
        public StarshipViewModel Get()
        {
            return new StarshipViewModel { 
                Identifier = "???",
                Name = "???"
            };
        }
    }
}
