using Application.Dtos.Post;
namespace Application.Interfaces
{
    public interface IPostService : IGenericService<PostDto>
    {
        Task<List<PostDisplayDto>> GetPostsByUserIdAsync(Guid userIds);
        Task<ICollection<PostDto>> GetFriendsPostsAsync(Guid userId);

    }
}
