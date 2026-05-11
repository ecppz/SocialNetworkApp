
using Application.Dtos.Reaction;
using Application.Interfaces;
using AutoMapper;
using Domain.Common.Enums;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ReactionService : GenericService<Reaction, ReactionDto>, IReactionService
    {
        private readonly IReactionRepository reactionRepository;
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;

        public ReactionService(IReactionRepository reactionRepository, IPostRepository postRepository, IMapper mapper) 
            : base(reactionRepository, mapper)
        {
            this.reactionRepository = reactionRepository;
            this.postRepository = postRepository;
            this.mapper = mapper;
        }

        public async Task<bool> ReactAsync(Guid userId, Guid postId, ReactionType type)
        {
            var existing = await reactionRepository.GetByUserAndPostAsync(userId, postId);

            if (existing != null)
            {
                if (existing.Type == type)
                {
                    await reactionRepository.DeleteAsync(existing.Id);
                }
                else
                {
                    existing.Type = type;
                    await reactionRepository.UpdateAsync(existing.Id, existing);
                }
            }
            else
            {
                var post = await postRepository.GetByIdAsync(postId);
                if (post == null)
                    return false;

                var reaction = new Reaction
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    PostId = postId,
                    Post = post,
                    Type = type
                };
                await reactionRepository.AddAsync(reaction);
            }

            return true;
        }
    }
}