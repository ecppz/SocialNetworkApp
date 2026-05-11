using Application.Dtos.Comment;
using Application.Dtos.Reaction;

namespace Application.Dtos.Post
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public string? ImageFile { get; set; }
        public string? YouTubeUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
        public ICollection<ReactionDto> Reactions { get; set; } = new List<ReactionDto>();
    }
}
