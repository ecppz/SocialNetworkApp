using Application.Dtos.Comment;
using Application.Dtos.Reaction;

namespace Application.Dtos.Post
{
    public class PostDisplayDto
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public required string ImageFile { get; set; }
        public required string YouTubeUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public required string UserName { get; set; }
        public required string ProfileImage { get; set; }
        public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
        public ICollection<ReactionDto> Reactions { get; set; } = new List<ReactionDto>();
    }
}
