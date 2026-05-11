using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Post
{
    public class EditPostViewModel
    {
        public Guid Id { get; set; }
        [MaxLength(500)]
        [Required(ErrorMessage = "Debes ingresar un texto")]
        public required string Content { get; set; }
        public string? ContentType { get; set; }
        public string? ImagePath { get; set; } 
        public IFormFile? ImageFile { get; set; }
        [Url]
        public string? YouTubeUrl { get; set; }

    }
}
