using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;

namespace GigHub.Controllers.Api
{
    [System.Web.Http.Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId))

                return BadRequest("The attendance already exist");

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
                 
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            string userId = User.Identity.GetUserId();
            var attendance = _context.Attendances
                .SingleOrDefault(a => a.AttendeeId == userId && a.GigId == id);

            if (attendance == null)
            {
                return NotFound();
            }

            _context.Attendances.Remove(attendance);
            _context.SaveChanges();
            return Ok(id);
        }
    }
}
