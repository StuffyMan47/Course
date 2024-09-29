using Course.Domain.Entities.Users;

namespace Course.Application.Services.UserContext.Models;

public class UserContextModel
{
    public Guid Id { get; init; } = Guid.Empty;
    public string Login { get; init; } = "Anonymous";
    public int TenantId { get; init; }
    public UserRole UserRole { get; init; } = UserRole.User;
    public int? EmployeeId { get; init; }
}