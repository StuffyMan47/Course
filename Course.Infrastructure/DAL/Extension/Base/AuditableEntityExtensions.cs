using Course.Domain.Entities.Users;
using Course.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.Infrastructure.DAL.Extension.Base;

public static class AuditableEntityExtensions
{
    public static void IsAuditable<T>(this EntityTypeBuilder<T> builder)
        where T : class, IAuditableEntity
    {
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.LastModifiedBy);
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.CreatedBy);
    }
}