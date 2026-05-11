using Application.Dtos.Comment;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CommentService : GenericService<Comment, CommentDto>, ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;

        public CommentService(ICommentRepository commentRepository, IPostRepository postRepository, IMapper mapper)
            : base(commentRepository, mapper)
        {
            this.commentRepository = commentRepository;
            this.postRepository = postRepository;
            this.mapper = mapper;
        }

        public async Task<List<CommentDto>> GetByPostIdAsync(Guid postId)
        {
            var comments = await commentRepository.GetByPostIdAsync(postId);
            return mapper.Map<List<CommentDto>>(comments);
        }

        public async Task<List<CommentDto>> GetRepliesAsync(Guid commentId)
        {
            var replies = await commentRepository.GetRepliesAsync(commentId);
            return mapper.Map<List<CommentDto>>(replies);
        }

        public async Task<CommentDto?> AddCommentAsync(Guid postId, Guid userId, string text)
        {
            var post = await postRepository.GetByIdAsync(postId);
            if (post == null)
            {
                return null;
            }

            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                PostId = postId,
                Post = post,
                UserId = userId,
                Text = text,
                CreatedAt = DateTime.Now
            };

            await commentRepository.AddAsync(comment);
            return mapper.Map<CommentDto>(comment);
        }

        public async Task<CommentDto?> AddReplyAsync(Guid postId, Guid replyToCommentId, Guid userId, string text)
        {
            var post = await postRepository.GetByIdAsync(postId);
            if (post == null)
            {
                return null;
            }

            var parentComment = await commentRepository.GetByIdAsync(replyToCommentId);
            if (parentComment == null || parentComment.ReplyToCommentId != null)
            {
                return null;
            }

            var reply = new Comment
            {
                Id = Guid.NewGuid(),
                PostId = postId,
                Post = post,
                UserId = userId,
                Text = text,
                CreatedAt = DateTime.Now,
                ReplyToCommentId = replyToCommentId
            };

            await commentRepository.AddAsync(reply);
            return mapper.Map<CommentDto>(reply);
        }
        public async Task<(bool Success, bool IsReply)> UpdateAsync(Guid commentId, Guid userId, string newText)
        {
            var comment = await commentRepository.GetByIdAsync(commentId);

            if (comment == null)
            {
                return (false, false);
            }

            if (comment.UserId != userId)
            {
                return (false, comment.ReplyToCommentId != null); 
            }

            comment.Text = newText;
            var updated = await commentRepository.UpdateAsync(commentId, comment);

            return (updated != null, comment.ReplyToCommentId != null);
        }


        public async Task<bool> DeleteAsync(Guid commentId, Guid userId)
        {
            var comment = await commentRepository.GetByIdAsync(commentId);
            if (comment == null)
            {
                return false;
            }

            if (comment.ReplyToCommentId == null)
            {
                await commentRepository.DeleteWithRepliesAsync(commentId);
            }
            else
            {
                await commentRepository.DeleteAsync(commentId);
            }

            return true;
        }


    }
}
