using Domain.Common.Enums;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly SocialMediaContextDB context;

        public FriendRepository(SocialMediaContextDB context)
        {
            this.context = context;
        }

        public async Task<bool> RemoveFriendAsync(Guid userId, Guid friendId)
        {
            var request = await context.FriendRequests.FirstOrDefaultAsync(fr =>
                fr.Status == FriendshipStatus.Accepted &&
                ((fr.SenderUserId == userId && fr.ReceiverUserId == friendId) ||
                 (fr.SenderUserId == friendId && fr.ReceiverUserId == userId)));

            if (request == null)
            {
                return false;
            }

            context.FriendRequests.Remove(request);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Guid>> GetAllTheFriendsIds(Guid userId)
        {
            var friendsIds = await context.FriendRequests
                .AsNoTracking()
                .Where(fr => fr.Status == FriendshipStatus.Accepted &&
                            (fr.SenderUserId == userId || fr.ReceiverUserId == userId))
                .Select(fr => fr.SenderUserId == userId ? fr.ReceiverUserId : fr.SenderUserId)
                .Distinct()
                .ToListAsync();

            return friendsIds;
        }
    }
}