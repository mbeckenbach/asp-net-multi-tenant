using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarFleetOs.Database.App;
using StarFleetOs.Database.App.Models;

namespace StarFleetOs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrewMembersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CrewMembersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CrewMembers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CrewMember>>> GetCrewMembers()
        {
            return await _context.CrewMembers.ToListAsync();
        }

        // GET: api/CrewMembers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CrewMember>> GetCrewMember(Guid id)
        {
            var crewMember = await _context.CrewMembers.FindAsync(id);

            if (crewMember == null)
            {
                return NotFound();
            }

            return crewMember;
        }

        // PUT: api/CrewMembers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCrewMember(Guid id, CrewMember crewMember)
        {
            if (id != crewMember.Id)
            {
                return BadRequest();
            }

            _context.Entry(crewMember).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CrewMemberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CrewMembers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CrewMember>> PostCrewMember(CrewMember crewMember)
        {
            _context.CrewMembers.Add(crewMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCrewMember", new { id = crewMember.Id }, crewMember);
        }

        // DELETE: api/CrewMembers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CrewMember>> DeleteCrewMember(Guid id)
        {
            var crewMember = await _context.CrewMembers.FindAsync(id);
            if (crewMember == null)
            {
                return NotFound();
            }

            _context.CrewMembers.Remove(crewMember);
            await _context.SaveChangesAsync();

            return crewMember;
        }

        private bool CrewMemberExists(Guid id)
        {
            return _context.CrewMembers.Any(e => e.Id == id);
        }
    }
}
