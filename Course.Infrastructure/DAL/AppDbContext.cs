using Course.Application.Exceptions;
using Course.Application.Services.UserContext;
using Course.Domain.Entities.Authors;
using Course.Domain.Entities.NewsFeed;
using Course.Domain.Entities.Users;
using Course.Domain.Interfaces;
using Course.Infrastructure.DAL.Extension;

namespace Course.Infrastructure.DAL;

public class AppDbContext(DbContextOptions<AppDbContext> options, IUserContextProvider userContextProvider)
    : DbContext(options)
{

    public DbSet<User> Users => Set<User>();
    public DbSet<UserToken> UserTokens => Set<UserToken>();
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Author> Authors => Set<Author>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        // 1. Глобальные фильтры для запросов к БД через EF
        // Фильтр 1.1. Убирает все "удалённые" записи из запроса у тех сущностей, которые реализуют интерфейс ISoftDelete
        modelBuilder.AppendGlobalQueryFilter<ISoftDelete>(x => !x.DeletedAt.HasValue);
        // Фильтр 1.2. Фильтрует по овнеру все сущности, которые реализуют интерфейс IMultiOwner, IOptionalOwner
        modelBuilder.AppendGlobalQueryFilter<IMultiuser>(x => x.UserId == userContextProvider.GetUserContext().Id);

        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("public");

        // 2. Применение всех конфигов
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        // 3. Отключаем каскадное удаление связанных сущностей. А то мало ли
        DisableCascadeDelete(modelBuilder);

        // 4. Трансляция имен сущностей ИзВотТакихВот в вот_такие_имена
        ApplySnakeCaseNames(modelBuilder);
    }

    private static void ApplySnakeCaseNames(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetDefaultTableName()!.ToLowerCaseWithUnderscore());

            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName().ToLowerCaseWithUnderscore());
            }
        }
    }

    private static void DisableCascadeDelete(ModelBuilder modelBuilder)
    {
        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => fk is { IsOwnership: false, DeleteBehavior: DeleteBehavior.Cascade });

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;
    }
}