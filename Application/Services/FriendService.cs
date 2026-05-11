
using Application.Interfaces;
using Domain.Interfaces;

public class FriendService : IFriendService
{
    private readonly IFriendRepository friendRepository;
    private readonly IFriendRequestRepository friendRequestRepository;

    public FriendService(IFriendRepository friendRepository, IFriendRequestRepository friendRequestRepository)
    {
        this.friendRepository = friendRepository;
        this.friendRequestRepository = friendRequestRepository;
    }

    public async Task<int> GetMutualFriendAsync(Guid userAId, Guid userBId)
    {
        var userAFriends = await friendRequestRepository.GetAcceptedFriendIdsAsync(userAId);
        var userBFriends = await friendRequestRepository.GetAcceptedFriendIdsAsync(userBId);

        var mutualFriends = userAFriends.Intersect(userBFriends);
        return mutualFriends.Count();
    }


    public async Task<bool> RemoveFriendAsync(Guid userId, Guid friendId)
    {
       return await friendRepository.RemoveFriendAsync(userId, friendId);
    }

}
