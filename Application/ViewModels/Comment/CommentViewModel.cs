
using Application.ViewModels.Post;

namespace Application.ViewModels.Comment
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        public required string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }

        public Guid PostId { get; set; }
        public required PostViewModel Post { get; set; }

        public Guid? ReplyToCommentId { get; set; }
        public CommentViewModel? ReplyToComment { get; set; }

        public ICollection<CommentViewModel> Replies { get; set; } = new List<CommentViewModel>();
    }
}
