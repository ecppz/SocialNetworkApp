using Application.Dtos.Post;
using Domain.Common.Enums;

namespace Application.Dtos.Reaction
{
    public class ReactionDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid PostId { get; set; }
        public required PostDto Post { get; set; }

        public ReactionType Type { get; set; }
    }
}
