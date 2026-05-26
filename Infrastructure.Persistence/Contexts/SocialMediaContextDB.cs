using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace Infrastructure.Persistence.Contexts
{
    public class SocialMediaContextDB : DbContext 
    {
        public SocialMediaContextDB(DbContextOptions<SocialMediaContextDB> options) : base(options) { }
        //entities social media
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        //entities battleship
        public DbSet<BattleshipGame> BattleshipGames { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<ShipPosition> ShipPositions { get; set; }
        public DbSet<Attack> Attacks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //Liskov-substitution

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
