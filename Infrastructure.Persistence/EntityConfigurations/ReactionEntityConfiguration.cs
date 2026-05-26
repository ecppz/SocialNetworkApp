using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class ReactionEntityConfiguration : IEntityTypeConfiguration<Reaction>
    {
        public void Configure(EntityTypeBuilder<Reaction> builder)
        {
            //basic configuration
            builder.ToTable("Reactions");
            builder.HasKey(r => r.Id);

            //relationships
            builder.HasOne(r => r.Post)
                   .WithMany(p => p.Reactions)
                   .HasForeignKey(r => r.PostId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
