using Course.Domain.Entities.Base;
using Course.Domain.Entities.NewsFeed;
using Course.Domain.Entities.Users;

namespace Course.Domain.Entities.Authors;

public class Author : BaseEntity<long>
{
    public required string Name { get; set; }
    public string? CompanyName { get; set; }
    public required Guid UserId { get; set; }

    public required User User { get; set; }
    public List<Article> Articles { get; set; } = [];
}