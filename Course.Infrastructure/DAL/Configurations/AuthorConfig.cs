using Course.Domain.Entities.Authors;
using Course.Infrastructure.DAL.Extension;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.Infrastructure.DAL.Configurations;

public class AuthorConfig : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasLongIdentifier();

        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        builder.HasMany(x=>x.Articles).WithOne(x => x.Author).HasForeignKey(x => x.AuthorId);
    }
}