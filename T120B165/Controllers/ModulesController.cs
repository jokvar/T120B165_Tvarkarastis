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
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly T120B165Context _context;

        public ModulesController(T120B165Context context)
        {
            _context = context;
        }

        // GET: api/Modules
        /// <summary>
        /// Return list of all module objects
        /// </summary>
        /// <returns>List of Module objects</returns>
        /// <response code="200">Returns List of Module objects</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Module>>> GetModules()
        {
            return await _context.Modules
                .Include(m => m.Students)
                .ThenInclude(s => s.Student)
                .Include(m => m.Lecturer)
                .ToListAsync();
        }

        // GET: api/Modules/5
        /// <summary>
        /// Return module object
        /// </summary>
        /// <returns>Module object</returns>
        /// <response code="200">Returns Module object</response>
        /// <response code="404">The object cannot be found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Module>> GetModule(int id)
        {
            var @module = await _context.Modules.FindAsync(id);

            if (@module == null)
            {
                return NotFound();
            }

            return @module;
        }

        // PUT: api/Modules/5
        /// <summary>
        /// Update module object
        /// </summary>
        /// <returns>Nothing</returns>
        /// <response code="204">Returns nothing on success</response>
        /// <response code="400">Supplied id does not match id in details</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutModule(int id, Module @module)
        {
            if (id != @module.ID)
            {
                return BadRequest();
            }

            _context.Entry(@module).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(id))
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

        // POST: api/Modules
        /// <summary>
        /// Create a new module object
        /// </summary>
        /// <param name="module"></param>
        /// <returns>Created Module object</returns>
        /// <response code="201">Returns Created Module object</response>
        /// <response code="400">Bad request if invalid data</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Module>> PostModule(Module @module)
        {
            _context.Modules.Add(@module);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModule", new { id = @module.ID }, @module);
        }

        // DELETE: api/Modules/5
        /// <summary>
        /// Delete module object
        /// </summary>
        /// <returns>Deleted object</returns>
        /// <response code="200">Returns deleted module object on success</response>
        /// <response code="404">Supplied id not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Module>> DeleteModule(int id)
        {
            var @module = await _context.Modules.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }

            _context.Modules.Remove(@module);
            await _context.SaveChangesAsync();

            return @module;
        }

        private bool ModuleExists(int id)
        {
            return _context.Modules.Any(e => e.ID == id);
        }
    }
}
