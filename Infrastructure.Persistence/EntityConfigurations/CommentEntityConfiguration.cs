using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            //basic configuration
            builder.ToTable("Comments");
            builder.HasKey(c => c.Id);

            //property configurations
            builder.Property(c => c.Text).IsRequired().HasMaxLength(500);

            //relationships
            builder.HasOne(c => c.Post)
                   .WithMany(p => p.Comments)
                   .HasForeignKey(c => c.PostId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.ReplyToComment)
                   .WithMany(c => c.Replies)
                   .HasForeignKey(c => c.ReplyToCommentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
