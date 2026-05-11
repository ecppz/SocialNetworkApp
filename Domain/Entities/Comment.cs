
namespace Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }

        public required string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }

        public Guid PostId { get; set; }
        public required Post Post { get; set; }

        public Guid? ReplyToCommentId { get; set; }
        public Comment? ReplyToComment { get; set; }

        public ICollection<Comment> Replies { get; set; } = new List<Comment>();
    }
}
