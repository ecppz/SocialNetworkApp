using Domain.Common.Enums;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IReactionRepository : IGenericRepository<Reaction>
    {
        Task<Reaction?> GetByUserAndPostAsync(Guid userId, Guid postId);
        Task<int> CountByPostAndTypeAsync(Guid postId, ReactionType type);
    }
}
