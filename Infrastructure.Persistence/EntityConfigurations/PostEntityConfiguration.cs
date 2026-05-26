using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class PostEntityConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            //basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Posts");

            //property configurations
            builder.Property(p => p.Content).IsRequired().HasMaxLength(500);
            
            //relationships
            builder.HasMany(p => p.Comments)
                   .WithOne(c => c.Post)
                   .HasForeignKey(c => c.PostId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Reactions)
                   .WithOne(r => r.Post)
                  .HasForeignKey(r => r.PostId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
