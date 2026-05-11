using Domain.Common.Enums;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;


namespace Infrastructure.Persistence.Repositories
{
    public class ReactionRepository : GenericRepository<Reaction>, IReactionRepository
    {
        public ReactionRepository(SocialMediaContextDB context) : base(context)
        {
        }

        public async Task<Reaction?> GetByUserAndPostAsync(Guid userId, Guid postId)
        {
            return await context.Reactions
                .FirstOrDefaultAsync(r => r.UserId == userId && r.PostId == postId);
        }

        public async Task<int> CountByPostAndTypeAsync(Guid postId, ReactionType type)
        {
            return await context.Reactions
                .CountAsync(r => r.PostId == postId && r.Type == type);
        }
    }
}
