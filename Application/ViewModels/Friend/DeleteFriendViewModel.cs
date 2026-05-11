namespace Application.ViewModels.Friend
{
    public class DeleteFriendViewModel
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? ProfileImage { get; set; }
        public int MutualFriends { get; set; }
    }
}
