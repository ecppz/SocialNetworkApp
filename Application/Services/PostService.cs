
using Application.Dtos.Post;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class PostService : GenericService<Post, PostDto>, IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly IFriendRequestRepository friendRequestRepository;
        private readonly IMapper mapper;

        public PostService(IPostRepository postRepository, IFriendRequestRepository friendRequestRepository, IMapper mapper)
            : base(postRepository, mapper)
        {
            this.postRepository = postRepository;
            this.friendRequestRepository = friendRequestRepository;
            this.mapper = mapper;
        }
        public async Task<ICollection<PostDto>> GetFriendsPostsAsync(Guid userId)
        {
            var friendIds = await friendRequestRepository.GetAcceptedFriendIdsAsync(userId);
            var posts = await postRepository.GetFriendsPostsAsync(friendIds);
            return mapper.Map<ICollection<PostDto>>(posts);
        }


        public async Task<List<PostDisplayDto>> GetPostsByUserIdAsync(Guid userId)
        {
            var posts = await postRepository.GetPostsByUserIdAsync(userId);
            return mapper.Map<List<PostDisplayDto>>(posts);
        }


    }
}
