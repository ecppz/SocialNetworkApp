using Domain.Common.Enums;

namespace Application.ViewModels.FriendRequest
{
    public class FriendRequestViewModel
    {
        public Guid Id { get; set; }
        public Guid SenderUserId { get; set; }
        public Guid ReceiverUserId { get; set; }
        public FriendshipStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
