using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Details(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return HttpNotFound("Required details not found");

            var viewModel = new GigDetailsViewModel{Gig = gig};

            if(User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending = _unitOfWork.Attendances.GetAttendance(gig, userId);

                //viewModel.IsFollowing = _unitOfWork.Followings.GetIsFollowing(userId, gig.ArtistId);

                
            }
            return View("Details", viewModel);
        }


        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs.GetUpComingGigByArtist(userId);
            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel()
            {
                UpcommingGigs = _unitOfWork.Gigs.GetGigUserIsAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'M Attending",
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.GigId)
            };
            
            return View("Gigs", viewModel);

        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("index","Home", new {query = viewModel.SearchTerm});
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel()
            {
                Genres = _unitOfWork.Genres.GetGenres(),
                Heading = "Add a Gig"
            };
            return View("GigForm", viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

                var gig = new Gig()
                {
                    ArtistId = User.Identity.GetUserId(),
                    DateTime = viewModel.GetDateTime(),
                    GenreId = viewModel.Genre,
                    Venue = viewModel.Venue

                };
                _unitOfWork.Gigs.Add(gig);

            try
            {
                _unitOfWork.Complete();
            }
            catch (DbEntityValidationException dbe)
            {
                foreach (var err in dbe.EntityValidationErrors)
                {
                    foreach (var ermsg in err.ValidationErrors )
                    {
                        string msg = ermsg.ErrorMessage;
                    }
                }

                
            }


            return RedirectToAction("Mine","Gigs");
        }


        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGig(id);
            var viewModel = new GigFormViewModel()
            {
                Heading = "Edit a Gig",           
                Id = gig.GigId,
                Genres = _unitOfWork.Genres.GetGenres(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue
            };
            return View("GigForm",viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = _unitOfWork.Gigs.GetGigWithAttendees(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

//            gig.Venue = viewModel.Venue;
//            gig.DateTime = viewModel.GetDateTime();
//            gig.GenreId = viewModel.Genre;

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

    }
} 