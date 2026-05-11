namespace Application.Dtos.FriendRequest
{
    public class FriendRequestListDto
    {
        public List<FriendRequestDisplayDto> ReceivedRequests { get; set; } = new();
        public List<FriendRequestDisplayDto> SentRequests { get; set; } = new();
    }

}
