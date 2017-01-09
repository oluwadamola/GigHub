using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;
        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool GetIsFollowing(Gig gig, string userId)
        {
            return _context.Followings
                .Any(f => f.FolloweeId == gig.ArtistId && f.FollowerId == userId);
        }


    }
}