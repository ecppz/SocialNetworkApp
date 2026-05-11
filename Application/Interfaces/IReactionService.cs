using Application.Dtos.Reaction;
using Domain.Common.Enums;
namespace Application.Interfaces
{
    public interface IReactionService : IGenericService<ReactionDto>
    {
        Task<bool> ReactAsync(Guid userId, Guid postId, ReactionType type);
    }
}
