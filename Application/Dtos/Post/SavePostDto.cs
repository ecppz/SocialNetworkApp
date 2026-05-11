
namespace Application.Dtos.Post
{
    public class SavePostDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; } 
        public required string Content { get; set; }
        public required string ContentType { get; set; }
        public string? ImageFile { get; set; }
        public string? YouTubeUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
