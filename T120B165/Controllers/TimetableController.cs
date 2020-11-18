using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T120B165.Data;
using T120B165.Models;

namespace T120B165.Controllers
{
    [Route("timetable")]
    [Produces("application/json")]
    [ApiController]
    public class TimetableController : Controller
    {
        //lecturers can make new modules:
        //Post: at /Modules

        //lecturers and students can see events by lecturer, time:
        //Get: Timetable/LecturerEvents/lecturerId
        //Get: Timetable/Events(DateTime)

        //students can subscribe to modules, events, lecturers:
        //Post
        private readonly UserManager<IdentityUser> _userManager;
        private T120B165Context _context;
        public TimetableController(T120B165Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Return list of subscribed events
        /// </summary>
        /// <returns>List of events</returns>
        /// <response code="200">Returns List of events</response>
        /// <response code="401">Unauthorized.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public IActionResult SubscribedEvents()
        {
            var username = GetUsernameFromClaims(HttpContext.User.Identity as ClaimsIdentity);
            var userId = _context.Students.Where(s => s.Username == username).FirstOrDefault().ID;
            var lectureStudentIds = _context.LectureStudents.Where(ls => ls.StudentID == userId).Select(ls => ls.LectureID).ToList();
            var lectures = _context.Lectures.Where(l => lectureStudentIds.Contains(l.ID)).ToList();
            return Ok(new { lectures });
        }

        /// <summary>
        /// Return list of all all events
        /// </summary>
        /// <returns>List of events</returns>
        /// <response code="200">Returns List of events</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("All")]
        public IActionResult AllEvents()
        {
            var lectures = _context.Lectures.ToList();
            //var gatherings = _context.InformalGatherings.ToList();
            return Ok(new { lectures });//, gatherings = gatherings });
        }

        /// <summary>
        /// Return list of all all events within specified time range
        /// </summary>
        /// <returns>List of events</returns>
        /// <response code="200">Returns List of events</response>
        /// <param name="startDate">Date from which to search for start of events</param>
        /// <param name="endDate">Date until which to search for end of events</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
      //Timetable/Events/2018-7-23T00:00:00/2018-7-24T10:24:00
        [HttpGet("Events/{startDate}/{endDate}")]
        public IActionResult DateTimeEvents(DateTime startDate, DateTime endDate)
        {
            var lectures = _context.Lectures.Where(l => l.StartDate > startDate).Where(l => l.EndDate < endDate).ToList();
            return Ok(new { lectures });//, gatherings = _gatherings });
        }

        /// <summary>
        /// Return list of all all events hosted by specified lecturer
        /// </summary>
        /// <returns>List of events</returns>
        /// <response code="200">Returns List of events</response>
        /// <param name="lecturerId">Integer id of lecturer</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("LecturerEvents/{lecturerId}")]
        public IActionResult LecturerEvents(int lecturerId)
        {
            var moduleId = _context.Modules.Where(m => m.LecturerID == lecturerId).FirstOrDefault()?.ID;
            if (moduleId == null)
            {
                return NotFound("Module with specified lecturerId does not exist.");
            }
            var lectures = _context.Lectures.Where(l => l.ModuleID == moduleId).ToList();
            return Ok(new { lectures });
        }

        /// <summary>
        /// Return list of all all events from specified module
        /// </summary>
        /// <returns>List of events</returns>
        /// <response code="200">Returns List of events</response>
        /// <param name="moduleId">Integer id of module</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("ModuleEvents/{moduleId}")]
        public IActionResult ModuleEvents(int moduleId)
        {
            var lectures = _context.Lectures.Where(l => l.ModuleID == moduleId).ToList();
            return Ok(new { lectures });
        }

        /// <summary>
        /// Subscribe to selected event
        /// </summary>
        /// <returns>List of events</returns>
        /// <response code="200">Returns List of newly subscribed events</response>
        /// <response code="404">Lecture with specified eventId does not exist.</response>
        /// <response code="401">Unauthorized.</response>
        /// <param name="eventId">Integer id of lecture</param>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Events/{eventId}")]
        public async Task<IActionResult> SubscribeEvent(int eventId)
        {
            var username = GetUsernameFromClaims(HttpContext.User.Identity as ClaimsIdentity);

            var lectures = _context.Lectures.Where(l => l.ID == eventId).FirstOrDefault();
            if (lectures == null)
            {
                return NotFound("Lecture with specified eventId does not exist.");
            }
            var userId = _context.Students.Where(s => s.Username == username).FirstOrDefault().ID;
            _context.LectureStudents.Add(new LectureStudent() { LectureID = lectures.ID, StudentID = userId });
            await _context.SaveChangesAsync();
            return Ok( new { subscribed_to = lectures } );
        }

        /// <summary>
        /// Subscribe to events from selected module
        /// </summary>
        /// <returns>List of events</returns>
        /// <response code="200">Returns List of newly subscribed events</response>
        /// <response code="404">Lectures with specified moduleId do not exist.</response>
        /// <response code="401">Unauthorized.</response>
        /// <param name="moduleId">Integer id of module</param>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("ModuleEvents/{moduleId}")]
        public async Task<IActionResult> SubscribeModule(int moduleId)
        {
            var username = GetUsernameFromClaims(HttpContext.User.Identity as ClaimsIdentity);

            var lectures = _context.Lectures.Where(l => l.ModuleID == moduleId).ToList();
            if (lectures.Count == 0)
            {
                return NotFound("Lecture with specified moduleId does not exist.");
            }
            var userId = _context.Students.Where(s => s.Username == username).FirstOrDefault().ID;
            foreach (var lecture in lectures)
            {
                _context.LectureStudents.Add(new LectureStudent() { LectureID = lecture.ID, StudentID = userId });
            }
            await _context.SaveChangesAsync();
            return Ok( new { subscribed_to = lectures });
        }

        /// <summary>
        /// Subscribe to events from selected lecture
        /// </summary>
        /// <returns>List of events</returns>
        /// <response code="200">Returns List of newly subscribed events</response>
        /// <response code="404">Lectures with specified lecturerId do not exist.</response>
        /// <response code="401">Unauthorized.</response>
        /// <param name="lecturerId">Integer id of lecturer</param>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("SubscribeLecturer/{lecturerId}")]
        public async Task<IActionResult> SubscribeLecturer(int lecturerId)
        {
            var username = GetUsernameFromClaims(HttpContext.User.Identity as ClaimsIdentity);
            var userId = _context.Students.Where(s => s.Username == username).FirstOrDefault().ID;
            var moduleIds = _context.Modules.Where(m => m.LecturerID == lecturerId).Select(m => m.ID).ToList();
            if (moduleIds.Count == 0)
            {
                return NotFound("Lectures with specified lecturerId do not exist.");
            }
            var lectures = _context.Lectures.Where(l => moduleIds.Contains(l.ModuleID)).ToList();
            if (lectures.Count == 0)
            {
                return NotFound("Lectures with specified lecturerId do not exist.");
            }
            foreach (var lecture in lectures)
            {
                _context.LectureStudents.Add(new LectureStudent() { LectureID = lecture.ID, StudentID = userId });
            }
            await _context.SaveChangesAsync();
            return Ok(new { subscribed_to = lectures });
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
