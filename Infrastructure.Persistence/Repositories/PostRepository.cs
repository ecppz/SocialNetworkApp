using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(SocialMediaContextDB context) : base(context)
        {
        }
        public async Task<ICollection<Post>> GetPostsByUserIdAsync(Guid userId)
        {
            return await context.Posts
                .Where(p => p.UserId == userId)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Replies)
                .Include(p => p.Reactions)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<ICollection<Post>> GetFriendsPostsAsync(IEnumerable<Guid> userIds)
        {
            return await context.Posts
                .Where(p => userIds.Contains(p.UserId))
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Replies)
                .Include(p => p.Reactions)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}
