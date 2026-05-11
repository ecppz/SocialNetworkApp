using Domain.Common.Enums;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class FriendRequestRepository : GenericRepository<FriendRequest>, IFriendRequestRepository
    {
        public FriendRequestRepository(SocialMediaContextDB context) : base(context) { }

        public async Task<bool> HasExistingRequestAsync(Guid userAId, Guid userBId)
        {
            return await context.FriendRequests.AnyAsync(f =>
                (f.SenderUserId == userAId && f.ReceiverUserId == userBId) ||
                (f.SenderUserId == userBId && f.ReceiverUserId == userAId));
        }

        public async Task<List<FriendRequest>> GetPendingReceivedAsync(Guid userId)
        {
            return await context.FriendRequests
                .Where(f => f.ReceiverUserId == userId && f.Status == FriendshipStatus.Pending)
                .ToListAsync();
        }

        public async Task<List<FriendRequest>> GetSentRequestsAsync(Guid userId)
        {
            return await context.FriendRequests
                .Where(f => f.SenderUserId == userId)
                .ToListAsync();
        }

        public async Task<FriendRequest?> GetByIdAsync(Guid id)
        {
            return await context.FriendRequests.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task AcceptAsync(Guid id)
        {
            var friend = await context.FriendRequests.FindAsync(id);
            if (friend is null)
            {
                return;
            }

            friend.Status = FriendshipStatus.Accepted;
            await context.SaveChangesAsync();
        }

        public async Task RejectAsync(Guid id)
        {
            var friend = await context.FriendRequests.FindAsync(id);
            if (friend is null)
            {
                return;
            }

            friend.Status = FriendshipStatus.Rejected;
            await context.SaveChangesAsync();
        }

        public async Task<List<Guid>> GetAcceptedFriendIdsAsync(Guid userId)
        {
            return await context.FriendRequests
                .Where(f => f.Status == FriendshipStatus.Accepted &&
                            (f.SenderUserId == userId || f.ReceiverUserId == userId))
                .Select(f => f.SenderUserId == userId ? f.ReceiverUserId : f.SenderUserId)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<Guid>> GetUsersWithActiveRequestAsync(Guid userId)
        {
            return await context.FriendRequests
                .Where(f => f.Status == FriendshipStatus.Pending && (f.SenderUserId == userId || f.ReceiverUserId == userId))
                .Select(f => f.SenderUserId == userId ? f.ReceiverUserId : f.SenderUserId)
                .Distinct()
                .ToListAsync();
        }

        public async Task<bool> MarkAsRejectedAsync(Guid userId, Guid friendId)
        {
            var request = await context.FriendRequests.FirstOrDefaultAsync(r =>
                (r.SenderUserId == userId && r.ReceiverUserId == friendId) ||
                (r.SenderUserId == friendId && r.ReceiverUserId == userId));

            if (request == null)
                return false;

            request.Status = FriendshipStatus.Rejected;
            await context.SaveChangesAsync();
            return true;
        }

    }
}