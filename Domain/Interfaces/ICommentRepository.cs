using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<List<Comment>> GetByPostIdAsync(Guid postId);
        Task<List<Comment>> GetRepliesAsync(Guid commentId);
        Task DeleteWithRepliesAsync(Guid commentId);
    }
}
