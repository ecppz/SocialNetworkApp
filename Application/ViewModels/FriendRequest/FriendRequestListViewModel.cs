namespace Application.ViewModels.FriendRequest
{
    public class FriendRequestListViewModel
    {
        public List<FriendRequestDisplayViewModel> ReceivedRequests { get; set; } = new();
        public List<FriendRequestDisplayViewModel> SentRequests { get; set; } = new();
    }

}
