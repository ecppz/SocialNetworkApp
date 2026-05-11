using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class FriendRequestEntityConfiguration : IEntityTypeConfiguration<FriendRequest>
    {
        public void Configure(EntityTypeBuilder<FriendRequest> builder)
        {
            // basic configuration
            builder.ToTable("FriendRequests");
            builder.HasKey(f => f.Id);

            // property
            builder.Property(f => f.SenderUserId).IsRequired();

            builder.Property(f => f.ReceiverUserId).IsRequired();

            builder.Property(f => f.Status).IsRequired();

            builder.Property(f => f.CreatedAt).IsRequired();

            //index
            builder.HasIndex(f => new { f.SenderUserId, f.ReceiverUserId }).IsUnique();
        }
    }
}
