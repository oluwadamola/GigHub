using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        bool GetIsFollowing(Gig gig, string userId);
    }
}