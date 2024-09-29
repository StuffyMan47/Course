using System.Collections.ObjectModel;

namespace Course.Domain.Entities.Permissions;

public static class CourseAction
{
    public const string Create = nameof(Create);
    public const string Read = nameof(Read);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);

    public const string Export = nameof(Export);
    public const string Import = nameof(Import);
    public const string Use = nameof(Use);
}

public static class CourseResource
{
    public const string Audit = nameof(Audit);
    public const string News = nameof(News);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Roles = nameof(Roles);
    public const string RoleTemplates = nameof(RoleTemplates);
    public const string RoleTemplateClaims = nameof(RoleTemplateClaims);
    public const string UserRoles = nameof(UserRoles);
    public const string Users = nameof(Users);
    public const string Permissions = nameof(Permissions);
    public const string SystemUserMethods = nameof(SystemUserMethods);
}

public static class StoreWebPermissions
{
    private static readonly CoursePermission[] All =
    [
        new("Использовать возможности системного пользователя", CourseAction.Use, CourseResource.SystemUserMethods),

        new("Создание Пользователей", CourseAction.Create, CourseResource.Users),
        new("Просмотр Пользователей", CourseAction.Read, CourseResource.Users, IsBasic: true),
        new("Изменение Пользователей", CourseAction.Update, CourseResource.Users),
        new("Удаление Пользователей", CourseAction.Delete, CourseResource.Users),

        new("Изменение Ролей Пользователей", CourseAction.Update, CourseResource.UserRoles),

        new("Создание Ролей", CourseAction.Create, CourseResource.Roles),
        new("Просмотр Ролей", CourseAction.Read, CourseResource.Roles, IsBasic: true),
        new("Изменение Ролей", CourseAction.Update, CourseResource.Roles),
        new("Удаление Ролей", CourseAction.Delete, CourseResource.Roles),

        new("Создание Признаков Роли", CourseAction.Create, CourseResource.RoleClaims),
        new("Просмотр Признаков Роли", CourseAction.Read, CourseResource.RoleClaims, IsBasic: true),
        new("Изменение Признаков Роли", CourseAction.Update, CourseResource.RoleClaims),
        new("Удаление Признаков Роли", CourseAction.Delete, CourseResource.RoleClaims),

        new("Просмотр общего лога системы", CourseAction.Read, CourseResource.Audit, IsBasic: true),

        new("Создание Новостей", CourseAction.Create, CourseResource.News, IsBasic: true),
        new("Просмотр Новостей", CourseAction.Read, CourseResource.News, IsBasic: true),
        new("Изменение Новостей", CourseAction.Update, CourseResource.News, IsBasic: true),
        new("Удаление Новостей", CourseAction.Delete, CourseResource.News),

        new("Создание Шаблонов ролей", CourseAction.Create, CourseResource.RoleTemplates),
        new("Просмотр Шаблонов ролей", CourseAction.Read, CourseResource.RoleTemplates),
        new("Изменение Шаблонов ролей", CourseAction.Update, CourseResource.RoleTemplates),
        new("Удаление Шаблонов ролей", CourseAction.Delete, CourseResource.RoleTemplates),

        new("Создание Признаков шаблонов ролей", CourseAction.Create, CourseResource.RoleTemplateClaims),
        new("Просмотр Признаков шаблонов ролей", CourseAction.Read, CourseResource.RoleTemplateClaims),
        new("Изменение Признаков шаблонов ролей", CourseAction.Update, CourseResource.RoleTemplateClaims),
        new("Удаление Признаков шаблонов ролей", CourseAction.Delete, CourseResource.RoleTemplateClaims),

        new("Просмотр Разрешений", CourseAction.Read, CourseResource.Permissions)
    ];

    /* Наборы разрешений для стандартных шаблонов ролей */
    public static IReadOnlyList<CoursePermission> FullAccess { get; } = new ReadOnlyCollection<CoursePermission>(All);
    public static IReadOnlyList<CoursePermission> Basic { get; } = new ReadOnlyCollection<CoursePermission>(All.Where(x => x.IsBasic).ToList());
    public static IReadOnlyList<CoursePermission> Cashier { get; } = new ReadOnlyCollection<CoursePermission>(All.Where(x => x.IsCashier).ToList());
}

public record CoursePermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsCashier = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}
