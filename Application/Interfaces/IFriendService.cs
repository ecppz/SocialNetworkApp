namespace Application.Interfaces
{
    public interface IFriendService
    {
        Task<bool> RemoveFriendAsync(Guid userId, Guid friendId);
        Task<int> GetMutualFriendAsync(Guid userAId, Guid userBId);
    }
}
