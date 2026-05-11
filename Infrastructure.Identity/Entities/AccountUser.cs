using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Entities
{
    public class AccountUser : IdentityUser
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public string? ProfileImage { get; set; }
    }
}
