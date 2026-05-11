
namespace Domain.Interfaces
{
    public interface IFriendRepository 
    {
        Task<List<Guid>> GetAllTheFriendsIds(Guid userId);
        Task<bool> RemoveFriendAsync(Guid userId, Guid friendId);
    }
}
