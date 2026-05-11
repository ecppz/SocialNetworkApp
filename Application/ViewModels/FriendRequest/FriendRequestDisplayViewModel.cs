using Domain.Common.Enums;

namespace Application.ViewModels.FriendRequest
{
    public class FriendRequestDisplayViewModel
    {
        public Guid Id { get; set; }
        public Guid SenderUserId { get; set; }
        public Guid ReceiverUserId { get; set; }
        public string? UserName { get; set; }
        public string? ProfileImage { get; set; }
        public int MutualFriends { get; set; }
        public DateTime CreatedAt { get; set; }
        public FriendshipStatus Status { get; set; }

    }
}
