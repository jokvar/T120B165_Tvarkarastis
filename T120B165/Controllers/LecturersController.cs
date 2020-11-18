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
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LecturersController : ControllerBase
    {
        private readonly T120B165Context _context;

        public LecturersController(T120B165Context context)
        {
            _context = context;
        }

        // GET: Lecturers
        /// <summary>
        /// Return list of all lecturer objects
        /// </summary>
        /// <returns>List of lecturer objects</returns>
        /// <response code="200">Returns List of lecturer objects</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Lecturer>>> GetLecturers()
        {
            return await _context.Lecturers.ToListAsync();
        }

        // GET: Lecturers/5
        /// <summary>
        /// Return lecturer object
        /// </summary>
        /// <returns>lecturer object</returns>
        /// <response code="200">Returns lecturer object</response>
        /// <response code="404">The object cannot be found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Lecturer>> GetLecturer(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);

            if (lecturer == null)
            {
                return NotFound();
            }

            return lecturer;
        }

        // PUT: api/Lecturers/5
        /// <summary>
        /// Update lecturer object
        /// </summary>
        /// <returns>Nothing</returns>
        /// <response code="204">Returns nothing on success</response>
        /// <response code="400">Supplied id does not match id in details</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutLecturer(int id, Lecturer lecturer)
        {
            if (id != lecturer.ID)
            {
                return BadRequest();
            }

            _context.Entry(lecturer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LecturerExists(id))
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

        // POST: api/Lecturers
        /// <summary>
        /// Create a new lecturer object
        /// </summary>
        /// <param name="lecturer"></param>
        /// <returns>Created lecturer object</returns>
        /// <response code="201">Returns Created lecturer object</response>
        /// <response code="400">Bad request if invalid data</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Lecturer>> PostLecturer(Lecturer lecturer)
        {
            _context.Lecturers.Add(lecturer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLecturer", new { id = lecturer.ID }, lecturer);
        }

        // DELETE: api/Lecturers/5
        /// <summary>
        /// Delete lecturer object
        /// </summary>
        /// <returns>Deleted object</returns>
        /// <response code="200">Returns deleted lecturer object on success</response>
        /// <response code="404">Supplied id not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Lecturer>> DeleteLecturer(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null)
            {
                return NotFound();
            }

            _context.Lecturers.Remove(lecturer);
            await _context.SaveChangesAsync();

            return lecturer;
        }

        private bool LecturerExists(int id)
        {
            return _context.Lecturers.Any(e => e.ID == id);
        }
    }
}
