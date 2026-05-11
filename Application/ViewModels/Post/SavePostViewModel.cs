using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Post
{
    public class SavePostViewModel
    {
        public Guid Id { get; set; }
        [MaxLength(500)]
        [Required(ErrorMessage = "Debes ingresar un texto")]
        public required string Content { get; set; }
        [Required(ErrorMessage = "Debes seleccionar una opcion subir imagen o link de youtube")]
        public required string ContentType { get; set; }
        public string? ImagePath { get; set; } 
        public IFormFile? ImageFile { get; set; }
        [Url]
        public string? YouTubeUrl { get; set; }

    }
}
