using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly AttendancesRepository _attendancesRepository;

        private readonly GigRepository _gigRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();

            _gigRepository = new GigRepository(_context);
            
            _attendancesRepository = new AttendancesRepository(_context);
        }
        public ActionResult Index(string query = null)
        {
            var upcommingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!string.IsNullOrWhiteSpace(query))
            {
                upcommingGigs = upcommingGigs
                    .Where(g => g.Artist.Name.Contains(query) ||
                                g.Genre.GenreName.Contains(query) ||
                                g.Venue.Contains(query));
            }

            string userId = User.Identity.GetUserId();
            var attendances = _attendancesRepository.GetFutureAttendances(userId)
                .ToLookup(a => a.GigId);  

            var viewModel = new GigsViewModel
            {
                UpcommingGigs = upcommingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcomming Gigs",
                SearchTerm = query,
                Attendances = attendances
            };
            return View("Gigs",viewModel); 
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}