using Course.Domain.Entities.Users;
using Course.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.Infrastructure.DAL.Extension.Base;

public static class Multiuser
{
    public static void IsMultiUser<T>(this EntityTypeBuilder<T> builder)
        where T : class, IMultiuser
    {
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);
    }
}