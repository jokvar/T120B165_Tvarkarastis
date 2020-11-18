using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T120B165.Data;
using T120B165.Models;

namespace T120B165.Controllers
{
    [Route("Modules/{moduleId}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LecturesController : ControllerBase
    {
        private readonly T120B165Context _context;

        public LecturesController(T120B165Context context)
        {
            _context = context;
        }

        // GET: api/Lectures
        /// <summary>
        /// Return list of all lecture objects
        /// </summary>
        /// <returns>List of lecture objects</returns>
        /// <response code="200">Returns List of lecture objects</response>
        /// <param name="moduleId">Integer id of module</param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Lecture>>> GetLectures(int moduleId)
        {
            return await _context.Lectures.Where(l => l.ModuleID == moduleId).ToListAsync();    
        }

        // GET: api/Lectures/5
        /// <summary>
        /// Return lecture object
        /// </summary>
        /// <returns>lecture object</returns>
        /// <response code="200">Returns lecture object</response>
        /// <response code="404">The object cannot be found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Lecture>> GetLecture(int id)
        {
            var lecture = await _context.Lectures.FindAsync(id);

            if (lecture == null)
            {
                return NotFound();
            }

            return lecture;
        }

        // PUT: api/Lectures/5
        /// <summary>
        /// Update lecture object
        /// </summary>
        /// <returns>Nothing</returns>
        /// <response code="204">Returns nothing on success</response>
        /// <response code="400">Supplied id does not match id in details</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutLecture(int id, Lecture lecture)
        {
            if (id != lecture.ID)
            {
                return BadRequest();
            }

            _context.Entry(lecture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LectureExists(id))
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

        // POST: api/Lectures
        /// <summary>
        /// Create a new lecture object
        /// </summary>
        /// <param name="lecture"></param>
        /// <returns>Created lecture object</returns>
        /// <response code="201">Returns Created lecture object</response>
        /// <response code="400">Bad request if invalid data</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Lecture>> PostLecture(Lecture lecture)
        {
            if (lecture.Duration == null)
            {
                if (lecture.EndDate == default)
                {
                    lecture.Duration = TimeSpan.FromHours(1);
                    lecture.EndDate = lecture.StartDate.Add(lecture.Duration);
                }
                else
                {
                    lecture.Duration = lecture.EndDate.Subtract(lecture.StartDate);
                }
            }
            if (lecture.EndDate == default)
            {
                lecture.EndDate = lecture.StartDate.Add(lecture.Duration);
            }
            _context.Lectures.Add(lecture);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLecture", new { id = lecture.ID }, lecture);
        }

        // DELETE: api/Lectures/5
        /// <summary>
        /// Delete lecture object
        /// </summary>
        /// <returns>Deleted object</returns>
        /// <response code="200">Returns deleted lecture object on success</response>
        /// <response code="404">Supplied id not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Lecture>> DeleteLecture(int id)
        {
            (var isLecturer, var username) = IsLecturer();
            if (!isLecturer)
            {

            }
            var lecture = await _context.Lectures.FindAsync(id);
            if (lecture == null)
            {
                return NotFound();
            }

            _context.Lectures.Remove(lecture);
            await _context.SaveChangesAsync();

            return lecture;
        }

        private bool LectureExists(int id)
        {
            return _context.Lectures.Any(e => e.ID == id);
        }

        private (bool,string) IsLecturer()
        {
            var username = GetUsernameFromClaims(HttpContext.User.Identity as ClaimsIdentity);
            var student = _context.Students.Where(s => s.Username == username).FirstOrDefault();
            if (student != null)
            {
                //this is a student
                return (false, "Students are not authorized for this action");
            }
            var user = _context.Lecturers.Where(l => l.Username == username).FirstOrDefault();
            if (user == null)
            {
                //invalid username
                return (false, "Invalid Lecturer");
            }
            return (true, user.Username);
        }
        private string GetUsernameFromClaims(ClaimsIdentity claimsIdentity)
        {
            var claims = claimsIdentity.Claims;
            var usernameClaim = claims.Where(c => c.Type == "Username").FirstOrDefault();
            if (usernameClaim == null)
            {
                throw new NullReferenceException("Authorized user provided no valid username claim. Definitely internal error.");
            }
            return usernameClaim.Value;
        }
    }
}
