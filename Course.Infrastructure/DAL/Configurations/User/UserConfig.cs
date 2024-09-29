using Course.Domain.Entities.Users;
using Course.Infrastructure.DAL.Extension;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.Infrastructure.DAL.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasGuidIdentifier();

        builder.Property(x => x.Login).HasColumnType(SqlColumnTypes.Varchar(255));
        builder.Property(x => x.Username).HasColumnType(SqlColumnTypes.Text);
        builder.Property(x => x.PasswordHash).HasColumnType(SqlColumnTypes.Text);
        builder.Property(x => x.PasswordSalt).HasColumnType(SqlColumnTypes.Text);
        builder.Property(x => x.ImageUrl).HasColumnType(SqlColumnTypes.Text);
        builder.Property(x => x.UserRole).HasConversion<int>();
        builder.Property(x => x.UserStatus).HasConversion<int>();
        
        builder.HasIndex(x => x.Login).IsUnique();
    }
}