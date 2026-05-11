using Application.ViewModels.Post;

namespace Application.ViewModels.Friend
{
    public class FriendFeedViewModel
    {
        public List<PostDisplayViewModel> Posts { get; set; } = new();
        public List<FriendViewModel> Friends { get; set; } = new();
    }
}
