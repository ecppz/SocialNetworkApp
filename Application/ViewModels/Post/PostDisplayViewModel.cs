
using Application.ViewModels.Comment;
using Application.ViewModels.Reaction;
using Domain.Common.Enums;

namespace Application.ViewModels.Post
{
    public class PostDisplayViewModel
    {
        public Guid Id { get; set; }

        public required string Content { get; set; }

        public required string ImageFile { get; set; }
        public required string YouTubeUrl { get; set; }

        public DateTime CreatedAt { get; set; }
        public  Guid UserId { get; set; }
        public required string UserName { get; set; }
        public required string ProfileImage { get; set; }
        public ReactionType? CurrentUserReaction { get; set; }
        public ICollection<CommentDisplayViewModel> Comments { get; set; } = new List<CommentDisplayViewModel>();
        public ICollection<ReactionViewModel> Reactions { get; set; } = new List<ReactionViewModel>();
    }
}
