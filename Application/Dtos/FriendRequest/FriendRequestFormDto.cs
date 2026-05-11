
namespace Application.Dtos.FriendRequest
{
    public class FriendRequestFormDto
    {
        public string? SearchTerm { get; set; }
        public List<FriendRequestDisplayDto> AvailableUsers { get; set; } = new();
        public Guid? SelectedUserId { get; set; }
    }
}
