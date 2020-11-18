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
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class InformalGatheringsController : ControllerBase
    {
        private readonly T120B165Context _context;

        public InformalGatheringsController(T120B165Context context)
        {
            _context = context;
        }

        // GET: api/InformalGatherings
        /// <summary>
        /// Return list of all informal gathering objects
        /// </summary>
        /// <returns>List of Informal Gatheting objects</returns>
        /// <response code="200">Returns List of Informal Gatheting objects</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<InformalGathering>>> GetInformalGatherings()
        {
            return await _context.InformalGatherings.ToListAsync();
        }

        // GET: api/InformalGatherings/5
        /// <summary>
        /// Return informal gathering object
        /// </summary>
        /// <returns>Informal Gatheting object</returns>
        /// <response code="200">Returns Informal Gatheting object</response>
        /// <response code="404">The object cannot be found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Update informal gathering object
        /// </summary>
        /// <returns>Nothing</returns>
        /// <response code="204">Returns nothing on success</response>
        /// <response code="400">Supplied id does not match id in details</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// <summary>
        /// Create a new informal gathering object
        /// </summary>
        /// <param name="informalGathering"></param>
        /// <returns>Created Informal Gathering object</returns>
        /// <response code="201">Returns Created Informal Gathering object</response>
        /// <response code="400">Bad request if invalid data</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InformalGathering>> PostInformalGathering(InformalGathering informalGathering)
        {
            if (informalGathering.EndDate == default)
            {

            }
            _context.InformalGatherings.Add(informalGathering);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInformalGathering", new { id = informalGathering.ID }, informalGathering);
        }

        // DELETE: api/InformalGatherings/5
        /// <summary>
        /// Delete informal gathering object
        /// </summary>
        /// <returns>Deleted object</returns>
        /// <response code="200">Returns deleted informal gathering object on success</response>
        /// <response code="404">Supplied id not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
