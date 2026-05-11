namespace Application.ViewModels.Post
{
    public class DeletePostViewModel
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public string? ImageFile { get; set; }
        public string? YouTubeUrl { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
