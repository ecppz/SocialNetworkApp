using Application.Dtos.Comment;

namespace Application.Interfaces
{
    public interface ICommentService : IGenericService<CommentDto>
    {
        Task<List<CommentDto>> GetByPostIdAsync(Guid postId);
        Task<List<CommentDto>> GetRepliesAsync(Guid commentId);
        Task<CommentDto?> AddCommentAsync(Guid postId, Guid userId, string text);
        Task<CommentDto?>AddReplyAsync(Guid postId, Guid replyToCommentId, Guid userId, string text);
        Task<(bool Success, bool IsReply)> UpdateAsync(Guid commentId, Guid userId, string newText);
        Task<bool> DeleteAsync(Guid commentId, Guid userId);

    }
}
