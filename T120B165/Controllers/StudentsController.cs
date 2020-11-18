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
    public class StudentsController : ControllerBase
    {
        private readonly T120B165Context _context;

        public StudentsController(T120B165Context context)
        {
            _context = context;
        }

        // GET: api/Students
        /// <summary>
        /// Return list of all student objects
        /// </summary>
        /// <returns>List of student objects</returns>
        /// <response code="200">Returns List of student objects</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students
                //.Include("ModuleStudent.Module")
                .ToListAsync();
                                    
        }

        // GET: api/Students/5
        /// <summary>
        /// Return student object
        /// </summary>
        /// <returns>student object</returns>
        /// <response code="200">Returns student object</response>
        /// <response code="404">The object cannot be found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        /// <summary>
        /// Update student object
        /// </summary>
        /// <returns>Nothing</returns>
        /// <response code="204">Returns nothing on success</response>
        /// <response code="400">Supplied id does not match id in details</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.ID)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        /// <summary>
        /// Create a new student object
        /// </summary>
        /// <param name="student"></param>
        /// <returns>Created student object</returns>
        /// <response code="201">Returns Created student object</response>
        /// <response code="400">Bad request if invalid data</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.ID }, student);
        }

        // DELETE: api/Students/5
        /// <summary>
        /// Delete student object
        /// </summary>
        /// <returns>Deleted object</returns>
        /// <response code="200">Returns deleted student object on success</response>
        /// <response code="404">Supplied id not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}
