
using Application.ViewModels.Comment;
using Application.ViewModels.Reaction;

namespace Application.ViewModels.Post
{
    public class PostViewModel
    {
        public Guid Id { get; set; }

        public required string Content { get; set; }

        public required string ImageFile { get; set; }
        public required string YouTubeUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public  Guid UserId { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
        public ICollection<ReactionViewModel> Reactions { get; set; } = new List<ReactionViewModel>();
    }
}
