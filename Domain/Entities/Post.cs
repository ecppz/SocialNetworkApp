namespace Domain.Entities
{
    public class Post
    {
        public required Guid Id { get; set; }
        public required string Content { get; set; }
        public string? ImageFile { get; set; }
        public string? YouTubeUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();
    }
}
