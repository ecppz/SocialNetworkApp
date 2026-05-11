using Application.Dtos.Post;

namespace Application.Dtos.Comment
{
    public class CommentDto
    {
        public Guid Id { get; set; }

        public required string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }

        public Guid PostId { get; set; }
        public required PostDto Post { get; set; }

        public Guid? ReplyToCommentId { get; set; }
        public required CommentDto ReplyToComment { get; set; }

        public ICollection<CommentDto> Replies { get; set; } = new List<CommentDto>();
    }
}
