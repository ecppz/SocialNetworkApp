using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<ICollection<Post>> GetPostsByUserIdAsync(Guid userId);
        Task<ICollection<Post>> GetFriendsPostsAsync(IEnumerable<Guid> userIds);


    }
}
