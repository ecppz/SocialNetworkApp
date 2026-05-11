using Application.Dtos.FriendRequest;

namespace Application.Interfaces
{
    public interface IFriendRequestService : IGenericService<FriendRequestDto>
    {
        Task<bool> SendFriendRequestAsync(Guid senderId, Guid receiverId);
        Task<List<FriendRequestDisplayDto>> GetReceivedRequestsAsync(Guid userId);
        Task<List<FriendRequestDisplayDto>> GetSentRequestsAsync(Guid userId);
        Task<List<Guid>> GetUsersWithActiveRequestAsync(Guid userId);
        Task<List<Guid>> GetAcceptedFriendIdsAsync(Guid userId);
        Task<FriendRequestDisplayDto?> GetByIdAsync(Guid id);
        Task AcceptRequestAsync(Guid id);
        Task RejectRequestAsync(Guid id);
    }
}
