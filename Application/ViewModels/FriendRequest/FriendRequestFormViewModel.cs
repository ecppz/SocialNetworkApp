
namespace Application.ViewModels.FriendRequest
{
    public class FriendRequestFormViewModel
    {
        public string? SearchTerm { get; set; }
        public List<FriendRequestDisplayViewModel> AvailableUsers { get; set; } = new();
        public Guid? SelectedUserId { get; set; }
    }
}
