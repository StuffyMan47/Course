using Course.Domain.Entities.Authors;
using Course.Domain.Entities.Base;
using Course.Domain.Entities.Users;
using Course.Domain.Interfaces;

namespace Course.Domain.Entities.NewsFeed;

public class Article : AuditableEntity<long>
{
    public required string Title { get; set; }
    public string Description { get; set; } = String.Empty;
    public required string Content { get; set; }
    public required long AuthorId { get; set; }
    
    public required Author Author { get; set; }
}