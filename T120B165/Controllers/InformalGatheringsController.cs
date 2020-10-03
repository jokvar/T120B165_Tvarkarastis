using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T120B165.Data;
using T120B165.Models;

namespace T120B165.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformalGatheringsController : ControllerBase
    {
        private readonly T120B165Context _context;

        public InformalGatheringsController(T120B165Context context)
        {
            _context = context;
        }

        // GET: api/InformalGatherings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InformalGathering>>> GetInformalGatherings()
        {
            return await _context.InformalGatherings.ToListAsync();
        }

        // GET: api/InformalGatherings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InformalGathering>> GetInformalGathering(int id)
        {
            var informalGathering = await _context.InformalGatherings.FindAsync(id);

            if (informalGathering == null)
            {
                return NotFound();
            }

            return informalGathering;
        }

        // PUT: api/InformalGatherings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInformalGathering(int id, InformalGathering informalGathering)
        {
            if (id != informalGathering.ID)
            {
                return BadRequest();
            }

            _context.Entry(informalGathering).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InformalGatheringExists(id))
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

        // POST: api/InformalGatherings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<InformalGathering>> PostInformalGathering(InformalGathering informalGathering)
        {
            _context.InformalGatherings.Add(informalGathering);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInformalGathering", new { id = informalGathering.ID }, informalGathering);
        }

        // DELETE: api/InformalGatherings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InformalGathering>> DeleteInformalGathering(int id)
        {
            var informalGathering = await _context.InformalGatherings.FindAsync(id);
            if (informalGathering == null)
            {
                return NotFound();
            }

            _context.InformalGatherings.Remove(informalGathering);
            await _context.SaveChangesAsync();

            return informalGathering;
        }

        private bool InformalGatheringExists(int id)
        {
            return _context.InformalGatherings.Any(e => e.ID == id);
        }
    }
}
