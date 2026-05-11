using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;


namespace Infrastructure.Persistence.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(SocialMediaContextDB context) : base(context) {}

        public async Task<List<Comment>> GetByPostIdAsync(Guid postId)
        {
            return await context.Comments
                .Where(c => c.PostId == postId)
                .Include(c => c.Replies)
                .ToListAsync();
        }

        public async Task<List<Comment>> GetRepliesAsync(Guid commentId)
        {
            return await context.Comments
                .Where(c => c.ReplyToCommentId == commentId)
                .ToListAsync();
        }


        public async Task DeleteWithRepliesAsync(Guid commentId)
        {
            var replies = await context.Comments
                .Where(c => c.ReplyToCommentId == commentId)
                .ToListAsync();

            if (replies.Any())
            {
                context.Comments.RemoveRange(replies);
                await context.SaveChangesAsync();
            }

            var parent = await context.Comments.FindAsync(commentId);
            if (parent != null)
            {
                context.Comments.Remove(parent);
                await context.SaveChangesAsync(); 
            }
        }




    }
}
