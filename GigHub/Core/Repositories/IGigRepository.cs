using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        Gig GetGig(int id);
        IEnumerable<Gig> GetGigUserIsAttending(string userId);
        Gig GetGigWithAttendees(int gigId);
        Gig Add(Gig gig);
        IEnumerable<Gig> GetUpComingGigByArtist(string userId);
    }
}