using Domain.Common.Enums;

namespace Domain.Entities
{
    public class Reaction
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid PostId { get; set; }
        public required Post Post { get; set; }

        public ReactionType Type { get; set; }
    }
}