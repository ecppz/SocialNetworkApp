using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IFriendRequestRepository : IGenericRepository<FriendRequest>
    {
        Task<bool> HasExistingRequestAsync(Guid userAId, Guid userBId);
        Task<List<FriendRequest>> GetPendingReceivedAsync(Guid userId);
        Task<List<FriendRequest>> GetSentRequestsAsync(Guid userId);
        Task<List<Guid>> GetAcceptedFriendIdsAsync(Guid userId);
        Task<List<Guid>> GetUsersWithActiveRequestAsync(Guid userId);
        Task<FriendRequest?> GetByIdAsync(Guid id);
        Task AcceptAsync(Guid id);
        Task RejectAsync(Guid id);
    }
}
