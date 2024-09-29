using Course.Domain.Entities.Users;
using Course.Infrastructure.DAL.Extension;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.Infrastructure.DAL.Configurations;

public class UserTokenConfig : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.HasLongIdentifier();
        builder.HasOne<User>().WithMany(x => x.UserTokens).HasForeignKey(x => x.UserId);
    }
}