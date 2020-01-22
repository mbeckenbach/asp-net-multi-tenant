using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarFleetOs.Database.App;
using StarFleetOs.Database.App.Models;
using StarFleetOs.Database.Tenants.Models;

namespace StarFleetOs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrewMembersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AppTenant appTenant;

        public CrewMembersController(AppDbContext context, AppTenant appTenant)
        {
            _context = context;
            this.appTenant = appTenant;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CrewMember>>> GetCrewMembers()
        {
            return await _context.CrewMembers.ToListAsync();
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CrewMember>>> GetAllCrewMembers()
        {
            return await _context.CrewMembers.IgnoreQueryFilters().ToListAsync();
        }
    }
}
