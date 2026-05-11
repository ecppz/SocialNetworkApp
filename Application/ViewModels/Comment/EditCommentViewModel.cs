namespace Application.ViewModels.Comment
{
    public class EditCommentViewModel
    {
        public Guid Id { get; set; }    
        public Guid PostId { get; set; }    
        public required string Text { get; set; }

    }
}
