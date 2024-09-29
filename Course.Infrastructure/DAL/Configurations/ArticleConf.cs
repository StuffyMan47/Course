using Course.Domain.Entities.NewsFeed;
using Course.Domain.Entities.Users;
using Course.Infrastructure.DAL.Extension;
using Course.Infrastructure.DAL.Extension.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.Infrastructure.DAL.Configurations;

public class ArticleConf : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasLongIdentifier();
        builder.IsAuditable();
        
        builder.Property(x => x.Content).HasColumnType(SqlColumnTypes.Text).IsRequired();
        builder.Property(x => x.Title).HasColumnType(SqlColumnTypes.Varchar(255)).IsRequired();
        builder.Property(x => x.Description).HasColumnType(SqlColumnTypes.Text);
        
        builder.HasOne(x=>x.Author).WithMany(x=>x.Articles).HasForeignKey(x=>x.AuthorId);
    }
}


