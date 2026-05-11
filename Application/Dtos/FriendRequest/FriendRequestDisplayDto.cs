using Domain.Common.Enums;
namespace Application.Dtos.FriendRequest
{
    public class FriendRequestDisplayDto
    {
        public Guid Id { get; set; }
        public Guid SenderUserId { get; set; }
        public Guid ReceiverUserId { get; set; }
        public required string UserName { get; set; }
        public required string ProfileImage { get; set; }
        public int MutualFriends { get; set; }
        public DateTime CreatedAt { get; set; }
        public FriendshipStatus Status { get; set; }

    }
}
