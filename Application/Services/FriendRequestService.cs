using Application.Dtos.FriendRequest;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Common.Enums;
using Domain.Entities;
using Domain.Interfaces;

public class FriendRequestService : GenericService<FriendRequest, FriendRequestDto>, IFriendRequestService
{
    private readonly IFriendRequestRepository friendRequestRepository;
    private readonly IMapper mapper;

    public FriendRequestService(IFriendRequestRepository friendRequestRepository, IMapper mapper) : base(friendRequestRepository, mapper)
    {
        this.friendRequestRepository = friendRequestRepository;
        this.mapper = mapper;
    }

    public async Task<bool> SendFriendRequestAsync(Guid senderId, Guid receiverId)
    {
        var exists = await friendRequestRepository.HasExistingRequestAsync(senderId, receiverId);
        if (exists)
        {
            return false;
        }

        var dto = new FriendRequestDto
        {
            SenderUserId = senderId,
            ReceiverUserId = receiverId
        };

        var entity = mapper.Map<FriendRequest>(dto);
        entity.Status = FriendshipStatus.Pending;
        entity.CreatedAt = DateTime.Now;
        entity.Id = Guid.NewGuid();

        await friendRequestRepository.AddAsync(entity);
        return true;
    }
    public async Task<List<Guid>> GetUsersWithActiveRequestAsync(Guid userId)
    {
        return await friendRequestRepository.GetUsersWithActiveRequestAsync(userId);
    }
    public async Task<List<FriendRequestDisplayDto>> GetReceivedRequestsAsync(Guid userId)
    {
        var entities = await friendRequestRepository.GetPendingReceivedAsync(userId);
        return mapper.Map<List<FriendRequestDisplayDto>>(entities);
    }

    public async Task<List<FriendRequestDisplayDto>> GetSentRequestsAsync(Guid userId)
    {
        var entities = await friendRequestRepository.GetSentRequestsAsync(userId);
        return mapper.Map<List<FriendRequestDisplayDto>>(entities);
    }
    public async Task<List<Guid>> GetAcceptedFriendIdsAsync(Guid userId)
    {
        return await friendRequestRepository.GetAcceptedFriendIdsAsync(userId);
    }

    public async Task<FriendRequestDisplayDto?> GetByIdAsync(Guid id)
    {
        var entity = await friendRequestRepository.GetByIdAsync(id);
        return mapper.Map<FriendRequestDisplayDto>(entity);
    }

    public async Task AcceptRequestAsync(Guid id)
    {
        await friendRequestRepository.AcceptAsync(id);
    }

    public async Task RejectRequestAsync(Guid id)
    {
        await friendRequestRepository.RejectAsync(id);
    }
}
