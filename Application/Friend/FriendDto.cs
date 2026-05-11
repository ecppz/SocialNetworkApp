namespace Application.Dtos.Friend
{
    public class FriendDto
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string FullName { get; set; } 
        public required string ProfileImage { get; set; }
    }
}
