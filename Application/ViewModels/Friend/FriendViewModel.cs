namespace Application.ViewModels.Friend
{
    public class FriendViewModel
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string ProfileImage { get; set; }
    }
}
