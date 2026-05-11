using Application.ViewModels.Post;
using Domain.Common.Enums;

namespace Application.ViewModels.Reaction
{
    public class ReactionViewModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid PostId { get; set; }
        public required PostViewModel Post { get; set; }

        public ReactionType Type { get; set; }
    }
}
