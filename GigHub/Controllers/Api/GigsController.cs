using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using GigHub.Core.Models;
using GigHub.Persistence;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.GigId == id && g.ArtistId == userId);

            if(gig.IsCanceled) return NotFound();

            gig.Canceled();

            _context.SaveChanges();
            return Ok();

            //            var attendees = _context.Attendances
            //                .Where(a => a.GigId == gig.GigId)
            //                .Select(a => a.Attendee)
            //                .ToList();




        }
    }
}
