namespace Application.Dtos.Comment
{
    public class CommentDisplayDto
    {
        public Guid Id { get; set; }
        public required string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? ProfileImage { get; set; }
        public Guid PostId { get; set; }

        public Guid? ReplyToCommentId { get; set; }
        public CommentDisplayDto? ReplyToComment { get; set; }

        public ICollection<CommentDisplayDto> Replies { get; set; } = new List<CommentDisplayDto>();
    }
}
