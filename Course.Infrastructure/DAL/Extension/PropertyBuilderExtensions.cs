using Course.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.Infrastructure.DAL.Extension;

public static class PropertyBuilderExtensions
{
    public static void HasIntIdentifier<T>(this EntityTypeBuilder<T> builder)
        where T : BaseEntity<int>
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }

#pragma warning disable S4144 // Methods should not have identical implementations

    public static void HasLongIdentifier<T>(this EntityTypeBuilder<T> builder)
#pragma warning restore S4144 // Methods should not have identical implementations
        where T : BaseEntity<long>
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }

    public static void HasGuidIdentifier<T>(this EntityTypeBuilder<T> builder)
        where T : BaseEntity<Guid>
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasDefaultValueSql("gen_random_uuid()");
    }

    public static PropertyBuilder<T> HasEnumToStringConversion<T>(this PropertyBuilder<T> builder) where T : Enum
    {
        return builder
            .HasColumnType(SqlColumnTypes.Varchar(255))
            .HasConversion(
                x => Enum.GetName(typeof(T), x),
                x => (T)Enum.Parse(typeof(T), x!)
            );
    }
}
